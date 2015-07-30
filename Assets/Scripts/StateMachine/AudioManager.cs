using UnityEngine;
using System.Collections;

namespace GSP
{
	public class AudioManager : MonoBehaviour 
	{
		public AudioSource sfxSource;					//Sound effects player
		public AudioSource musicSource;					//Background music player
		public AudioClip menuMusic;						//Main Menu music
		public AudioClip desertMusic;					//Desert map music
		public AudioClip snowMusic;						//Snow map music
		public AudioClip euroMusic;						//European map music
		public AudioClip metroMusic;					//Metropolis map music
		public AudioClip victoryMusic;					//Victory music
		public AudioClip drawMusic;						//Tie/Draw music
		public AudioClip lossMusic;						//Loss music
		public AudioClip coinSFX;						//Coin Jangling SFX
		public AudioClip walkingSFX;					//Walking noise SFX
		public AudioClip swordHit1SFX;					//One variation of a sword clash
		public AudioClip swordHit2SFX;					//Second variation of a sword clash
		public AudioClip swordHit3SFX;					//Third variation of a sword clash
		public AudioClip diceSFX;						//Rolling dice SFX
		public AudioClip fishingSFX;					//Fish catch SFX
		public AudioClip miningSFX;						//Ore mining SFX
		public AudioClip shearingSFX;					//Wool shearing SFX
		public AudioClip woodSFX;						//Woodcutting SFX
		public AudioClip explosionSFX;					//Cake chart explosion
		public static AudioManager instance = null;		//Singleton reference
		public float lowPitchRange = 0.95f;				//Low end of change spectrum
		public float highPitchRange = 1.05f;			//High end of change spectrum
		
		// Use this for initialization
		void Awake () 
		{
			//Set instance if one does not exist
			if(instance == null)
			{
				instance = this;

				//Play menu music
				playMenu();
			} //end if
			
			//If one does exist, and it does not equal this, destroy this new object
			else if (instance != this)
			{
				Destroy(gameObject);
			} //end else if
			
			//Protect SoundManager from destruction each restart
			DontDestroyOnLoad (gameObject);
		} //end Awake()

		//SFX clip functions
		//Sets a single passed clip to source clip and plays it
		public void playSingle (AudioClip clip)
		{
			sfxSource.pitch = Random.Range (lowPitchRange, highPitchRange);
			sfxSource.clip = clip;
			sfxSource.Play ();
		} //end playSingle (AudioClip clip)
		
		//Picks one clip out of parameter and plays it as the source clip
		public void randomizeSFX(params AudioClip [] clips)
		{
			//Select random clip
			int randomIndex = Random.Range (0, clips.Length);
			
			//Select random pitch so sounds don't get old
			float randomPitch = Random.Range (lowPitchRange, highPitchRange);
			
			//Apply and play clip
			sfxSource.pitch = randomPitch;
			sfxSource.clip = clips [randomIndex];
			sfxSource.Play ();
		} //end randomizeSFX (params AudioClip [] clips)

		//Background music functions
		public void playMenu()
		{
			musicSource.Stop ();
			musicSource.clip = menuMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playMenu

		public void playDesert()
		{
			musicSource.Stop ();
			musicSource.clip = desertMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playDesert

		public void playSnow()
		{
			musicSource.Stop ();
			musicSource.clip = snowMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playSnow

		public void playEuro()
		{
			musicSource.Stop ();
			musicSource.clip = euroMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playEuro

		public void playMetro()
		{
			musicSource.Stop ();
			musicSource.clip = metroMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playMetro

		public void playVictory()
		{
			musicSource.Stop ();
			musicSource.clip = victoryMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playDesert

		public void playDraw()
		{
			musicSource.Stop ();
			musicSource.clip = drawMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playDraw

		public void playLoss()
		{
			musicSource.Stop ();
			musicSource.clip = lossMusic;
			musicSource.PlayDelayed (1.0f);
		} //end playLoss

		public void PlayCoin()
		{
			PlaySingle (coinSFX);
		} //end PlayCoin()

		public void PlayWalk()
		{
			PlaySingle (walkingSFX);
		} //end PlayWalk()

		public void PlaySword()
		{
			RandomizeSFX (swordHit1SFX, swordHit2SFX, swordHit3SFX);
		} //end PlaySword()

		public void PlayDie()
		{
			PlaySingle (diceSFX);
		} //end PlayDie()

		public void PlayFish()
		{
			PlaySingle (fishingSFX);
		} //end PlayFish()

		public void PlayMine()
		{
			PlaySingle (miningSFX);
		} //end PlayMine()

		public void PlayShear()
		{
			PlaySingle (shearingSFX);
		} //end PlayShear()

		public void PlayWood()
		{
			PlaySingle (woodSFX);
		} //end PlayWood()

		public void PlayExplosion()
		{
			PlaySingle (explosionSFX);
		} //end PlayExplosion()
	} //end AudioManager class
} //end namespace GSP