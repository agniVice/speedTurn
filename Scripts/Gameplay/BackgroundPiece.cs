using UnityEngine;

public class BackgroundPiece : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BackgroundManager.Instance.SetCenter(_spriteRenderer);
        }
    }
}
