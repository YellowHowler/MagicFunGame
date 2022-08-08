using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Enemy"))
        {
            //other.GetComponent<EnemyAI>();
        }
    }
}
