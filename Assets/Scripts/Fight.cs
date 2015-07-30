/*******************************************************************************
 *
 *  File Name: Fight.cs
 *
 *  Description: Simulates a simple fight between player and enemy
 *
 *******************************************************************************/
using GSP.Char;
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
