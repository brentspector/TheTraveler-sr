using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP;


namespace GSP.JAVIERGUI
{
	public class GUIEndGame : MonoBehaviour 
	{
		//textures
		public Texture2D m_bgtex;
		
		
		//scripts
		GSP.EndSceneData m_EndSceneDataScript;
		GSP.Misc	m_MiscScript;
		GSP.Char.EndSceneCharData m_EndSceneCharDataObject;
		
		//main container values
		int m_mainStartX 	= -1;
		int m_mainStartY 	= -1; 			//65 is just below the main GUI, and I added a gap of 32 from the end of that
		int m_mainWidth 	= -1;
		int m_mainHeight	= -1;
		int m_sectionsInY	= -1;
		
		bool m_isActionRunning = false;
		string m_headerString;
		string m_bodyString;
		
		
		// Use this for initialization
		void Start () 
		{
			//scripts
			m_EndSceneDataScript 	 = this.GetComponent<GSP.EndSceneData>();
			m_MiscScript = this.GetComponent<GSP.Misc>(); 
			
			ScaleValues();
			
			m_EndSceneCharDataObject = m_EndSceneDataScript.GetData( m_MiscScript.DetermineWinner() );
			
			m_headerString = "";
			m_headerString = "Player " +(m_EndSceneCharDataObject.PlayerNumber).ToString() +" is the Winner!\n" 
				+"Player " +(m_EndSceneCharDataObject.PlayerNumber).ToString() +" collected " +(m_EndSceneCharDataObject.PlayerCurrency).ToString() +" Gold.";
			AudioManager.instance.playVictory ();

			List<KeyValuePair<int, int>> playerList = m_MiscScript.GetList();
			m_bodyString = "";
			for( int i =1; i <= (m_EndSceneDataScript.Count -1); i++)
			{
				m_EndSceneCharDataObject = m_EndSceneDataScript.GetData(playerList[i].Key);
				m_bodyString = m_bodyString +"[" +(i+1).ToString() +" Place] "
					+"Player " +(m_EndSceneCharDataObject.PlayerNumber).ToString() +" collected " +(m_EndSceneCharDataObject.PlayerCurrency).ToString() +" Gold.\n";
			}
			
			m_isActionRunning = true;
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.D))
			{
				AudioManager.instance.playDraw();
			} //end if

			if(Input.GetKeyDown(KeyCode.L))
			{
				AudioManager.instance.playLoss();
			} //end if

		}
		
		// Update is called once per frame
		void OnGUI () 
		{
			ScaleValues();

			if ( m_isActionRunning )
			{
				//background
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), m_bgtex);
				
				//default Color
				GUI.backgroundColor = Color.red;
				
				ConfigHeader();
				ConfigBody();
				ConfigOKButton();
			}
		}	//end void OnGUI()
		
		
		private void ScaleValues()
		{
			m_EndSceneDataScript 	 = this.GetComponent<GSP.EndSceneData>();
			
			m_mainWidth  = Screen.width / 2;
			m_mainHeight = Screen.height / 1;
			m_mainStartX = (Screen.width/2) -(m_mainWidth/2);
			m_mainStartY = (Screen.height/2) -((m_mainHeight/2)); 			//65 is just below the main GUI, and I added a gap of 32 from the end of that
			m_sectionsInY = 7;
		}	//end private void ScaleValues()
		
		
		private void ConfigHeader ()
		{
			int headWdth = m_mainWidth;
			int headHght = (m_mainHeight/m_sectionsInY) *1;
			int headX = m_mainStartX;
			int headY = m_mainStartY +( ((m_mainHeight/m_sectionsInY)*1) );		// -((m_mainHeight/m_sectionsInY)/2));
			
			//winner
			GUI.Box(new Rect(headX, headY, headWdth, headHght), m_headerString);
			
		}	//end private void ConfigHeader()
		
		
		private void ConfigBody()
		{
			int bodyWdth = m_mainWidth;
			int bodyHght = (m_mainHeight*(5/2))/m_sectionsInY;
			int bodyX = m_mainStartX;
			int bodyY = m_mainStartY + ((m_mainHeight / m_sectionsInY) * 2);	// -((m_mainHeight/m_sectionsInY)/2);
			
			GUI.Box(new Rect(bodyX, bodyY, bodyWdth, bodyHght*2), m_bodyString);
			
		}	//end private void ConfigBody()
		
		
		private void ConfigOKButton()
		{
			//done button
			int doneWidth = m_mainWidth/2;
			int doneHeight = m_mainHeight / 8;
			int doneStartX = m_mainStartX +(m_mainWidth -doneWidth) /2;
			int doneStartY = m_mainStartY +(doneHeight *5);
			GUI.backgroundColor = Color.red;
			
			if ( GUI.Button (new Rect( doneStartX, doneStartY, doneWidth, doneHeight), "OK") )
			{
				m_isActionRunning = false;
				Destroy(AudioManager.instance.gameObject);
				Application.LoadLevel(0);
			}
		} //end private void ConfigDoneButton()
		
		
		public bool GetIsActionRunning()
		{
			return m_isActionRunning;
		}
		
		
	}	//end GUIEndGame
}	//end namespace GUI.JAVIERGUI