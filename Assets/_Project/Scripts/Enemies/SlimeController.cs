using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class SlimeController : Enemy
{
    [SerializeField] private float _lookRadius;
    [SerializeField] private float _rotationSlerp = 5f;

    [SerializeField] private EnemyAIState _state;

    private Transform _target;
    private NavMeshAgent _agent;
    private Animator _animator;

    private float _distance;


    private void Start()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out _animator);
        _target = PlayerManager.Instance.GetPlayerTransform();
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, _target.position);

        switch (_state)
        {
            case EnemyAIState.IdleState:

                if(_distance <= _lookRadius)
                {
                    _state = EnemyAIState.ChasingState;
                }
                break;

            case EnemyAIState.ChasingState:
                Move();
                if (_distance <= _agent.stoppingDistance)
                {
                    FaceTarget();
                }
                break;

            case EnemyAIState.AttackingState:
                break;
        }     
    }

    private void Move()
    {
        _agent.SetDestination(_target.position);
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }


    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSlerp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
        
    }
}
