using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    private Transform newWood { get; set; }
    private int hp = 1;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ShootWood());
    }

    void Update()
    {
        if (hp == 0)
        {
            Destroy(gameObject, 4);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Card")
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(-other.GetComponent<Rigidbody>().velocity.x, -other.GetComponent<Rigidbody>().velocity.y, -other.GetComponent<Rigidbody>().velocity.z);
            hp--;
        }
    }
    private IEnumerator ShootWood()
    {
        newWood = GetComponent<Transform>();

        WaitForSeconds sec1 = new WaitForSeconds(0.15f);
        WaitForSeconds sec2 = new WaitForSeconds(0.02f);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                newWood.transform.localScale = new Vector3(newWood.localScale.x, newWood.localScale.y + 0.3f, newWood.localScale.z);
                GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
                yield return sec2;
            }

            yield return sec1;
        }

        Destroy(gameObject, 4);
    }
}
