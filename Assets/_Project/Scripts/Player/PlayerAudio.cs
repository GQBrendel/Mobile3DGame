using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.instance;
    }


    //Animation Event
    public void FootStepLeft()
    {
        _audioManager.Play("LeftFootStep");
    }

    //Animation Event
    public void FootStepRight()
    {
        _audioManager.Play("RightFootStep");
    }

    internal void PlayDamageSound()
    {
        _audioManager.Play("PlayerTookDamage");
    }
}
