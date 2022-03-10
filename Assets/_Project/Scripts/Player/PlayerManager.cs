using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    [SerializeField] private PlayerInteraction _player;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform GetPlayerTransform()
    {
        return _player.transform;
    }

    public void ApplyPlayerHit(float damage)
    {
        _player.ApplyHit(damage);
    }
}
