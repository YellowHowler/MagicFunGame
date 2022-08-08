using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollision : MonoBehaviour
{
    CardManager.Element thisElement;

    // Start is called before the first frame update
    void Start()
    {
        thisElement = GetComponent<CardManager>().type;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CardManager.Element thisElement;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
