using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioClip[] cardSounds; // select, throw, combine

    CardRotation deck;
    CardManager cm;
    Rigidbody rb;
    CardManager.Element currentCard;
    
    private AudioSource au;

    public static Dictionary<CardManager.Element[], CardManager.Element> dict = new Dictionary<CardManager.Element[], CardManager.Element>();
    
    public bool gripping{get;set;}
    public bool inDeck{get;set;}

    public bool canCombine{get;set;}

    public void Start()
    {
        au = GetComponent<AudioSource>();
        cm = GetComponent<CardManager>();
        rb = GetComponent<Rigidbody>();

        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<CardRotation>();
        currentCard = this.GetComponent<CardManager>().type;

        dict.Add(new CardManager.Element[] { CardManager.Element.fire, CardManager.Element.earth }, CardManager.Element.lava);//lava
        dict.Add(new CardManager.Element[] { CardManager.Element.fire, CardManager.Element.water }, CardManager.Element.steam);//steam
        dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.wind }, CardManager.Element.storm);//storm
        dict.Add(new CardManager.Element[] { CardManager.Element.wind, CardManager.Element.earth }, CardManager.Element.sand);//sand
        dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.earth }, CardManager.Element.wood); //wood
        //dict.Add(new CardManager.Element[] { CardManager.Element.water, CardManager.Element.wind }, CardManager.Element.none);//ice
        dict.Add(new CardManager.Element[] { CardManager.Element.storm, CardManager.Element.fire }, CardManager.Element.lightning);
       // dict.Add(new CardManager.Element[] { CardManager.Element.sand, CardManager.Element.fire }, CardManager.Element.glass);
       // dict.Add(new CardManager.Element[] { CardManager.Element.lava, CardManager.Element.water }, CardManager.Element.rock);
       // dict.Add(new CardManager.Element[] { CardManager.Element.earth, CardManager.Element.water }, CardManager.Element.life);

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

        //dictionary 
        //keys of arrays of spells
        //def of enum type 
        else if (canCombine && other.tag == "Card" && !gripping && !other.GetComponent<CardState>().gripping && other.GetComponent<CardManager>().type != GetComponent<CardManager>().type && !GetComponent<CardManager>().thrown)
        {
            foreach (KeyValuePair<CardManager.Element[], CardManager.Element> elements in dict)
            {
                CardManager.Element[] temp = elements.Key;
                CardManager.Element thisCard = GetComponent<CardManager>().type;
                CardManager.Element otherCard = other.GetComponent<CardManager>().type;
                if (other.transform.position.y < transform.position.y)
                {
                    if ((temp[0] == otherCard && temp[1] == thisCard) || (temp[0] == thisCard && temp[1] == otherCard))
                    {
                        au.PlayOneShot(cardSounds[2], 1);
                        other.GetComponent<CardManager>().type = elements.Value;
                        other.GetComponent<CardManager>().UpdateGlyph();
                        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        deck.cards.Remove(gameObject);
                        Destroy(gameObject);
                        deck.AdjustCards();
                    }
                }
                else 
                {
                    if ((temp[0] == otherCard && temp[1] == thisCard) || (temp[0] == thisCard && temp[1] == otherCard))
                    {
                        au.PlayOneShot(cardSounds[2], 1);
                        GetComponent<CardManager>().type = elements.Value;
                        GetComponent<CardManager>().UpdateGlyph();
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        deck.cards.Remove(other.gameObject);
                        Destroy(other.gameObject);
                        deck.AdjustCards();
                    }


                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Deck")
        {
            inDeck = true;
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
        ActTest();
    }

    public void DeAct()
    {
        DeActTest();
    }

    private void ActTest()
    {
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        au.PlayOneShot(cardSounds[0]);

        gripping = true;
        GetComponent<CardManager>().isSelected = true;

        deck.cards.Remove(gameObject);
        deck.AdjustCards();

        GetComponent<NewCard>().Pick();
    }

    private void DeActTest()
    {
        gripping = false;
        GetComponent<CardManager>().isSelected = false;

        if(!GetComponent<CardManager>().thrown)
        {
            if(inDeck)
            {
                transform.parent = deck.gameObject.transform;

                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);

                StartCoroutine(SnapToDeck());
            }
        }
    }

    private IEnumerator SnapToDeck()
    {
        int index = 0;
        yield return new WaitForSeconds(0.2f);

        for(int i = 0; i < deck.cards.Count; i++)
        {
            if(transform.position.x > deck.cards[i].transform.position.x) index += 1;
        }

        if (index < deck.cards.Count)
        {
            deck.cards.Insert(index, gameObject);
        }
        else
        {
            deck.cards.Add(gameObject);
        }
        deck.AdjustCards();
    }
}
