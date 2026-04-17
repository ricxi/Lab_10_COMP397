using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private float projectileForce = 30.0f;

    private InputAction _fire;

    private void Awake()
    {
        _fire = InputSystem.actions.FindAction("Player/Attack");
    }

    private void OnEnable()
    {
        _fire.started += ShootPooledBullet;
    }

    private void OnDisable()
    {
        _fire.started -= ShootPooledBullet;
    }

    private void ShootPooledBullet(InputAction.CallbackContext _)
    {
        Bullet bullet = BulletObjectPool.Instance.Get();
        bullet.transform.SetPositionAndRotation(projectileSpawn.position, projectileSpawn.rotation);
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileForce, ForceMode.Impulse);
    }
}
