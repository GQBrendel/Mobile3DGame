using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInteractible<T>
{
    void StartInteraction();
    void StopInteraction();
}
