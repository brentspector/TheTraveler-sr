/*******************************************************************************
 *
 *  File Name: Fight.cs
 *
 *  Description: Simulates a simple fight between player and enemy
 *
 *******************************************************************************/
using GSP.Char;
using GSP.Core;
using GSP.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace GSP
{
    /*******************************************************************************
     *
     * Name: Fight
     * 
     * Description: The simple fight class for the players battling enemies.
     * 
     *******************************************************************************/
    public class Fight
	{
		// Used when player character fights an enemy
		public string CharacterFight(GameObject enemy, GameObject player)
		{
			// Get the enemyID from the list of enemy IDs; since this a 1v1 fight there should only be a single ID
            int enemyID = GameMaster.Instance.EnemyIdentifiers[0];

            // Get the enemy's type
            var enemyType = EntityManager.Instance.GetEntity(enemyID).Type;

            // Switch over the enemyType
            switch (enemyType)
            {
                case EntityType.Bandit:
                    {
                        break;
                    } // end case Bandit
                case EntityType.Mimic:
                    {
                        break;
                    } // end case Mimic
            } // end enemyType

            // Get the enemy script from the enemy GameObject
            var enemyS = "";
            
            // Get the Character scripts for both the enemy and player
            Character enemyScript = enemy.GetComponent<Character>();
			Character playerScript = player.GetComponent<Character>();

            // Get the AttackPower of both the enemy and player
			int enemyDamage = Clamp(enemyScript.AttackPower - playerScript.DefencePower);
			int playerDamage = Clamp(playerScript.AttackPower - enemyScript.DefencePower);

            // Check if the enemy does more damage than the player
			if(enemyDamage > playerDamage)
			{
				// It does so return that the enemy wins
                return "Player damage: " + playerDamage + 
					"\nEnemy damage: " + enemyDamage + "\nEnemy wins";
			} // end if
			else
			{
				// Otherwise, return that the player wins
                return "Player damage: " + playerDamage + 
					"\nEnemy damage: " + enemyDamage + "\nPlayer wins";
			} // end else
		} // end CharacterFight

        // Clamps the values to zero and above
        int Clamp(int damage)
        {
            // Check if the damage is less than zero
            if (damage < 0)
            {
                // Clamp to zero
                return 0;
            }
            else
            {
                // Otherwise, return the damage
                return damage;
            }
        } // end Clamp
	} // end Fight
} // end GSP
