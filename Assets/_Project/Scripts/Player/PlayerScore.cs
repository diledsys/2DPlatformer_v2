using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int _value;

    public int Value => _value;

    public event Action<int> Changed;

    public void Add(int value)
    {
        if (value <= 0)
            return;

        _value += value;
        Changed?.Invoke(_value);

        Debug.Log($"Player score: {_value}");
    }
}