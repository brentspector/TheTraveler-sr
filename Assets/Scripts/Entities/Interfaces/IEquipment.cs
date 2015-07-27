using UnityEngine;
using System.Collections;

namespace GSP.Entities.Interfaces
{
    // Note: Use this if you want to use both Armour and Weapons.
    interface IEquipment : IArmour, IWeapon
    {
        // This is left blank for now.
    }
}
