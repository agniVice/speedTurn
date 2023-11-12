using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUserInterface : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _settingsPanel;

    [Header("Settings")]
    [SerializeField] private Button _soundToggle;
    [SerializeField] private Button _musicToggle;
    [SerializeField] private Button _vibrationToggle;

    [SerializeField] private Sprite _soundEnabled;
    [SerializeField] private Sprite _soundDisabled;

    [SerializeField] private Sprite _musicEnabled;
    [SerializeField] private Sprite _musicDisabled;

    [SerializeField] private Sprite _vibrationEnabled;
    [SerializeField] private Sprite _vibrationDisabled;

    [SerializeField] private List<Transform> _transformsMenu = new List<Transform>();
    [SerializeField] private List<Transform> _transformsSettings = new List<Transform>();

    [SerializeField] private Button _playButton;
    [SerializeField] private Image _fadeIn;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        AudioVibrationManager.Instance.SoundChanged += UpdateSoundImage;
        AudioVibrationManager.Instance.MusicChanged += UpdateMusicImage;
        //AudioVibrationManager.Instance.VibrationChanged += UpdateVibrationImage;

        _soundToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleSound);
        _musicToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleMusic);
        //_vibrationToggle.onClick.AddListener(AudioVibrationManager.Instance.ToggleVibration);

        UpdateSoundImage();
        UpdateMusicImage();
        //UpdateVibrationImage();

        _fadeIn.transform.DOScale(0, 0.5f).SetLink(_fadeIn.gameObject);
        _fadeIn.DOFade(0, 0.5f).SetLink(_fadeIn.gameObject);

        OnCloseSettingsButtonClicked();
    }
    private void OnDisable()
    {
        AudioVibrationManager.Instance.SoundChanged -= UpdateSoundImage;
        AudioVibrationManager.Instance.MusicChanged -= UpdateMusicImage;
        //AudioVibrationManager.Instance.VibrationChanged -= UpdateVibrationImage;
    }
    private void UpdateSoundImage()
    {
        if (AudioVibrationManager.Instance.IsSoundEnabled) 
            _soundToggle.GetComponent<Image>().sprite = _soundEnabled;
        else
            _soundToggle.GetComponent<Image>().sprite = _soundDisabled;
    }
    private void UpdateMusicImage()
    {
        if (AudioVibrationManager.Instance.IsMusicEnabled)
            _musicToggle.GetComponent<Image>().sprite = _musicEnabled;
        else
            _musicToggle.GetComponent<Image>().sprite = _musicDisabled;
    }
    private void UpdateVibrationImage()
    {
        if (AudioVibrationManager.Instance.IsVibrationEnabled)
            _vibrationToggle.GetComponent<Image>().sprite = _vibrationEnabled;
        else
            _vibrationToggle.GetComponent<Image>().sprite = _vibrationDisabled;
    }
    public void OnSettingsButtonClicked()
    {
        _menuPanel.SetActive(false);
        _settingsPanel.SetActive(true);

        foreach (Transform transform in _transformsSettings)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    public void OnCloseSettingsButtonClicked()
    {
        _menuPanel.SetActive(true);
        _settingsPanel.SetActive(false);

        foreach (Transform transform in _transformsMenu)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(1, Random.Range(0.2f, 0.7f)).SetEase(Ease.OutBack).SetLink(transform.gameObject).SetUpdate(true);
        }
    }
    public void OnPlayButtonClicked()
    {
        _playButton.enabled = false;
        _playButton.transform.DOScale(0.8f, 0.2f).SetLink(_playButton.gameObject).SetEase(Ease.OutBack);
        _playButton.transform.DOScale(1f, 0.2f).SetLink(_playButton.gameObject).SetEase(Ease.OutBack).SetDelay(0.2f);

        _fadeIn.color = new Color32(0, 0, 0, 255);
        _fadeIn.transform.localScale = Vector3.zero;

        _fadeIn.DOFade(1, 0.5f).SetLink(_fadeIn.gameObject).SetDelay(0.2f);
        _fadeIn.transform.DOScale(30f, 0.5f).SetLink(_fadeIn.gameObject).SetDelay(0.2f); ;

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Click, 1f, 0.7f);

        Invoke("PlayLoad", 0.65f);
    }
    private void PlayLoad()
    {
        SceneLoader.Instance.LoadScene("Gameplay");
    }
    public void UpdateLanguageIcon()
    {

    }
    public void OnExitButtonClicked()
    { 
        Application.Quit();
    }
}
