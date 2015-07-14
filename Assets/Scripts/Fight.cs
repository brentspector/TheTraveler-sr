using UnityEngine;
using System.Collections;
using GSP.Char;

namespace GSP
{
	public class Fight
	{
		//Used when player/ally character fights an enemy
		public string CharacterFight(GameObject enemy, GameObject player)
		{
			Character m_enemyScript = enemy.GetComponent<Character>();
			Character m_playerScript = player.GetComponent<Character>();
			int enemyDamage = m_enemyScript.AttackPower - m_playerScript.DefencePower;
			int playerDamage = m_playerScript.AttackPower - m_enemyScript.DefencePower;
			if(enemyDamage > playerDamage)
			{
				return "Player damage: " + playerDamage + 
					"\nEnemy damage: " + enemyDamage + "\nEnemy wins";
			} //end if
			else
			{
				return "Player damage: " + playerDamage + 
					"\nEnemy damage: " + enemyDamage + "\nPlayer wins";
			} //end else
		} //end CharacterFight
	} //end Fight class
} //end namespace GSP
