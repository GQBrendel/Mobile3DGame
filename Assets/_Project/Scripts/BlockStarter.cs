using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BlockStarter : MonoBehaviour
{
    [SerializeField] private BlockReference _myBlock;

    public void ExecuteBlock()
    {
        _myBlock.Execute();
    }
}
