/*******************************************************************************
 *
 *  File Name: EntitySet.cs
 *
 *  Description: A list that holds specific sub-entities using polymorphism
 *
 *******************************************************************************/
using System.Collections.Generic;

namespace GSP.Entities
{
    /*******************************************************************************
     *
     * Name: EntitySet
     * 
     * Description: Provides the functionality of the hashset with a more readable
     *              name.
     * 
     *******************************************************************************/
    public class EntitySet<TSubEntity> : HashSet<TSubEntity> where TSubEntity : Entity
	{
        // Empty class for now.
		// Leave empty.
	} // end EntitySet
} // end GSP.Entities
