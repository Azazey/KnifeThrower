using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Log : MonoBehaviour
{
    [SerializeField] private AnimationCurve _rotationIntesive;
    [SerializeField] private float _explosionPower;
    [SerializeField] private Transform[] _logParts;
    
    private Vector3 _rotationSpeed;
    private float _currentTime;
    private bool _isShattered;

    public void LogShatter()
    {
        transform.GetComponent<Collider>().isTrigger = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Knife knife))
            {
                knife.OnLogShatter();
            }

            if (transform.GetChild(i).TryGetComponent(out Apple apple))
            {
                apple.OnLogShatter();
            }
        }

        transform.DetachChildren();
        Vector3 force = (transform.position).normalized * _explosionPower;
        for (int i = 0; i < _logParts.Length; i++)
        {
            Rigidbody rigidbody = _logParts[i].GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            if (i == 0)
            {
                rigidbody.AddForce(force + new Vector3(-2, 2, 0), ForceMode.VelocityChange);
                rigidbody.AddTorque(0, 0, Random.Range(5f, 10f), ForceMode.VelocityChange);
            }
            else
            {
                rigidbody.AddForce(force + new Vector3(2, 2, 0), ForceMode.VelocityChange);
                rigidbody.AddTorque(0, 0, Random.Range(5f, 10f), ForceMode.VelocityChange);
            }
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
}