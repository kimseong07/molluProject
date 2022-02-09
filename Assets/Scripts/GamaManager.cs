using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public bool isGameOver;

    public static GamaManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameSet()
    {
        isGameOver = false;
    }

    public void DeadCheck(float hp)
    {
        if (hp <= 0)
        {
            isGameOver = true;
        }
    }
}
