using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPlayer : MonoBehaviour
{
    public void CallCommand()
    {
        EventCenter.Broadcast(EventType.playerAction); // �s���ƥ�
    }
}
