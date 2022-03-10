using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
    public UnityEvent OnFinishInteraction;


    [SerializeField] private int _playerDamage = 1;
    [SerializeField] private Transform _playerHead;

    private Interactible[] _interactibles;

    private Animator _animator;

    private bool _inRange;
    private float _speed;

    private ResourceProvider _currentResourceProvider;

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

    public Transform GetPlayerHead()
    {
        return _playerHead;
    }

    private void HandleInteractionStart(Interactible interactible)
    {
        if (interactible.GetType() == typeof(Tree))
        {
            HandleTree(interactible.GetComponent<ResourceProvider>());
        }
        else if (interactible.GetType() == typeof(CollectibleItem))
        {
            Debug.Log("Hello CollectibleItem");
        }
        else if (interactible.GetType().BaseType == typeof(Enemy))
        {
            Debug.Log("Hello Enemy");
        }
    }

    private void HandleTree(ResourceProvider tree)
    {
        _inRange = true;
        _currentResourceProvider = tree;

        StartCoroutine(InTreeRangeRoutine());
    }

    private IEnumerator InTreeRangeRoutine()
    {
        while (_inRange)
        {
            Debug.Log(_speed);
            if (_speed < 0.01f)
            {
                transform.LookAt(_currentResourceProvider.transform);
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
        OnFinishInteraction?.Invoke();
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
        _currentResourceProvider.Hit(_playerDamage);
    }
}
