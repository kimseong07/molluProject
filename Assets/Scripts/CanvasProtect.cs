using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasProtect : MonoBehaviour
{
    public static CanvasProtect Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance);
    }
}
