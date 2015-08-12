using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using GSP.Core;
using System.Collections;

namespace GSP.Items.Inventory
{
    public class Inventory : MonoBehaviour
    {

        List<GameObject> slots; // The list of slots for the inventory
        List<Item> items;       // The list of items for the inventory
        Transform bottomGrid;   // The Inentory's Bottom Panel
        int numSlotsCreate;     // The number of slots to create
        int numSlots;           // The number of slots currently created
        GameObject tooltip;     // The tooltip's GameObject

        void Awake()
        {
            if (ItemDatabase.Instance == null) { }
        }

        // Use this for initialisation
        void Start()
        {
            // Get Inventory's Bottom panel
            Transform bottomGrid = GameObject.Find("Inventory/Bottom").transform;

            // Get the tooltip GameObject
            tooltip = GameObject.Find("Tooltip");
            tooltip.SetActive(false);

            // Initialise the lists
            slots = new List<GameObject>();
            items = new List<Item>();

            // Create fifteen slots
            numSlotsCreate = 15;

            // Initalise the number of slots to zero
            numSlots = 0;

            // Loop to create the slots
            for (int index = 0; index < numSlotsCreate; index++)
            {
                // Create the slot
                GameObject slot = Instantiate(PrefabReference.prefabSlot) as GameObject;

                // Parent the slot to the Inventory's Bottom panel
                slot.transform.SetParent(bottomGrid);

                // Name the slot in the editor for convienience
                slot.name = "Slot " + (index + 1);

                // Change the slotId
                slot.GetComponent<Slot>().SlotId = numSlots;

                // Add the slot to the list
                slots.Add(slot);

                // Add an empty item to the list for the slot
                items.Add(new EmptyItem());

                // Increment the number of slots
                numSlots++;
            } // end for

            StartCoroutine(Add());
        } // end Start

        IEnumerator Add()
        {
            Debug.Log("Waiting");
            yield return new WaitForSeconds(3);
            Debug.Log("Done");
            AddItem(ItemDatabase.Instance.Items[0].Id);
            Debug.LogFormat("items count: {0}", items.Count);
        }

        // Add an item to the inventory
        public void AddItem(int itemId)
        {
            // Get the list of items from the ItemDatabase
            List<Item> database = ItemDatabase.Instance.Items;
            
            // Only proceed if the ID exists in the database
            if (database.Exists(item => item.Id == itemId))
            {
                // Get the item from the database
                Item tempItem = database[database.FindIndex(item => item.Id == itemId)];

                // Place it in the next free slot
                items[FindFreeSlot()] = tempItem;
            } // end if
            else
            {
                Debug.LogErrorFormat("The Id '{0}' does not exist in the ItemDatabase!", itemId);
            }
        } // end AddItem

        // Gets the first empty inventory slot
        public int FindFreeSlot()
        {
            // Return the first empty slot.
            return items.FindIndex(item => item.Name == string.Empty);
        } // end FindFreeSlot

        // Gets an item at the given index
        public Item GetItem(int itemId)
        {
            return items[itemId];
        } // end GetItem

        // Shows the tooltip window for item information
        public void ShowTooltip(Vector3 position, Item item, bool canShow = true)
        {
            // Check if we're showing the tooltip
            if (canShow)
            {
                // Enable the tooltip window
                tooltip.SetActive(true);

                float posHorizontal = gameObject.GetComponent<RectTransform>().position.x;
                float posVertical = tooltip.GetComponent<RectTransform>().rect.height;

                // Correct the position of the tooltip window
                tooltip.GetComponent<RectTransform>().localPosition = new Vector3(position.x + (posHorizontal / 3 + 25), position.y - (posVertical / 2), position.z);

                // Get the Title Text child
                Text tooltipTitleText = tooltip.transform.GetChild(0).GetComponent<Text>();

                // Get the Body Text Child
                Text tooltipBodyText = tooltip.transform.GetChild(1).GetComponent<Text>();

                // Set the tooltip's title text
                tooltipTitleText.text = item.Name;

                // Check if the item is a piece of armour
                if (item is Armor)
                {
                    //
                }
                // Check if the item is a weapon
                else if (item is Weapon)
                {
                    //
                    Debug.Log("Item is a weapon");
                }
                // Check if the item is a bonus
                else if (item is Weapon)
                {
                    //
                }
                // Otherwise, the item must be a resource
                else
                {
                    //
                }
                
                // Set the tooltip's body text
                tooltipBodyText.text = "Weight: +10\nCost: 1000";

            }
            else
            {
                // Otherwise, disable the tooltip window
                tooltip.SetActive(false);
            }
        } // end ShowTooltip
    } // end GSP.Items.Inventory
} // end GSP.Items
