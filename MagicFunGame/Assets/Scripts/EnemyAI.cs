using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] SorcererType wizard;
    bool CD;
    Rigidbody rb;
    ParticleSystem waterP;
    AudioSource au;
    AudioClip[] elementSounds;
    bool move;
    GameObject player;
    public static int speed;
    public int enemyHealth;
    [SerializeField] private Material stormSky;
    [SerializeField] private GameObject woodObj;
    [SerializeField] private GameObject fireCard;
    ParticleSystem fireP;

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
        enemyHealth = 100;
        rb = GetComponent<Rigidbody>();
        player = Camera.main.gameObject;
        speed = 5;
        au = GetComponent<AudioSource>();
        waterP = transform.GetChild(0).GetComponent<ParticleSystem>();
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
        if (enemyHealth == 0)
        {
            Destroy(this.gameObject);
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
                    for(int i = 0; i < 3; i++)
                    {
                        Vector3 firePos = transform.position - (transform.position - player.transform.position).normalized * 2;
                        firePos = new Vector3(firePos.x + Random.Range(-0.7f, 0.7f), player.transform.position.y - Random.Range(-0.1f, 0.7f), firePos.z);
                        Rigidbody fireRb = Instantiate(fireCard, firePos, Quaternion.Euler(90, transform.rotation.y, 0)).GetComponent<Rigidbody>();
                        fireRb.velocity = (player.transform.position - transform.position).normalized * 6;
                    }

                    break;
                case 1:
                    Vector3 woodPos = transform.position - (transform.position - player.transform.position).normalized * 5;
                    woodPos = new Vector3(woodPos.x, 1f, woodPos.z);
                    GameObject newWood = Instantiate(woodObj, woodPos, Quaternion.Euler(0, transform.rotation.y, 0));
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
                    StartCoroutine(ShootWater());
                    break;
                case 1:
                    //steam
                    print("steam");
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
        yield return new WaitForSeconds(Random.Range(3f, 5f));
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
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector3(Random.Range(-1, 2) * speed, 0, 0);
        move = true;
    }

    private IEnumerator ShootWater()
    {
        au.PlayOneShot(elementSounds[1], 1);
        waterP.Play();
        yield return new WaitForSeconds(4);
        waterP.Stop();
    }
}
