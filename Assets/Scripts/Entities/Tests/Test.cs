using UnityEngine;
using System.Collections;

namespace GSP.Entities.Tests
{
    public class Test : MonoBehaviour
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
    }
}
