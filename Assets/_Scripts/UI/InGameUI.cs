using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : SingletonComponent<InGameUI>
{
    public WaveUI WaveUI;
    public TopUI TopUI;

    public void Initialize()
    {
        WaveUI.Initialize();
        TopUI.Initialize();
    }
}
