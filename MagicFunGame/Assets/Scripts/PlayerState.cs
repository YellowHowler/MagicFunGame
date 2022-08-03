using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public static int health;
    [SerializeField] public static int mana;
    bool regening; 
    void Start()
    {
        regening = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (regening)
        {
            StartCoroutine(manaRegen());
        }
    }
    IEnumerator manaRegen()
    {
        regening = false;
        yield return new WaitForSeconds(5);
        mana += 10;
        regening = true;
    }
}
