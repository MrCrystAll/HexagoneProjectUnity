using System;
using UnityEngine;

namespace model
{
    public class Vector2Args : EventArgs
    {
        public Vector2 Point;

        public Vector2Args(Vector2 point)
        {
            Point = point;
        }
    }
}