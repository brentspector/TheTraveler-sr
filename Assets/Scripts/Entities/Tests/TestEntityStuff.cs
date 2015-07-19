// NOTE: Don't use this file. :P

using UnityEngine;
using System.Collections;

namespace GSP.Entities.Tests
{
    public class TestEntityStuff : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            //
        }

        // Update is called once per frame
        void Update()
        {
            // Pairs
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TestPairs();
            }

            // Entity creation with no generator.
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TestEntityCreationNoGenerator();
            }

            // Entity disposal.
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TestEntityDisposal();
            }

            // EntityData empty.
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TestEntityDataEmpty();
            }

            // EntityData nonempty
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                TestEntityDataNonEmpty();
            }

            // EntitySet empty.
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                TestEntitySetEmpty();
            }

            // EntitySet nonempty.
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                TestEntitySetNonEmpty();
            }

            // EntityMap empty.
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                TestEntityMapEmpty();
            }

            // EntityMap nonempty.
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                TestEntityMapNonEmpty();
            }

            // EntityManager map count.
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                TestEntityManagerEmptyMap();
            }

            // EntityManager EntityGenerator existence.
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                TestEntityManagerGetGeneratorExistence();
            }

            // EntityManager AddEntity.
            if (Input.GetKeyDown(KeyCode.Equals))
            {
                TestEntityManagerAddEntity();
            }

            // EntityManager GetEntity.
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                TestEntityManagerGetEntity();
            }

            // EntityManager GetAllEntities.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TestEntityManagerGetAllEntities();
            }

            // EntityManager GetEntities.
            if (Input.GetKeyDown(KeyCode.W))
            {
                TestEntityManagerGetEntities();
            }

            // EntityManager AddEntity duplicate same.
            if (Input.GetKeyDown(KeyCode.E))
            {
                TestEntityManagerAddEntityDuplicateSame();
            }

            // EntityManager AddEntity duplicate different.
            if (Input.GetKeyDown(KeyCode.R))
            {
                TestEntityManagerAddEntityDuplicateDifferent();
            }

            // EntityManager RemoveEntity.
            if (Input.GetKeyDown(KeyCode.T))
            {
                TestEntityManagerRemoveEntity();
            }

            // EntityManager RemoveEntity duplicate.
            if (Input.GetKeyDown(KeyCode.Y))
            {
                TestEntityManagerRemoveEntityDuplicate();
            }

            // EntityManager disposal.
            if (Input.GetKeyDown(KeyCode.U))
            {
                TestEntityManagerDisposal();
            }

            // EntityManager EntityGenerator CreateEntity.
            if (Input.GetKeyDown(KeyCode.I))
            {
                TestEntityManagerEntityGeneratorCreateEntity();
            }
        }

        void TestPairs()
        {
            Pair<int, int> testPair1 = new Pair<int, int>(10, 15);
            Debug.LogWarningFormat("testPair1 is: ({0}, {1})", testPair1.First, testPair1.Second);

            Pair<int, int> testPair2 = new Pair<int, int>();
            testPair2.First = 20;
            testPair2.Second = 25;
            Debug.LogWarningFormat("testPair2 is: ({0}, {1})", testPair2.First, testPair2.Second);
        }

        void TestEntityCreationNoGenerator()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(1, this.gameObject, PlayerColours.COL_BLUE, "George");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);
        }

        void TestEntityDisposal()
        {
            // Get entity disposal game object.
            GameObject go = GameObject.Find("EntityDisposal");

            if (go != null)
            {
                // Create a merchant.
                Entity ent = new Neutrals.Merchant(1000, go, PlayerColours.COL_GREEN, "Jeff");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

                ent.Dispose();
            }
            else
            {
                Debug.LogWarning("Entity already disposed of!");
            }
        }

        void TestEntityDataEmpty()
        {
            // Create an empty EntityData.
            EntityData entData = new EntityData();

            Debug.LogWarningFormat("Entity Pair: {0}, {1}", entData.First, entData.Second == null ? "Null" : "Not Null");
        }

        void TestEntityDataNonEmpty()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(2000, this.gameObject, PlayerColours.COL_ORANGE, "Sally");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);
            
            // Create the EntityData.
            EntityData entData = new EntityData(ent.ID, ent);

            Debug.LogWarningFormat("Entity Pair: {0}, {1}", entData.First, entData.Second.ToString());
        }

        void TestEntitySetEmpty()
        {
            // Crate an empty EntitySet.
            EntitySet<Neutrals.Merchant> entSet = new EntitySet<Neutrals.Merchant>();

            Debug.LogWarningFormat("EntitySet Count: {0}", entSet.Count);
        }

        void TestEntitySetNonEmpty()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(3000, this.gameObject, PlayerColours.COL_PINK, "Jane");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create the EntitySet.
            EntitySet<Neutrals.Merchant> entSet = new EntitySet<Neutrals.Merchant>();
            
            // Add the merchant entity.
            entSet.Add((Neutrals.Merchant)ent);

            Debug.LogWarningFormat("EntitySet Count: {0}", entSet.Count);

            if (entSet.Count > 0)
            {
                // Loop over the set.
                foreach(var item in entSet)
                {
                    Debug.LogWarningFormat("Entity found: {0}", item.Type);
                }
            }
        }

        void TestEntityMapEmpty()
        {
            // Create an empty EntityMap.
            EntityMap entMap = new EntityMap();

            Debug.LogWarningFormat("EntityMap Count: {0}", entMap.Count);
        }

        void TestEntityMapNonEmpty()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(4000, this.gameObject, PlayerColours.COL_BLACK, "James");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create the EntityMap.
            EntityMap entMap = new EntityMap();

            // Add the merchant entity.
            entMap.Add(ent.ID, ent);

            Debug.LogWarningFormat("EntityMap Count: {0}", entMap.Count);

            if (entMap.Count > 0)
            {
                // Loop over the map.
                foreach (var item in entMap)
                {
                    Debug.LogWarningFormat("Entity found: key is '{0}' and value is '{1}'", item.Key, item.Value);
                }
            }
        }

        void TestEntityManagerEmptyMap()
        {
            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Get the count of the entity manager's map.
            Debug.LogWarningFormat("EntityManager Map: {0}", entMan.GetNumEntities());
        }

        void TestEntityManagerGetGeneratorExistence()
        {
            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Get the entity generator.
            EntityGenerator entGen = entMan.GetEntityGenerator();

            // Check for existence.
            Debug.LogWarningFormat("EntityGenerator Exists: {0}", entGen != null ? "True" : "False");
        }

        void TestEntityManagerAddEntity()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(5000, this.gameObject, PlayerColours.COL_PURPLE, "John");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Add the entity to the entity manager.
            entMan.AddEntity(ent);

            Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());
        }

        void TestEntityManagerGetEntity()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(6000, this.gameObject, PlayerColours.COL_RED, "David");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Add the entity to the entity manager.
            entMan.AddEntity(ent);

            Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

            Entity resEnt = entMan.GetEntity(ent.ID);

            // Check for existence.
            Debug.LogWarningFormat("Entity Exists: {0}", resEnt != null ? "True" : "False");

            // Transform to merchant.
            Debug.LogWarningFormat("Entity Colour: {0}", ((Neutrals.Merchant)resEnt).Colour);
        }

        void TestEntityManagerGetAllEntities()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(7000, this.gameObject, PlayerColours.COL_RED, "Sarah");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create a bandit.
            Entity enemy = new Hostiles.Bandit(8000, this.gameObject);
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1}", enemy.ID, enemy.GameObj.name);

            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Add the entities to the entity manager.
            entMan.AddEntity(ent);
            entMan.AddEntity(enemy);

            Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

            // Get all the entities.
            EntityMap entMap = entMan.GetAllEntities();

            Debug.LogWarningFormat("entMap Count: {0}", entMap.Count);

            // Loop through the map.
            foreach (var entity in entMap)
            {
                Debug.LogWarningFormat("Entity {0} ({1}) is of type {2}", entity.Key, entity.Value.ID, entity.Value.Type);
            }
        }

        void TestEntityManagerGetEntities()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(9000, this.gameObject, PlayerColours.COL_YELLOW, "Fred");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create another merchant.
            Entity ent2 = new Neutrals.Merchant(10000, this.gameObject, PlayerColours.COL_BLACK, "Kent");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent2.ID, ent2.GameObj.name, ((Neutrals.Merchant)ent2).Colour, ((Neutrals.Merchant)ent2).Name);

            // Create a bandit.
            Entity enemy = new Hostiles.Bandit(11000, this.gameObject);
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1}", enemy.ID, enemy.GameObj.name);

            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Add the entities to the entity manager.
            entMan.AddEntity(ent);
            entMan.AddEntity(enemy);
            entMan.AddEntity(ent2);

            Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

            // Get all the merchant entities.
            EntitySet<Neutrals.Merchant> entSet = entMan.GetEntities<Neutrals.Merchant>();

            Debug.LogWarningFormat("entSet Count: {0}", entSet.Count);

            // Loop through the map.
            foreach (var entity in entSet)
            {
                Debug.LogWarningFormat("Entity {0} is of type {1} with the name {2}", entity.ID, entity.Type, entity.Name);
            }
        }

        void TestEntityManagerAddEntityDuplicateSame()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(12000, this.gameObject, PlayerColours.COL_BLUE, "Lisa");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Add the entity to the entity manager twice.
            entMan.AddEntity(ent);
            entMan.AddEntity(ent);

            Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());
        }

        void TestEntityManagerAddEntityDuplicateDifferent()
        {
            // Create a merchant.
            Entity ent = new Neutrals.Merchant(13000, this.gameObject, PlayerColours.COL_GREEN, "Phil");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

            Entity ent2 = new Neutrals.Merchant(13000, this.gameObject, PlayerColours.COL_ORANGE, "Dan");
            Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent2.ID, ent2.GameObj.name, ((Neutrals.Merchant)ent2).Colour, ((Neutrals.Merchant)ent2).Name);

            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Add the entities to the entity manager.
            if (entMan.AddEntity(ent))
            {
                Debug.LogWarning("AddEntity returned true.");
            }
            else
            {
                Debug.LogWarning("AddEntity returned false.");
            }

            if (entMan.AddEntity(ent2))
            {
                Debug.LogWarning("AddEntity returned true.");
            }
            else
            {
                Debug.LogWarning("AddEntity returned false.");
            }

            Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());
        }

        void TestEntityManagerRemoveEntity()
        {
            // Get entity disposal game object.
            GameObject go = GameObject.Find("EntityRemoval");

            if (go != null)
            {
                // Create a merchant.
                Entity ent = new Neutrals.Merchant(14000, this.gameObject, PlayerColours.COL_PINK, "Robert");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

                // Create another merchant.
                Entity ent2 = new Neutrals.Merchant(15000, go, PlayerColours.COL_PURPLE, "Alex");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent2.ID, ent2.GameObj.name, ((Neutrals.Merchant)ent2).Colour, ((Neutrals.Merchant)ent2).Name);

                // Create a bandit.
                Entity enemy = new Hostiles.Bandit(16000, this.gameObject);
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1}", enemy.ID, enemy.GameObj.name);

                // Create the entity manager.
                EntityManager entMan = EntityManager.Instance;

                // Add the entities to the entity manager.
                entMan.AddEntity(ent);
                entMan.AddEntity(enemy);
                entMan.AddEntity(ent2);

                Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

                if (entMan.RemoveEntity(ent2.ID))
                {
                    Debug.LogWarning("RemoveEntity returned true");
                }
                else
                {
                    Debug.LogWarning("RemoveEntity returned false");
                }

                Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

                // Get all the entities.
                EntityMap entMap = entMan.GetAllEntities();

                Debug.LogWarningFormat("entMap Count: {0}", entMap.Count);

                // Loop through the map.
                foreach (var entity in entMap)
                {
                    Debug.LogWarningFormat("Entity {0} ({1}) is of type {2}", entity.Key, entity.Value.ID, entity.Value.Type);
                }
            }
            else
            {
                Debug.LogWarning("Entity game object already removed!");
            }
        }

        void TestEntityManagerRemoveEntityDuplicate()
        {
            // Get entity disposal game object.
            GameObject go = GameObject.Find("EntityRemoval2");

            if (go != null)
            {
                // Create a merchant.
                Entity ent = new Neutrals.Merchant(17000, this.gameObject, PlayerColours.COL_RED, "Joe");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

                // Create another merchant.
                Entity ent2 = new Neutrals.Merchant(18000, go, PlayerColours.COL_YELLOW, "Bob");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent2.ID, ent2.GameObj.name, ((Neutrals.Merchant)ent2).Colour, ((Neutrals.Merchant)ent2).Name);

                // Create a bandit.
                Entity enemy = new Hostiles.Bandit(19000, this.gameObject);
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1}", enemy.ID, enemy.GameObj.name);

                // Create the entity manager.
                EntityManager entMan = EntityManager.Instance;

                // Add the entities to the entity manager.
                entMan.AddEntity(ent);
                entMan.AddEntity(enemy);
                entMan.AddEntity(ent2);

                Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

                if (entMan.RemoveEntity(18000))
                {
                    Debug.LogWarning("RemoveEntity returned true");
                }
                else
                {
                    Debug.LogWarning("RemoveEntity returned false");
                }

                if (entMan.RemoveEntity(18000))
                {
                    Debug.LogWarning("RemoveEntity returned true");
                }
                else
                {
                    Debug.LogWarning("RemoveEntity returned false");
                }

                Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

                // Get all the entities.
                EntityMap entMap = entMan.GetAllEntities();

                Debug.LogWarningFormat("entMap Count: {0}", entMap.Count);

                // Loop through the map.
                foreach (var entity in entMap)
                {
                    Debug.LogWarningFormat("Entity {0} ({1}) is of type {2}", entity.Key, entity.Value.ID, entity.Value.Type);
                }
            }
            else
            {
                Debug.LogWarning("Entity game object already removed!");
            }
        }

        void TestEntityManagerDisposal()
        {
            // Get the game objects.
            GameObject go1 = GameObject.Find("EntityManagerDispose1");
            GameObject go2 = GameObject.Find("EntityManagerDispose2");
            GameObject go3 = GameObject.Find("EntityManagerDispose3");

            if (go1 != null && go2 != null && go3 != null)
            {
                // Create a merchant.
                Entity ent = new Neutrals.Merchant(20000, go1, PlayerColours.COL_BLACK, "Carl");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent.ID, ent.GameObj.name, ((Neutrals.Merchant)ent).Colour, ((Neutrals.Merchant)ent).Name);

                // Create another merchant.
                Entity ent2 = new Neutrals.Merchant(21000, go2, PlayerColours.COL_BLUE, "Wanda");
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1} with colour '{2}' and is named: {3}", ent2.ID, ent2.GameObj.name, ((Neutrals.Merchant)ent2).Colour, ((Neutrals.Merchant)ent2).Name);

                // Create a bandit.
                Entity enemy = new Hostiles.Bandit(22000, go3);
                Debug.LogWarningFormat("Entity '{0}' is on gameobject named {1}", enemy.ID, enemy.GameObj.name);

                // Create the entity manager.
                EntityManager entMan = EntityManager.Instance;

                // Add the entities to the entity manager.
                entMan.AddEntity(ent);
                entMan.AddEntity(ent2);
                entMan.AddEntity(enemy);

                Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());

                // Dispose of the entity manager now.
                entMan.Dispose();

                Debug.LogWarningFormat("NumEntities: {0}", entMan.GetNumEntities());
            }
            else
            {
                Debug.LogWarning("EntityManager already disposed of!");
            }
        }

        void TestEntityManagerEntityGeneratorCreateEntity()
        {
            // Create the entity manager.
            EntityManager entMan = EntityManager.Instance;

            // Get the entity generator.
            EntityGenerator entGen = entMan.GetEntityGenerator();

            // Create a merchant.
            if (entGen.CreateEntity(EntityType.ENT_MERCHANT, this.gameObject))
            {
                Debug.LogWarning("EntityGenerator returned true");
            }
            else
            {
                Debug.LogWarning("EntityGenerator returned false");
            }

            Debug.LogWarningFormat("NumEnities: {0}", entMan.GetNumEntities());

            // Create a bandit.
            if (entGen.CreateEntity(EntityType.ENT_BANDIT, this.gameObject))
            {
                Debug.LogWarning("EntityGenerator returned true");
            }
            else
            {
                Debug.LogWarning("EntityGenerator returned false");
            }

            Debug.LogWarningFormat("NumEnities: {0}", entMan.GetNumEntities());

            // Create a mimic.
            if (entGen.CreateEntity(EntityType.ENT_MIMIC, this.gameObject))
            {
                Debug.LogWarning("EntityGenerator returned true");
            }
            else
            {
                Debug.LogWarning("EntityGenerator returned false");
            }

            Debug.LogWarningFormat("NumEnities: {0}", entMan.GetNumEntities());

            // Create a porter.
            if (entGen.CreateEntity(EntityType.ENT_PORTER, this.gameObject))
            {
                Debug.LogWarning("EntityGenerator returned true");
            }
            else
            {
                Debug.LogWarning("EntityGenerator returned false");
            }

            Debug.LogWarningFormat("NumEnities: {0}", entMan.GetNumEntities());

            // Create a mercinary.
            if (entGen.CreateEntity(EntityType.ENT_MERCINARY, this.gameObject))
            {
                Debug.LogWarning("EntityGenerator returned true");
            }
            else
            {
                Debug.LogWarning("EntityGenerator returned false");
            }

            Debug.LogWarningFormat("NumEnities: {0}", entMan.GetNumEntities());

            // Get all the entities.
            EntityMap entMap = entMan.GetAllEntities();

            Debug.LogWarningFormat("entMap Count: {0}", entMap.Count);

            // Loop through the map.
            foreach (var entity in entMap)
            {
                Debug.LogWarningFormat("Entity {0} ({1}) is of type {2}", entity.Key, entity.Value.ID, entity.Value.Type);
            }
        }
    }
}