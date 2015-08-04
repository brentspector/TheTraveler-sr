/*******************************************************************************
 *
 *  File Name: EntityData.cs
 *
 *  Description: Wrapper for the Pair<int, Entity> construct
 *
 *******************************************************************************/

namespace GSP.Entities
{
    /*******************************************************************************
     *
     * Name: EntityData
     * 
     * Description: Provides the functionality of the pair with a more readable
     *              name.
     * 
     *******************************************************************************/
    public class EntityData : Pair<int, Entity>
	{
		// Constructor; Creates an empty object for you to assign it yourself
        public EntityData()
		{
			this.First = -1;
			this.Second = null;
		} // end EntityData

		// Constructor; Creates an object with the given values
        public EntityData(int id, Entity ent)
		{
			this.First = id;
			this.Second = ent;
		} // end EntityData constructor
	} // end EntityData
} // end GSP.Entities
