using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody Rigidbody;
    
    public event Action<Bullet> OnHit;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TargetForShoot>(out var target)) 
        {
            OnHit?.Invoke(this);
        }
    }
    
    public void Initialize(Vector3 vector3Direction, Vector3 startPosition, float speed)
    {
        transform.position = startPosition + vector3Direction;
        Rigidbody.transform.up = vector3Direction;
        Rigidbody.velocity = vector3Direction * speed;
    }
}
