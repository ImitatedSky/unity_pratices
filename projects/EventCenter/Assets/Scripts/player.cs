using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        EventCenter.AddListener(EventType.command, Move); // �K�[�ƥ���ť eventType:command  Move:�e�U
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.command, Move); // �����ƥ��ť
    }

    private void Move()
    {
        Debug.Log("Player Move");
        playerTransform.position += new Vector3(1, 0, 0);

    }

}
