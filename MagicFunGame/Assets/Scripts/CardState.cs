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
        dict.Add(new CardManager.Element[] {CardManager.Element.fire, CardManager.Element.earth}, CardManager.Element.lava );//lava
        dict.Add(new CardManager.Element[] { CardManager.Element.fire, CardManager.Element.water }, CardManager.Element.steam);//steam
        dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.wind}, CardManager.Element.storm);//storm
        dict.Add(new CardManager.Element[] { CardManager.Element.wind, CardManager.Element.earth }, CardManager.Element.none);//sand
        dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.earth }, CardManager.Element.wood); //wood

    }
    private void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0)
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
        else if (other.tag == "Card" && !gripping && other.GetComponent<CardManager>().type != GetComponent<CardManager>().type)
        {
            foreach (KeyValuePair< CardManager.Element[], CardManager.Element> elements in dict)
            {
                CardManager.Element[] temp = elements.Key;
                int numMatching = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == other.GetComponent<CardManager>().type)
                    {
                        numMatching++;

                    }
                    if (temp[i] == GetComponent<CardManager>().type)
                    {
                        numMatching++;
                    }

                }
                if(numMatching==2)
                {
                    Destroy(this.gameObject);
                    other.GetComponent<CardManager>().type = elements.Value;


                }



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
