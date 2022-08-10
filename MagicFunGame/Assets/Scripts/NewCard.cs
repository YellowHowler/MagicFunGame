using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCard : MonoBehaviour
{
    //make sure the card locks position when you drag it in as a prefab
    [SerializeField] GameObject card;
    [SerializeField] cardType thisCard;

    private Vector3 startPos;

    public bool isSpawn;

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
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        startPos = transform.position;

        GetComponent<CardState>().canCombine = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Pick()
    {
        if(isSpawn) PickUp();
    }

    public void PickUp()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        StartCoroutine(RestoreCard());
        
        GetComponent<CardState>().canCombine = true;
        isSpawn = false;
    }

    private IEnumerator RestoreCard()
    {
        yield return new WaitForSecond(2);

        GameObject newCard = Instantiate(card, startPos, Quaternion.identity);
        newCard.GetComponent<NewCard>().isSpawn = true;

        if(thisCard == cardType.fire) newCard.GetComponent<CardManager>().type = CardManager.Element.fire;
        else if(thisCard == cardType.water) newCard.GetComponent<CardManager>().type = CardManager.Element.water;
        else if(thisCard == cardType.earth) newCard.GetComponent<CardManager>().type = CardManager.Element.earth;
        else if(thisCard == cardType.wind) newCard.GetComponent<CardManager>().type = CardManager.Element.wind;

        newCard.GetComponent<CardManager>().UpdateGlyph();

    }
}
