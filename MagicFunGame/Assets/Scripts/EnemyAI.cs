using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] SorcererType wizard;
    bool CD;
    Rigidbody rb;
    bool move;
    GameObject player;
    

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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CD)
        {

           StartCoroutine(AttackCD());
        }
        if (move)
        {
            
           StartCoroutine(Movement());
        
        
        }
    }
    void AIAttack()
    {
        if (wizard == SorcererType.fire)
        {

            int spell = Random.Range(0, 3);

            switch (spell)
            {
                case 0:
                    print("fire");
                    break;
                case 1:
                    print("wood");
                    break;
                case 2:
                    print("steam");
                    break;
                
            }


        }
        else if (wizard == SorcererType.water)
        {

            int spell = Random.Range(0, 3);

            switch (spell)
            {
                case 0:
                    print("water");
                    break;
                case 1:
                    print("steam");
                    break;
                case 2:
                    print("storm");
                    break;

            }


        }

    }
    void Attacked()
    { 
        
    
    }
    IEnumerator AttackCD()
    {
        CD = false;
        yield return new WaitForSeconds(5);
        WaitForSeconds sec = new WaitForSeconds(.05f);
        for (float i = 0; i < 1; i = i + .1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), i);
            yield return sec;
        }
        yield return new WaitForSeconds(0.2f);
        AIAttack();
        CD = true;
    }
    IEnumerator Movement()
    {
        move = false;
        yield return new WaitForSeconds(2);
        rb.velocity = new Vector3(Random.Range(-1, 2) * 5, 0, 0);
        move = true;
    }

}
