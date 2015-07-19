using UnityEngine;
using System.Collections;

namespace GSP.Entities.Friendlies
{
	public class Porter : Friendly
	{
		public Porter(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter.
			Type = EntityType.ENT_PORTER;
		}
	}
}
