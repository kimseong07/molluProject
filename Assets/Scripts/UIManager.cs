using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("?г?")]
    public GameObject inGamePanel;
    public GameObject pausePanel;
    public GameObject optionPanel;
    public GameObject clearPanel;

    [Header("??ư")]
    public Button selectBtn;
    public Button nextBtn;
    public Button exitBtn;

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
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(Instance);
        selectBtn.onClick.AddListener(() => {
            boolCanvasGroup(clearPanel, 0, false);
            Time.timeScale = 1;
            SceneManager.LoadScene("StageSelect");
            GameManager.Instance.isGoal = false;
        });
        nextBtn.onClick.AddListener(() => {
            boolCanvasGroup(clearPanel, 0, false);
            Time.timeScale = 1;
            StageManager.Instance.StageSelcetBtn(StageManager.Instance.nowStage + 1);
            GameManager.Instance.isGoal = false;
        });
        exitBtn.onClick.AddListener(() =>
        {
            ExitBtnClick();
        });


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        PauseGame();
    }

    public void flowHp(float hp, float maxHp)
    {
        if(Hpbar != null)
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

    public void boolCanvasGroup(GameObject Panel, float alp,bool check)
    {
        if (Panel != null)
        {
            Panel.GetComponent<CanvasGroup>().alpha = alp;
            Panel.GetComponent<CanvasGroup>().interactable = check;
            Panel.GetComponent<CanvasGroup>().blocksRaycasts = check;
        }
    }

    void OnSceneLoaded(Scene arg, LoadSceneMode arg1)
    {
        if(arg.name == "Stage")
        {
            boolCanvasGroup(inGamePanel, 1, true);


        }
        else
        {
            //exitBtn.onClick.AddListener(() =>
            //{
            //    SceneManager.LoadScene("StageSelect");
            //    Time.timeScale = 1;

            //    boolCanvasGroup(pausePanel, 0, false);
            //    isPause = false;
            //    return;

            //});
            boolCanvasGroup(inGamePanel, 0, false);
        }
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }
}
