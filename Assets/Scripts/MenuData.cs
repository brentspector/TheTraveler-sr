using UnityEngine;
using System.Collections;

namespace GSP
{
	public class MenuData : MonoBehaviour
	{
		// Holds the number of players for the game.
		int m_numberPlayers;
		
		public int NumberPlayers
		{
			get { return m_numberPlayers; }
			set
			{
				m_numberPlayers = value;
				
				// Check if the maximum inventory is less than zero.
				if (m_numberPlayers < 0)
				{
					// Clamp the max inventory to zero.
					m_numberPlayers = 0;
				} // end if statement
			} // end Set accessor
		} // end NumberPlayers property
		
		// Use this for initialisation.
		void Start()
		{
			// Initialise the variables to zero.
			m_numberPlayers = 0;
		} // end Start function
	} // end MenuData class
}
