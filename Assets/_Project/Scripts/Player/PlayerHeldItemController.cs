using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeldItemController : MonoBehaviour
{
    [SerializeField] private HeldItem _axe;
    [SerializeField] private HeldItem _sword;

    private HeldItem[] _heldItens;

    private void Awake()
    {
        _heldItens = GetComponentsInChildren<HeldItem>(true);
        HideAll();
    }

    public void EquipAxe()
    {
        HideAll();
        _axe.gameObject.SetActive(true);
    }

    public void EquipSword()
    {
        HideAll();
        _sword.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        foreach (var item in _heldItens)
        {
            item.gameObject.SetActive(false);
        }
    }
}
