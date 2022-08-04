using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public static int health;
    [SerializeField] public static int mana;
    public static int tickDmg;
    bool regening;
    bool ticking;
    public static Dictionary<CardManager.Element, int> damage = new Dictionary<CardManager.Element, int>();
    void Start()
    {
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
        if (regening)
        {
            StartCoroutine(manaRegen());
        }
        if (ticking)
        {

            StartCoroutine(tickDamage());

        }

    }

    IEnumerator manaRegen()
    {
        regening = false;
        yield return new WaitForSeconds(5);
        mana += 10;
        regening = true;
    }
    IEnumerator tickDamage()
    {
        ticking = false;
        health -= tickDmg / 10;
        yield return new WaitForSeconds(1);
        ticking = true;

    }
    private void OnCollisionEnter(Collision collision)
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
                health -= i.Value;

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
        if (spell == CardManager.Element.ice)
        {
            //slowdown enemie
            StartCoroutine(speedChange());
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
