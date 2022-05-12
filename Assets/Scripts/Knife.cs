using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    private bool _isPinnedDown = false;
    private bool _collidedWithKnife = false;
    private bool _isFallingDown = false;

    public KnifeProperties KnifeProperties;
    public bool IsPinnedDown => _isPinnedDown;
    public bool CollidedWithKnife => _collidedWithKnife;
    public bool IsFallingDown => _isFallingDown;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Log") && !_collidedWithKnife && !_isFallingDown)
        {
            _isPinnedDown = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.gameObject.transform;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Knife") && !_isFallingDown)
        {
            _collidedWithKnife = true;
            Ricochet(0, -1, -5);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Apple") && !_isFallingDown)
        {
            Debug.Log("Apple was hitted");
            collider.GetComponent<Apple>().OnKnifeHit();
        }
    }
    
    private void Start()
    {
        Init(KnifeStorage.Storage.GetCurrentKnife());
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