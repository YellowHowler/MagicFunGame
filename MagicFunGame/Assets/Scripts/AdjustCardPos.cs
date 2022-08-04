using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCardPos : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = Camera.main.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, 1.25f, player.position.z);
        transform.rotation = Quaternion.Euler(0, player.rotation.y, 0);
    }
}
