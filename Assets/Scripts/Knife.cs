using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    private bool _isPinnedDown;
    private bool _collidedWithKnife;
    private bool _isFallingDown;

    public KnifeProperties KnifeProperties;

    public bool IsPinnedDown
    {
        get => _isPinnedDown;
        set => _isPinnedDown = value;
    }

    public bool CollidedWithKnife => _collidedWithKnife;

    public void Throw()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0, ForceMode.VelocityChange);
    }
    
    public void OnLogShatter()
    {
        _isFallingDown = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Ricochet(0,5, 0);
    }

    public void Init(KnifeProperties knifeProperties)
    {
        Instantiate(knifeProperties.Prefab, transform.position, transform.rotation, transform);
        _isPinnedDown = false;
        _collidedWithKnife = false;
        _isFallingDown = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Log") && !_collidedWithKnife && !_isFallingDown)
        {
            _isPinnedDown = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.gameObject.transform;
            PlayerBelongs.AddScore(1);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Knife") && !_isFallingDown)
        {
            _collidedWithKnife = true;
            Ricochet(0, -1, -5);
            if (!_isPinnedDown)
            {
                Vibration.Vibrate();
                Debug.Log("Vibrate");
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Apple") && !_isFallingDown)
        {
            collider.GetComponent<Apple>().OnKnifeHit();
        }
    }

    private void Ricochet(float x, float y, float z)
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddForce(x, y, z, ForceMode.VelocityChange);
        gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(10f, 30f), Random.Range(10f, 30f),
            Random.Range(10f, 30f), ForceMode.VelocityChange);
    }
}