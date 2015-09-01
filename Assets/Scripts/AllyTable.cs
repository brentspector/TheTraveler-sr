﻿/*******************************************************************************
 *
 *  File Name: AllyTable.cs
 *
 *  Description: Contains the logic for displaying the allies.
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Core;
using UnityEngine;
using UnityEngine.UI;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: AllyTable
     * 
     * Description: Contains the logic for displaying the allies.
     * 
     *******************************************************************************/
    public class AllyTable : MonoBehaviour
    {
        Transform title;    // Used for setting the player's name
        Transform body;     // Used for setting the number of allies

        // Use this for initialisation
        void Awake()
        {
            // Get the reference to the title panel
            title = GameObject.Find("Canvas").transform.Find("Allies/Title");

            // Get the reference to the body panel
            body = GameObject.Find("Canvas").transform.Find("Allies/Body");
        } // end Awake

        // Sets the player's ally stats and colour
        public void SetPlayer(int playerNum)
        {
            // Get the player's script
            Player player = GameMaster.Instance.GetPlayerScript(playerNum);

            // Set the player's name
            title.GetChild(0).GetComponent<Text>().text = player.Name + "'s Allies";
            
            // Set the number of allies
            body.GetChild(1).GetComponent<Text>().text = player.NumAllies.ToString();
            
            // Set the interface colour to the player's colour
            GameObject.Find("Canvas").transform.Find("Allies").GetComponent<Image>().color =
                Utility.InterfaceColorToColor(GameMaster.Instance.GetPlayerColor(playerNum));
        } // end SetPlayer
    } // end AllyTable
} // end GSP
