using System;
using System.Collections;

using UnityEngine;

public class Weapon : MonoBehaviour, IWeaponInteractable
{
    [Header("Basic Weapon values")]
    [SerializeField] private int _damage;
    [SerializeField] private int _projectileSpeed;
    [SerializeField] private float _attackDelay;
    private RaycastHit _hitInfo;

    [Header("Spread values")]
    [SerializeField] private float _spreadAimed;
    [SerializeField] private float _spreadDefault;
    private float _timer;
    public bool IsAiming { get; set; }

    [Header("Objects")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _muzzleFlash;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] _shotSounds;
    private AudioSource _audioSource;

    private Coroutine _coroutine;

    [field: SerializeField] public Transform LeftHandTarget { get; private set; }
    public event Action OnShotEvent;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void PerformAttack(bool shootInput)
    {
        if (shootInput)
            _coroutine = StartCoroutine(Shot());
        else
            StopCoroutine(_coroutine);
    }
    private IEnumerator Shot()
    {
        if (!IsAiming)
            yield return new WaitForSeconds(0.15f);

        while (true)
        {
            if (_timer >= _attackDelay)
            {
                _muzzleFlash.Play();
                OnShotEvent?.Invoke();
                SpawnProjectile(Spread(IsAiming));
                _audioSource.PlayOneShot(_shotSounds[UnityEngine.Random.Range(0, _shotSounds.Length)]);
                _timer = 0;
            }
            yield return null;
        }
    }

    public Weapon WeaponInteract()
    {
        return this;
    }

    private void SpawnProjectile(Vector3 spread)
    {
        if (Physics.Raycast(_muzzle.transform.position, spread, out _hitInfo))
        {
            GameObject bullet = ObjectPool.Instance.GetProjectile();
            bullet.TryGetComponent(out Projectile projectile);
            projectile.SetProjectile(_muzzle.position, _muzzle.rotation, _damage, _projectileSpeed, _hitInfo.point);
        }
    }
    private Vector3 Spread(bool aimed)
    {
        Vector3 direction = _muzzle.forward;

        float spreadX = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault);
        float spreadY = aimed ? UnityEngine.Random.Range(-_spreadAimed, _spreadAimed) : UnityEngine.Random.Range(-_spreadDefault, _spreadDefault);

        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
        return spreadRotation * direction;
    }
}
