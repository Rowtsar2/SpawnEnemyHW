using UnityEngine;

public class Mover : MonoBehaviour
{
    private Enemy _enemyForMove;
    private Transform _enemyPosition;
    private Transform _targetPosition;

    public void Move(Enemy enemy, Transform target)
    {
        _enemyForMove = enemy;
        _enemyPosition = enemy.transform;
        _targetPosition = target;
    }

    private void Update()
    {
        _enemyForMove.transform.position = Vector3.MoveTowards(_enemyPosition.position, _targetPosition.position,
        _enemyForMove.Speed * Time.deltaTime);
        
        if (Vector3.Distance(_enemyPosition.position, _targetPosition.position) < 0.1f)
        {
            _enemyForMove.ComeToTarget();
        }
    }
}
