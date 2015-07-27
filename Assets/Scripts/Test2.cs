using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Alpha0 ) )
		{
			// Play the sound effect as a 1-shot sound.
			GSP.AudioManager.instance.playSingle(GSP.AudioReference.sfxCoins);
		}
	}
}
