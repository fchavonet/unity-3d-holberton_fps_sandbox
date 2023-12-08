using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {

	// Name identifier for the sound
	public string name;

	// Audio clip to be played for the sound
	public AudioClip clip;

	// Volume level of the sound (0 to 1)
	[Range(0f, 1f)]
	public float volume;

	// Pitch of the sound (-3 to 3)
	[Range(-3f, 3f)]
	public float pitch;

	// Determines if the sound should loop
	public bool loop = false;

	// Reference to the AudioSource component associated with the sound
	[HideInInspector]
	public AudioSource source;
}
