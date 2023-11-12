using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private GameObject _panel;

    private bool _isInitialized;

    private void OnEnable()
    {
        if (!_isInitialized)
            return;

        SubscribeAll();
    }
    private void OnDisable()
    {
        UnsubscribeAll();
    }
    public void Initialize()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;

        Hide();

        _isInitialized = true;
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameLosed += Show;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GameLosed -= Show;
    }
    private void Show()
    {
        _panel.SetActive(true);
    }
    private void Hide()
    {
        _panel.SetActive(false);
    }
    public void OnRestartButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    public void OnMenuButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Menu");
    }
    public void OnNextButtonClicked()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
}