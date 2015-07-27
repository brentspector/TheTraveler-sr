﻿using UnityEngine;
using System.Collections.Generic;

namespace GSP.Entities
{
	/// <summary>
	/// Provides the functionality of the hashset with a more readable name.
	/// </summary>
    public class EntitySet<TSubEntity> : HashSet<TSubEntity> where TSubEntity : Entity
	{
        // Empty class for now.
		// Leave empty.
	}
}