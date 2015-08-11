/*******************************************************************************
 *
 *  File Name: SerializableVector3.cs
 *
 *  Description: A serialisable version of the Vector3 struct.
 *
 *******************************************************************************/
using System;
using UnityEngine;

namespace GSP.Core
{
    /*******************************************************************************
     *
     * Name: SerializableVector3
     * 
     * Description: Since unity doesn't flag the Vector3 as serializable, we need to
     *              create our own version. This one will automatically convert
     *              between Vector3 and SerializableVector3.
     * 
     *******************************************************************************/
    [Serializable]
    public struct SerializableVector3
    {
        // x component
        public float x;

        // y component
        public float y;

        // z component
        public float z;

        // Constructor that used to build a SerializableVector3 object
        public SerializableVector3(float rX, float rY, float rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        } // end SerializableVector3

        // Returns a string representation of the object
        public override string ToString()
        {
            return String.Format("[{0}, {1}, {2}]", x, y, z);
        } // end ToString

        // Automatic conversion from SerializableVector3 to Vector3
        public static implicit operator Vector3(SerializableVector3 rValue)
        {
            return new Vector3(rValue.x, rValue.y, rValue.z);
        } // end Vector3

        // Automatic conversion from Vector3 to SerializableVector3
        public static implicit operator SerializableVector3(Vector3 rValue)
        {
            return new SerializableVector3(rValue.x, rValue.y, rValue.z);
        } // end SerializableVector3
    } // end SerializableVector3
} // end GSP.Core
