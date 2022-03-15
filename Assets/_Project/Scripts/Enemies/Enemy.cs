using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyStats))]
public class Enemy : Interactible
{
    [SerializeField] private BlockStarter _pushBlock;
    [SerializeField] private GameObject _selectionRing;

    protected Animator Animator;
    protected EnemyStats EnemyStats;

    public bool IsAlive { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out Animator);
        TryGetComponent(out EnemyStats);
        SetRingState(false);

        IsAlive = true;

        EnemyStats.OnLostAllHealth.AddListener(HandleAllHealthLost);
    }

    internal void Hit(int playerDamage)
    {
        Animator.SetTrigger("Hit");
        _pushBlock.ExecuteBlock();

        EnemyStats.Hit(playerDamage);
    }

    private void HandleAllHealthLost()
    {
        IsAlive = false;
        Animator.SetBool("Dead", true);
        SetRingState(false);
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    internal void SetRingState(bool v)
    {
        _selectionRing.SetActive(v);
    }
}
