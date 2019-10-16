using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace SummerGameProject.Src.Client.Utilities
{
    public class Camera
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

            //logger.Debug("animation on screen = " + (objSize.X + objPos.X) + " screen pos = " + (Position.X + Size.X));

            if ((objSize.X + objPos.X) > Position.X && (objPos.X) < (Position.X + Size.X))
                if ((objSize.Y + objPos.Y) > Position.Y && (objPos.Y) < (Position.Y + Size.Y))
                    isOnScreen = true;

            return new Tuple<bool, Vector2>(isOnScreen, screenPos);
        }

        /// <summary>
        /// Adds the camera position to the value
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Vector2 CalcWorldCoords(Vector2 pos)
        {
            return pos + Position;
        }
    }
}
