using Observe;
using Observe.EventData;
using Observe.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveManager : Singleton<ObserveManager>
{
    private TestObserver _testObserver = new TestObserver();

    public void Initialize()
    {
        EventSystem.ClearAllEvent();

        _testObserver.Initialize();
    }
}
