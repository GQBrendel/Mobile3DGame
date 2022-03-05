using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectibleItem : MonoBehaviour
{
    public UnityEvent OnItemCollected;

    [SerializeField] private GameObject _itemRender;

    [SerializeField] private float _totalGrabTime = 2f;
    [SerializeField] private float _currentGrabTime = 0f;

    private Coroutine DoingInteractionRoutine;
    private Coroutine StopingInteractionRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerTag>())
        {
            if (StopingInteractionRoutine != null)
            {
                StopCoroutine(StopingInteractionRoutine);
            }
            DoingInteractionRoutine = StartCoroutine(InteractionRoutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerTag>())
        {
            if (DoingInteractionRoutine != null)
            {
                StopCoroutine(DoingInteractionRoutine);
            }
            StopingInteractionRoutine = StartCoroutine(StopInteractionRoutine());
        }
    }

    private IEnumerator InteractionRoutine()
    {   while (_currentGrabTime < _totalGrabTime)
        {
            _currentGrabTime += Time.deltaTime;
            yield return null;
        }
        _currentGrabTime = _totalGrabTime;
        ItemFinishedInteraction();
    }

    private IEnumerator StopInteractionRoutine()
    {
        while (_currentGrabTime > 0)
        {
            _currentGrabTime -= Time.deltaTime;
            yield return null;
        }
        _currentGrabTime = 0;
    }

    private void ItemFinishedInteraction()
    {
        _itemRender.SetActive(false);
        OnItemCollected?.Invoke();
    }
}
