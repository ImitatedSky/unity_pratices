using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanPlayer : MonoBehaviour
{
    public void CallCommand()
    {
        EventCenter.Broadcast(EventType.playerAction); // ¼s¼½¨Æ¥ó
    }
}
