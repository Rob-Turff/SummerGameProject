using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Utilities
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Camera(Vector2 screenSize)
        {
            this.Size = screenSize;
        }

        public Tuple<bool, Vector2> CalcScreenCoords(Vector2 objPos, Vector2 objSize)
        {
            bool isOnScreen = false;
            Vector2 screenPos = objPos - Position;

            if ((objSize.X + objPos.X) > Position.X && (objSize.X + objPos.X) < (Position.X + Size.X))
                if ((objSize.Y + objPos.Y) > Position.Y && (objSize.Y + objPos.Y) < (Position.Y + Size.Y))
                    isOnScreen = true;

            return new Tuple<bool, Vector2>(isOnScreen, screenPos);
        }
    }
}
