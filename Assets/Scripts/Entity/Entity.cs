using UnityEngine;
using System.Collections;

namespace GSP.Entity
{
	public class Entity : MonoBehaviour, System.IDisposable
	{

		// Use this for initialisation
		void Start()
		{
			//
		}
		
		// Update is called once per frame
		void Update()
		{
			//
		}

		#region IDisposable Members
		
		// Public dispose method that will call the internal dispose method.
		public void Dispose()
		{
			// Dispose the entity object.
			Dispose(true);
			
			// Now since we've done the cleanup already, there is nothing left
			// for tge finalizer to do. So tell the GC not to call it later.
			System.GC.SuppressFinalize(this);
		}
		
		// Internal dispose method. It executes in two distinct scenarios. 
		// If disposing equals true, the method has been called directly 
		// or indirectly by a user's code. Managed and unmanaged resources 
		// can be disposed. 
		// If disposing equals false, the method has been called by the 
		// runtime from inside the finalizer and you should not reference 
		// other objects. Only unmanaged resources can be disposed. 
		private void Dispose(bool disposing)
		{
			// Only proceed if we're disposing directly.
			if (disposing)
			{
				// TODO: Destroy the entity's game object.
			}
		}
		
		#endregion
	}
}
