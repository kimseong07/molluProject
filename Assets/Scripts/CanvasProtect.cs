using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProtect : MonoBehaviour
{
    private void Awake()
    {

        DontDestroyOnLoad(this);
    }
}
