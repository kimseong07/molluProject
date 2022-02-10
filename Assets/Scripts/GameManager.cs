using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    public bool isGoal;

    public float deadTime;
    
    public static GameManager Instance;
    private Vector2 spawnPoint;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
	private void Start()
	{
        SelectStage("StageDatas/StageData" + StageManager.Instance.nowStage.ToString());

    }

    public void SelectStage(string name)
    {
        GameObject stage = Resources.Load<GameObject>(name);
        Instantiate(stage, new Vector3(0,0,0), Quaternion.identity);
    }
}
