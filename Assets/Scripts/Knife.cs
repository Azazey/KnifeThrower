using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    private bool _isPinnedDown = false;
    private bool _collidedWithKnife = false;

    public bool IsPinnedDown => _isPinnedDown;
    public bool CollidedWithKnife => _collidedWithKnife;

    public void Throw()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Log") && !_collidedWithKnife)
        {
            _isPinnedDown = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.gameObject.transform;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Knife"))
        {
            _collidedWithKnife = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().AddForce(0, -1, -5, ForceMode.VelocityChange);
            gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(10f, 30f), Random.Range(10f, 30f),
                Random.Range(10f, 30f), ForceMode.VelocityChange);
        }
    }
}