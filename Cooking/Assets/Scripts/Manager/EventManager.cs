using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cooking.Manager
{
    /// <summary>
    /// 专门管理事件 事件管理器
    /// </summary>
    public static class EventManager
    {
        //存储事件类型
        private static Dictionary<EventType, Delegate> eventTable = new Dictionary<EventType, Delegate>();

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventRegistrationToken">委托事件</param>
        public static void RegisterEvent(EventType eventType, Delegate eventRegistrationToken)
        {
            if (eventRegistrationToken == null)
            {
                Debug.LogError($"[EventManager] RegisterEvent 事件类型 [{eventType}] 传入的委托为 null，注册无效");
                return;
            }

            if (eventTable.ContainsKey(eventType))
            {
                eventTable[eventType] = Delegate.Combine(eventTable[eventType], eventRegistrationToken);
            }
            else
            {
                eventTable[eventType] = eventRegistrationToken;
            }
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventRegistrationToken"></param>
        public static void UnregisterEvent(EventType eventType, Delegate eventRegistrationToken)
        {
            if (eventTable.ContainsKey(eventType))
            {
                Delegate newDelegate = Delegate.Remove(eventTable[eventType], eventRegistrationToken);
                if (newDelegate == null)
                {
                    eventTable.Remove(eventType);
                }
                else
                {
                    eventTable[eventType] = newDelegate;
                }
            }
        }

        /// <summary>
        /// 触发事件——不带参数
        /// </summary>
        /// <param name="eventType"></param>
        public static void TriggerEvent(EventType eventType)
        {
            if (eventTable.TryGetValue(eventType, out Delegate eventRegistrationToken))
            {
                var action = eventRegistrationToken as Action;
                if (action != null)
                {
                    action.Invoke();
                }
                else
                {
                    Debug.LogError(
                        $"TriggerEvent失败，事件类型{eventType},委托签名不匹配，期望委托签名：Action，实际注册委托：{eventRegistrationToken}");
                }
            }
        }

        /// <summary>
        /// 触发事件——带一个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="arg"></param>
        /// <typeparam name="T"></typeparam>
        public static void TriggerEvent<T>(EventType eventType, T arg)
        {
            if (eventTable.TryGetValue(eventType, out Delegate eventRegistrationToken))
            {
                var action = eventRegistrationToken as Action<T>;
                if (action != null)
                {
                    action.Invoke(arg);
                }
                else
                {
                    Debug.LogError(
                        $"TriggerEvent失败，事件类型{eventType},委托签名不匹配，期望委托签名：Action<{typeof(T).Name}>，实际注册委托：{eventRegistrationToken}");
                }
            }
        }

        /// <summary>
        /// 触发事件——带两个参数
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public static void TriggerEvent<T1, T2>(EventType eventType, T1 arg1, T2 arg2)
        {
            if (eventTable.TryGetValue(eventType, out Delegate eventRegistrationToken))
            {
                var action = eventRegistrationToken as Action<T1, T2>;
                if (action != null)
                {
                    action.Invoke(arg1, arg2);
                }
                else
                {
                    Debug.LogError(
                        $"TriggerEvent失败，事件类型{eventType},委托签名不匹配，期望委托签名：Action<{typeof(T1).Name}, {typeof(T2).Name}>，实际注册委托：{eventRegistrationToken}");
                }
            }
        }
    }
}
