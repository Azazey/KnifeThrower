using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using Random = UnityEngine.Random;

public class Knife : MonoBehaviour
{
    [SerializeField] private AudioSource _throwSound;
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _fallSound;
    [SerializeField]private GameObject _effectPrefab;
    
    private bool _isPinnedDown;
    private bool _collidedWithKnife;
    private bool _isFallingDown;
    private GameObject _effect;

    public KnifeProperties KnifeProperties;

    public GameObject Effect => _effect;

    public bool IsPinnedDown
    {
        get => _isPinnedDown;
        set => _isPinnedDown = value;
    }

    public AudioSource FallSound => _fallSound;

    public bool CollidedWithKnife => _collidedWithKnife;

    public void Throw()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(0, 20, 0, ForceMode.VelocityChange);
        _throwSound.Play();
    }
    
    public void OnLogShatter()
    {
        _isFallingDown = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Ricochet(0,5, 0);
    }

    public void Init(KnifeProperties knifeProperties)
    {
        KnifeProperties = knifeProperties;
        Instantiate(knifeProperties.Prefab, transform.position, transform.rotation, transform);
        _isPinnedDown = false;
        _collidedWithKnife = false;
        _isFallingDown = false;
        if (KnifeProperties)
        {
            if (KnifeProperties.EffectPrefab)
            {
                _effectPrefab = KnifeProperties.EffectPrefab;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Log") && !_collidedWithKnife && !_isFallingDown)
        {
            _isPinnedDown = true;
            _hitSound.Play();
            _effect = Instantiate(_effectPrefab);
            _effect.transform.parent = transform;
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
                Instantiate(_effectPrefab);
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