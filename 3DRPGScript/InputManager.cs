using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public InputActionAsset inputAsset;

    public bool Attack1
    {
        get 
        {
            return inputAsset["Attack1"].WasPerformedThisFrame();
        }
    }

    public bool Attack2
    {
        get
        {
            return inputAsset["Attack2"].WasPerformedThisFrame();
        }
    }

    public bool Jump
    {
        get
        {
            return inputAsset["Jump"].WasPerformedThisFrame();
        }
    }

    public bool Interactive
    {
        get
        {
            return inputAsset["Interactive"].WasPerformedThisFrame();
        }
    }

    public Vector2 Move
    {
        get
        {
            //Debug.Log(inputAsset["Move"].ReadValue<Vector2>());
            return inputAsset["Move"].ReadValue<Vector2>();
            
        }
    }

    public Vector2 Camera
    {
        get
        {
            return inputAsset["Camera"].ReadValue<Vector2>();
        }
    }

    void Awake()
    {
        Instance = this;
        inputAsset.Enable();
    }
}
