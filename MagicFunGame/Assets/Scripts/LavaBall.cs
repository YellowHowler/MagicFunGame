using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        transform.parent.gameObject.GetComponent<CardManager>().Explode();
        Destroy(gameObject);
    }
}
