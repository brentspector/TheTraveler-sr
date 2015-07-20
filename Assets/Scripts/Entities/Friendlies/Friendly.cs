using UnityEngine;
using System.Collections;

namespace GSP.Entities.Friendlies
{
	public class Friendly : Entity
	{
		public Friendly(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Empty for now.

			// Hosts the ally's specifc common code. That is code that is specific to ally's but common among them.
		}
	}
}
