using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private float _explosionPower;
    [SerializeField] private Transform _parts;
    
    private bool _collected = false;
    private bool _isExploded;
    private MeshRenderer _meshRenderer;
    private Rigidbody[] _rigidbodies;

    public void OnKnifeHit()
    {
        //Здесь надо написать логику разрушения модельки яблока и зачисления очков 
        if (!_collected)
        {
            _collected = true;
            transform.parent = null;
            PlayerBelongs.AddMoney(1);
            PlayerBelongs.AddScore(2);
            Explode();
        }
    }
    
    public void OnLogShatter()
    {
        Explode();
    }

    private void Explode()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        Vector3 origin = GetAvaragePosition();
        Vector3 forceToCamera = new Vector3(0,0, -2);

        _parts.gameObject.SetActive(true);
        _meshRenderer.enabled = false;

        foreach (var rigidbody in _rigidbodies)
        {
            Vector3 force = (rigidbody.transform.position - origin).normalized * _explosionPower;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(force + forceToCamera, ForceMode.VelocityChange);
        }
    }

    private void Awake()
    {
        _meshRenderer = _parts.GetComponent<MeshRenderer>();
        _rigidbodies = _parts.GetComponentsInChildren<Rigidbody>();
    }

    private Vector3 GetAvaragePosition()
    {
        Vector3 position = Vector3.zero;
        foreach (var rigidbody in _rigidbodies)
        {
            position += rigidbody.transform.position;
        }

        position /= _rigidbodies.Length;
        return position;
    }
}
