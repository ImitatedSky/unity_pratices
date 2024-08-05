using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform; //playerTransform = GetComponent<Transform>();
        EventCenter.AddListener(EventType.playerAction, Move); // 添加事件到監聽 eventType:command  Move:委託
        EventCenter.AddListener(EventType.playerAction, Rotate); // 添加事件到監聽 eventType:command  Move:委託
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.playerAction, Move); // 移除事件監聽
        EventCenter.RemoveListener(EventType.playerAction, Rotate); // 移除事件監聽
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
