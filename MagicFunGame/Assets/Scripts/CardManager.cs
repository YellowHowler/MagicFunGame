using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public enum Element
    {
        none = -1, 
        fire = 0, 
        water = 1, 
        earth = 2, 
        wind = 3, 
        lava = 4, 
        storm = 5, 
        steam = 6, 
        sand = 7, 
        wood = 8, 
        ice = 9,
    }

    [SerializeField] public Element type;
    [SerializeField] private Material stormSky;
    [SerializeField] private GameObject woodObj;

    [SerializeField] private Texture[] glyphs;
    [SerializeField] private Color[] glyphColors;

    private Rigidbody rb;
    private ParticleSystem waterP;
    private ParticleSystem windP;
    private ParticleSystem stormCloudP;
    private ParticleSystem stormFogP;
    private Transform player;
    private Renderer childRend;

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

        childRend = transform.GetChild(1).gameObject.GetComponent<Renderer>();

        RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.3f, 1);

        UpdateGlyph();
    }

    void Update()
    {   
    }

    public void UpdateGlyph()
    {
        if(type == Element.none)
        {
            childRend.material.DisableKeyword("_EMISSION");
        }
        else
        {
            childRend.material.EnableKeyword("_EMISSION");
            childRend.material.SetTexture ("_EmissionMap", glyphs[(int)type]);
            childRend.material.SetColor("_EmissionColor", glyphColors[(int)type]);
        }
    }

    private void ThrowFire()
    {
        isUsed = true;
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public void TouchGround()
    {
        if(!isUsed && type == Element.wood && rb.velocity.magnitude > 0)
        {
            isUsed = true;

            Instantiate(woodObj, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
            Destroy(gameObject);
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
                    RenderSettings.skybox = stormSky;
                    stormCloudP.Play();
                }
                else if(type == Element.steam)
                {
                   // stormFogP.Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(!isUsed && col.gameObject.CompareTag("Check2"))
        {
            if(type == Element.fire && rb.velocity.magnitude > 0)
            {
                ThrowFire();
            }
            else if(type == Element.lava && rb.velocity.magnitude > 0)
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
        waterP.Play();
        yield return new WaitForSeconds(4);
        waterP.Stop();
    }

    private void OnParticleTrigger()
    {
        
    }
}
