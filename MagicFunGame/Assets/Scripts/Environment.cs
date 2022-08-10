using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : Singleton<Environment>
{
    public Vector3 windDir;
    public bool isStorm;
    public bool isSteam;
    private ParticleSystem stormCloudP;
    private ParticleSystem stormFogP;
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
    private void Start()
    {
        stormCloudP = GameObject.FindGameObjectWithTag("StormCloud").GetComponent<ParticleSystem>();
        stormFogP = GameObject.FindGameObjectWithTag("StormFog").GetComponent<ParticleSystem>();
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
}
