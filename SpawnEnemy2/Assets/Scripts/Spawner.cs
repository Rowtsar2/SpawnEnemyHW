using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrafab;
    [SerializeField] private Target _targetPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 30;
    [SerializeField] private int _maxPoolSize = 30;
    
    private ObjectPool<Enemy> _pool;
    private Target _targetObject;
    
    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrafab),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxPoolSize
            );
    }

    private void Start()
    {
        _targetObject = Instantiate(_targetPrefab);
        _targetObject.SetDirection(_targetPoints);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GetEnemy();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
    
    private void ActionOnGet(Enemy enemy)
    {
        enemy.ComeToTargetPoint += ReturnToPool;
        
        enemy.transform.position = _spawnPoint.position;
        enemy.SetDirection(_targetObject.transform);
        enemy.transform.LookAt(_targetObject.transform);
        enemy.gameObject.SetActive(true);
    }

    private void ReturnToPool(Enemy enemy)
    {
        enemy.ComeToTargetPoint -= ReturnToPool;
        _pool.Release(enemy);
    }
    
    private void GetEnemy()
    {
        if (_pool.CountActive < _maxPoolSize)
        {
            _pool.Get();
        }
    }
}
