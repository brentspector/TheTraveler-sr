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
		// Used when player fights an enemy
		public int CharacterFight<TEnemy>(Merchant player, bool turn, Die die) where TEnemy : Hostile
		{
			// Get the enemyID from the list of enemy IDs; since this a 1v1 fight there should only be a single ID
            int enemyID = GameMaster.Instance.EnemyIdentifiers[0];

            // Get the player's and enemy's entity
            TEnemy enemyEntity = (TEnemy)EntityManager.Instance.GetEntity(enemyID);

			// Determine if it's the player's turn, and get respective values
			int attack;
			int defense;
			if(turn)
			{
				attack = die.Roll(1, player.AttackPower);
				defense = die.Roll(1, enemyEntity.DefencePower);
			} //end if
			else
			{
				attack = die.Roll(1, enemyEntity.AttackPower);
				defense = die.Roll(1, player.AttackPower);
			} //end else

			// Calculate resulting damage
			int damage = attack - defense;
			if(damage < 0)
			{
				damage = 45;
			} //end if

			// Return damage
			return damage;
		} // end CharacterFight
	} // end Fight
} // end GSP
