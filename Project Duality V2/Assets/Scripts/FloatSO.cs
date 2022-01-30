using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "int Data", menuName = "ScriptableObjects/Int Data", order = 1)]
public class FloatSO : ScriptableObject
{
    int _Value;

    public int Value
    {
        get => _Value;
        set
        {
            _Value = value;
            OnValueChange?.Invoke(_Value);
        }
    }

    public event Action<int> OnValueChange;


}
