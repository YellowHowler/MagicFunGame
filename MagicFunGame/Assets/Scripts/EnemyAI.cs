using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] SorcererType wizard;
    bool CD;
    Rigidbody rb;
    bool move;
    enum SorcererType
    { 
        fire,
        water
    }
    // Start is called before the first frame update
    void Start()
    {
        CD = true;
        move = true;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CD)
        {

           StartCoroutine (AttackCD());
        }
        if (move)
        {
            
           StartCoroutine( Movement());
        
        
        }
    }
    void AIAttack()
    {
        if (wizard == SorcererType.fire)
        {

            print("call fireball");

            

        }
        else if (wizard == SorcererType.water)
        {

            print("call water");


        }

    }
    IEnumerator AttackCD()
    {
        CD = false;
        yield return new WaitForSeconds(10);
        AIAttack();
        CD = true;
    }
    IEnumerator Movement()
    {
        move = false;
        yield return new WaitForSeconds(2);
        rb.velocity = new Vector3(Random.Range(-5, 5), 0, 0);
        move = true;
    }

}
