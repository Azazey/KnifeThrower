using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private bool collected = false;

    public void OnKnifeHit()
    {
        //Здесь надо написать логику разрушения модельки яблока и зачисления очков 
        if (!collected)
        {
            collected = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
        }
    }

}
