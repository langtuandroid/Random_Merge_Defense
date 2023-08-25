using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataLayer
{
    public abstract void SetData<T>(T value);
}