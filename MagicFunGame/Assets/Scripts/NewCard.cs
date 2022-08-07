using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCard : MonoBehaviour
{
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
            if (thisCard == cardType.fire)
            {
                Instantiate(fireCard);
            }
            else if (thisCard == cardType.earth)
            {
                Instantiate(earthCard);
            }
            else if (thisCard == cardType.water)
            {
                Instantiate(waterCard);
            }
            else if (thisCard == cardType.wind)
            {
                Instantiate(windCard);
            }

        }
    }
}
