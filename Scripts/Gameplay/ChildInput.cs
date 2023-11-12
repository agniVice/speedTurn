using UnityEngine;

public class ChildInput : MonoBehaviour
{
    [SerializeField] private int _side;

    // 0 - left
    // 1 - right

    private void OnMouseDown()
    {
        PlayerInput.Instance.OnPlayerMouseDown(_side);
    }
    private void OnMouseUp() 
    {
        PlayerInput.Instance.OnPlayerMouseUp(_side);
    }
}
