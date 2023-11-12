using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    public static PoliceSpawner Instance;

    [Header("Other")]

    [SerializeField] private GameObject _policePrefab;

    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float _maxDistance = 5f;


    [SerializeField] private float _minTime = 2f;
    [SerializeField] private float _maxTime = 5f;

    [SerializeField] private float _spawnRate;

    private float _currentTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else Instance = this;
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;
        if (_currentTime < _spawnRate)
        {
            _currentTime += Time.fixedDeltaTime;
        }
        else
        {
            _currentTime = 0f;
            _spawnRate = Random.Range(_minTime, _maxTime);
            SpawnPolice();
        }
    }
    public void SpawnPolice()
    {
        float randomAngle = Random.Range(0, 2f * Mathf.PI);
        float randomDistance = Random.Range(_minDistance, _maxDistance);

        Vector3 spawnPosition = Player.Instance.transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f) * randomDistance;

        AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Spawned, 1f);

        Instantiate(_policePrefab, spawnPosition, Quaternion.identity);
    }
}
