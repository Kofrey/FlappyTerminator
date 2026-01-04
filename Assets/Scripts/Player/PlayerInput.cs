using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public const KeyCode ShootKey = KeyCode.LeftControl;
    public const KeyCode LeapKey = KeyCode.Space;

    public event Action<KeyCode> InputPerformed;

    private void Update()
    {
        if (Input.GetKeyDown(ShootKey))
        {
            InputPerformed?.Invoke(ShootKey);
        }

        if (Input.GetKeyDown(LeapKey))
        {
            InputPerformed?.Invoke(LeapKey);
        }
    }
}
