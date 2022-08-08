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
    }

    [SerializeField] public Element type;
    [SerializeField] private Material stormSky;
    [SerializeField] private GameObject woodObj;

    [SerializeField] private Texture[] glyphs;
    [SerializeField] private Color[] glyphColors;

    [SerializeField] private AudioClip[] elementSounds;

    private Rigidbody rb;
    private AudioSource au;
    private ParticleSystem waterP;
    private ParticleSystem windP;
    private ParticleSystem stormCloudP;
    private ParticleSystem stormFogP;
    private ParticleSystem fireP;
    private Transform player;
    private Renderer childRend;

    private PlayerState ps;

    private float fastSpell;

    private bool isUsed = false;
    private bool inDeck = false;
    public bool isSelected { get; set; }

    private float holdFrontTime = 0;

    public bool gameStarted { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();

        player = Camera.main.gameObject.GetComponent<Transform>();
        waterP = transform.GetChild(0).GetComponent<ParticleSystem>();
        windP = GameObject.FindGameObjectWithTag("Wind").GetComponent<ParticleSystem>();
        stormCloudP = GameObject.FindGameObjectWithTag("StormCloud").GetComponent<ParticleSystem>();
        stormFogP = GameObject.FindGameObjectWithTag("StormFog").GetComponent<ParticleSystem>();
        fireP = transform.GetChild(0).GetComponent<ParticleSystem>();

        childRend = transform.GetChild(1).gameObject.GetComponent<Renderer>();

        ps = player.gameObject.GetComponent<PlayerState>();

        RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.3f, 1);

        isSelected = true;

        UpdateGlyph();
    }

    void Update()
    {
        if (!isSelected && !isUsed)
        {
            if (type == Element.fire && rb.velocity.magnitude > 0)
            {
                UseSpell(-20);
                if(isUsed) ThrowFire();
            }
            else if (type == Element.lava && rb.velocity.magnitude > 0)
            {
                UseSpell(-20);
                if(isUsed) ThrowFire();
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
            childRend.material.EnableKeyword("_EMISSION");
            childRend.material.SetTexture("_EmissionMap", glyphs[(int)type]);
            childRend.material.SetColor("_EmissionColor", glyphColors[(int)type]);
        }
    }

    private void ThrowFire()
    {
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
            UseSpell(-30);

            if(isUsed) 
            {
                Instantiate(woodObj, transform.position, Quaternion.Euler(0, transform.rotation.y, 0));
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (isSelected && !isUsed && col.gameObject.CompareTag("Check"))
        {
            holdFrontTime += Time.deltaTime;

            if (holdFrontTime > 1.2f)
            {
                if (type == Element.water)
                {
                    UseSpell(-20);
                    if(isUsed) StartCoroutine(ShootWater());
                }
                else if (type == Element.wind)
                {
                    UseSpell(-10);
                    if(isUsed) 
                    {
                        windP.gameObject.transform.rotation = Quaternion.Euler(0, player.rotation.y, 0);
                        windP.Play();
                    }
                }
                else if (type == Element.storm)
                {
                    UseSpell(-20);

                    if(isUsed) 
                    {
                        //PlayerState.damage[CardManager.Element.water] += 5;
                        RenderSettings.skybox = stormSky;
                        stormCloudP.Play();
                    }
                }
                else if (type == Element.steam)
                {
                    //col.gameObject.GetComponent<PlayerState>().tickDmg = 5;
                    //player.gameObject.GetComponent<PlayerState>().mana -= 20;
                    // stormFogP.Play();
                }
            }
        }
        if(!isSelected)
        {
            holdFrontTime = 0;
        }
    }

    private void OnTriggerEnter(Collider col)
    {

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Check"))
        {
            holdFrontTime = 0;
        }
    }

    private IEnumerator ShootWater()
    {
        au.PlayOneShot(elementSounds[1], 1);
        waterP.Play();
        yield return new WaitForSeconds(4);
        waterP.Stop();
    }

    private void OnParticleTrigger()
    {

    }

    private void OnActivated()
    {
        childRend.material.color = new Color(0, 1, 0, 1);
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
