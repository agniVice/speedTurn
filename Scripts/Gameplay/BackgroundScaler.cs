using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private BoxCollider2D backgroundCollider;
    private SpriteRenderer backgroundSpriteRenderer;

    private void Start()
    {
        // Получаем компоненты коллайдера и спрайт-рендера
        backgroundCollider = GetComponent<BoxCollider2D>();
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();

        // Проверяем, если нет компонентов, выходим
        if (backgroundCollider == null || backgroundSpriteRenderer == null)
        {
            Debug.LogError("BackgroundScaler: Missing components (BoxCollider2D or SpriteRenderer).");
            return;
        }
    }

    private void Update()
    {
        // Расчет размера фона и коллайдера
        Vector2 backgroundSize = backgroundSpriteRenderer.bounds.size;
        Vector2 colliderSize = backgroundCollider.size;

        // Расширяем коллайдер, если камера выходит за его рамки
        if (Camera.main.transform.position.x < backgroundCollider.bounds.min.x)
        {
            colliderSize.x += backgroundSize.x;
            backgroundCollider.size = colliderSize;
        }
    }
}
