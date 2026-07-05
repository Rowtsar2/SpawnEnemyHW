using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Mover _mover;
    
    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public float Speed { get; private set; }
    
    public event Action<Enemy> ComeToTargetPoint;
    
    private void Start()
    {
        Speed = _speed;
    }

    public void ComeToTarget() => ComeToTargetPoint?.Invoke(this);
    
    public void SetDirection(Transform targetPoint)
    {
        _mover.Move(this, targetPoint);
    }
}
