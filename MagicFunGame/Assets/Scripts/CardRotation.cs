using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotation : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private float distBetweenCards;
    [SerializeField] Transform midPoint;    

    private Transform player;

    private int cardNum;
    [HideInInspector] public List<GameObject> cards;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(WaitStand());
    }


    // Update is called once per frame
    public void AdjustCards()
    {
        cardNum = transform.childCount-1;
        cards = new List<GameObject>();
        GameObject[] temp = new GameObject[cardNum];

        Debug.Log(cardNum);

        for(int i = 0; i < cardNum; i++)
        {
            temp[i] = transform.GetChild(i+1).gameObject;
            cards.Add(temp[i]);
        }

        float midY = midPoint.localPosition.y;
        float midZ = midPoint.localPosition.z;

        if (cardNum % 2 == 0)
        {
            cards[0].transform.localPosition = new Vector3(midPoint.localPosition.x + distBetweenCards / 2, midY, midZ);
            cards[1].transform.localPosition = new Vector3(midPoint.localPosition.x - distBetweenCards / 2, midY, midZ);

            for (int i = 2; i < cardNum; i++)
            {
                if (i % 2 == 0)
                {
                    cards[i].transform.localPosition = new Vector3(cards[i - 2].transform.localPosition.x + distBetweenCards, midY, midZ);
                }
                else
                {
                    cards[i].transform.localPosition = new Vector3(cards[i - 2].transform.localPosition.x - distBetweenCards, midY, midZ);
                }
            }


        }
        else
        {
            cards[0].transform.localPosition = new Vector3(midPoint.localPosition.x, midY, midZ);

            for(int i = 0; i < (cardNum - 1)/2; i++)
            {
                cards[i*2 + 1].transform.localPosition = new Vector3(midPoint.localPosition.x + (i+1)*distBetweenCards, midY, midZ);
                cards[i*2 + 2].transform.localPosition = new Vector3(midPoint.localPosition.x - (i+1)*distBetweenCards, midY, midZ);
            }
        }

    }
    void Update()
    {

        //AdjustCards();

    }

    //wait until player stands than adjust cards
    private IEnumerator WaitStand()
    {
        WaitForSeconds sec = new WaitForSeconds(0.5f);

        while(player.position.y < 0.1f)
        {
            yield return sec;
            Debug.Log("hello");
        }

        AdjustCards();

        for(int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i+1).gameObject.GetComponent<CardManager>().gameStarted = true;
        }
    }
}
