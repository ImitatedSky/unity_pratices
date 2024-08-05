using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    private int id = 2;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform; //playerTransform = GetComponent<Transform>();
        EventCenter.AddListener<int>(EventType.player2Action, Player2);
    }
    
    private void Player2(int com)
    {
        if (com != id)
        {
            return;
        }

        Debug.Log("Player2");
        playerTransform.position += new Vector3(0, 1, 0);

    }
}
