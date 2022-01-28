using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "HealthData", menuName = "ScriptableObjects/Health Data", order = 1)]
public class PlayerHealthDataSO : ScriptableObject
{

    float _CurrentHealth;

    float _MaxHealth;

    public float CurrentHealth
    {
        get => _CurrentHealth;
        set
        {
            _CurrentHealth = value;
            OnHealthChange?.Invoke(_CurrentHealth, _MaxHealth);
        }
    }

    public float MaxHealth
    {
        get => _MaxHealth;
        set
        {
            _MaxHealth = value;
            _CurrentHealth = MaxHealth;
            OnHealthChange?.Invoke(_CurrentHealth, _MaxHealth);
        }
    }

    public event Action<float, float> OnHealthChange;
}
