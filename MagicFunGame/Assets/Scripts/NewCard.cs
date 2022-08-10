using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCard : MonoBehaviour
{
    //make sure the card locks position when you drag it in as a prefab
    [SerializeField] GameObject fireCard;
    [SerializeField] GameObject waterCard;
    [SerializeField] GameObject earthCard;
    [SerializeField] GameObject windCard;
    [SerializeField] cardType thisCard;

    private Vector3 startPos;

    enum cardType
    {
        fire,
        water,
        earth,
        wind

    }
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        GetComponent<CardManager>().enabled = true;
        GetComponent<CardManager>().Start();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<CardManager>().UpdateGlyph();
        GetComponent<CardManager>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CardManager>().isSelected)
        {

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            
            GetComponent<CardManager>().enabled = true;
            GetComponent<CardManager>().Start();

            GetComponent<CardState>().enabled = true;
            GetComponent<CardState>().Start();

            

            if (thisCard == cardType.fire)
            {
                Instantiate(fireCard, startPos, Quaternion.identity);
                
            }
            else if (thisCard == cardType.earth)
            {
                Instantiate(earthCard, startPos, Quaternion.identity);
            }
            else if (thisCard == cardType.water)
            {
                Instantiate(waterCard, startPos, Quaternion.identity);

            }
            else if (thisCard == cardType.wind)
            {
                Instantiate(windCard, startPos, Quaternion.identity);
            }
            
            GetComponent<CardManager>().isSelected = false;
            GetComponent<NewCard>().enabled = false;
        }
    }
}
