using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private AnimationCurve _rotationIntesive;
    [SerializeField] private float _explosionPower;
    [SerializeField] private Transform _parts;

    private Vector3 _rotationSpeed;
    private float _currentTime;
    private bool _isShattered;
    private Rigidbody[] _rigidbodies;

    public void LogShatter()
    {
        Vector3 origin = GetAvaragePosition();

        transform.GetComponent<Collider>().isTrigger = true;
        _parts.gameObject.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Knife knife))
            {
                knife.OnLevelPass();
            }
        }
        
        transform.DetachChildren();
        
        foreach (var rigidbody in _rigidbodies)
        {
            Vector3 force = (rigidbody.transform.position - origin).normalized * _explosionPower;
            rigidbody.isKinematic = false;
            rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }

    private void Rotate()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime);
    }

    private void SetRotationSpeed()
    {
        _rotationSpeed = new Vector3(0, _rotationIntesive.Evaluate(_currentTime), 0);
        _currentTime += Time.deltaTime;
    }
    
    private void Update()
    {
        Rotate();
        SetRotationSpeed();
    }

    private void Awake()
    {
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
