using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] SorcererType wizard;
    bool CD;
    Rigidbody rb;
    enum SorcererType
    { 
        fire,
        water
    }
    // Start is called before the first frame update
    void Start()
    {
        CD = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CD)
        {

            AttackCD();
        }
    }
    void AIAttack()
    {
        if (wizard == SorcererType.fire)
        {
            if (Random.Range(1, 12) == 6)
            {
                //call fireball

            }

        }
        else if (wizard == SorcererType.water)
        {
            if (Random.Range(1, 12) == 6)
            {
                //call water

            }


        }

    }
    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(10);
        AIAttack();
    }
    IEnumerator Movement()
    {
        yield return new WaitForSeconds(4);

    }

}
