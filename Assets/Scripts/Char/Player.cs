using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	public class Player : MonoBehaviour
	{
		//

		// Destroy the game object this script is attached to.
		public void DestroyGO()
		{
			Destroy(this.gameObject);
		}
	}
}
