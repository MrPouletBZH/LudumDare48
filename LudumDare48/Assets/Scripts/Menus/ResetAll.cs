using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAll : MonoBehaviour
{

    [SerializeField] private InputActionAsset inputActions;

    public void Reset(){
        foreach(InputActionMap map in inputActions.actionMaps)
            map.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey("rebinds");
    }
}