using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnablesCount
{
    public int Min;
    public int Max;
}

public abstract class Interactible : MonoBehaviour, IPlayerInteractible<Interactible>
{
    public delegate void InteractionHandler(Interactible a);
    public InteractionHandler OnInteractionStart;
    public InteractionHandler OnInteractionStop;

    private InteractionTrigger _myTrigger;


    protected virtual void Awake()
    {
        _myTrigger = GetComponentInChildren<InteractionTrigger>(true);
        _myTrigger.OnEnter.AddListener(() => StartInteraction());
        _myTrigger.OnExit.AddListener(() => StopInteraction());
    }

    public virtual void StartInteraction()
    {
        OnInteractionStart?.Invoke(this);
    }
    public virtual void StopInteraction()
    {
        OnInteractionStop?.Invoke(this);
    }

    protected void RemoveTrigger()
    {
        _myTrigger.RemoveRadius();
    }

    public void ResetTrigger()
    {
        _myTrigger.ResetTrigger();        
    }
}
