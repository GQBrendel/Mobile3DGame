using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyStats))]
public class Enemy : Interactible
{
    [SerializeField] private BlockStarter _pushBlock;

    protected Animator Animator;
    protected EnemyStats EnemyStats;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out Animator);
        TryGetComponent(out EnemyStats);
    }

    internal void Hit(int playerDamage)
    {
        Animator.SetTrigger("Hit");
        _pushBlock.ExecuteBlock();

        EnemyStats.Hit(playerDamage);
    }
}
