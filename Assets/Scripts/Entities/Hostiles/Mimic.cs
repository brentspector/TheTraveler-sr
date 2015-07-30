using UnityEngine;
using System.Collections;
using GSP.Entities.Interfaces;

namespace GSP.Entities.Hostiles
{
	public class Mimic : Hostile
	{
        public Mimic(int ID, GameObject gameObject) : base(ID, gameObject)
		{
			// Set the entity's type to porter.
			Type = EntityType.ENT_MIMIC;
		}
    }
}
