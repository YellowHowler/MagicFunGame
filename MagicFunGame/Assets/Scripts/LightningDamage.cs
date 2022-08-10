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
            
            if(!Environment.Instance.isStorm) ea.ChangeHealth(-2);
            else ea.ChangeHealth(-4);
        }
    }
}
