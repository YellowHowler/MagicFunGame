using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int maxHealth;
    [SerializeField] public int maxMana;

    public int health{get;set;}
    public int mana{get;set;}

    public int tickDmg;
    bool regening;
    bool ticking;

    public static Dictionary<CardManager.Element, int> damage = new Dictionary<CardManager.Element, int>();

    void Start()
    {
        health = maxHealth;
        mana = maxMana;

        regening = true;
        ticking = true;
        damage.Add(CardManager.Element.fire, 10);
        damage.Add(CardManager.Element.water, 30);
        damage.Add(CardManager.Element.lava, 30);
        damage.Add(CardManager.Element.ice, 20);
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
        ChangeHealth(-1 * (tickDmg / 10));
        yield return new WaitForSeconds(1);
        ticking = true;
    }

    public void ChangeMana(int amount)
    {
        mana = Mathf.Clamp(mana + amount, 0, maxMana);
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            CardManager.Element hitSpell = collision.gameObject.GetComponent<CardManager>().type;
            takeDmg(hitSpell);
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
}
