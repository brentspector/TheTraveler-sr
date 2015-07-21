using UnityEngine;
using System.Collections;

namespace GSP.Entities.Hostiles
{
	public class Bandit : Hostile
	{
		public Bandit(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter.
			Type = EntityType.ENT_BANDIT;
		}
	}
}
