using UnityEngine;
using System.Collections;

namespace GSP.Core
{
    public class BaseSingleton<T> : MonoBehaviour where T : Component
    {
        // Holds the instance of the singleton.
        static T m_instance;

        public static T Instance
        {
            get
            {
                // Check if the object isn't there.
                if (m_instance == null)
                {
                    // Try to find the instance in the level.
                    m_instance = FindObjectOfType<T>();
                    // Check if we found such an object.
                    if (m_instance == null)
                    {
                        // No instance so create one.
                        GameObject obj = new GameObject();
                        // Make the object hidden.
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        // Add the component to the object and set the instance to it.
                        m_instance = obj.AddComponent<T>();
                    }
                }
                return m_instance;
            }
        }

        // Initialise the singleton's inner functionality.
        public virtual void Awake()
        {
            // Tell the object to be persistent.
            DontDestroyOnLoad(this.gameObject);

            // Destroy any duplicates on scenes.
            if (m_instance == null)
            {
                m_instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
