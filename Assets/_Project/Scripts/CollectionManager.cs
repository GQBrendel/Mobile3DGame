using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CollectionManager : MonoBehaviour
{
    public UnityEvent OnCollectionCompleted;

    [SerializeField] private TextMeshProUGUI _currentCollectiblesCountText;

    private CollectibleItem[] _collectibleItens;

    private int _totalCollectibles;
    private int _currentCollectibles = 0;

    private void Awake()
    {
        FillCollection();
    }

    private void FillCollection()
    {
        _collectibleItens = FindObjectsOfType<CollectibleItem>();
        _totalCollectibles = _collectibleItens.Length;
        UpdateUIText();

        foreach (var collectible in _collectibleItens)
        {
            collectible.OnItemCollected.AddListener(HandleItemCollected);
        }
    }

    private void HandleItemCollected()
    {
        _currentCollectibles++;
        UpdateUIText();

        if(_currentCollectibles == _totalCollectibles)
        {
            FinishedCollection();
        }
    }

    private void FinishedCollection()
    {
        OnCollectionCompleted?.Invoke();
    }

    private void UpdateUIText()
    {
        _currentCollectiblesCountText.SetText("- 0" + _currentCollectibles.ToString());
    }
}