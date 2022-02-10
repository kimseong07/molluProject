using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    public static OptionScript Instance;
    private static OptionScript _instance { get { return Instance; } }

    [Header("소리")]
    public Slider backVol;
    public Slider sfxkVol;
    public Slider mainVol;
    public AudioSource audioSource;
    private float volume = 1f;

    [Header("해상도")]
    public Dropdown resDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    public int resNum;
    FullScreenMode screenMode;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SoundOption();
        InitUI();
    }

    private void Update()
    {
        LimitVolume(mainVol);
        LimitVolume(backVol);
        LimitVolume(sfxkVol);
    }

    public void SoundOption()
    {
        mainVol.onValueChanged.AddListener(delegate { SoundManager.Instance.MainVolume(mainVol.value); });
        backVol.onValueChanged.AddListener(delegate { SoundManager.Instance.BGMVolume(backVol.value); });
        sfxkVol.onValueChanged.AddListener(delegate { SoundManager.Instance.SFXVolume(sfxkVol.value); });
    }

    public void LimitVolume(Slider vol)
    {
        if(vol.value <= 0.00001)
        {
            vol.value = 0.00001f;
        }
    }

    public void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 144 )
            {
                resolutions.Add(Screen.resolutions[i]);
            }
            else if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }

        //resolutions.AddRange(Screen.resolutions);
        resDropdown.options.Clear();

        int optionNum = 0;

        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + " X " + item.height + " " + item.refreshRate + "hz";
            resDropdown.options.Add(option);

            if(item.width == Screen.width && item.height == Screen.height)
            {
                resDropdown.value = optionNum;
            }
            optionNum++;
        }

        resDropdown.RefreshShownValue();
        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void UIOptionChange(int x)
    {
        resNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OKBtnClick()
    {
        Screen.SetResolution(resolutions[resNum].width, resolutions[resNum].height, screenMode);
    }
}
