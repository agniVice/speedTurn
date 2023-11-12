using UnityEngine;

public class Player : MonoBehaviour, ISubscriber
{
    public static Player Instance;

    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private Sprite[] _carSprites;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody2D _rigidBody;

    private bool _turningRight;
    private bool _turningLeft;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _rigidBody = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = _carSprites[PlayerPrefs.GetInt("PlayerCarId")];
    }
    public void SubscribeAll()
    {
        PlayerInput.Instance.PlayerMouseDownLeft += StartTurnLeft;
        PlayerInput.Instance.PlayerMouseUpLeft += StopTurnLeft;

        PlayerInput.Instance.PlayerMouseDownRight += StartTurnRight;
        PlayerInput.Instance.PlayerMouseUpRight += StopTurnRight;
    }
    public void UnsubscribeAll()
    {
        PlayerInput.Instance.PlayerMouseDownLeft -= StartTurnLeft;
        PlayerInput.Instance.PlayerMouseUpLeft -= StopTurnLeft;

        PlayerInput.Instance.PlayerMouseDownRight -= StartTurnRight;
        PlayerInput.Instance.PlayerMouseUpRight -= StopTurnRight;
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        _rigidBody.velocity = transform.up * _speed;

        if (_turningLeft)
            _rigidBody.angularVelocity = _rotationSpeed;
        else if (_turningRight)
            _rigidBody.angularVelocity = -_rotationSpeed;
        else
            _rigidBody.angularVelocity = 0;
    }
    public void DestroyPlayer()
    {
        GameState.Instance.FinishGame();
        SpawnParticle();
        //AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Burst, 1f);
        CameraController.Instance.OnPlayerDestroy();
        Destroy(gameObject);
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void StartTurnLeft()
    {
        if (_turningRight)
            _turningRight = false;
        //CameraController.Instance.ScaleDown();
        _turningLeft = true;
    }
    private void StartTurnRight()
    {
        if (_turningLeft)
            _turningLeft = false;
        //CameraController.Instance.ScaleDown();
        _turningRight = true;
    }
    private void StopTurnLeft()
    {
        //CameraController.Instance.ScaleUp();
        _turningLeft = false;
    }
    private void StopTurnRight()
    {
        //CameraController.Instance.ScaleUp();
        _turningRight = false;
    }
}