using UnityEngine;
using System.Collections;

namespace GSP.Cake
{
	// NOTE: This script should only be used on the easter egg scene!
	public class Cake : MonoBehaviour
	{
		GameObject m_cakeChart;			// Holds the cakechart object reference.
		Sprite[] m_cakeSprites;			// Holds the sprites for the cake chart.
		SpriteRenderer m_renderer;		// Holds the reference for the sprite renderer component.
		BoxCollider2D m_boxCollider;	// Holds the reference for the box collider 2d component.
		GameObject m_audioSource;		// Holds the audio source object reference.
		int m_slicesLeft;				// Holds the number of slices left.
		GameObject m_guiText;			// Holds the child object that holds the gui text.
		
		// Use this for initialization
		void Start()
		{
			// Load all the sprites for the cake chart.
			m_cakeSprites = Resources.LoadAll<Sprite>( "cake_spritesheet" );
			
			// Find the object with the cake chart tag.
			m_cakeChart = GameObject.FindGameObjectWithTag( "CakeChartTag" );

			// Add a sprite renderer to the cake chart.
			m_renderer = m_cakeChart.AddComponent<SpriteRenderer>();
			
			// Default the cake chart to full.
			m_renderer.sprite = m_cakeSprites[0];
			
			// Add a rigid body 2d to the cake chart.
			var rigidBody2D = m_cakeChart.AddComponent<Rigidbody2D>();
			// Turn off gravity.
			rigidBody2D.gravityScale = 0.0f;
			
			// Add a BoxCollider2D component to the player.
			m_boxCollider = m_cakeChart.AddComponent<BoxCollider2D>();

			// Get the audio source by finding the object with the audio source tag.
			m_audioSource = GameObject.FindGameObjectWithTag(  "AudioSourceTag" );

			// Set the slices left to five.
			m_slicesLeft = 5;

			// Find the gui text child.
			m_guiText = GameObject.Find( "CakeChart/CakeText" );

			m_guiText.GetComponent<GUIText>().text = "Have some Cake Chart! (Click to eat)";
		} // end Start function

		void OnMouseDown()
		{
			// Reduce the slices left by one.
			m_slicesLeft -= 1;

			// When clicked, we want to update things to progress to the next slice.
			if ( m_slicesLeft > 0 )
			{
				// Get the cake sprite index.
				int cakeIndex = 5 - m_slicesLeft;

				// Advance the sprite.
				m_renderer.sprite = m_cakeSprites[ cakeIndex ];

				// Next we need to ajust the collider.
				m_boxCollider.size = new Vector2( m_cakeSprites[ cakeIndex ].bounds.size.x, m_cakeSprites[ cakeIndex ].bounds.size.y );
			}// end if statement
			else
			{
				m_renderer.sprite = null;

				// There are no more slices of cake left so start the quit coroutine.
				StartCoroutine( "Quit" );
			} // end else statement.
		} // end OnMouseDown function

		// Coroutine for the ending.
		IEnumerator Quit()
		{
			m_guiText.GetComponent<GUIText>().text = "The universe is ending!";

			// Play an explosion sound.
			m_audioSource.GetComponent<AudioSource>().PlayOneShot( AudioReference.sfxExplosion );

			// Wait for a second.
			yield return new WaitForSeconds( 3.0f );

			// Load the menu scene.
			Application.LoadLevel( 0 );
		} // end Quit coroutine function
	} // end Cake class
} // end namespace