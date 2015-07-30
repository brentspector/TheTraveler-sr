/*******************************************************************************
 *
 *  File Name: ResourceTile
 *
 *  Description: Special type of tile signifying a resource
 *
 *******************************************************************************/
using GSP.Char;
using UnityEngine;

namespace GSP.Tiles
{
    /*******************************************************************************
     *
     * Name: ResourceTile
     * 
     * Description: Sets the tile to be a special tile and sets its ResourceType.
     * 
     *******************************************************************************/
    public class ResourceTile : MonoBehaviour
    {
        // The ResourceType the resource on the map is; This is set inside the exporter
		ResourceType resourceType;
        
        // Gets and Sets the resource's type
        public ResourceType Type
        {
            get { return resourceType; }
            set { resourceType = value; }
        } // end Type
    } // end ResourceTile
} // end GSP.Tiles
