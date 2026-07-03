using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 30;
    [SerializeField] private int _maxPoolSize = 30;
    
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => GetRandomEnemy(),
            actionOnGet: (enemy) => ActionOnGet(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maxPoolSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0, _repeatRate);
    }
    
    private void ActionOnGet(Enemy enemy)
    {
        Transform target = GetRandomTargetPoint();

        enemy.ComeToTargetPoint += ReturnToPool;
        
        enemy.transform.position = GetRandomSpawnPoint().position;
        enemy.SetDirection(target);
        enemy.transform.LookAt(target);
        enemy.gameObject.SetActive(true);
    }

    private void ReturnToPool(Enemy enemy)
    {
        enemy.ComeToTargetPoint -= ReturnToPool;
        _pool.Release(enemy);
    }
    
    private void GetEnemy()
    {
        _pool.Get();
    }
    
    private Enemy GetRandomEnemy() => Instantiate(_enemies[UnityEngine.Random.Range(0, _enemies.Count)]);

    private Transform GetRandomSpawnPoint() => _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length - 1)].transform;
    
    private Transform GetRandomTargetPoint() => _targetPoints[UnityEngine.Random.Range(0, _spawnPoints.Length - 1)].transform;

}
