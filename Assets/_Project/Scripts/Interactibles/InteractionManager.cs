using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private PlayerInteraction _playerInteraction;

    private Interactible[] _interactibles;

    private void Awake()
    {
        _playerInteraction = FindObjectOfType<PlayerInteraction>();
        _interactibles = FindObjectsOfType<Interactible>();

        _playerInteraction.OnFinishInteraction.AddListener(ResetTriggers);
    }

    private void ResetTriggers()
    {
        _interactibles = FindObjectsOfType<Interactible>();
        foreach (var interactible in _interactibles)
        {
            interactible.ResetTrigger();
        }
    }
}
