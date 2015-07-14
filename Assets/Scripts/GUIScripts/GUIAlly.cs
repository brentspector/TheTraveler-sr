using UnityEngine;
using System.Collections;

namespace GSP.JAVIERGUI
{
	public class GUIAlly : MonoBehaviour 
		////////////////////////////////////////////////////////////////////////
		//	Creates the GUI for ALLY MapEvent
		//		on Start()		
		//			+gets informati4on from Char
		//			+ask to add Ally
		//				yes: addAlly, then displayResults and doneButton
		//				no: display results and doneButton
		//
		//		//on DoneButton()
		//			exit()	
		//
		//	Steps:
		//		1. GetPlayer Values
		//		2. Would you like to add Ally?
		//		3. Yes, increase player's MAXWEIGHT
		//		4. Show results and display done button
		////////////////////////////////////////////////////////////////////////
	{
		GameObject m_PlayerEntity; 							//will initialize to the actual player in InitThis.
		//GSP.Char.Ally m_PlayerAllyScript;					//used during testing
		//GSP.Char.Character m_PlayerCharacterScript;		//used during testing
		GSP.GUIMapEvents m_GUIMapEventsScript;
		GSP.MapEvent m_MapEventScript;
		GSP.JAVIERGUI.GUIBottomBar m_GUIBottomBarScript;
		
		const int m_AllyHelpMAXWEIGHTIncrease =150;
		string m_headerString;
		string m_resultsString;

//		int m_playerWeight = -1;
//		int m_playerMaxWeight = -1;

		int m_mainStartX 	= -1;
		int m_mainStartY 	= -1;
		int m_mainWidth 	= -1;
		int m_mainHeight	= -1;

		bool m_selectionMadeAddRemove = false;	//for internal use, determines if player w to add Ally or not
		bool m_isActionRunning = false;


		// Use this for initialization
		void Start () {
			m_GUIBottomBarScript = GameObject.FindGameObjectWithTag("GamePlayStateMachineTag").GetComponent<GSP.JAVIERGUI.GUIBottomBar>();

		}	//end Start()


		public void InitGUIAlly(GameObject p_PlayerEntity, int p_startX, int p_startY, int p_startWidth, int p_startHeight)
		{
			m_PlayerEntity = p_PlayerEntity;
//			m_PlayerAllyScript = m_PlayerEntity.GetComponent<GSP.Char.Ally>();	//-->			//used during testing
			//m_PlayerCharacterScript = m_PlayerEntity.GetComponent<GSP.Char.Character>();
			m_GUIMapEventsScript = GameObject.FindGameObjectWithTag("GUIMapEventSpriteTag").GetComponent<GSP.GUIMapEvents>();
			m_MapEventScript = GameObject.FindGameObjectWithTag ("DieTag").GetComponent<GSP.MapEvent>();

			m_isActionRunning = true;

			//GUIMapEvents values transferred over
			m_mainStartX = p_startX;
			m_mainStartY = p_startY;
			m_mainWidth = p_startWidth;
			m_mainHeight = p_startHeight;
		}


		private void getPlayerAllyValues()		//TODO: if all the ally calculations get done in mapevent, this is no longer needed.
		{
//			m_playerMaxWeight = m_PlayerCharacterScript.MaxWeight;
//			m_playerWeight = m_PlayerCharacterScript.ResourceWeight;
		}	//end private void getPlayerAllyValues()


		void OnGUI()
		{
			GUI.backgroundColor = Color.red;
			if( m_isActionRunning == true )
			{
				if(m_selectionMadeAddRemove == false)
				{
					ConfigHeader ();
					ConfigAddButton ();
					ConfigCancelButton ();
				}
				else
				{
					ConfigHeader();
					ConfigDoneButton();
				}
			}
		}	//end void OnGUI()

		private void ConfigHeader()
		{
			if( m_selectionMadeAddRemove == false )
			{
				m_headerString = "Would You Like\nto Add An Ally?";
			}

			int headWdth = m_mainWidth - 2;
			int headHght = m_mainHeight / 6;
			int headX = m_mainStartX + ((m_mainWidth -headWdth) /2);
			int headY = m_mainStartY + (headHght*2);

			GUI.Box(new Rect(headX, headY, headWdth, headHght*2), m_headerString);
		}	//end private void ConfigHeader()

		private void ConfigAddButton()
		{
			int newWdth = m_mainWidth / 5;
			int newHght = m_mainHeight / 6;
			int newX = m_mainStartX + (newWdth*1);
			int newY = m_mainStartY + (newHght*4);
			
			if( GUI.Button(new Rect(newX, newY, newWdth, newHght*2), "Yes") )
			{
				#region Add Ally Sound
				m_GUIBottomBarScript.AnimateAllyButton();
				//TODO:	Ally is added here
				#endregion

				m_headerString = m_MapEventScript.ResolveAlly(m_PlayerEntity, "YES");  //"New Ally Added.\nNew Max Weight is "+m_playerMaxWeight.ToString();

				m_selectionMadeAddRemove = true;
			}
		}	// end 	private void ConfigAddButton()

		private void ConfigCancelButton()
		{
			int newWdth = m_mainWidth / 5;
			int newHght = m_mainHeight / 6;
			int newX = m_mainStartX + (newWdth*3);
			int newY = m_mainStartY + (newHght*4);
			
			if( GUI.Button(new Rect(newX, newY, newWdth, newHght*2), "No") )
			{
				//m_headerString = "Ally was not Added";
				m_headerString = m_MapEventScript.ResolveAlly(m_PlayerEntity, "NO");

				m_selectionMadeAddRemove = true;
			}
		}	//end 	private void ConfigCancelButton()
		

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
				//stop animation
				m_GUIBottomBarScript.StopAnimation();

				m_isActionRunning = false;
				m_selectionMadeAddRemove = false;
				//once nothing is happening, program returns to Controller's End Turn State
				m_GUIMapEventsScript.MapeEventDone();
			}
		}	//end private void ConfigDoneButton()
	} //end public class GUIAlly
} //end namespace GSP.JAVIERGUI
