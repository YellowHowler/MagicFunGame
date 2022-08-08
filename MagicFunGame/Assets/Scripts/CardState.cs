using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform SpellWeaverCoords;
    [SerializeField] private AudioClip[] cardSounds; // select, throw, combine

    CardRotation deck;
    CardManager cm;
    Rigidbody rb;
    CardManager.Element currentCard;

    private AudioSource au;

    public static Dictionary<CardManager.Element[], CardManager.Element> dict = new Dictionary<CardManager.Element[], CardManager.Element>();

    bool gripping;
    public bool inDeck{get;set;}

    private void Start()
    {
        au = GetComponent<AudioSource>();
        cm = GetComponent<CardManager>();
        rb = GetComponent<Rigidbody>();

        deck = transform.parent.gameObject.GetComponent<CardRotation>();
        currentCard = this.GetComponent<CardManager>().type;

        dict.Add(new CardManager.Element[] { CardManager.Element.fire, CardManager.Element.earth }, CardManager.Element.lava);//lava
        dict.Add(new CardManager.Element[] { CardManager.Element.fire, CardManager.Element.water }, CardManager.Element.steam);//steam
        dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.wind }, CardManager.Element.storm);//storm
        dict.Add(new CardManager.Element[] { CardManager.Element.wind, CardManager.Element.earth }, CardManager.Element.sand);//sand
        dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.earth }, CardManager.Element.wood); //wood
        //dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.wind }, CardManager.Element.none);//ice
        dict.Add(new CardManager.Element[] { CardManager.Element.storm, CardManager.Element.fire }, CardManager.Element.light);
        dict.Add(new CardManager.Element[] { CardManager.Element.sand, CardManager.Element.fire }, CardManager.Element.glass);
        dict.Add(new CardManager.Element[] { CardManager.Element.lava, CardManager.Element.water }, CardManager.Element.rock);
        dict.Add(new CardManager.Element[] { CardManager.Element.earth, CardManager.Element.water }, CardManager.Element.life);

        inDeck = true;
    }
    private void Update()
    {

    }
    //add when player lets go of card
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deck")
        {
            inDeck = true;
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
            foreach (KeyValuePair<CardManager.Element[], CardManager.Element> elements in dict)
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
                if (numMatching == 2)
                {
                    if (other.transform.position.y > transform.position.y)
                    {
                        au.PlayOneShot(cardSounds[2], 1);
                        GetComponent<CardManager>().type = elements.Value;
                        GetComponent<CardManager>().UpdateGlyph();
                        print(GetComponent<CardManager>().type);
                        deck.cards.Remove(other.gameObject);
                        Destroy(other.gameObject);
                        deck.AdjustCards();
                    }
                }

            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Deck")
        {
            inDeck = false;
        }
    }

    public void Act()
    {
        gripping = true;
        rb.useGravity = false;

        if (inDeck) deck.cards.Remove(gameObject);
        deck.AdjustCards();
    }

    public void DeAct()
    {
        gripping = false;
        rb.useGravity = true;

        if(inDeck) 
        {
            rb.useGravity = false;
            
            int index = 0;

            for(int i = 0; i < deck.cards.Count; i++)
            {
                if(transform.localPosition.x > deck.cards[i].transform.localPosition.x) index = i + 1;
            }

            deck.cards.Insert(index, gameObject);
            deck.AdjustCards();
        }
    }
}
