using UnityEngine;
using System.Collections;

namespace GSP
{
	public static class AudioReference
	{
		/*
		 * Audio References
		 */
		
		// This is the reference to the coins clip.
		public static AudioClip sfxCoins = Resources.Load<AudioClip>( "GSP362_TeamA_Character_CoinClashing" );

		// This is the reference to the walking clip.
		public static AudioClip sfxWalking = Resources.Load<AudioClip>( "GSP362_TeamA_Character_Walking" );

		// This is the reference to the first sword hit clip.
		public static AudioClip sfxSwordHit1 = Resources.Load<AudioClip>( "GSP362_TeamA_Fight_SwordHit1" );

		// This is the reference to the second sword hit clip.
		public static AudioClip sfxSwordHit2 = Resources.Load<AudioClip>( "GSP362_TeamA_Fight_SwordHit2" );

		// This is the reference to the third sword hit clip.
		public static AudioClip sfxSwordHit3 = Resources.Load<AudioClip>( "GSP362_TeamA_Fight_SwordHit3" );

		// This is the reference to the rolling dice clip.
		public static AudioClip sfxDice = Resources.Load<AudioClip>( "GSP362_TeamA_Overall_RollingDice" );

		// This is the reference to the fishing resource clip.
		public static AudioClip sfxFishing = Resources.Load<AudioClip>( "GSP362_TeamA_Resource_Fishing" );

		// This is the reference to the mining resource clip.
		public static AudioClip sfxMining = Resources.Load<AudioClip>( "GSP362_TeamA_Resource_Mining" );

		// This is the reference to the shearing resource clip.
		public static AudioClip sfxShearing = Resources.Load<AudioClip>( "GSP362_TeamA_Resource_Shearing" );

		// This is the reference to the woodcutting resource clip.
		public static AudioClip sfxWoodcutting = Resources.Load<AudioClip>( "GSP362_TeamA_Resource_Woodcutting" );

		// This is the reference to the explosion clip.
		public static AudioClip sfxExplosion = Resources.Load<AudioClip>( "GSP362_TeamA_Secret_Explosion" );
	} // end PrefabReference class
} // end namespace

