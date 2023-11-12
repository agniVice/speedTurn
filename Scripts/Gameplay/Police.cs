using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private Sprite[] _carSprites;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 45f;

    private Transform _playerTransform;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerTransform = Player.Instance.transform;
        GetComponent<SpriteRenderer>().sprite = _carSprites[PlayerPrefs.GetInt("PoliceCarId")];
    }

    private void FixedUpdate()
    {
        if (_playerTransform != null)
        {
            Vector2 directionToPlayer = _playerTransform.position - transform.position;

            directionToPlayer.Normalize();

            float angle = Vector2.SignedAngle(transform.up, directionToPlayer);

            _rigidBody.angularVelocity = angle * _rotationSpeed;
        }
        else
        {
            _speed *= 0.95f;
            _rigidBody.angularVelocity = 0;
        }
        _rigidBody.velocity = transform.up * _speed;
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Wrong, 1f);

            Player.Instance.DestroyPlayer();
        }
        if (collision.gameObject.CompareTag("Police"))
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Burst, 1f);

            SpawnParticle();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
