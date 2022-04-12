using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] private AnimationCurve _rotationIntesive;

    private Vector3 _rotationSpeed;
    private float _currentTime;
    private void Update()
    {
        Rotate();
        SetRotationSpeed();
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
}
