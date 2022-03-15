using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("MainLoop", true);

        InventoryData previousData = SaveSystem.LoadInventory();      

        if (previousData != null)
        {
            InventoryManager.Instance.FillData(previousData);
        }
        StartCoroutine(SaveGameRoutine());
    }

    private IEnumerator SaveGameRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            SaveSystem.SaveInventory(InventoryManager.Instance);
        }
    }
}