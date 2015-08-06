/*******************************************************************************
 *
 *  File Name: Entity.cs
 *
 *  Description: The base for the entity system
 *
 *******************************************************************************/
using GSP.Char;
using UnityEngine;

namespace GSP.Entities
{
    /*******************************************************************************
     *
     * Name: Entity
     * 
     * Description: The base class for all entity objects.
     * 
     *******************************************************************************/
    public abstract class Entity : System.IDisposable
	{
		int			entID;		// The Entity's ID
		Vector3		position;   // The Entity's position

		EntityType	type;		// The Entity's type
		GameObject	gameObj;    // The GameObject the Entity is attached to

        string      name;     // Holds the eneity's name

		// Constructor; Used by derived classes to create an object
		public Entity(int ID, GameObject gameObject)
		{
            // Set the type to none initially
            type = EntityType.None;
            
            // Set the other variables
            entID = ID;
			gameObj = gameObject;
		} // end Entity

        // Gets the ID of the Entity
        public int ID
        {
            get { return entID; }
        } // end ID
        
        // Gets and Sets the Entity's Position. (not always used)
		public Vector3 Position
		{
            get { return position; }
            set
            { 
                // Set the Entity's position
                position = value;

                // Set the Entity's GameObject's position to match the new position
                GameObj.transform.position = position;
            } // end set
		} // end Position

        // Gets and Protected Sets the Entity's Type
        public EntityType Type
        {
            get { return type; }
            protected set { type = value; }
        } // end Type

        // Gets the GameObject stored on the Entity
        public GameObject GameObj
        {
            get { return gameObj;}
        } // end GameObj

        // Gets and Sets the Name of the Entity
        public string Name
        {
            get { return name;}
            set { name = value;}
        } // end Name

		#region IDisposable Members
		
		// Public dispose method that will call the internal dispose method
		public void Dispose()
		{
            // Dispose the entity object
			Dispose(true);
			
			// Now since we've done the cleanup already, there is nothing left
			// for tge finalizer to do. So tell the GC not to call it later
			System.GC.SuppressFinalize(this);
		} // end Dispose
		
		// Internal dispose method. It executes in two distinct scenarios. 
		// If disposing equals true, the method has been called directly 
		// or indirectly by a user's code. Managed and unmanaged resources 
		// can be disposed
		// If disposing equals false, the method has been called by the 
		// runtime from inside the finalizer and you should not reference 
		// other objects. Only unmanaged resources can be disposed
		void Dispose(bool disposing)
		{
            // Only proceed if we're disposing directly
			if (disposing)
			{
                // Switch over the entity types
				switch (type)
				{
					case EntityType.Merchant:
					{
						// Get the Player script attached to the GameObject
						var script = GameObj.GetComponent<Player>();

						// Tell the script to destroy its GameObject
						script.DestroyGO();

						break;
					} // end case Merchant
					case EntityType.Porter:
					{
                        // Get the Ally of type Porter script attached to the GameObject
                        var script = GameObj.GetComponent<Ally<Friendlies.Porter>>();

                        // Tell the script to destroy its GameObject
						script.DestroyGO();
						
						break;
					} // end case Porter
					case EntityType.Mercenary:
					{
                        // Get the Ally of type Mercenary script attached to the GameObject
                        var script = GameObj.GetComponent<Ally<Friendlies.Mercenary>>();

                        // Tell the script to destroy its GameObject
                        script.DestroyGO();

                        break;
					} // end case Mercenary
					case EntityType.Bandit:
					{
                        // Get the Enemy of type Bandit script attached to the GameObject
                        var script = GameObj.GetComponent<Enemy<Hostiles.Bandit>>();

                        // Tell the script to destroy its GameObject
						script.DestroyGO();
						
						break;
					} // end case Bandit
					case EntityType.Mimic:
					{
                        // Get the Enemy of type Mimic script attached to the GameObject
                        var script = GameObj.GetComponent<Enemy<Hostiles.Mimic>>();

                        // Tell the script to destroy its GameObject
                        script.DestroyGO();

                        break;
					} // end case Mimic
                } // end switch type
			} // end it
		} // end Dispose
		
		#endregion
	} // end Entity
} // end GSP.Entities
