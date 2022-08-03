using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public enum Element
    {
        none, fire, water, earth, wind, storm, steam, wood
    }

    [SerializeField] public Element type;
    [SerializeField] private GameObject woodObj;

    private Rigidbody rb;
    private ParticleSystem waterP;
    private ParticleSystem windP;
    private ParticleSystem stormCloudP;
    private ParticleSystem stormFogP;
    private Transform player;

    private bool isUsed = false;
    [HideInInspector] public bool isSelected = false;

    private float holdFrontTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        waterP = transform.GetChild(0).GetComponent<ParticleSystem>();
        windP = GameObject.FindGameObjectWithTag("Wind").GetComponent<ParticleSystem>();
        stormCloudP = GameObject.FindGameObjectWithTag("StormCloud").GetComponent<ParticleSystem>();
        stormFogP = GameObject.FindGameObjectWithTag("StormFog").GetComponent<ParticleSystem>();
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

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        else if (col.gameObject.CompareTag("Ground"))
        {
            if(type == Element.wood && rb.velocity.magnitude > 0.01f)
            {
                StartCoroutine(ShootWood());
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(!isUsed && col.gameObject.CompareTag("Check"))
        {
            holdFrontTime += Time.deltaTime;

            if(holdFrontTime > 0.7f)
            {
                isUsed = true;

                if(type == Element.water)
                {
                    StartCoroutine(ShootWater());
                }
                else if(type == Element.wind)
                {
                    windP.gameObject.transform.rotation = Quaternion.Euler(0, player.rotation.y, 0);
                    windP.Play();
                }
                else if(type == Element.storm)
                {
                    stormCloudP.Play();
                    stormFogP.Play();
                }
                else if(type == Element.steam)
                {
                    stormFogP.Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!isUsed && col.gameObject.CompareTag("Check2"))
        {
            if(type == Element.fire && rb.velocity.magnitude > 0.05f)
            {
                ThrowFire();
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

    private IEnumerator ShootWater()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 1);

        waterP.Play();
        yield return new WaitForSeconds(10);
        waterP.Stop();
    }

    private IEnumerator ShootWood()
    {
        Transform newWood = Instantiate(woodObj, transform.position, Quaternion.Euler(0, transform.rotation.y, 0)).transform;
        
        WaitForSeconds sec1 = new WaitForSeconds(0.15f);
        WaitForSeconds sec2 = new WaitForSeconds(0.02f);

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                newWood.transform.localScale = new Vector3(newWood.localScale.x, newWood.localScale.y + 0.1f, newWood.localScale.z);
                yield return sec2;
            }

            yield return sec1;
        }

        Destroy(newWood.gameObject, 4);
        Destroy(gameObject);
    }
}
