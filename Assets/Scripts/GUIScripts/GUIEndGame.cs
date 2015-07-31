/*******************************************************************************
 *
 *  File Name: GUIEndGane.cs
 *
 *  Description: Old GUI for the end game screen
 *
 *******************************************************************************/
using GSP;
using GSP.Char;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GSP.JAVIERGUI
{
    //TODO: Damien: Replace with the GameMaster functionality later.
    //TODO: Brent: Replace this with the new In-Game UI later; probably not in the same namespace
    /*******************************************************************************
     *
     * Name: GUIEndGame
     * 
     * Description: Creates the GUI for the end game screen.
     * 
     *******************************************************************************/
    public class GUIEndGame : MonoBehaviour 
	{
		// textures
		public Texture2D backgroundTexture;    // The background's texture
		
		
		// scripts
		EndSceneData endSceneDataScript;            // The EndSceneData script reference
		Misc miscScript;                            // The misc script reference; used for sorting the winner
		EndSceneCharData endSceneCharDataObject;    // The EndSceneCharData script reference

        // main container values//65 is just below the main GUI, and I added a gap of 32 from the end of that
        int mainStartX  = -1;   // The starting x value
        int mainStartY  = -1;   // The starting y value
        int mainWidth   = -1;   // The starting width value
        int mainHeight  = -1;   // The starting height value
		int sectionsInY	= -1;   // The number of sections in the y-axis
		
		bool isActionRunning = false;   // Whether the action is running
        string headerString;            // The text in the OnGUI UI; the winner
		string m_bodyString;            // The text in the OnGUI UI; the other places


        //TODO: Damien: Replace with the GameMaster functionality later.
        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Use this for initialization
		void Start() 
		{
			// scripts
			endSceneDataScript = this.GetComponent<GSP.EndSceneData>();
			miscScript = this.GetComponent<GSP.Misc>(); 
			
			ScaleValues();

            endSceneCharDataObject = endSceneDataScript.GetData(miscScript.DetermineWinner());

            headerString = "Player " + (endSceneCharDataObject.PlayerNumber).ToString() + " is the Winner!\n"
                + "Player " + (endSceneCharDataObject.PlayerNumber).ToString() + " collected " + (endSceneCharDataObject.PlayerCurrency).ToString() + " Gold.";
			AudioManager.Instance.PlayVictory();

            List<KeyValuePair<int, int>> playerList = miscScript.GetList();
            for (int index = 1; index <= (endSceneDataScript.Count - 1); index++)
			{
				endSceneCharDataObject = endSceneDataScript.GetData(playerList[index].Key);
                m_bodyString = m_bodyString + "[" + (index + 1).ToString() + " Place] "
                    + "Player " + (endSceneCharDataObject.PlayerNumber).ToString() + " collected " + (endSceneCharDataObject.PlayerCurrency).ToString() + " Gold.\n";
			} // end for
			
			isActionRunning = true;
		} // end Start

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.D))
			{
				AudioManager.Instance.PlayDraw();
			} // end if

			if(Input.GetKeyDown(KeyCode.L))
			{
				AudioManager.Instance.PlayLoss();
			} // end if
		} // end Update

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // The old style GUI functioned using an OnGUI function; Runs each frame
		void OnGUI()
		{
			ScaleValues();

            if (isActionRunning) 
			{
				// background
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
				
				// default Color
				GUI.backgroundColor = Color.red;
				
				ConfigHeader();
				ConfigBody();
				ConfigOKButton();
			} // end if
		} // end OnGUI

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Scales the values to fit with the OnGui UI system
        void ScaleValues()
		{
            endSceneDataScript = this.GetComponent<GSP.EndSceneData>();

            mainWidth = Screen.width / 2;
            mainHeight = Screen.height / 1;
            mainStartX = (Screen.width / 2) - (mainWidth / 2);
            mainStartY = (Screen.height / 2) - ((mainHeight / 2)); 			// 65 is just below the main GUI, and I added a gap of 32 from the end of that
            sectionsInY = 7;
		} // end ScaleValues

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the header of the OnGUI UI system
        void ConfigHeader()
		{
            int headWdth = mainWidth;
            int headHght = (mainHeight / sectionsInY) * 1;
            int headX = mainStartX;
            int headY = mainStartY + (((mainHeight / sectionsInY) * 1));
			
			// winner
			GUI.Box(new Rect(headX, headY, headWdth, headHght), headerString);
			
		} // end ConfigHeader

        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the body of the OnGUI UI system
        void ConfigBody()
		{
            int bodyWdth = mainWidth;
            int bodyHght = (mainHeight * (5 / 2)) / sectionsInY;
            int bodyX = mainStartX;
            int bodyY = mainStartY + ((mainHeight / sectionsInY) * 2);

            GUI.Box(new Rect(bodyX, bodyY, bodyWdth, bodyHght * 2), m_bodyString);
		} // end ConfigBody


        //TODO: Brent: Replace OnGUI stuff with the new In-Game UI later
        // Configures the Ok button of the OnGUI UI system
        void ConfigOKButton()
		{
			// done button
            int doneWidth = mainWidth / 2;
            int doneHeight = mainHeight / 8;
            int doneStartX = mainStartX + (mainWidth - doneWidth) / 2;
            int doneStartY = mainStartY + (doneHeight * 5);
            GUI.backgroundColor = Color.red;

            if (GUI.Button(new Rect(doneStartX, doneStartY, doneWidth, doneHeight), "OK"))
			{
				isActionRunning = false;
				Destroy(AudioManager.Instance.gameObject);
				Application.LoadLevel(0);
			} // end if
		} // end ConfigOKButton
		
		// Get whether the action is running
        public bool IsActionRunning
		{
            get { return isActionRunning; }
		} // end IsActionRunning
	} // end GUIEndGame
} //end GUI.JAVIERGUI