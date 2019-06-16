using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public static InputController Instance {
        get {
            return instance;
        }
    }
    private static InputController instance;
    public void Awake() {
        if (instance == null)
            instance = this;
    }
    public VariableJoystick moveInput;
    public VariableJoystick actionInput;
}
