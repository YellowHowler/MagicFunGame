using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCardPos : MonoBehaviour
{
    [SerializeField] private float yPos = 1.5f;
    private Transform player;

    void Start()
    {
        player = Camera.main.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, yPos, player.position.z);
        transform.rotation = Quaternion.Euler(0, player.rotation.y, 0);
    }
}
