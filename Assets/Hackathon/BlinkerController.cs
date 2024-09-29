using UnityEngine;

public class BlinkerController : MonoBehaviour
{
    public Renderer LBblinkerRenderer; // Ссылка на рендерер сферы
    public Renderer LFblinkerRenderer; // Ссылка на рендерер сферы
    

    public float blinkInterval = 0.5f; // Интервал мигания
    private float nextBlinkTime = 0f; // Время следующего мигания
    private bool isBlinking = false; // Состояние мигания

    private void Start()
    {
        //StartBlinking(); // Начинаем мигание при запуске
    }

    private void Update()
    {
        if (isBlinking)
        {
            if (Time.time >= nextBlinkTime)
            {
                ToggleBlinkers(); // Переключаем состояние мигания
                nextBlinkTime = Time.time + blinkInterval; // Устанавливаем время следующего мигания
            }
        }
    }

    private void ToggleBlinkers()
    {
        Color currentColor = LBblinkerRenderer.material.color;

        if (currentColor == Color.clear) // Если цвет прозрачный, устанавливаем оранжевый
        {
            LBblinkerRenderer.material.color = new Color(1f, 0.647f, 0f); // Оранжевый
           
            LFblinkerRenderer.material.color = new Color(1f, 0.647f, 0f); // Оранжевый
           

        }
        else
        {
            LFblinkerRenderer.material.color = Color.clear; // Прозрачный
           
            LBblinkerRenderer.material.color = Color.clear; // Прозрачный

        }
    }

    public void StartBlinking()
    {
        isBlinking = true; // Начинаем мигание
    }

    public void StopBlinking()
    {
        isBlinking = false; // Останавливаем мигание
        LFblinkerRenderer.material.color = Color.clear; // Прозрачный
        LBblinkerRenderer.material.color = Color.clear; // Прозрачный // Устанавливаем прозрачный цвет
    }
}
