using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Interactible[] _interactibles;

    [SerializeField] private int _playerDamage = 1;

    private Animator _animator;

    private bool _inRange;
    private float _speed;

    private Tree _currentTree;

    private void Awake()
    {
        TryGetComponent(out _animator);
        _interactibles = FindObjectsOfType<Interactible>();
        foreach (var interactible in _interactibles)
        {
            interactible.OnInteractionStart += HandleInteractionStart;
            interactible.OnInteractionStop += HandleInteractionStop;
        }
    }

    private void HandleInteractionStart(Interactible interactible)
    {
        if (interactible.GetType() == typeof(Tree))
        {
            HandleTree(interactible.GetComponent<Tree>());
        }
        else if (interactible.GetType() == typeof(CollectibleItem))
        {
            Debug.Log("Hello CollectibleItem");
        }
    }

    private void HandleTree(Tree tree)
    {
        _inRange = true;
        _currentTree = tree;

        StartCoroutine(InTreeRangeRoutine());
    }

    private IEnumerator InTreeRangeRoutine()
    {
        while (_inRange)
        {
            Debug.Log(_speed);
            if (_speed < 0.01f)
            {
                transform.LookAt(_currentTree.transform);
                _animator.SetBool("Cut", true);
            }
            yield return null;
        }
    }

    private void HandleInteractionStop(Interactible interactible)
    {
        if (interactible.GetType() == typeof(Tree))
        {
            _inRange = false;
            _animator.SetBool("Cut", false);

        }
        else if (interactible.GetType() == typeof(CollectibleItem))
        {
        }
        ResetTriggers();
    }

    private void ResetTriggers()
    {
        foreach (var interactible in _interactibles)
        {
            interactible.ResetTrigger();
        }
    }

    private void OnDestroy()
    {
        foreach (var interactible in _interactibles)
        {
            interactible.OnInteractionStart -= HandleInteractionStart;
            interactible.OnInteractionStop -= HandleInteractionStop;
        }
    }

    internal void SetSpeed(float speed)
    {
        _speed = speed;
    }

    //Called by AnimationEvent
    public void HitTree()
    {
        _currentTree.Hit(_playerDamage);
    }
}
