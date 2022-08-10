using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Environment.Instance.windDir, ForceMode.Force);
    }
}
