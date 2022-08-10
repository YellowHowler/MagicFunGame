using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : Singleton<Environment>
{
    public Vector3 windDir;
    public bool isStorm;
    public bool isSteam;
    public bool isMeteor;
    private ParticleSystem stormCloudP;
    private ParticleSystem stormFogP;
    private ParticleSystem meteorP;
    [SerializeField] private Material stormSky;
    [SerializeField] private Material regSky;
   
    private GameObject player;
    private GameObject enemy;
    public void StartStorm()
    {
        if (!isStorm)
        {
            StartCoroutine(Storm());

        }
    }
    public void StartSteam()
    {
        if (!isSteam)
        {
            StartCoroutine(Steam());
        }
    }

    public void StartMeteor()
    {
        if(!isMeteor)
        {
            StartCoroutine(Meteor()); 
        }
    }
    private void Start()
    {
        stormCloudP = GameObject.FindGameObjectWithTag("StormCloud").GetComponent<ParticleSystem>();
        stormFogP = GameObject.FindGameObjectWithTag("StormFog").GetComponent<ParticleSystem>();
        meteorP = GameObject.FindGameObjectWithTag("Meteor").GetComponent<ParticleSystem>();

        player = GameObject.FindGameObjectWithTag("MainCamera");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        
    }
    public IEnumerator Storm()
    {
        isStorm = true;
        PlayerState.damage[CardManager.Element.water] += 10;
        RenderSettings.skybox = stormSky;
        stormCloudP.Play();
        yield return new WaitForSeconds(10);
        stormCloudP.Stop();
        PlayerState.damage[CardManager.Element.water] -= 10;
        RenderSettings.skybox = regSky;
        isStorm = false;
    }
    public IEnumerator Steam()
    {
        isSteam = true;
        player.GetComponent<PlayerState>().tickDmg += 5;
        enemy.GetComponent<EnemyAI>().tickDmg += 5;
        stormFogP.Play();
        PlayerState.damage[CardManager.Element.water] += 5;
        PlayerState.damage[CardManager.Element.fire] += 5;
        yield return new WaitForSeconds(5);
        stormFogP.Stop();
        PlayerState.damage[CardManager.Element.water] -= 5;
        PlayerState.damage[CardManager.Element.fire] -= 5;
        isSteam = false;
    }

    public IEnumerator Meteor()
    {
        isMeteor = true;

        meteorP.gameObject.transform.position = new Vector3(enemy.transform.position.x, 25, enemy.transform.position.z);
        meteorP.Play();
        yield return new WaitForSeconds(2);
        meteorP.Stop();

        isMeteor = false;
    }
}
