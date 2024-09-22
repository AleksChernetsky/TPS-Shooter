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
    public void PerformAttack(bool aimed)
    {
        if (_timer >= _attackSpeed)
        {
            OnShoot?.Invoke();
            _muzzle.Rotate(Spread(aimed));

            _muzzleFlash.Play();

            SpawnProjectile();
            _audioSource.PlayOneShot(_shotSounds[UnityEngine.Random.Range(0, _shotSounds.Length)]);

            _timer = 0;
        }
    }
    private void SpawnProjectile()
    {
        GameObject bullet = ProjectilePool.instance.GetProjectile();
        bullet.transform.SetPositionAndRotation(_muzzle.position, _muzzle.rotation);
        bullet.SetActive(true);
        bullet.TryGetComponent(out Projectile projectile);
        projectile.Damage = _damage;
        projectile.RigidBody.AddForce(_muzzle.forward * projectile.Speed, ForceMode.Impulse);
    }
    private Vector3 Spread(bool aimed)
    {
        return new Vector3
        {
            x = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault),
            y = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault),
            z = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault)
        };
    }
}
