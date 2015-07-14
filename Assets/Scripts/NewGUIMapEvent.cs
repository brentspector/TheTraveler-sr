using UnityEngine;
using System.Collections;
using GSP.Tiles;

namespace GSP.JAVIERGUI
{
	public class NewGUIMapEvent : MonoBehaviour 
	{
		GSP.MapEvent m_MapEventsScript;
		GameObject m_PlayerEntity;

		//main container values
		int m_mainStartX = 0;
		int m_mainStartY = 65 + 32; //65 is just below the main GUI, and I added a gap of 32 from the end of that
		int m_mainWidth = Screen.width / 3;
		int m_mainHeight = Screen.height / 2;
		
		bool m_isActionRunning = false;
		string m_headerString;
		
		// Use this for initialization
		void Start () 
		{
			m_MapEventsScript = GameObject.FindGameObjectWithTag ("DieTag").GetComponent<GSP.MapEvent> ();
		}
		
		public void InitThis( GameObject p_PlayerEntity )
		{

			m_PlayerEntity = p_PlayerEntity;
			
			m_isActionRunning = true;

			m_headerString = m_MapEventsScript.DetermineEvent (m_PlayerEntity);
		}	//end init this()
		
		// Update is called once per frame
		void OnGUI () 
		{
			if ( m_isActionRunning )
			{
				//default color
				GUI.backgroundColor = Color.red;
				
				ConfigHeader();
				ConfigDoneButton();
			}
		}	//end void OnGUI()
		
		
		private void ConfigHeader ()
		{
			int headWdth = m_mainWidth - 2;
			int headHght = m_mainHeight / 5;
			int headX = m_mainStartX + ((m_mainWidth -headWdth) /2);
			int headY = m_mainStartY + (headHght*2);
			
			GUI.Box(new Rect(headX, headY, headWdth, headHght*2), m_headerString);
		}	//end private void ConfigHeader()
		
		private void ConfigDoneButton()
		{
			//done button
			int doneWidth = m_mainWidth/2;
			int doneHeight = m_mainHeight / 8;
			int doneStartX = m_mainStartX +(m_mainWidth -doneWidth) /2;
			int doneStartY = m_mainStartY +(doneHeight *7);
			GUI.backgroundColor = Color.red;
			
			if ( GUI.Button (new Rect( doneStartX, doneStartY, doneWidth, doneHeight), "DONE") )
			{
				m_isActionRunning = false;
			}
		} //end private void ConfigDoneButton()

		public bool GetIsActionRunning()
		{
			return m_isActionRunning;
		}
		
	}	//END public class GUIEnemy
} //end namespace GSP.JAVIERGUI