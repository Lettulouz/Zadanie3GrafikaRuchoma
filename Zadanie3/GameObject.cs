using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    internal class GameObject
    {
        public Vector3 Position { get; set; }
        public float RotationAngle { get; set; }
        // Dodaj inne właściwości, takie jak rozmiar, kolor itp.

        public GameObject(Vector3 position)
        {
            Position = position;
            RotationAngle = 0.0f;
        }
    }
}
