using Observe.EventData;
using Observe.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observe
{
    public class EventContainer<T> : IObserveEventContainer where T : IObserveEventData
    {
        private Action<T> _onNoticeEvent;

        public static EventContainer<T> operator +(EventContainer<T> eventContainer, Action<T> action)
        {
            eventContainer._onNoticeEvent -= action;
            eventContainer._onNoticeEvent += action;
            return eventContainer;
        }

        public static EventContainer<T> operator -(EventContainer<T> eventContainer, Action<T> action)
        {
            eventContainer._onNoticeEvent -= action;
            return eventContainer;
        }

        public void Invoke(IObserveEventData eventData)
        {
            if (eventData is T == false) return;
            _onNoticeEvent?.Invoke((T)eventData);
        }

        public void ClearAllEvent()
        {
            _onNoticeEvent = null;
        }
    }
}