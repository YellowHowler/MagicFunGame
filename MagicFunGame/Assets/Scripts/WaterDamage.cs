using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject col)
    {
        if(col.tag == "Enemy")
        {
            EnemyAI ea = col.GetComponent<EnemyAI>();
            
            ea.ChangeHealth(-3);
        }

        else if(col.tag == "MainCamera")
        {
            PlayerState ps = col.GetComponent<PlayerState>();
            
            ps.ChangeHealth(-3);
        }
    }
}
