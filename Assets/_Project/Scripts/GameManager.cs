using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("MainLoop", true);
    }
}
