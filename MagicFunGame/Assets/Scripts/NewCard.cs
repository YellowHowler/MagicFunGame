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
            if (thisCard == cardType.fire)
            {
                Instantiate(fireCard, this.transform.position, Quaternion.identity);
                
            }
            else if (thisCard == cardType.earth)
            {
                Instantiate(earthCard, this.transform.position, Quaternion.identity);
            }
            else if (thisCard == cardType.water)
            {
                Instantiate(waterCard, this.transform.position, Quaternion.identity);

            }
            else if (thisCard == cardType.wind)
            {
                Instantiate(windCard, this.transform.position, Quaternion.identity);
            }
            
            GetComponent<CardManager>().isSelected = false;
            GetComponent<NewCard>().enabled = false;
        }
    }
}
