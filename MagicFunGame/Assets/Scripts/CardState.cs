using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform deckCoords;
    [SerializeField] Transform SpellWeaverCoords;
    CardRotation deck;
    private void Start()
    {
        deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<CardRotation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deck")
        {
            
           deck.cards.Add(this.gameObject);
        }
        else if (other.tag == "SpellWeaver")
        {
            transform.position = SpellWeaverCoords.position;
        
        
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Deck")
        {
            deck.cards.Remove(this.gameObject);
        }
    }
}
