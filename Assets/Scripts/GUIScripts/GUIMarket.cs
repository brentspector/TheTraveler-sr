using UnityEngine;
using System.Collections;
using GSP.Core;
using GSP.Items.Inventories;

namespace GSP
{
    public class GUIMarket : MonoBehaviour
    {
        // Used for initialisation
        void Awake()
        {
            // Load the players
            GameMaster.Instance.LoadPlayers(true);

            // Disable the ally's inventory
            GameObject.Find("Canvas").transform.Find("AllyInventory").gameObject.SetActive(false);

            // Set the colours
            GameObject.Find("Canvas").transform.Find("PlayerInventory").GetComponent<PlayerInventory>().SetPlayer(GameMaster.Instance.Turn);
            GameObject.Find("Canvas").transform.Find("Market").GetComponent<Market>().SetPlayer(GameMaster.Instance.Turn);
        } // end Awake

        public void LeaveMarket()
        {
            // enable the ally's inventory
            GameObject.Find("Canvas").transform.Find("AllyInventory").gameObject.SetActive(true);
            
            // Save the players
            GameMaster.Instance.SavePlayers();

            // Go to the next turn
            GameMaster.Instance.NextTurn();

            // Load the level we was on before
            GameMaster.Instance.LoadLevel(GameMaster.Instance.BattleMap.ToString());
        } // end LeaveMarket
    }
}
