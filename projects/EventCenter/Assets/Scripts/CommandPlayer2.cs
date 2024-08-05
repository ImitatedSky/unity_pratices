using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPlayer2 : MonoBehaviour
{

    public void CallCommand(int com)
    {
        EventCenter.Broadcast(EventType.player2Action , com); // 廣播事件
    }
}
