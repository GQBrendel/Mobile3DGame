using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleItem : Interactible
{
    public UnityEvent OnItemCollected;

    [SerializeField] private GameObject _itemRender;

    [SerializeField] private float _totalGrabTime = 2f;
    [SerializeField] private float _currentGrabTime = 0f;

    private Coroutine _doingInteractionRoutine;
    private Coroutine _stopingInteractionRoutine;

    private void ItemFinishedInteraction()
    {
        //_itemRender.SetActive(false);
        OnItemCollected?.Invoke();
        Destroy(gameObject);
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (_stopingInteractionRoutine != null)
        {
            StopCoroutine(_stopingInteractionRoutine);
        }

        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        _doingInteractionRoutine = StartCoroutine(StartInteractionRoutine());
        IEnumerator StartInteractionRoutine()
        {
            while (_currentGrabTime < _totalGrabTime)
            {
                _currentGrabTime += Time.deltaTime;
                yield return null;
            }
            _currentGrabTime = _totalGrabTime;
            ItemFinishedInteraction();
        }
    }

    public override void StopInteraction()
    {
        base.StopInteraction();
        if (_doingInteractionRoutine != null)
        {
            StopCoroutine(_doingInteractionRoutine);
        }

        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        _stopingInteractionRoutine = StartCoroutine(StopInteractionRoutine());
        IEnumerator StopInteractionRoutine()
        {
            while (_currentGrabTime > 0)
            {
                _currentGrabTime -= Time.deltaTime;
                yield return null;
            }
            _currentGrabTime = 0;
        }
    }
}