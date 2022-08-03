using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public enum Element
    {
        none, fire, water, earth, wind,
    }

    [SerializeField] public Element type;

    private Rigidbody rb;

    private bool isUsed = false;
    [HideInInspector] public bool isSelected = false;

    private float holdFrontTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {   
    }

    private void ThrowFire()
    {
        isUsed = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        rb.useGravity = true;
    }

    private void ShootWater()
    {
        isUsed = true;
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        else if (col.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(!isUsed && col.gameObject.CompareTag("Check"))
        {
            holdFrontTime += Time.deltaTime;

            if(type == Element.fire && rb.velocity.magnitude > 0.05f)
            {
                ThrowFire();
            }

            if(holdFrontTime > 0.7f)
            {
                if(type == Element.water)
                {
                    ShootWater();
                }
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Check"))
        {
            holdFrontTime = 0;
        }
    }
}
