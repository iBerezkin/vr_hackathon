using UnityEngine;

public class RearLightController : MonoBehaviour
{
    public Renderer rearLightRenderer; // Ссылка на рендерер сферы

    private void Start()
    {
        SetRearLightColor(Color.red); // Устанавливаем цвет сферы на красный при запуске
    }

    private void SetRearLightColor(Color color)
    {
        rearLightRenderer.material.color = color; // Меняем цвет материала сферы
    }
}
