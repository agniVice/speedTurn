using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class HUD : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _scoreText;

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

        Show();

        _isInitialized = true;

        _scoreText.text = "0";

        UpdateScore();
    }
    public void SubscribeAll()
    {
        GameState.Instance.GameFinished += Hide;
        GameState.Instance.GamePaused += Hide;
        GameState.Instance.GameUnpaused += Show;

        GameState.Instance.ScoreAdded += UpdateScore;
    }
    public void UnsubscribeAll()
    {
        GameState.Instance.GameFinished -= Hide;
        GameState.Instance.GamePaused -= Hide;
        GameState.Instance.GameUnpaused -= Show;

        GameState.Instance.ScoreAdded -= UpdateScore;
    }
    private void UpdateScore()
    {
        _scoreText.transform.DOScale(1.5f, 0.3f).SetLink(_scoreText.gameObject).SetUpdate(true);
        _scoreText.transform.DOScale(1, 0.3f).SetLink(_scoreText.gameObject).SetDelay(0.3f).SetUpdate(true);

        _scoreText.text = PlayerScore.Instance.Score.ToString();
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
    public void OnPauseButtonClicked()
    {
        GameState.Instance.PauseGame();
    }
}