using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        EventCenter.AddListener(EventType.command, Move); // 添加事件到監聽 eventType:command  Move:委託
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.command, Move); // 移除事件監聽
    }

    private void Move()
    {
        Debug.Log("Player Move");
        playerTransform.position += new Vector3(1, 0, 0);

    }

}
