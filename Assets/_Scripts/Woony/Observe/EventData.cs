using Observe.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observe.EventData
{
    public class TestData : IObserveEventData
    {
        public int TestVar;

        public TestData(int testVar)
        {
            TestVar = testVar;
        }
    }
}
