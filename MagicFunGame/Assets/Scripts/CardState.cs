using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform deckCoords;
    [SerializeField] Transform SpellWeaverCoords;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deck")
        {
            
            CardRotation.cards.Add(this.gameObject);
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
            CardRotation.cards.Remove(this.gameObject);
        }
    }
}
