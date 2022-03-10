using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ResourceProvider
{
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
}
