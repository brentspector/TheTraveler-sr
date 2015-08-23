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
		// Used when player targets an enemy's head
		public int CharacterFightHead<TEnemy>(Merchant player, bool turn, Die die) where TEnemy : Hostile
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
			int damage = attack - defense - (attack/4);
			if(damage < 0)
			{
				damage = 1;
			} //end if

			// Return damage
			return damage;
		} // end CharacterFightHead

		// Used when player targets an enemy's torso
		public int CharacterFightTorso<TEnemy>(Merchant player, bool turn, Die die) where TEnemy : Hostile
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
				damage = 5;
			} //end if
			
			// Return damage
			return damage;
		} // end CharacterFightTorso

		// Used when player tries to feint a blow at an enemy
		public int CharacterFightFeint<TEnemy>(Merchant player, bool turn, Die die) where TEnemy : Hostile
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
				int result = die.Roll(1, 2);
				if(result == 1)
				{
					attack = player.AttackPower * 2;
				} //end if
				else
				{
					attack = 0;
				} //end else
				defense = die.Roll(1, enemyEntity.DefencePower);
			} //end if
			else
			{
				int result = die.Roll(1, 2);
				if(result == 1)
				{
					attack = enemyEntity.AttackPower * 2;
				} //end if
				else
				{
					attack = 0;
				} //end else
				defense = die.Roll(1, player.DefencePower);
			} //end else
			
			// Calculate resulting damage
			int damage = attack - defense;
			if(damage < 0)
			{
				damage = 0;
			} //end if
			
			// Return damage
			return damage;
		} // end CharacterFightFeint
	} // end Fight
} // end GSP
