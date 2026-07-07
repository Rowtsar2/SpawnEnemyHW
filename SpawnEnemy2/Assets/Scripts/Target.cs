using System;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _speed;

    private List<Transform> _targetPoints = new();
    private int _currentPoint = 1;
    
    public void SetDirection(Transform[] targetPoints)
    {
        foreach (var point in targetPoints)
        {
            _targetPoints.Add(point);
        }
    }

    private void Start()
    {
        transform.position = _targetPoints[0].position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoints[_currentPoint].position,
            _speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, _targetPoints[_currentPoint].position) < 0.1f)
        {
            _currentPoint++;

            if (_currentPoint >= _targetPoints.Count)
                _currentPoint = 0;
        }
    }
}
