using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SlimeController : Enemy
{
    [SerializeField] private float _lookRadius;
    [SerializeField] private float _rotationSlerp = 5f;
    [SerializeField] private float _attackInitialDelay = 2f;
    [SerializeField] private float _attackCooldown = 3f;
    [SerializeField] private float _cooldownCount = 0f;

    [SerializeField] private EnemyAIState _state;

    private Transform _target;
    private NavMeshAgent _agent;

    //private bool _attacking = false;

    private float _distance;


    private void Start()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out Animator);
        _target = PlayerManager.Instance.GetPlayerTransform();
    }

    private void Update()
    {
        _distance = Vector3.Distance(transform.position, _target.position);

        if (!IsAlive)
        {
            _state = EnemyAIState.DeadState;
        }

        switch (_state)
        {
            case EnemyAIState.IdleState:

                if (_distance <= _lookRadius)
                {
                    _state = EnemyAIState.ChasingState;
                }
                break;

            case EnemyAIState.ChasingState:
                Move();
                if (_distance <= _agent.stoppingDistance)
                {
                    StartCoroutine(ChangeToAttackStateRoutine());
                }
                break;

            case EnemyAIState.AttackingState:

                // if (!_attacking)
                {
                    StartCoroutine(AttackRoutine());
                }
                FaceTarget();

                if (_distance > _agent.stoppingDistance + 1)
                {
                    StopAttack();
                    _state = EnemyAIState.ChasingState;
                }
                break;
            case EnemyAIState.DeadState:
                break;
        }

        if (_cooldownCount > 0)
        {
            _cooldownCount -= Time.deltaTime;
        }
    }

    private IEnumerator ChangeToAttackStateRoutine()
    {
        yield return new WaitForSeconds(_attackInitialDelay);
        _state = EnemyAIState.AttackingState;
    }

    private void Move()
    {
        _agent.SetDestination(_target.position);
        Animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    private IEnumerator AttackRoutine()
    {
       // _attacking = true;

        Animator.SetBool("Attack", true);
        Animator.SetFloat("Cooldown", _cooldownCount);

        yield return null;

    }

    private void StopAttack()
    {
        Animator.SetBool("Attack", false);
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

    //Animation Event
    private void AttackHit()
    {
        _cooldownCount = _attackCooldown;
        PlayerManager.Instance.ApplyPlayerHit(EnemyStats.Damage);
    }
}
