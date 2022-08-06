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
    public static int speed;
    [SerializeField] private Material stormSky;
    [SerializeField] private GameObject woodObj;

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
        speed = 5;
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
                    Vector3 woodPos = transform.position;
                    woodPos.x += 10;
                    Instantiate(woodObj, woodPos, Quaternion.Euler(0, transform.rotation.y, 0));
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
                    //CardManager.getShootWater();
                    break;
                case 1:
                    //steam
                    break;
                case 2:
                    RenderSettings.skybox = stormSky;
                    GameObject.FindGameObjectWithTag("StormCloud").GetComponent<ParticleSystem>().Play();
                    //storm
                    break;

            }


        }

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
        rb.velocity = new Vector3(Random.Range(-1, 2) * speed, 0, 0);
        move = true;
    }

}
