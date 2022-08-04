using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    private Transform newWood {get;set;}

    void Start()
    {
        StartCoroutine(ShootWood());
    }

    void Update()
    {
        
    }

    private IEnumerator ShootWood()
    {
        newWood = GetComponent<Transform>();

        WaitForSeconds sec1 = new WaitForSeconds(0.15f);
        WaitForSeconds sec2 = new WaitForSeconds(0.02f);

        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                newWood.transform.localScale = new Vector3(newWood.localScale.x, newWood.localScale.y + 0.1f, newWood.localScale.z);
                yield return sec2;
            }

            yield return sec1;
        }

        Destroy(gameObject, 4);
    }
}
