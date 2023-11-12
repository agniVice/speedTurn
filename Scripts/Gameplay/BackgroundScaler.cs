using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private BoxCollider2D backgroundCollider;
    private SpriteRenderer backgroundSpriteRenderer;

    private void Start()
    {
        // �������� ���������� ���������� � ������-�������
        backgroundCollider = GetComponent<BoxCollider2D>();
        backgroundSpriteRenderer = GetComponent<SpriteRenderer>();

        // ���������, ���� ��� �����������, �������
        if (backgroundCollider == null || backgroundSpriteRenderer == null)
        {
            Debug.LogError("BackgroundScaler: Missing components (BoxCollider2D or SpriteRenderer).");
            return;
        }
    }

    private void Update()
    {
        // ������ ������� ���� � ����������
        Vector2 backgroundSize = backgroundSpriteRenderer.bounds.size;
        Vector2 colliderSize = backgroundCollider.size;

        // ��������� ���������, ���� ������ ������� �� ��� �����
        if (Camera.main.transform.position.x < backgroundCollider.bounds.min.x)
        {
            colliderSize.x += backgroundSize.x;
            backgroundCollider.size = colliderSize;
        }
    }
}
