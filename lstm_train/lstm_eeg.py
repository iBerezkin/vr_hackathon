import pandas as pd
import numpy as np

import matplotlib.pyplot as plt
import seaborn as sbn

from sklearn.decomposition import PCA
from sklearn.metrics import confusion_matrix, classification_report
from sklearn.model_selection import train_test_split

import torch
import torch.nn as nn

def get_default_device():
    '''Pick gpu if available else pick cpu'''
    if torch.cuda.is_available():
        return torch.device('cuda')
    else:
        return torch.device('cpu')

def to_device(data, device):
    '''Move tensors to choosen device'''
    if isinstance(data, (list, tuple)):
        return [to_device(d, device) for d in data]
    return data.to(device, non_blocking=True)

device = get_default_device()

eeg_data = pd.read_csv(r"C:\Users\Klaes\Desktop\emotions.csv.zip")
print(eeg_data.head())

eeg_data['label'] = eeg_data['label'].replace(['NEUTRAL', 'NEGATIVE', 'POSITIVE'],['CALM', 'STRESSED', 'GOOD-MOOD'])
eeg_data_copy = eeg_data.copy()
eeg_data['label'] = eeg_data['label'].astype('category').cat.codes
X_train, X_test, y_train, y_test = train_test_split(eeg_data.drop('label', axis=1), eeg_data['label'], random_state=42, test_size=0.2, stratify=eeg_data['label'])

# convert test to numpy
y_test = y_test.to_numpy()
# convert to numpy arrays
inputs_array = X_train.to_numpy()
targets_array = y_train.to_numpy()

# convert to tensors
inputs = to_device(torch.FloatTensor(inputs_array),device)
targets = to_device(torch.FloatTensor(targets_array), device)

# define batch size
batch_size = 128

class LSTMModel(nn.Module):
    def __init__(self, input_size, output_size, hidden_dim, n_layers):
        super(LSTMModel, self).__init__()

        self.hidden_dim = hidden_dim
        self.n_layers = n_layers

        self.lstm = nn.LSTM(input_size, hidden_dim, n_layers, batch_first=True, dropout=0.2)
        self.fc = nn.Linear(hidden_dim, output_size).float()
        self.relu = nn.ReLU()
        
    def forward(self, x):
        out, h = self.lstm(x)
        out = self.fc(self.relu(out))
        return out, h
    
    def init_hidden(self, batch_size):
        weight = next(self.parameters()).data
        hidden = weight.new(self.n_layers, batch_size, self.hidden_dim).zero_()
        return hidden

input_size = len(X_train.columns)
output_size = len(y_train.unique())
hidden_dim = 128
n_layers = 2
# init hyperparameters
n_epochs = 430
# init model
LSTMmodel = LSTMModel(input_size, output_size, hidden_dim, n_layers)

to_device(LSTMmodel, device)

#Training the LSTM Model
#define loss and optimizer
losses = []
l_rates = [1e-1, 1e-2, 1e-3, 1e-4]
l_r_i = 2
criterion = nn.CrossEntropyLoss()
losses = []
optimizer = torch.optim.Adam(LSTMmodel.parameters(), l_rates[l_r_i])
# scheduler = torch.optim.lr_scheduler.ReduceLROnPlateau(optimizer, factor=0.01)
for epoch in range(1, n_epochs + 1):
    optimizer.zero_grad() 
    output, hidden = LSTMmodel(inputs.unsqueeze(0))
    loss = criterion(output.squeeze(0).float(), targets.long())
    loss_detached = loss.detach().cpu().clone().numpy()
    losses.append(loss_detached)

    loss.backward() 
    optimizer.step()

    if epoch%10 == 0:
        print('Epoch: {}/{}.............'.format(epoch, n_epochs), end=' ')
        print("Loss: {:.4f}".format(loss.item()))

test_data = to_device(torch.FloatTensor(X_test.to_numpy()).unsqueeze(0), device)
output = LSTMmodel(test_data)[0]
output = output.squeeze(0)
output_ = output.detach().cpu().clone()
predictions = np.array(torch.argmax(output_, 1, keepdim=True))
