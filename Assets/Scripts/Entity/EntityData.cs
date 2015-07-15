using UnityEngine;
using System.Collections;
using GSP;

namespace GSP.Entity
{
	/// <summary>
	/// Provides the functionality of the pair with a more readable name.
	/// </summary>
	public class EntityData : Pair<int, Entity>
	{
		public EntityData()
		{
			this.First = -1;
			this.Second = null;
		}

		public EntityData(int id, Entity ent)
		{
			this.First = id;
			this.Second = ent;
		}
	}
}
