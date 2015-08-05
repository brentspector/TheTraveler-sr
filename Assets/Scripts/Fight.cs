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
using GSP.Entities.Hostiles;
using GSP.Entities.Neutrals;

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
		// Used when player fights an enemy
		public string CharacterFight<TEnemy>(Player player) where TEnemy : Hostile
		{
			// Get the enemyID from the list of enemy IDs; since this a 1v1 fight there should only be a single ID
            int enemyID = GameMaster.Instance.EnemyIdentifiers[0];

            // Get the player's and enemy's entity
            TEnemy enemyEntity = (TEnemy)EntityManager.Instance.GetEntity(enemyID);
            Merchant playerEntity = (Merchant)player.Entity;

            // Get the AttackPower of both the enemy and player
			int enemyDamage = Utility.ZeroClampInt(enemyEntity.AttackPower - playerEntity.DefencePower);
            int playerDamage = Utility.ZeroClampInt(playerEntity.AttackPower - enemyEntity.DefencePower);

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
	} // end Fight
} // end GSP
