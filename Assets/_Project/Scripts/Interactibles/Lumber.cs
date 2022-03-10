using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumber : MonoBehaviour
{
    [SerializeField] private float _timeBeforeGoToPlayer;
    [SerializeField] private float _minRandomForce;
    [SerializeField] private float _maxRandomForce;
    [SerializeField] private float _yForce;

    private MagnetEffect _magnetEffect;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _magnetEffect = GetComponentInChildren<MagnetEffect>(true);

        MoveAway();
    }


    public void MoveAway()
    {
        //Add force to rigdbody

        Vector3 force = new Vector3(Random.Range(_minRandomForce, _maxRandomForce), _yForce, Random.Range(_minRandomForce, _maxRandomForce));
        _rb.AddForce(force, ForceMode.Impulse);

        StartCoroutine(GoToPLayerRoutine());
    }
    private IEnumerator GoToPLayerRoutine()
    {
        yield return new WaitForSeconds(_timeBeforeGoToPlayer);
        _magnetEffect.gameObject.SetActive(true);
    }
}
