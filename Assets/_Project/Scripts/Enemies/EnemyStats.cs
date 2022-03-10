using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private NavMeshAgent _agent;

    private void Awake()
    {
        TryGetComponent(out _agent);
        _agent.speed = _moveSpeed;
    }
}
