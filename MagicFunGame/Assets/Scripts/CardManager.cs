using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

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
        lightning = 10,
        glass = 11,
        smoke = 12,
        rock = 13,
        life = 14
    }

    [SerializeField] public Element type;

    [SerializeField] private GameObject woodObj;
    [SerializeField] private GameObject glassObj;
    [SerializeField] private Texture[] glyphs;
    [SerializeField] private Texture[] glyphMetals;
    [SerializeField] private Texture[] glyphEmissions;
    [SerializeField] private Color[] glyphColors;

    [SerializeField] private AudioClip[] elementSounds;

    private Rigidbody rb;
    private AudioSource au;
    [SerializeField] private ParticleSystem waterP;
    private ParticleSystem windP;
    private ParticleSystem stormCloudP;
    private ParticleSystem stormFogP;
    private ParticleSystem fireP;
    [SerializeField] private ParticleSystem lightningP;
    private ParticleSystem magicGlassP;
    private ParticleSystem smokeP;
    private ParticleSystem rockP;
    [SerializeField] private ParticleSystem lifeP;
    private Transform player;
    private Renderer childRend;

    private PlayerState ps;

    private float fastSpell;

    private bool isUsed = false;
    public bool isHolding = false;
    public bool isSelected { get; set; }

    private float holdFrontTime = 0;
    public bool thrown;
    public bool gameStarted { get; set; }
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();

        player = Camera.main.gameObject.GetComponent<Transform>();
        windP = GameObject.FindGameObjectWithTag("Wind").GetComponent<ParticleSystem>();
        stormCloudP = GameObject.FindGameObjectWithTag("StormCloud").GetComponent<ParticleSystem>();
        stormFogP = GameObject.FindGameObjectWithTag("StormFog").GetComponent<ParticleSystem>();
        //lightningP = transform.GetChild(2).GetComponent<ParticleSystem>();

        enemy = GameObject.FindGameObjectWithTag("Enemy");

        childRend = transform.GetChild(1).gameObject.GetComponent<Renderer>();

        ps = player.gameObject.GetComponent<PlayerState>();

        RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.3f, 1);

        fireP = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();
        fireP.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        float factor = Mathf.Pow(2,3);
        for(int i = 0; i < glyphColors.Length; i++)
        {
            glyphColors[i] = new Color(glyphColors[i].r*factor,glyphColors[i].g*factor,glyphColors[i].b*factor);
        }
        
        UpdateGlyph();
    }

    void Update()
    {
        if (!isSelected && !isUsed)
        {
            if (type == Element.fire && rb.velocity.magnitude > 1f)
            {
                UseSpell(20);
                if (isUsed)
                {
                    thrown = true;
                    ThrowFire();
                }
            }
            else if (type == Element.lava && rb.velocity.magnitude > 1f)
            {
                UseSpell(20);
               
                if (isUsed) 
                {
                    thrown = true;
                    ThrowFire();
                }
            }
        }

        if(isHolding)
        {
            holdFrontTime += Time.deltaTime;

            if (holdFrontTime > 1.2f)
            {
                //hi
                if (type == Element.water)
                {
                    UseSpell(20);
                    if (isUsed)
                    {
                        StartCoroutine(ShootWater());

                    }
                }
                else if (type == Element.earth)
                {
                    UseSpell(10);
                    if (isUsed)
                    {
                        player.GetComponent<PlayerState>().ChangeHealth(10);
                    }
                }
                else if (type == Element.lightning)
                {
                    UseSpell(40);
                    if (isUsed)
                    {
                        StartCoroutine(ShootLightning());
                    }
                }
                else if (type == Element.wind)
                {
                    UseSpell(10);
                    if (isUsed)
                    {
                        windP.gameObject.transform.rotation = Quaternion.Euler(0, player.rotation.y, 0);
                        Environment.Instance.windDir = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
                        windP.Play();
                    }
                }
                else if (type == Element.storm)
                {
                    UseSpell(20);

                    if (isUsed)
                    {
                        Environment.Instance.isStorm = true;
                        Environment.Instance.StartStorm();
                    }
                }
                else if (type == Element.steam)
                {
                    UseSpell(20);

                    if (isUsed)
                    {
                        Environment.Instance.isSteam = true;
                        Environment.Instance.StartSteam();
                    }
                }


                isHolding = false;
                holdFrontTime = 0;
            }
        }
        else 
        {
            holdFrontTime = 0;
        }
    }

    public void UpdateGlyph()
    {
        if (type == Element.none)
        {
            childRend.material.DisableKeyword("_EMISSION");
        }
        else
        {
            childRend.material.SetTexture("_BaseMap", glyphs[(int)type]);

            childRend.material.SetTexture("_MetallicGlossMap", glyphMetals[(int)type]);

            childRend.material.EnableKeyword("_EMISSION");
            childRend.material.SetTexture("_EmissionMap", glyphEmissions[(int)type]);
            childRend.material.SetColor("_EmissionColor", glyphColors[(int)type]);
        }
    }

    public void UpdateGlyph(int typeInt)
    {
        if (type == Element.none)
        {
            childRend.material.DisableKeyword("_EMISSION");
        }
        else
        {
            childRend.material.SetTexture("_BaseMap", glyphs[typeInt]);

            childRend.material.SetTexture("_MetallicGlossMap", glyphMetals[typeInt]);

            childRend.material.EnableKeyword("_EMISSION");
            childRend.material.SetTexture("_EmissionMap", glyphEmissions[typeInt]);
            childRend.material.SetColor("_EmissionColor", glyphColors[typeInt]);
        }
    }

    private void ThrowFire()
    {
        GetComponent<Projectile>().enabled = true;

        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        //rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -1, 1), rb.velocity.z);
        rb.velocity = rb.velocity.normalized * 7;

        fireP.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        fireP.Play();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void TouchGround()
    {
        if (!isUsed && type == Element.wood && rb.velocity.magnitude > 0 && player.gameObject.GetComponent<PlayerState>().mana > 0)
        {
            UseSpell(30);

            if (isUsed)
            {
                Instantiate(woodObj, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(0, transform.rotation.y, 0));
                Destroy(gameObject);
            }
        }
        else if (!isUsed && type == Element.glass && rb.velocity.magnitude > 0 && player.gameObject.GetComponent<PlayerState>().mana > 0)
        {
            UseSpell(60);
            if (isUsed)
            {
                Instantiate(glassObj, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.Euler(0, transform.rotation.y, 0));
                Destroy(gameObject);

            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (isSelected && !isUsed && col.gameObject.CompareTag("Check"))
        {
            isHolding = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {

    }

    private void OnTriggerExit(Collider col)
    {
       if(col.gameObject.CompareTag("Check"))
        {
            isHolding = false;
        }
    }

    private IEnumerator ShootWater()
    {
        au.PlayOneShot(elementSounds[1], 1);
        waterP.Play();
        yield return new WaitForSeconds(4);
        waterP.Stop();
    }

    private IEnumerator ShootLightning()
    {
        au.PlayOneShot(elementSounds[9], 1);
        lightningP.Play();
        yield return new WaitForSeconds(4);
        lightningP.Stop();
    }

    
    

    private void UseSpell(int manaCost)
    {
        if(ps.mana > manaCost)
        {
            isUsed = true;
            au.PlayOneShot(elementSounds[(int)type], 1);
            ps.ChangeMana(-1 * manaCost);
            type = Element.none;
            UpdateGlyph();
        }
    }
}
