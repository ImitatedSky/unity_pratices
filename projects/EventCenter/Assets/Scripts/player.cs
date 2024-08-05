using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform; //playerTransform = GetComponent<Transform>();
        EventCenter.AddListener(EventType.playerAction, Move); // �K�[�ƥ���ť eventType:command  Move:�e�U
        EventCenter.AddListener(EventType.playerAction, Rotate); // �K�[�ƥ���ť eventType:command  Move:�e�U
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.playerAction, Move); // �����ƥ��ť
        EventCenter.RemoveListener(EventType.playerAction, Rotate); // �����ƥ��ť
    }

    private void Move()
    {
        Debug.Log("Player Move");
        playerTransform.position += new Vector3(1, 0, 0);

    }

    private void Rotate()
    {
        Debug.Log("Player Rotate");
        playerTransform.Rotate(new Vector3(15, 15, 15));
    }

}
