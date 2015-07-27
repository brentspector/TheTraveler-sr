using UnityEngine;
using System.Collections;
using GSP.Entities.Hostiles;
using GSP.Entities;

namespace GSP.Char.Enemies
{
    public class MimicMB : Enemy<Mimic>
    {
        public override void Start()
        {
            // Call the parent's start first.
            base.Start();

            // Set the name of the enemy.
            Name = "Mimic";
        }
    }
}