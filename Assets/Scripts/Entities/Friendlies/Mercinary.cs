using UnityEngine;
using System.Collections;

namespace GSP.Entities.Friendlies
{
	public class Mercinary : Friendly
	{
		public Mercinary(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter.
			Type = EntityType.ENT_MERCINARY;
		}
	}
}
