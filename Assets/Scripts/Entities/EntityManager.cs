using UnityEngine;
using System.Collections;

namespace GSP.Entities
{
	public sealed class EntityManager : System.IDisposable
	{
		static EntityMap m_entities;		// Holds the list of entities currently on the map.

		static EntityGenerator m_entGen;	// Holds the entity generator for the entity manager.

		#region Entity Manager Singleton

		// Creates one and only one instance of the entity manager.
		static readonly EntityManager m_instance = new EntityManager();
		// Getter for returning the instance of the entity manager.
		public static EntityManager Instance
		{
			get 
			{
				return m_instance;
			}
		}
		
		// The private constructor for initialising the entity manager.
		EntityManager()
		{
			// Initialize the entity map.
			m_entities = new EntityMap();

			// Create the entity manager.
			m_entGen = new EntityGenerator();
		}

		#endregion

		void EntityMapDestroyer(EntityData dataPair)
		{
            // Dispose of the entity.
			dataPair.Second.Dispose();
		}

		// Get's the instance of the entity generator.
		public EntityGenerator GetEntityGenerator()
		{
			// Return the entity generator.
			return m_entGen;
		}

		// Returns the entity at the given ID.
		public Entity GetEntity(int entityID)
		{
			// Try to get the value at the given key.
			Entity ent;
			m_entities.TryGetValue (entityID, out ent);

			// Return the entity. If it wasn't found, null is returned.
			return ent;
		}

		// Returns a specific set of subentities of Entity.
        public EntitySet<TSubEntity> GetEntities<TSubEntity>() where TSubEntity : Entity
		{
			// Declare the set that will hold the result
            EntitySet<TSubEntity> result = new EntitySet<TSubEntity>();

			// Loop over the the entities to find the entity type we are looking for.
			foreach(var ent in m_entities)
			{
				// Get the entity.
				Entity baseEntity = ent.Value;

				// Now try to cast it to the subclass we are looking for.
                TSubEntity subEntity = baseEntity as TSubEntity;

				// Were we successful?
				if (subEntity != null)
				{
					// We were successful in the casting so add the entity to the result.
					result.Add(subEntity);
				}
			}

			// Now return the result.
			return result;
		}

		// Get all the entities.
		public EntityMap GetAllEntities()
		{
			return m_entities;
		}

		// Adds an entity to the entity manager.
		public bool AddEntity(Entity entity)
		{
			// We don't want to add an entity with an ID that already exists.
			var exists = m_entities.ContainsKey(entity.ID);

			// Does the given entity 's ID already exist?
			if (exists)
			{
				// The ID already exixts so log it and return failure.
				Debug.LogErrorFormat("Entity of ID '{0}' already exists!", entity.ID);
				return false;
			}

			// It doesn't exist so add the entity and return success.
			m_entities.Add(entity.ID, entity);
			return true;
		}

		// Removes an entity from the entity manager.
		public bool RemoveEntity(int entityID)
		{
			// Get the entity from the given ID.
			Entity ent = GetEntity (entityID);

			// Does the entity at the given ID exist?
			if (ent == null)
			{
				// The entity doesn't exist so log it and return failure.
				Debug.LogErrorFormat("Entity of ID '{0}' was not found!", entityID);
                return false;
			}

			// The enity exists so tell it to dispose of itself.
			ent.Dispose();

			// Now remove the entity from the list and return success.
			m_entities.Remove(entityID);
			return true;
		}

		// Gets the number of entities.
		public int GetNumEntities()
		{
			return m_entities.Count;
		}

		#region IDisposable Members

		// Public dispose method that will call the internal dispose method.
		public void Dispose()
		{
			// Dispose of the entity manager.
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
				// Loop through the entity map and dispose any entity object.
				foreach (var entity in m_entities)
				{
					// Create an entity data pair to send to the destroyer.
					EntityData data = new EntityData(entity.Key, entity.Value);
					// Destroy the pair.
					EntityMapDestroyer(data);
				}
				
				// Clear the map when finished destroying.
				m_entities.Clear();
			}
		}

		#endregion
	}

}