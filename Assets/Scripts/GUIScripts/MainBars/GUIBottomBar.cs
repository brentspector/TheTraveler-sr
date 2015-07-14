using UnityEngine;
using System.Collections;


namespace GSP.JAVIERGUI
{

	public class GUIBottomBar : MonoBehaviour 
	{
		//...Scripts...
		GSP.Char.Character m_CharacterScript;

		//....Bottom Bar Configuration values....
		private int m_gapInYdirection;
		private int m_gapInXdirection;
		//private int m_numOfButtonsInXDirection;
		//private int m_numOfButtonsInYdirection;
		private int m_buttonHeight;
		private int m_buttonWidth;
		private int m_barStartX;
		private int m_barStartY;


		//....Feedback Values....
		private double m_animTimer = 0.0;
		private bool m_runAnimation = false;

		private bool m_viewItems = false;
		private bool m_viewAllies = false;

		private bool m_runItemAnim = false;
		private bool m_runAllyAnim = false;

		// Use this for initialization
		void Awake () {
			ReScaleValues ();
		}


		// Use this for initialization
		void Start () {

		}


		public void RefreshBottomBarGUI( GameObject p_playerEntity )
		{
			m_CharacterScript = p_playerEntity.GetComponent<GSP.Char.Character>();
			
		}	//end void RefreshBottomBarGUI()


		public void ReScaleValues()
		{
			m_gapInYdirection = 1;
			m_gapInXdirection = 2;
			//m_numOfButtonsInXDirection = 1;
			//m_numOfButtonsInYdirection = 2;
			m_buttonHeight = 32;
			m_buttonWidth = 64;
			m_barStartX = (m_buttonWidth/4);
			m_barStartY = ( Screen.height - (m_buttonHeight*1) );
		}	//end public void ReScaleValues()


		void OnGUI()
		{
			//Rescale valus
			ReScaleValues ();
			//default button color
			GUI.backgroundColor = Color.red;

			int col;
			int row;

			//.............
			//   Row 0
			//.............
			row = 0;
				//,,,,,,,,,,,,
				//   Col 0
				//,,,,,,,,,,,,
			col = 0;
			ConfigItemButton( (m_barStartX +(col*m_gapInXdirection)), (m_barStartY -(row*m_gapInYdirection)), m_buttonWidth, m_buttonHeight );
				//,,,,,,,,,,,,
				//	Col 1
				//,,,,,,,,,,,,
			col = col +1;
			ConfigAllyButton( (m_barStartX +(col*m_buttonWidth +m_gapInXdirection)), (m_barStartY -(row*m_buttonHeight +m_gapInYdirection)), m_buttonWidth, m_buttonHeight );


			//.............
			//	Row 1
			//.............
			row = row +1;
				//,,,,,,,,,,,,
				//	Col 0
				//,,,,,,,,,,,,
			col = 0;
			ConfigItemBarDisplay( (m_barStartX +(col*m_buttonWidth +m_gapInXdirection)), (m_barStartY -(row*m_buttonHeight +m_gapInYdirection)), m_buttonWidth, m_buttonHeight );
			ConfigAllyBarDisplay( (m_barStartX +(col*m_buttonWidth +m_gapInXdirection)), (m_barStartY -(row*m_buttonHeight +m_gapInYdirection)), (2*m_buttonWidth), m_buttonHeight);

		}	//end OnGUI()



		private void ConfigItemButton( int p_x, int p_y, int p_width, int p_height )
		{
			if( m_runAnimation == false)
			{
				PickButtonColor (m_viewItems);
			}
			else
			{
				AnimTimer( m_runItemAnim );
			}

	
			if( GUI.Button( new Rect(p_x, p_y, p_width, p_height), "ITEMS") )
			{
				//stop animation
				m_runItemAnim = false;
				m_runAnimation = false;

				//set view values
				if( m_viewItems == false )
				{
					m_viewAllies = false;
					m_viewItems = true;
				}
				else
				{
					m_viewItems = false;
				}
			}
		}	//end private void ConfigItemButton()



		private void ConfigItemBarDisplay( int p_x, int p_y, int p_width, int p_height )
		{
			if( m_viewItems == true )
			{
				int row = 0;
				int col = 0;
				string resultString = (m_CharacterScript.AttackPower).ToString();
				GUI.Box(new Rect (p_x +(col *p_width), p_y +(row *p_height), p_width, p_height), "AP: " +resultString );

				col = col+1;
				resultString = (m_CharacterScript.DefencePower).ToString();
				GUI.Box(new Rect (p_x +(col *p_width), p_y +(row *p_height), p_width, p_height), "DP: " +resultString );

				col = col+1;
				resultString = ( m_CharacterScript.ResourceValue.ToString() +"/" +m_CharacterScript.MaxInventory.ToString() ) ;
				GUI.Box(new Rect (p_x +(col *p_width), p_y +(row *p_height), p_width, p_height), "DP: " +resultString );
			}
		}	//edn private void ConfigItemBarDisplay


		private void ConfigAllyButton( int p_x, int p_y, int p_width, int p_height )
		{
			if( m_runAnimation == false)
			{
				PickButtonColor (m_viewAllies );
			}
			else
			{
				AnimTimer( m_runAllyAnim );
			}


			if( GUI.Button( new Rect(p_x, p_y, p_width, p_height), "ALLIES") )
			{
				//stop animation
				m_runAllyAnim = false;
				m_runAnimation = false;

				//set view values
				if( m_viewAllies == false )
				{
					m_viewItems = false;
					m_viewAllies = true;
				}
				else
				{
					m_viewAllies = false;
				}
			}

		}	//end private void ConfigAllyButton( int p_x, int p_y, int p_width, int p_height )


		private void ConfigAllyBarDisplay( int p_x, int p_y, int p_width, int p_height )
		{
			if(m_viewAllies == true)
			{
				int row = 0;
				int col = 0;
				string resultString = (m_CharacterScript.NumAllies).ToString();
				GUI.Box(new Rect (p_x +(col *p_width), p_y +(row *p_height), p_width, p_height), "# of Allies: " +resultString );
			}

		}	//private void ConfigAllyBarDisplay( int p_x, int p_y, int p_width, int p_height )


		private void PickButtonColor(bool p_isSelected)
		{
			if (p_isSelected == true) 
			{
				GUI.backgroundColor = Color.yellow;
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
		}


		public void AnimateAllyButton()
		{
			m_runAllyAnim = true;
			m_runAnimation = true;
		}	//public void AnimateAllyButton()


		public void AnimateItemButton()
		{
			m_runItemAnim = true;
			m_runAnimation = true;
		}	//public void AnimateAllyButton()


		private void AnimTimer(bool p_isAnimating)
		{
			m_animTimer = m_animTimer +Time.deltaTime;
			if(m_animTimer > 2.0)
			{
				m_animTimer = 0.0;
			}
			
			if(p_isAnimating == true)
			{
				if (m_animTimer < 1.0f)
				{
					GUI.backgroundColor = Color.red;
				}
				else
				{
					GUI.backgroundColor = Color.yellow;
				}	
			}
			else
			{
				GUI.backgroundColor = Color.red;
			}
		}	//end private void animTimer()


		public void StopAnimation()
		{
			m_runAllyAnim = false;
			m_runItemAnim = false;
			m_runAnimation = false;

		}	//end public void StopAnimation()

	}	//end public class GUIBottomBar : MonoBehaviour

}	//end namespace GSP.JAVIERGUI