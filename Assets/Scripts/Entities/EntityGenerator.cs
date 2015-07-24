using UnityEngine;
using System.Collections;
using GSP;
using GSP.Char;
using GSP.Entities.Friendlies;
using GSP.Entities.Hostiles;
using GSP.Entities.Neutrals;
using GSP.Core;

namespace GSP.Entities
{
	public class EntityGenerator
	{
		// Holds the next ID used for giving each entity a unique ID upon their creation.
		static int m_entID;

		public EntityGenerator()
		{
			// Initialise the ID to zero.
			m_entID = 0;
		}

		public bool CreateEntity(out int entID, EntityType type, GameObject gameObject, int playerNum = 0)
		{
			// Holds the result of creation.
			bool result = false;

			// Create the proper type of entity based upon the given type.
			switch (type)
			{
				case EntityType.ENT_MERCHANT:
				{
					// Create the entity.
                    Merchant merchant = new Merchant(m_entID, gameObject, GameMaster.Instance.GetPlayerColour(playerNum), GameMaster.Instance.GetPlayerName(playerNum));
					
					// Now try to add the entity to the manager.
					if (!EntityManager.Instance.AddEntity(merchant))
					{
						// The entity couldn't be added to the manager so return failure.
						result = false;
					}

					// Return success.
					result = true;
					
					break;
				}
				case EntityType.ENT_PORTER:
				{
					// Create the entity.
					Porter porter = new Porter(m_entID, gameObject);
					
					// Now try to add the entity to the manager.
					if (!EntityManager.Instance.AddEntity(porter))
					{
						// The entity coulnd't be added to the manager so return failure.
						result = false;
					}
					
					// Return success.
					result = true;

					break;
				}
				case EntityType.ENT_MERCENARY:
				{
					// Create the entity.
					Mercenary mercenary = new Mercenary(m_entID, gameObject);
					
					// Now try to add the entity to the manager.
					if (!EntityManager.Instance.AddEntity(mercenary))
					{
						// The entity coulnd't be added to the manager so return failure.
						result = false;
					}
					
					// Return success.
					result = true;
					
					break;
				}
				case EntityType.ENT_BANDIT:
				{
					// Create the entity.
					Bandit bandit = new Bandit(m_entID, gameObject);
					
					// Now try to add the entity to the manager.
					if (!EntityManager.Instance.AddEntity(bandit))
					{
						// The entity coulnd't be added to the manager so return failure.
						result = false;
					}
					
					// Return success.
					result = true;
					
					break;
				}
				case EntityType.ENT_MIMIC:
				{
					// Create the entity.
					Mimic mimic = new Mimic(m_entID, gameObject);
					
					// Now try to add the entity to the manager.
					if (!EntityManager.Instance.AddEntity(mimic))
					{
						// The entity coulnd't be added to the manager so return failure.
						result = false;
					}
					
					// Return success.
					result = true;
					
					break;
				}
			}

            // Set the out ID variable.
            entID = m_entID;

			// Increment to the next ID.
			m_entID++;

			// Return the result.
			return result;
		}
	}
}
