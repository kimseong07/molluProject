using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
	public static StageManager Instance;

	[SerializeField] private int stageCount;
	public int nowStage;
	[SerializeField] private GameObject parentSelectBtn;
	[SerializeField] private GameObject btnPrefab;

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

		parentSelectBtn = GameObject.Find("Stages");
		btnPrefab = Resources.Load("StageSelcetBtn") as GameObject;
		CreateStageSelcet();
	}
	public void StageSelcetBtn(int idx)
	{
		Instance.nowStage = idx;
		Time.timeScale = 1;
		print(idx + "," + nowStage);

		SceneManager.LoadScene("Stage");
	}
	private void CreateStageSelcet()
	{
		for (int i = 0; i < stageCount; i++)
		{
			int idx = i + 1;
			GameObject btnObj = Instantiate(btnPrefab, parentSelectBtn.transform);
			btnObj.GetComponentInChildren<Text>().text = idx.ToString();
			btnObj.GetComponent<Button>().onClick.AddListener(() => StageSelcetBtn(idx));
		}
	}
	public void ReStartScene()
	{
		print(SceneManager.GetActiveScene().name);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
