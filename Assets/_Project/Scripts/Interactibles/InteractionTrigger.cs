using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnEnter;

    [HideInInspector] public UnityEvent OnExit;

    private Transform _transform;
    private Vector3 _originalScale;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _originalScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnExit?.Invoke();
        }
    }

    public void RemoveRadius()
    {
        _transform.localScale = Vector3.zero;
    }

    public void ResetTrigger()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        _transform.localScale = Vector3.zero;
        StartCoroutine(WaitAndRestore());

        IEnumerator WaitAndRestore()
        {
            yield return new WaitForSeconds(.1f);
            _transform.localScale = _originalScale;
        }
    }
}