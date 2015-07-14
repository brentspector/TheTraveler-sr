using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour
{
	// Holds the audio source game object.
	GameObject audioSrc;

	// Use this for initialisation.
	void Start()
	{
		// Initialise the variables here.
		audioSrc = GameObject.FindGameObjectWithTag( "AudioSourceTag" );
	}
	
	// Update is called once per frame
	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Alpha0 ) )
		{
			// Play the sound effect as a 1-shot sound.
			audioSrc.audio.PlayOneShot( GSP.AudioReference.sfxCoins );
		}
	}
}
