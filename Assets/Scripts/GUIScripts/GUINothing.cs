using UnityEngine;
using System.Collections;


namespace GSP.JAVIERGUI
{
	public class GUINothing : MonoBehaviour 
	{

		GSP.GUIMapEvents m_GUIMapEventsScript;

		//main container values
		int m_mainWidth = -1;
		int	m_mainHeight = -1;
		int m_mainStartX = -1;
		int m_mainStartY = -1;
		
		bool m_isActionRunning = false;
		string m_headerString;
		
		// Use this for initialization
		void Start () {
			m_GUIMapEventsScript = GameObject.FindGameObjectWithTag ("GUIMapEventSpriteTag").GetComponent<GSP.GUIMapEvents> ();
		}
		
		public void InitThis( int p_startX, int p_startY, int p_startWdth, int p_startHght, string p_result)
		{
			m_mainStartX = p_startX;
			m_mainStartY = p_startY;
			m_mainWidth = p_startWdth;
			m_mainHeight = p_startHght;
			
			m_isActionRunning = true;
			
			m_headerString = "No map event.\nClick done to\nend your turn."; //"Needs to call a\nfunction in Fight!\nThat returns a string.";
		}
		
		// Update is called once per frame
		void OnGUI () 
		{
			if ( m_isActionRunning )
			{
				//DEFAULT color
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
				//once nothing is happening, program returns to Controller's End Turn State
				m_GUIMapEventsScript.MapeEventDone();
			}
		}
	}	//end class GUINothing : MonoBehaviour
}	//end namespace GSP.JAVIERGUI