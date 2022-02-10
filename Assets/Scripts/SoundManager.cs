using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		Instance = this;
		DontDestroyOnLoad(Instance);
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SoundPlay(string soundName, AudioClip clip)
    {
		GameObject go = new GameObject(soundName + "Sound");
		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.Play();

		Destroy(go, clip.length);
    }
}
