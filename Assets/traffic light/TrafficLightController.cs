using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    private float timer;
    private int currentLight = 0; // 0 = Red, 1 = Yellow, 2 = Green

    void Start()
    {
        // Включаем красный свет в начале
        SetLight(0);
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Время переключения светофора
        switch (currentLight)
        {
            case 0: // Красный
                if (timer >= 5f) // Красный свет горит 5 секунд
                {
                    SetLight(1); // Переход к желтому
                }
                break;
            case 1: // Желтый
                if (timer >= 2f) // Желтый свет горит 2 секунды
                {
                    SetLight(2); // Переход к зеленому
                }
                break;
            case 2: // Зеленый
                if (timer >= 5f) // Зеленый свет горит 5 секунд
                {
                    SetLight(0); // Переход к красному
                }
                break;
        }
    }

    void SetLight(int light)
    {
        // Сброс таймера
        timer = 0f;

        // Выключаем все огни
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);

        // Включаем нужный свет
        switch (light)
        {
            case 0:
                redLight.SetActive(true);
                break;
            case 1:
                yellowLight.SetActive(true);
                break;
            case 2:
                greenLight.SetActive(true);
                break;
        }

        currentLight = light; // Обновляем текущее состояние светофора
    }
}
