/*******************************************************************************
 *
 *  File Name: EntityGenerator.cs
 *
 *  Description: Gemerates entities for the EntityManager
 *
 *******************************************************************************/
using GSP.Core;
using GSP.Entities.Friendlies;
using GSP.Entities.Hostiles;
using GSP.Entities.Neutrals;
using UnityEngine;

namespace GSP.Entities
{
    /*******************************************************************************
     *
     * Name: EntityGenerator
     * 
     * Description: Creates entities and adds them to the EntityManager.
     * 
     *******************************************************************************/
    public class EntityGenerator
	{
		static int m_entID; // The next ID used for giving each entity a unique ID upon their creation

		// Constructor; creates a new object
        public EntityGenerator()
		{
			// Initialise the ID to zero
			m_entID = 0;
		} // end EntityGenerator

		public bool CreateEntity(out int entID, EntityType type, GameObject gameObject, int playerNum = 0)
		{
			// The result of creation
			bool result = false;

			// Create the proper type of entity based upon the given type
			switch (type)
			{
				case EntityType.Merchant:
				    {
					    // Create the entity
                        Merchant merchant = new Merchant(m_entID, gameObject, GameMaster.Instance.GetPlayerColor(playerNum), GameMaster.Instance.GetPlayerName(playerNum));
					
					    // Now try to add the entity to the manager
					    if (!EntityManager.Instance.AddEntity(merchant))
					    {
						    // The entity couldn't be added to the manager so return failure
						    result = false;
					    }

					    // Return success
					    result = true;
					
					    break;
				    } // end case Merchant
				case EntityType.Porter:
				    {
					    // Create the entity
					    Porter porter = new Porter(m_entID, gameObject);
					
					    // Now try to add the entity to the manager
					    if (!EntityManager.Instance.AddEntity(porter))
					    {
						    // The entity coulnd't be added to the manager so return failure
						    result = false;
					    }
					
					    // Return success
					    result = true;

					    break;
				    } // end case Porter
				case EntityType.Mercenary:
				    {
					    // Create the entity
					    Mercenary mercenary = new Mercenary(m_entID, gameObject);
					
					    // Now try to add the entity to the manager
					    if (!EntityManager.Instance.AddEntity(mercenary))
					    {
						    // The entity coulnd't be added to the manager so return failure
						    result = false;
					    }
					
					    // Return success
					    result = true;
					
					    break;
				    } // end Mercenary
				case EntityType.Bandit:
				    {
					    // Create the entity
					    Bandit bandit = new Bandit(m_entID, gameObject);
					
					    // Now try to add the entity to the manager
					    if (!EntityManager.Instance.AddEntity(bandit))
					    {
						    // The entity coulnd't be added to the manager so return failure
						    result = false;
					    }
					
					    // Return success
					    result = true;
					
					    break;
				    } // end case Bandit
				case EntityType.Mimic:
                    {
					    // Create the entity
					    Mimic mimic = new Mimic(m_entID, gameObject);
					
					    // Now try to add the entity to the manager
					    if (!EntityManager.Instance.AddEntity(mimic))
					    {
						    // The entity coulnd't be added to the manager so return failure
						    result = false;
					    }
					
					    // Return success
					    result = true;
					
					    break;
                    } // end Mimic
            } // end switch type

            // Set the out ID variable
            entID = m_entID;

			// Increment to the next ID
			m_entID++;

			// Return the result
			return result;
		} // end CreateEntity
	} // end EntityGenerator
} // end GSP.Entities
