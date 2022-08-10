using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject col)
    {
        if(col.tag == "Enemy")
        {
            EnemyAI ea = col.GetComponent<EnemyAI>();
            
            ea.ChangeHealth(-5);
            //paralyze enemy
        }
    }
}
