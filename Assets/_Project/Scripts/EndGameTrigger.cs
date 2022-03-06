using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private BlockStarter _endLevelBlock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            EndLevel(); 
        }
    }

    private void EndLevel()
    {
        _endLevelBlock.ExecuteBlock();
    }
}
