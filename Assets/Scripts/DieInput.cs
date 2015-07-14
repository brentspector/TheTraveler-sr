using System.Collections;
using UnityEngine;

namespace GSP
{
	public class DieInput : MonoBehaviour
	{
		// The private die reference
		private Die m_dice;

		// Holds the die face references.
		public Sprite[] m_dieFaces;

		// Gets the Die object.
		public Die Dice
		{
			get { return m_dice; }
		}

		// Use this for initialization
		void Start()
		{
			// Create a die object.
			m_dice = new Die();
		}
		
		// Update is called once per frame
		void Update()
		{
			// Check if the space key is pressed to roll the di(c)e.
			if ( Input.GetKeyDown( KeyCode.Space ) )
			{
				// Get the die roll.
				var dieRoll = Dice.Roll();

				// Get the sprite renderer component.
				var spriteRenderer = GetComponent<SpriteRenderer>();

				// Set the die's sprite to the roll.
				spriteRenderer.sprite = m_dieFaces[ dieRoll - 1 ];

				// Print the roll to the console.
				print( "Die Roll: " +  dieRoll );
			}
		}
	}
}
