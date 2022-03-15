using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class EnemyStats : MonoBehaviour
{
    public UnityEvent OnLostAllHealth;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _maxHp = 300;
    [SerializeField] private int _damage = 1;

    public int Damage
    {
        get { return _damage; }
    }

    private NavMeshAgent _agent;
    private Enemy _enemy;


    private int _currentHp;

    private void Awake()
    {
        TryGetComponent(out _agent);
        TryGetComponent(out _enemy);

        _agent.speed = _moveSpeed;

        _currentHp = _maxHp;
    }

    internal void Hit(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        OnLostAllHealth?.Invoke();
        /*
        StartCoroutine(WaitAndDestroy());

        IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(.1f);
        }*/
    }
}
