using UnityEngine;
using System.Collections;
using GSP.Core;
using GSP.Items.Inventories;

namespace GSP
{
    public class TestMarket : MonoBehaviour
    {
        // Used for initialisation
        void Awake()
        {
            // Load the players
            GameMaster.Instance.LoadPlayers(true);

            // Set the colours
            GameObject.Find("Canvas").transform.Find("Inventory").GetComponent<Inventory>().SetPlayer(GameMaster.Instance.Turn);
            GameObject.Find("Canvas").transform.Find("Market").GetComponent<Market>().SetPlayer(GameMaster.Instance.Turn);
        } // end Awake

        public void LeaveMarket()
        {
            // Save the players
            GameMaster.Instance.SavePlayers();

            // Load the level we was on before
            GameMaster.Instance.LoadLevel(GameMaster.Instance.BattleMap.ToString());
        } // end LeaveMarket
    }
}
