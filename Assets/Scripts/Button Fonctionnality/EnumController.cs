using System;
using UnityEngine;

public abstract class EnumController<T> : MainBehaviour where T : struct, Enum
{
    [SerializeField] private T EnumValue;
    public T GetEnumValue => EnumValue;
}


