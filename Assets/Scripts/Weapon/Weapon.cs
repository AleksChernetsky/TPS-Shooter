using System;

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Basic Weapon values")]
    [SerializeField] private int _damage;
    [Tooltip("Lower = Faster")][SerializeField] private float _attackSpeed;

    [Header("Spread values")]
    [SerializeField] private float _spreadAimed;
    [SerializeField] private float _spreadDefault;

    [Header("Objects")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [field: SerializeField] public Transform WeaponCamera { get; private set; }
    [field: SerializeField] public Transform LeftHandPlace { get; private set; }

    [Header("Sounds")]
    [SerializeField] private AudioClip[] _shotSounds;
    private AudioSource _audioSource;

    private float _timer;

    public event Action OnShoot;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }
    private void SpawnProjectile(Vector3 spread)
    {
        GameObject bullet = ProjectilePool.instance.GetProjectile();
        bullet.transform.SetPositionAndRotation(_muzzle.position, _muzzle.rotation);
        bullet.SetActive(true);
        bullet.TryGetComponent(out Projectile projectile);
        projectile.Damage = _damage;
        projectile.RigidBody.AddForce(spread * projectile.Speed, ForceMode.Impulse);
    }
    private Vector3 Spread(bool aimed)
    {
        float spreadX = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault);
        float spreadY = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault);

        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
        return spreadRotation * _muzzle.forward;
    }
    public void PerformAttack(bool aimed)
    {
        if (_timer >= _attackSpeed)
        {
            OnShoot?.Invoke();
            _muzzleFlash.Play();

            SpawnProjectile(Spread(aimed));
            _audioSource.PlayOneShot(_shotSounds[UnityEngine.Random.Range(0, _shotSounds.Length)]);

            _timer = 0;
        }
    }
}
