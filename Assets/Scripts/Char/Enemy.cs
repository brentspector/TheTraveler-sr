using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	public class Enemy : MonoBehaviour
	{
		//
		
		// Destroy the game object this script is attached to.
		public void DestroyGO()
		{
			Destroy(this.gameObject);
		}
	}
}
