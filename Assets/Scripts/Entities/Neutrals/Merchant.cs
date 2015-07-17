using UnityEngine;
using System.Collections;

namespace GSP.Entities.Neutrals
{
	public class Merchant : Entity
	{
		public Merchant(int ID, GameObject gameObject, PlayerColours playerCoulours, string playerName) :
			base(ID, gameObject)
		{
			// Set the entity's type to merchant.
			Type = EntityType.ENT_MERCHANT;
		}
	}
}
