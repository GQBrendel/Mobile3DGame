using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private CollectibleItem _myItem;

    private bool _moveToPlayer;
    private Transform _player;

    private void Awake()
    {
        if(!_myItem)
           _myItem = transform.GetComponentInParent<CollectibleItem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
        {
            if (_myItem.GetComponent<Rigidbody>())
            {
                _myItem.GetComponent<Rigidbody>().isKinematic = true;
            }
            _moveToPlayer = true;
            _player = other.GetComponent<PlayerInteraction>().GetPlayerHead();
        }
    }

    private void Update()
    {
        if (!_moveToPlayer)
        {
            return;
        }

        _myItem.transform.position = Vector3.MoveTowards(_myItem.transform.position, _player.position, _speed * Time.deltaTime);        
    }
}