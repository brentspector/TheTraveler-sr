using UnityEngine;
using System.Collections;

namespace GSP.Entities.Hostiles
{
	public class Hostile : Entity
	{
		public Hostile(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Empty for now.

			// Hosts the enemy's specifc common code. That is code that is specific to enemy's but common among them.
		}
	}
}
