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
    void Start()
    {
        regening = true;
        ticking = true;
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
        health -= tickDmg/10;
        yield return new WaitForSeconds(1);
        ticking = true;

    }
}
