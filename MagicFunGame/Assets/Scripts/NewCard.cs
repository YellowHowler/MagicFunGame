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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CardManager>().isSelected)
        {
            GameObject newCard = null;

            if (thisCard == cardType.fire)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                newCard = Instantiate(fireCard, this.transform.position, Quaternion.identity);
            }
            else if (thisCard == cardType.earth)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                newCard = Instantiate(earthCard,this.transform.position,Quaternion.identity);
            }
            else if (thisCard == cardType.water)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                newCard = Instantiate(waterCard, this.transform.position, Quaternion.identity);
            }
            else if (thisCard == cardType.wind)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                newCard = Instantiate(windCard, this.transform.position, Quaternion.identity);
            }
            
            GetComponent<CardManager>().isSelected = false;
        }
    }
}
