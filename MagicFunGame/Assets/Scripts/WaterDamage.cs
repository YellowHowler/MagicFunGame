using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject col)
    {
        if (col.tag == "Enemy")
        {
            EnemyAI ea = col.GetComponent<EnemyAI>();

            if (!Environment.Instance.isStorm) ea.ChangeHealth(-1);
            else ea.ChangeHealth(-2);
        }

        else if (col.tag == "MainCamera")
        {
            PlayerState ps = col.GetComponent<PlayerState>();

            if (!Environment.Instance.isStorm) ps.ChangeHealth(-1);
            else ps.ChangeHealth(-2);
        }
    }
}
