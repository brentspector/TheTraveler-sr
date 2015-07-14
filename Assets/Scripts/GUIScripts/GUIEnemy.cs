using UnityEngine;
using System.Collections;

namespace GSP.JAVIERGUI
{

	public class GUIEnemy : MonoBehaviour 
	{
		GSP.GUIMapEvents m_GUIMapEventsScript;
		GSP.MapEvent m_MapEventScript;
		GameObject m_PlayerEntity;
		GameObject audioSrc;

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
			m_MapEventScript = GameObject.FindGameObjectWithTag ("DieTag").GetComponent<GSP.MapEvent> ();
			audioSrc = GameObject.FindGameObjectWithTag( "AudioSourceTag" );
		}

		public void InitThis( GameObject p_PlayerEntity, int p_startX, int p_startY, int p_startWdth, int p_startHght, string p_result)
		{
			m_mainStartX = p_startX;
			m_mainStartY = p_startY;
			m_mainWidth = p_startWdth;
			m_mainHeight = p_startHght;

			m_PlayerEntity = p_PlayerEntity;

			m_isActionRunning = true;

			m_headerString = m_MapEventScript.ResolveFight(m_PlayerEntity); //"Needs to call a\nfunction in Fight!\nThat returns a string.";
		
			#region TODO:AddSound for fight here
			Die m_die = new Die();
			int roll = m_die.Roll(1, 3);
			if(roll == 1)
			{
				audioSrc.audio.PlayOneShot( GSP.AudioReference.sfxSwordHit1 ); //Play sword sound
			} //end if
			else if(roll == 2)
			{
				audioSrc.audio.PlayOneShot( GSP.AudioReference.sfxSwordHit2 ); //Play sword sound
			} //end else if
			else
			{
				audioSrc.audio.PlayOneShot( GSP.AudioReference.sfxSwordHit3 ); //Play sword sound
			} //end else
			#endregion
		}	//end public void InitThis(blah, blah, blah, blah)

		// Update is called once per frame
		void OnGUI () 
		{
			if ( m_isActionRunning )
			{
				//default button color
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

	}	//END public class GUIEnemy

}	//end namepsace GSP.JAVIERGUI