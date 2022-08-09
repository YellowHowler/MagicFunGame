using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("MainCamera"))
        {

            col.gameObject.GetComponent<PlayerState>().ChangeHealth(-10);
            Debug.Log("hit");
            Destroy(this.gameObject);
                   
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
