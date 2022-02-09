using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("ÆÐ³Î")]
    public GameObject pausePanel;
    public GameObject optionPanel;
    public GameObject gameOverPanel;
    private bool isPause = false;
    private bool isOption = false;

    [Header("Hp")]
    public Image Hpbar;
    public float lerpSpeed = 5;

    public Transform stageInstParent;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        PauseGame();
    }

    public void flowHp(float hp, float maxHp)
    {
        Hpbar.fillAmount = Mathf.Lerp(Hpbar.fillAmount, hp / maxHp, Time.deltaTime * lerpSpeed);
    }

    public void PauseGame()
    {
        if(!isOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause)
                {
                    Time.timeScale = 0;

                    boolCanvasGroup(pausePanel, 1, true);
                    isPause = true;
                    return;
                }

                if (isPause)
                {
                    Time.timeScale = 1;

                    boolCanvasGroup(pausePanel, 0, false);
                    isPause = false;
                    return;
                }
            }
        }
        else if(isOption)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CloseOptionPan();
            }
        }
    }

    public void OnOptionPan()
    {
        boolCanvasGroup(optionPanel, 1, true);
        isOption = true;
    }

    public void CloseOptionPan()
    {
        boolCanvasGroup(optionPanel, 0, false);
        isOption = false;
    }

    void boolCanvasGroup(GameObject Panel, float alp,bool check)
    {
        Panel.GetComponent<CanvasGroup>().alpha = alp;
        Panel.GetComponent<CanvasGroup>().interactable = check;
        Panel.GetComponent<CanvasGroup>().blocksRaycasts = check;
    }

    public void SelectStage(string name)
    {
        GameObject stage = Resources.Load<GameObject>(name);
        Instantiate(stage, stageInstParent.position, stageInstParent.rotation);
    }
}
