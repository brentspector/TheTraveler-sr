/*******************************************************************************
 *
 *  File Name: BaseSingleton.cs
 *
 *  Description: Base for other Unity singletons
 *
 *******************************************************************************/
using UnityEngine;

namespace GSP.Core
{
    /*******************************************************************************
     *
     * Name: BaseSingleton
     * 
     * Description: The base class for other singletons.
     * 
     *******************************************************************************/
    public class BaseSingleton<T> : MonoBehaviour where T : Component
    {
        static T instance;    // Holds the instance of the singleton.

        // Initialise the singleton's inner functionality
        public virtual void Awake()
        {
            // Tell the object to be persistent
            DontDestroyOnLoad(this.gameObject);

            // Destroy any duplicates on scenes
            if (instance == null)
            {
                instance = this as T;
            } // end if
            else
            {
                Destroy(gameObject);
            } // end else
        } // end Awake

        public static T Instance
        {
            get
            {
                // Check if the object isn't there
                if (instance == null)
                {
                    // Try to find the instance in the level
                    instance = FindObjectOfType<T>();
                    // Check if we found such an object
                    if (instance == null)
                    {
                        // No instance so create one
                        GameObject obj = new GameObject();
                        // Make the GameObject hidden
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        // Add the component to the GameObject and set the instance to it
                        instance = obj.AddComponent<T>();
                    } // end if instance == null
                } // end outer if instance == null
                return instance;
            } // end get
        } // end Instance
    } // end BaseSingleton
} // end GSP.Core
