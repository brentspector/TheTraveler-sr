/*******************************************************************************
 *
 *  File Name: RankTable.cs
 *
 *  Description: Contains the logic for the Ranks Table for multiplayer.
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Neutrals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: RankTable
     * 
     * Description: Contains the logic for the Ranks Table for multiplayer.
     * 
     *******************************************************************************/
    public class RankTable : MonoBehaviour
    {
        Dictionary<int, int> playerCurrencies;      // The player's currency dictionary
        List<KeyValuePair<int, int>> currencies;    // The sorted list of players; It's sorted by place aka 1st place etc

        Transform body; // The panel at RankingTable/Body
        
        // Use this for initializasion
        void Start()
        {
            // Initialises the player currency dictionary
            playerCurrencies = new Dictionary<int, int>();
            
            // Get the reference to the body panel
            body = GameObject.Find("Canvas").transform.Find("RankingTable/Body").transform;
        } // end Start

        public void DisplayRanks(int numPlayers)
        {
            // Get the sorted list
            DetermineWinner(numPlayers);

            // Set the interface colour to the winning player's colour
            GameObject.Find("Canvas").transform.Find("RankingTable").GetComponent<Image>().color = 
                Utility.InterfaceColorToColor(GameMaster.Instance.GetPlayerColor(currencies[0].Key));
            
            // Loop over the table to set the values
            for (int index = 0; index < numPlayers; index++)
            {
                // Get the child at index + 1
                Transform entry = body.GetChild(index + 1);

                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(currencies[index].Key).Entity;

                // Get the children of the entry and their text components and set their text to the entry values
                entry.GetChild(0).GetComponent<Text>().text = (index + 1).ToString();
                entry.GetChild(1).GetComponent<Text>().text = GameMaster.Instance.GetPlayerName(currencies[index].Key);
                entry.GetChild(2).GetComponent<Text>().text = playerMerchant.Currency.ToString();
            } // end for
        } // end DisplayRanks

        // Determine who the winner is and fill in the sorted list
        void DetermineWinner(int numPlayers)
        {
            // Loop over the number of players
            for (int index = 0; index < numPlayers; index++)
            {
                // Get the player's merchant entity
                Merchant playerMerchant = (Merchant)GameMaster.Instance.GetPlayerScript(index).Entity;

                // Only proceed if the player's currency hasn't been added
                if (!playerCurrencies.ContainsKey(index))
                {
                    // Add the player's number as the key and their currency as the value to the dictionary
                    playerCurrencies.Add(index, playerMerchant.Currency);
                } // end if
            } // end for

            // Now we sort the currency dictionary
            IEnumerable<KeyValuePair<int, int>> sortedCurrencies = from entry in playerCurrencies orderby entry.Value descending select entry;

            // Create a list from this ordering
            currencies = sortedCurrencies.ToList();
        } // end DetermineWinner
    } // end RankTable
} // end GSP
