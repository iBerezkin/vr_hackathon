using UnityEngine;

public class FrontHeadlightController : MonoBehaviour
{
    public Renderer RFheadlightRenderer; // Ссылка на рендерер сферы
    public Renderer RBheadlightRenderer; // Ссылка на рендерер сферы
    public Renderer LFheadlightRenderer; // Ссылка на рендерер сферы
    public Renderer LBheadlightRenderer; // Ссылка на рендерер сферы

    private void Start()
    {
        SetHeadlightColor(Color.white); // Устанавливаем цвет сферы на белый при запуске
    }

    private void SetHeadlightColor(Color color)
    {
        RFheadlightRenderer.material.color = color;
        LBheadlightRenderer.material.color = color;
        RBheadlightRenderer.material.color = color;
        LFheadlightRenderer.material.color = color;
        // Меняем цвет материала сферы
    }
}
