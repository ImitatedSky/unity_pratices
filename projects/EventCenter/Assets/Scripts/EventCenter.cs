using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventCenter
{
    #region �ƥ�s���D��/�� : �K�[�ƥ�B�����ƥ�
    // �w�q�@�өe�U�C��
    public static Dictionary<EventType, Delegate> eventTable = new Dictionary<EventType, Delegate>();

    /// <summary>
    /// �K�[�ƥ��ť
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    /// <exception cref="Exception"></exception>
    private static void OnListenerAdding(EventType eventType, Delegate callBack)
    {
        // �p�G�S�� �ƥ�key�A�h�K�[�i�h
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null); // �K�[�ƥ�
        }

        // ���o��e�ƥ�
        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType()) // d�����ťB�A�������@��
        {
            // �������@�ˡA�ߥX���`
            // �ƥ�����{0}�K�[�F�����T���e�U{1}�C��e�����O{2}�A�K�[�����O{3}�C 0:�ƥ����� 1:�e�U 2:��e���� 3:�K�[����
            throw new Exception(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, d.GetType(), callBack.GetType()));
        }

    }


    /// <summary>
    /// �����ƥ��ť
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    /// <exception cref="Exception"></exception>
    private static void OnListenerRemoving(EventType eventType, Delegate callBack)
    {
        if (eventTable.ContainsKey(eventType)) // �p�G���ƥ�key
        {
            // ���o��e�ƥ�
            Delegate d = eventTable[eventType];
            if (d == null)
            {
                // �S���������ƥ�key
                // �����ƥ�����{0}���e�U{1}���ѡC�]����e�ƥ������������e�U���šC 0:�ƥ����� 1:�e�U
                throw new Exception(string.Format("Remove listener {0}\" for event \"{1}\" does not exist.", callBack, eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                // �ˬd��e�ƥ�������e�U�����O�_�P�n�������e�U�����@�P
                // �e�i�Ӫ�callBack������null�A���Odelegate��������null ����
                // �����ƥ�����{0}���e�U{1}���ѡC�]����e�ƥ������������e�U�P�n�������e�U�������@�P�C 0:�ƥ����� 1:�e�U
                throw new Exception(string.Format("Remove listener {0}\" for event \"{1}\" does not match expected type {2}.", callBack, eventType, d.GetType()));
            }
        }
        else
        {
            // �S�� event type
            // �����ƥ�����{0}���e�U{1}���ѡC�]����e�ƥ��������s�b�C 0:�ƥ����� 1:�e�U
            throw new Exception(string.Format("Attempting to remove listener for type \"{0}\" but EventCenter doesn't know about this event type.", eventType));
        }
    }

    /// <summary>
    /// �ƥ󬰪ū�A�����ƥ�
    /// </summary>
    /// <param name="eventType"></param>
    private static void OnListenerRemoved(EventType eventType)
    {
        if (eventTable[eventType] == null) // �p�G�ƥ󬰪� �h �R��
        {
            eventTable.Remove(eventType);
        }
    }

    #endregion




    #region 0�ӰѼƪ��s������

    public static void AddListener(EventType eventType, CallBack callBack)
    {
        OnListenerAdding(eventType, callBack); // �beventCenter���K�[�ƥ�
        eventTable[eventType] = (CallBack)eventTable[eventType] + callBack; // �K�[�ƥ� �beventTable��
    }
    public static void RemoveListener(EventType eventType , CallBack callBack)
    {
        OnListenerRemoving(eventType, null); // �N�ƥ�qeventCenter������
        eventTable.Remove(eventType); 
        OnListenerRemoved(eventType); // �����ƥ�
    }

    /// <summary>
    /// �I�s�ƥ�
    /// </summary>
    /// <param name="eventType"></param>
    /// <exception cref="Exception"></exception>
    public static void Broadcast(EventType eventType)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d)) // �p�G���ƥ�key
        {
            CallBack callBack = d as CallBack; // �Nd�ഫ��CallBack
            if (callBack != null) 
            {
                callBack();
            }
            else
            {
                // �ƥ�����{0}�S���������e�U�C 0:�ƥ�����
                throw new Exception(string.Format("Broadcasting event \"{0}\" has no listeners.", eventType));
            }
        }
    }
    #endregion

    #region 1�ӰѼƪ��s������
    public static void AddListener<T>(EventType eventType, CallBack<T> callBack)
    {
        OnListenerAdding(eventType, callBack);
        eventTable[eventType] = (CallBack<T>)eventTable[eventType] + callBack;
    }
    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack)
    {
        OnListenerRemoving(eventType, callBack);
        eventTable[eventType] = (CallBack<T>)eventTable[eventType] - callBack;
        OnListenerRemoved(eventType);
    }

    public static void Broadcast<T>(EventType eventType, T arg)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("Broadcasting event \"{0}\" has no listeners.", eventType));
            }
        }
    }
    #endregion

}
