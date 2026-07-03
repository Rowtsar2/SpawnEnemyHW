using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public float Speed { get; private set; }

    public event Action<Enemy, Transform> Spawn;
    public event Action<Enemy> ComeToTargetPoint;
    
    private void Start()
    {
        Speed = _speed;
    }

    public void ComeToTarget() => ComeToTargetPoint?.Invoke(this);
    
    public void SetDirection(Transform targetPoint)
    {
        Spawn?.Invoke(this, targetPoint);
    }
}
