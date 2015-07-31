/*******************************************************************************
 *
 *  File Name: EntityManager.cs
 *
 *  Description: Deals with all the entities
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Entities
{
    /*******************************************************************************
     *
     * Name: EntityManager
     * 
     * Description: Tracks and manages all entity references.
     * 
     *******************************************************************************/
    public sealed class EntityManager : System.IDisposable
	{
		static EntityMap entities;  // The list of entities currently added to the EntityManager

		static EntityGenerator entityGenerator; // The EntityGenerator reference

		// Creates one and only one instance of the EntityManager
		static readonly EntityManager instance = new EntityManager();

        // The private constructor for initialising the EntityManager
        EntityManager()
        {
            // Initialize the EntityMap
            entities = new EntityMap();

            // Create the EntityManager
            entityGenerator = new EntityGenerator();
        }

		// Destroys part of the EntityMap
        void EntityMapDestroyer(EntityData dataPair)
		{
            // Dispose of the entity
			dataPair.Second.Dispose();
		} // end EntityMapDestroyer

		// Returns the entity at the given ID
		public Entity GetEntity(int entityID)
		{
			// Try to get the value at the given key
			Entity ent;
			entities.TryGetValue (entityID, out ent);

			// Return the entity. If it wasn't found, null is returned
			return ent;
		} // end GetEntity

		// Returns a specific set of subentities of Entity
        public EntitySet<TSubEntity> GetEntities<TSubEntity>() where TSubEntity : Entity
		{
			// Declare the set that will hold the result
            EntitySet<TSubEntity> result = new EntitySet<TSubEntity>();

			// Loop over the the entities to find the entity type we are looking for
			foreach(var ent in entities)
			{
				// Get the entity
				Entity baseEntity = ent.Value;

				// Now try to cast it to the subclass we are looking for
                TSubEntity subEntity = baseEntity as TSubEntity;

				// Were we successful?
				if (subEntity != null)
				{
					// We were successful in the casting so add the entity to the result
					result.Add(subEntity);
				} // end if
			} // end foreach

			// Now return the result
			return result;
		} // end GetEntities

		// Adds an entity to the EntityManager
		public bool AddEntity(Entity entity)
		{
			// We don't want to add an entity with an ID that already exists
			var exists = entities.ContainsKey(entity.ID);

			// Does the given entity 's ID already exist?
			if (exists)
			{
				// The ID already exixts so log it and return failure
				Debug.LogWarningFormat("Entity of ID '{0}' already exists!", entity.ID);
				return false;
			} // end if

			// It doesn't exist so add the entity and return success
			entities.Add(entity.ID, entity);
			return true;
		} // end AddEntity

		// Removes an entity from the EntityManager
		public bool RemoveEntity(int entityID)
		{
			// Get the entity from the given ID
			Entity ent = GetEntity (entityID);

			// Does the entity at the given ID exist?
			if (ent == null)
			{
				// The entity doesn't exist so log it and return failure
				Debug.LogWarningFormat("Entity of ID '{0}' was not found!", entityID);
                return false;
			}

			// The enity exists so tell it to dispose of itself
			ent.Dispose();

			// Now remove the ntity from the list and return success
			entities.Remove(entityID);
			return true;
		} // end RemoveEntity

        // Getter for returning the instance of the EntityManager
        public static EntityManager Instance
        {
            get { return instance; }
        } // end Instance

        // Gets the instance of the EntityGenerator.
        public EntityGenerator Generator
        {
            get { return entityGenerator; }
        } // end Generator

        // Get all the entities.
        public EntityMap AllEntities
        {
            get
            { 
               // A temp EntityMap
                var tmp = entities;

                // Return the copy
                return tmp;
            } // end get
        } // end AllEntities

        // Gets the number of entities.
        public int NumEntities
        {
            get { return entities.Count; }
        } // end NumEntities

		#region IDisposable Members

		// Public dispose method that will call the internal dispose method
		public void Dispose()
		{
			// Dispose of the entity manager
			Dispose(true);

			// Now since we've done the cleanup already, there is nothing left
			// for tge finalizer to do. So tell the GC not to call it laters
			System.GC.SuppressFinalize(this);
		} // end Dispose

		// Internal dispose method. It executes in two distinct scenarios. 
		// If disposing equals true, the method has been called directly 
		// or indirectly by a user's code. Managed and unmanaged resources 
		// can be disposed. 
		// If disposing equals false, the method has been called by the 
		// runtime from inside the finalizer and you should not reference 
		// other objects. Only unmanaged resources can be disposed. 
		private void Dispose(bool disposing)
		{
			// Only proceed if we're disposing directly
			if (disposing)
			{
				// Loop through the entity map and dispose any entity object
				foreach (var entity in entities)
				{
					// Create an entity data pair to send to the destroyer
					EntityData data = new EntityData(entity.Key, entity.Value);
					// Destroy the pair
					EntityMapDestroyer(data);
				} // end foreach
				
				// Clear the map when finished destroying
				entities.Clear();
			} // end if
		} // end Dispose

		#endregion
	} // end EntityManager
} // end GSP.Entities