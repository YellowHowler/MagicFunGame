using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] SorcererType wizard;
    bool CD;
    Rigidbody rb;
    [SerializeField] ParticleSystem waterP;
    Animator ani;
    AudioSource au;
    //AudioClip[] elementSounds;
    bool move;
    GameObject player;
    public static int speed;
    private int enemyHealth;
    private int maxHealth = 100;

    public int tickDmg;
    bool ticking;

    [SerializeField] private Material stormSky;
    [SerializeField] private GameObject woodObj;
    [SerializeField] private GameObject fireCard;

    [SerializeField] private Slider healthBar;
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
        enemyHealth = maxHealth;
        ChangeHealth(0);
        rb = GetComponent<Rigidbody>();
        player = Camera.main.gameObject;
        speed = 5;
        au = GetComponent<AudioSource>();
        

        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tickDmg > 0 && !ticking)
        {
            StartCoroutine(tickDamage());
        }

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
        ani.SetTrigger("Attack");

        if (wizard == SorcererType.fire)
        {
            int spell = Random.Range(0, 3);

            switch (spell)
            {
                case 0:
                    for(int i = 0; i < 3; i++)
                    {
                        Vector3 firePos = transform.position - (transform.position - player.transform.position).normalized * 2;
                        firePos = new Vector3(firePos.x + Random.Range(-0.4f, 0.4f) * 2, player.transform.position.y - Random.Range(-0.6f, 0.6f) * 1.3f + 0.4f, firePos.z);
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
                    Environment.Instance.StartSteam();
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
                    Environment.Instance.StartSteam();
                    break;
                case 2:
                    Environment.Instance.StartStorm();
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
            Quaternion dest = Quaternion.LookRotation(player.transform.position - transform.position);
            dest = Quaternion.Euler(0, dest.y + 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, dest, i);
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

    IEnumerator tickDamage()
    {
        ticking = true;
        ChangeHealth(-1);
        //flash.StartFlash(0.25f, .5f, Color.red); Unessessary???
        tickDmg -=1;
        yield return new WaitForSeconds(1);
        ticking = false;
    }

    private IEnumerator ShootWater()
    {
        //au.PlayOneShot(elementSounds[1], 1);
        waterP.Play();
        print("water");
        yield return new WaitForSeconds(4);
        waterP.Stop();
    }

    public void ChangeHealth(int amount)
    {
        if(amount < 0) ani.SetTrigger("Attack");
        enemyHealth = Mathf.Clamp(enemyHealth + amount, 0, maxHealth);

        healthBar.value = (float)enemyHealth / maxHealth;

        if (enemyHealth == 0)
        {
            ani.SetBool("isDead", true);
            Destroy(this.gameObject, 0.8f);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Card")
        {
            ChangeHealth(-10);
            tickDmg += 5;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "LavaBall")
        {
            tickDmg += 5;
        }
    }
}
