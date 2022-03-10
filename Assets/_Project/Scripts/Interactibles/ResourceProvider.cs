using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : Interactible
{
    [SerializeField] protected CollectibleItem SpawnablePrefab;
    [SerializeField] protected SpawnablesCount SpawnableCount;

    [SerializeField] private Transform _spawnPosition;

    private int _maxHp = 3;
    private int _currentHp;

    [SerializeField] private bool _spawnOnHit;
    [SerializeField] private bool _spawnOnDestroy;

    private void Start()
    {
        _currentHp = _maxHp;
    }


    internal void Hit(int damage)
    {
        if (_spawnOnHit)
        {
            SpawnSpawnables();
        }
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        RemoveTrigger();
        StartCoroutine(WaitAndDestroy());

        IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(.1f);

            if (_spawnOnDestroy)
            {
                SpawnSpawnables();
            }

            gameObject.SetActive(false);
        }
    }

    private void SpawnSpawnables()
    {
        int spawns = Random.Range(SpawnableCount.Min, SpawnableCount.Max);

        if (spawns <= 0)
        {
            return;
        }

        for (int i = 0; i < spawns; i++)
        {
            GameObject.Instantiate(SpawnablePrefab, _spawnPosition.position, Quaternion.identity);
        }
    }
}
