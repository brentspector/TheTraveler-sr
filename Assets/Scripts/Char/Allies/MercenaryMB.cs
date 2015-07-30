using UnityEngine;
using System.Collections;
using GSP.Entities.Friendlies;
using GSP.Entities;

namespace GSP.Char.Allies
{
    public class MercenaryMB : Ally2<Mercenary>
    {
        public override void Start()
        {
            // Call the parent's start first.
            base.Start();

            // Set the name of the enemy.
            Name = "Mercenary";
        }
    }
}