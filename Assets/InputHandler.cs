using System;
using UnityEngine;

[DisallowMultipleComponent]
public class InputHandler : MonoBehaviour
{
    private static InputHandler _inputHandler;

    public static InputHandler Input => _inputHandler ? _inputHandler : throw new Exception();

    private void Awake()
    {
        _inputHandler = this;
    }

    public static bool PreviousState => UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow);

    public static bool NextState => UnityEngine.Input.GetKeyDown(KeyCode.RightArrow);
}
