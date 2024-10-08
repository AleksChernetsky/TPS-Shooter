using System.Threading.Tasks;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private int _damage;
    private int _speed;

    private float _timeToDestroy = 0.5f;
    private float _timer;

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToDestroy)
        {
            ResetProjectile();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        if (collision.gameObject.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(_damage);
        }
        PlayHitEffect(contact.point, Quaternion.LookRotation(contact.normal));
        ResetProjectile();
    }
    public void SetProjectile(Vector3 position, Quaternion rotation, int damage, int speed, Vector3 hitPosition)
    {
        transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
        _damage = damage;
        _speed = speed;
        _rigidbody.AddForce((hitPosition - position) * _speed, ForceMode.Impulse);
    }
    public void ResetProjectile()
    {
        _timer = 0;
        gameObject.SetActive(false);
        _rigidbody.linearVelocity = Vector3.zero;
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
    private async void PlayHitEffect(Vector3 position, Quaternion rotation)
    {
        GameObject hitEffect = ObjectPool.Instance.GetHitEffect();
        hitEffect.transform.SetPositionAndRotation(position, rotation);
        hitEffect.SetActive(true);
        hitEffect.TryGetComponent(out ParticleSystem particleSystem);
        await Task.Delay((int)particleSystem.main.duration * 1000);
        hitEffect.SetActive(false);
    }
}