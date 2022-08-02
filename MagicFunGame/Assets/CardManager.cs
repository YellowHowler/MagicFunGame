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

    private Transform player;

    private Rigidbody rb;

    private bool isUsed = false;
    public bool isSelected = false;

    private float holdFrontTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        //throw
        if(!isUsed && rb.velocity.magnitude > 0.01f && !isSelected)
        {
            if(type == Element.fire)
            {
                ThrowFire();
            }
        }

        //hold towards front
        if(isSelected && Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z)) > 0.4f)
        {
            holdFrontTime += Time.deltaTime;
        }
        else
        {
            holdFrontTime = 0;
        }

        if(holdFrontTime > 0.7f)
        {
            if(type == Element.water)
            {
                ShootWater();
            }
        }
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
}
