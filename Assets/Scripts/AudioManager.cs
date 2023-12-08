using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// Singleton instance to ensure only one AudioManager exists in the scene
	public static AudioManager instance;

	// Array of Sound objects containing audio settings and clips
	public Sound[] sounds;

	void Start ()
	{
		// Singleton pattern: check and handle existing instances
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		// Initialize audio sources for each sound
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	// Play a sound by its name
	public void Play (string sound)
	{
		// Find the Sound object and play its AudioClip
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Play();
	}

	// Stop playing a sound by its name
	public void Stop(string sound)
	{
		// Find the Sound object and stop its AudioClip
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Stop();
	}
}
