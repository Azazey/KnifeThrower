using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Log : MonoBehaviour
{
    [SerializeField] private float _explosionPower;
    [SerializeField] private Transform[] _logParts;
    [SerializeField] private AudioSource _logShatterSound;
    
    private Vector3 _rotationSpeed;
    private float _currentTime;
    private bool _isShattered;

    public void LogShatter()
    {
        _logShatterSound.Play();
        transform.GetComponent<Collider>().isTrigger = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Knife knife))
            {
                knife.OnLogShatter();
                if (knife.Effect)
                {
                    knife.Effect.transform.parent = null;   
                }
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

    private void SetLogMaterial()
    {
        foreach (var logPart in _logParts)
        {
            logPart.GetComponent<MeshRenderer>().material = LevelStorage.Storage.GetCurrentLevel().LogMaterial;
        }
    }

    private void Rotate()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime);
    }

    private void SetRotationSpeed()
    {
        _rotationSpeed = new Vector3(0, LevelStorage.Storage.GetCurrentLevel().LogRotation.Evaluate(_currentTime), 0);
        _currentTime += Time.deltaTime;
    }

    private void Update()
    {
        Rotate();
        SetRotationSpeed();
    }

    private void Awake()
    {
        SetLogMaterial();
        if (LevelStorage.Storage.GetCurrentLevel().BreakSound)
        {
            _logShatterSound.clip = LevelStorage.Storage.GetCurrentLevel().BreakSound;
        }
    }
}