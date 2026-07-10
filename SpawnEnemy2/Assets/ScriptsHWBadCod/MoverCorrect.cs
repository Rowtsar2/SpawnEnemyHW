using System.Collections.Generic;
using UnityEngine;

public class MoverCorrect : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetPoints;
    [SerializeField] private float _seepd;

    private int _currentPoint = 0;

    private void Start()
    {
        transform.position = _targetPoints[_currentPoint].transform.position;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoints[_currentPoint].position,
            _seepd * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPoints[_currentPoint].position) < 0.1f)
        {
            _currentPoint++;

            if (_currentPoint >= _targetPoints.Count)
                _currentPoint = 0;
        }
    }
}