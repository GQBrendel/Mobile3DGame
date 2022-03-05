using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private BlockReference _openDoors;

    [SerializeField] private CollectionManager _collectionManager;

    private void Awake()
    {
        _collectionManager.OnCollectionCompleted.AddListener(HandleCollectionCompleted);        
    }

    private void HandleCollectionCompleted()
    {
        _openDoors.Execute();
    }
}