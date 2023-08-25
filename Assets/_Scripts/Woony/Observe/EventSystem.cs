using Observe.EventData;
using Observe.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observe
{
    public static class EventSystem
    {
        public static EventContainer<TestData> TestData = new EventContainer<TestData>();

        private static Dictionary<ObserveEventType, IObserveEventContainer> _container = new Dictionary<ObserveEventType, IObserveEventContainer>()
        {
            { ObserveEventType.TestData, TestData },
        };
        private static IObserveEventContainer _getValueResult;

        public static void ClearAllEvent()
        {
            foreach (var item in _container)
            {
                item.Value.ClearAllEvent();
            }
        }

        public static void Notice(ObserveEventType observeEventType, IObserveEventData eventData)
        {
            InvokeEvent(observeEventType, eventData);
        }

        private static void InvokeEvent(ObserveEventType observeEventType, IObserveEventData eventData)
        {
            if (_container.TryGetValue(observeEventType, out _getValueResult) == false)
            {
                Debug.LogError($"{nameof(EventSystem)} : 타입에 대한 eventContainer 할당되지 않음 = {observeEventType}");
                return;
            }

            _getValueResult.Invoke(eventData);
        }
    }
}