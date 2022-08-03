using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotation : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private float distBetweenCards;
    [SerializeField] Transform midPoint;    

    private int cardNum;
    [HideInInspector] public List<GameObject> cards;

    void Start()
    {
        AdjustCards();
    }


    // Update is called once per frame
    public void AdjustCards()
    {
        cardNum = transform.childCount;
        GameObject[] temp = new GameObject[cardNum];

        Debug.Log(cardNum);

        for(int i = 0; i < cardNum; i++)
        {
            temp[i] = transform.GetChild(i).gameObject;
            cards.Add(temp[i]);
        }

        if (cardNum % 2 == 0)
        {
            cards[0].transform.localPosition = new Vector3(midPoint.position.x + distBetweenCards / 2, midPoint.position.y, midPoint.position.z);
            cards[1].transform.localPosition = new Vector3(midPoint.position.x - distBetweenCards / 2, midPoint.position.y, midPoint.position.z);
            for (int i = 2; i < cards.Count; i++)
            {
                if (i % 2 == 0)
                {
                    cards[i].transform.localPosition = new Vector3(cards[i - 2].transform.localPosition.x + distBetweenCards, cards[i - 2].transform.localPosition.y, cards[i - 2].transform.localPosition.z);
                }
                else
                {
                    cards[i].transform.localPosition = new Vector3(cards[i - 2].transform.localPosition.x - distBetweenCards, cards[i - 2].transform.localPosition.y, cards[i - 2].transform.localPosition.z);
                }
            }


        }
        else
        {
            cards[0].transform.localPosition = new Vector3(midPoint.position.x, midPoint.position.y, midPoint.position.z);
            for (int i = 2; i < cards.Count; i++)
            {
                if (i % 2 == 0)
                {
                    cards[i].transform.localPosition = new Vector3(cards[i - 2].transform.localPosition.x + distBetweenCards, cards[i - 2].transform.localPosition.y, cards[i - 2].transform.localPosition.z);
                }
                else
                {
                    cards[i].transform.localPosition = new Vector3(cards[i - 2].transform.localPosition.x - distBetweenCards, cards[i - 2].transform.localPosition.y, cards[i - 2].transform.localPosition.z);
                }
            }

        }

    }
    void Update()
    {

        //AdjustCards();

    }
}
