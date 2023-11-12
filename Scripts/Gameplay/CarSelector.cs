using UnityEngine;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    [SerializeField] private Sprite[] _playerCars;
    [SerializeField] private Sprite[] _policeCars;

    [SerializeField] private Image _playerCar;
    [SerializeField] private Image _policeCar;

    private int _playerCarId;
    private int _policeCarId;


    private void Start()
    {
        _playerCarId = PlayerPrefs.GetInt("PlayerCarId", 0);
        _policeCarId = PlayerPrefs.GetInt("PoliceCarId", 0);

        UpdateCarImages();
    }
    public void NextPlayerCar()
    {
        if (_playerCarId == _playerCars.Length - 1)
            _playerCarId = 0;
        else
            _playerCarId++;

        UpdateCarImages();
    }
    public void PrevPlayerCar()
    {
        if (_playerCarId == 0)
            _playerCarId = _playerCars.Length-1;
        else
            _playerCarId--;

        UpdateCarImages();
    }
    public void NextPoliceCar() 
    {
        if (_policeCarId == _policeCars.Length - 1)
            _policeCarId = 0;
        else
            _policeCarId++;

        UpdateCarImages();
    }
    public void PrevPoliceCar() 
    {
        if (_policeCarId == 0)
            _policeCarId = _policeCars.Length - 1;
        else
            _policeCarId--;

        UpdateCarImages();
    }
    private void UpdateCarImages()
    {
        _playerCar.sprite = _playerCars[_playerCarId];
        _policeCar.sprite = _policeCars[_policeCarId];

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Swap, 1f);

        Save();
    }
    private void Save()
    {
        PlayerPrefs.SetInt("PlayerCarId", _playerCarId);
        PlayerPrefs.SetInt("PoliceCarId", _policeCarId);
    }
}
