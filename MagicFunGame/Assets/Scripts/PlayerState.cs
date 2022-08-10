using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int maxHealth;
    [SerializeField] public int maxMana;
    [SerializeField] flashImage flash;
    public int health{get;set;}
    public int mana{get;set;}

    public int tickDmg;
    bool regening;
    bool ticking;

    bool showStatus = false;

    public static Dictionary<CardManager.Element, int> damage = new Dictionary<CardManager.Element, int>();

    [SerializeField] private Image status;
    private TextMeshProUGUI statusTxt;

    private AudioSource au;
    [SerializeField] private AudioClip damageClip;

    void Start()
    {
        health = maxHealth;
        mana = maxMana;

        regening = true;
        ticking = true;
        damage.Add(CardManager.Element.fire, 10);
        damage.Add(CardManager.Element.water, 30);
        damage.Add(CardManager.Element.lava, 30);

        statusTxt = status.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        au = GetComponent<AudioSource>();

        Status();
    }

    // Update is called once per frame
    void Update()
    {
        if (mana < maxMana && regening)
        {
            StartCoroutine(manaRegen());
        }
        if (tickDmg > 0 && ticking)
        {
            StartCoroutine(tickDamage());
        }
        Debug.Log(health);
    }

    IEnumerator manaRegen()
    {
        regening = false;
        yield return new WaitForSeconds(5);
        ChangeMana(10);
        regening = true;
    }
    IEnumerator tickDamage()
    {
        ticking = false;
        ChangeHealth(-1);
        //flash.StartFlash(0.25f, .5f, Color.red); Unessessary???
        tickDmg -=1;
        yield return new WaitForSeconds(1);
        ticking = true;
    }

    public void ChangeMana(int amount)
    {
        mana = Mathf.Clamp(mana + amount, 0, maxMana);
        if(statusTxt != null) statusTxt.text = "Health: " + health + "/" + maxHealth + "\n" + "Mana: " + mana + "/" + maxMana;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < -1)
        {
            au.PlayOneShot(damageClip, 1);
            flash.StartFlash(0.25f, .5f, Color.red);
        }
        else if (amount >0)
        {
            flash.StartFlash(0.25f, .5f, Color.green);

        }
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        if(statusTxt != null) statusTxt.text = "Health: " + health + "/" + maxHealth + "\n" + "Mana: " + mana + "/" + maxMana;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyFire")
        { 
            takeDmg(CardManager.Element.fire);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "EnemyLava")
        {
            takeDmg(CardManager.Element.lava);
            Destroy(collision.gameObject);
        }
    }

    void takeDmg(CardManager.Element spell)
    {

        foreach (KeyValuePair<CardManager.Element, int> i in damage)
        {
            if (spell == i.Key)
            {
                
                ChangeHealth(-1*i.Value);
            }

        }
        if (spell == CardManager.Element.fire || spell == CardManager.Element.lava)
        {
            tickDmg = 10;
        }
        if (spell == CardManager.Element.steam)
        {
            //coroutine time
            StartCoroutine(waterLength(10));
        }
        if (spell == CardManager.Element.storm)
        {
            StartCoroutine(waterLength(5));

        }

    }
    IEnumerator speedChange()
    {
        EnemyAI.speed += 5;
        yield return new WaitForSeconds(2);
        EnemyAI.speed -= 5;

    }
    IEnumerator waterLength(int amp)
    {
        damage[CardManager.Element.water] += amp;
        yield return new WaitForSeconds(amp);
        damage[CardManager.Element.water] -= amp;
    }

    public void OnStatus()
    {
        showStatus = !showStatus;
        Status();
    }

    private void Status()
    {
        if(statusTxt != null) statusTxt.text = "Health: " + health + "/" + maxHealth + "\n" + "Mana: " + mana + "/" + maxMana;

        if(showStatus)
        {
            status.gameObject.SetActive(true);
        }
        else
        {
            status.gameObject.SetActive(false);
        }
    }
}
