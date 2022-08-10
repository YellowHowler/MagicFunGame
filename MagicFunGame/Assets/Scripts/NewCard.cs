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
        fire = 0,
        water = 1,
        earth = 2,
        wind = 3,

    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CardManager>().enabled = true;
        GetComponent<CardManager>().Start();
        GetComponent<CardManager>().UpdateGlyph((int)thisCard);
        
        GetComponent<CardManager>().UpdateGlyph();
        GetComponent<CardManager>().enabled = false;

        GetComponent<CardState>().enabled = false;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Pick()
    {
        PickUp();
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

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
        

        GetComponent<CardState>().enabled = true;
        GetComponent<CardState>().Start();

        GetComponent<CardManager>().enabled = true;
        GetComponent<CardManager>().Start();

        GetComponent<NewCard>().enabled = false;
    }
}
