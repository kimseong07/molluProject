using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;

    public bool isMoving;

    public AudioSource bgm;

    public AudioClip[] bgmList;

    public static SoundManager Instance;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void SoundPlay(string soundName, AudioClip clip)
    {
		GameObject go = new GameObject(soundName + "Sound");
		AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BgSound(AudioClip clip)
    {
        if (bgm != null)
        {
            bgm.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
            bgm.clip = clip;
            bgm.loop = true;
            bgm.volume = 1f;
            bgm.Play();
        }
    }

	public void MoveSound(GameObject player)
    {
        AudioSource audios = player.GetComponent<AudioSource>();

        if(isMoving)
        {
            if(!audios.isPlaying)
            {
                audios.Play();
            }    
        }
        else
        {
            audios.Stop();
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            if(arg0.name == bgmList[i].name)
            {
                BgSound(bgmList[i]);
            }
        }
    }

    public void BGMVolume(float val)
    {
        mixer.SetFloat("BGMvolume", Mathf.Log10(val) * 20);
    }
    public void SFXVolume(float val)
    {
        mixer.SetFloat("SFXvolume", Mathf.Log10(val) * 20);
    }
    public void MainVolume(float val)
    {
        mixer.SetFloat("MAINvolume", Mathf.Log10(val) * 20);
    }
}
