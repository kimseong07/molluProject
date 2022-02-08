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

    [Header("Hp")]
    public Image Hpbar;
    public float lerpSpeed = 5;

    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        Hpbar.fillAmount = Mathf.Lerp(Hpbar.fillAmount, player.hp / player.maxHp, Time.deltaTime * lerpSpeed);
    }
}
