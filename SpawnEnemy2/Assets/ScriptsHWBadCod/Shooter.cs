using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Gun _objectToShoot;
    [SerializeField] private TargetForShoot _target;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _timeWaitShooting;
    [SerializeField] public float _speed;
    [SerializeField] private int _poolCapacity = 30;
    [SerializeField] private int _maxPoolSize = 30;


    private ObjectPool<Bullet> _poolBullets;

    private void Awake()
    {
        _poolBullets = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_bulletPrefab),
            actionOnGet: (bullet) => ActionOnGet(bullet),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxPoolSize
        );
    }

    private void Start()
    {
        StartCoroutine(Shoot());
    }
    
    private void ActionOnGet(Bullet bullet)
    {
        bullet.OnHit += ReturnToPool;
        bullet.gameObject.SetActive(true);
        
        var vector3Direction = (_target.GetPosition - _objectToShoot.transform.position).normalized;
        bullet.Initialize(vector3Direction, _objectToShoot.transform.position, _speed);
    }

    private void ReturnToPool(Bullet bullet)
    {
        bullet.OnHit -= ReturnToPool;
        _poolBullets.Release(bullet);
    }
    
    private void GetBullet()
    {
        _poolBullets.Get();
    }
    
    private IEnumerator Shoot()
    {
        while (enabled)
        {
            GetBullet();
            yield return new WaitForSeconds(_timeWaitShooting);
        }
    }
}

