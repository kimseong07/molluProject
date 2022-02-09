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
        SelectStage("StageData" + StageManager.Instance.nowStage.ToString());
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
    public void SelectStage(string name)
    {
        GameObject stage = Resources.Load<GameObject>(name);
        Instantiate(stage, new Vector3(0,0,0), Quaternion.identity);
    }
}
