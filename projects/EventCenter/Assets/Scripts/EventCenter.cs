using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventCenter
{
    #region 事件廣播主體/基站 : 添加事件、移除事件
    // 定義一個委託列表
    public static Dictionary<EventType, Delegate> eventTable = new Dictionary<EventType, Delegate>();

    /// <summary>
    /// 添加事件監聽
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    /// <exception cref="Exception"></exception>
    private static void OnListenerAdding(EventType eventType, Delegate callBack)
    {
        // 如果沒有 事件key，則添加進去
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, null); // 添加事件
        }

        // 取得當前事件
        Delegate d = eventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType()) // d不為空且，類型不一樣
        {
            // 類型不一樣，拋出異常
            // 事件類型{0}添加了不正確的委託{1}。當前類型是{2}，添加類型是{3}。 0:事件類型 1:委託 2:當前類型 3:添加類型
            throw new Exception(string.Format("Try to add not correct event {0}. Current type is {1}, adding type is {2}.", eventType, d.GetType(), callBack.GetType()));
        }

    }


    /// <summary>
    /// 移除事件監聽
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    /// <exception cref="Exception"></exception>
    private static void OnListenerRemoving(EventType eventType, Delegate callBack)
    {
        if (eventTable.ContainsKey(eventType)) // 如果有事件key
        {
            // 取得當前事件
            Delegate d = eventTable[eventType];
            if (d == null)
            {
                // 沒有對應的事件key
                // 移除事件類型{0}的委託{1}失敗。因為當前事件類型對應的委託為空。 0:事件類型 1:委託
                throw new Exception(string.Format("Remove listener {0}\" for event \"{1}\" does not exist.", callBack, eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                // 檢查當前事件對應的委託類型是否與要移除的委託類型一致
                // 送進來的callBack類型為null，但是delegate類型不為null 執行
                // 移除事件類型{0}的委託{1}失敗。因為當前事件類型對應的委託與要移除的委託類型不一致。 0:事件類型 1:委託
                throw new Exception(string.Format("Remove listener {0}\" for event \"{1}\" does not match expected type {2}.", callBack, eventType, d.GetType()));
            }
        }
        else
        {
            // 沒有 event type
            // 移除事件類型{0}的委託{1}失敗。因為當前事件類型不存在。 0:事件類型 1:委託
            throw new Exception(string.Format("Attempting to remove listener for type \"{0}\" but EventCenter doesn't know about this event type.", eventType));
        }
    }

    /// <summary>
    /// 事件為空後，移除事件
    /// </summary>
    /// <param name="eventType"></param>
    private static void OnListenerRemoved(EventType eventType)
    {
        if (eventTable[eventType] == null) // 如果事件為空 則 刪除
        {
            eventTable.Remove(eventType);
        }
    }

    #endregion




    #region 0個參數的廣播類型

    public static void AddListener(EventType eventType, CallBack callBack)
    {
        OnListenerAdding(eventType, callBack); // 在eventCenter中添加事件
        eventTable[eventType] = (CallBack)eventTable[eventType] + callBack; // 添加事件 在eventTable中
    }
    public static void RemoveListener(EventType eventType , CallBack callBack)
    {
        OnListenerRemoving(eventType, null); // 將事件從eventCenter中移除
        eventTable.Remove(eventType); 
        OnListenerRemoved(eventType); // 移除事件
    }

    /// <summary>
    /// 呼叫事件
    /// </summary>
    /// <param name="eventType"></param>
    /// <exception cref="Exception"></exception>
    public static void Broadcast(EventType eventType)
    {
        Delegate d;
        if (eventTable.TryGetValue(eventType, out d)) // 如果有事件key
        {
            CallBack callBack = d as CallBack; // 將d轉換為CallBack
            if (callBack != null) 
            {
                callBack();
            }
            else
            {
                // 事件類型{0}沒有對應的委託。 0:事件類型
                throw new Exception(string.Format("Broadcasting event \"{0}\" has no listeners.", eventType));
            }
        }
    }
    #endregion

    #region 1個參數的廣播類型
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
