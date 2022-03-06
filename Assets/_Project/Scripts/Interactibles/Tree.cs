using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactible
{
    private int _maxHp = 3;
    private int _currentHp;

    private void Awake()
    {
        base.Awake();
        _currentHp = _maxHp;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        Debug.Log("Entrou na arvere");
    }
    public override void StopInteraction()
    {
        base.StopInteraction();
        Debug.Log("Saiu da arvere");
    }

    internal void Hit(int damage)
    {
        _currentHp -= damage;
        if(_currentHp <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        RemoveTrigger();
        StartCoroutine(WaitAndDestroy());

        IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(.1f);
            gameObject.SetActive(false);
        }
    }
}
