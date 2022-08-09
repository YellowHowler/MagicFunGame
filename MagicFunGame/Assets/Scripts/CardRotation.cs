using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRotation : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private float angGap;
    [SerializeField] private float radius;
    [SerializeField] Transform midPoint;    

    private Transform player;

    private int cardNum;
    private float pi;

    [HideInInspector] public List<GameObject> cards;

    void Start()
    {
        pi = Mathf.PI;

        player = Camera.main.gameObject.GetComponent<Transform>();

        cardNum = transform.childCount-1;
        cards = new List<GameObject>();

        for(int i = 0; i < cardNum; i++)
        {
            cards.Add(transform.GetChild(i+1).gameObject);
        }

        AdjustCards();
    }


    // Update is called once per frame
    public void AdjustCards()
    {
        cardNum = cards.Count;

        float midY = midPoint.localPosition.y;
        float midZ = midPoint.localPosition.z;

        for(int i = 0; i < cardNum; i++)
        {
            float angle = pi/2 + (i - (cardNum-1)/2f) * angGap;
            cards[i].transform.localPosition = new Vector3(Mathf.Cos(angle) * radius, midY, Mathf.Sin(angle) * radius);
            cards[i].transform.localRotation = Quaternion.Euler(40, -1 * Mathf.Rad2Deg * (angle - pi/2), 0);
            cards[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            cards[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            cards[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    void Update()
    {
    }
}
