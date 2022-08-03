using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform deckCoords;
    [SerializeField] Transform SpellWeaverCoords;
    CardRotation deck;
    CardManager.Element currentCard;
    Dictionary<CardManager.Element[], CardManager.Element> dict = new Dictionary<CardManager.Element[], CardManager.Element>();
    bool gripping;
    private void Start()
    {
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<CardRotation>();
        currentCard = this.GetComponent<CardManager>().type;
        dict.Add(new CardManager.Element[] {CardManager.Element.fire, CardManager.Element.earth}, CardManager.Element.none);
    }
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 0)
        {
            gripping = false;
        }
        else
        {
            gripping = true;
        }
    }
    //add when player lets go of card
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deck" && !gripping)
        {

            deck.cards.Add(this.gameObject);
        }
        else if (other.tag == "SpellWeaver" && !gripping)
        {
            transform.position = SpellWeaverCoords.position;


        }
        //dictionary 
        //keys of arrays of spells
        //def of enum type 
        else if (other.tag == "Card" && !gripping)
        {
            switch (other.GetComponent<CardManager>().type)
            {
                case CardManager.Element.earth:
                    switch (currentCard)
                    {
                        case CardManager.Element.fire:
                            break;
                        case CardManager.Element.water:
                            break;
                        case CardManager.Element.wind:
                            break;
                    }
                    break;
                case CardManager.Element.fire:
                    switch (currentCard)
                    {
                        case CardManager.Element.earth:
                            break;
                        case CardManager.Element.fire:
                            break;
                        case CardManager.Element.water:
                            break;
                        case CardManager.Element.wind:
                            break;
                    }
                    break;
                case CardManager.Element.water:
                    switch (currentCard)
                    {
                        case CardManager.Element.earth:
                            break;
                        case CardManager.Element.fire:
                            break;
                        case CardManager.Element.water:
                            break;
                        case CardManager.Element.wind:
                            break;
                    }
                    break;
                case CardManager.Element.wind:
                    switch (currentCard)
                    {
                        case CardManager.Element.earth:
                            break;
                        case CardManager.Element.fire:
                            break;
                        case CardManager.Element.water:
                            break;
                        case CardManager.Element.wind:
                            break;
                    }
                    break;
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Deck" )
        {
            deck.cards.Remove(this.gameObject);
        }
    }
}
