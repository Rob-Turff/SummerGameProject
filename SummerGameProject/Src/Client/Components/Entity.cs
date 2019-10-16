using Microsoft.Xna.Framework;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components
{
    public abstract class Entity : Component
    {
        // Default values for physics calculations
        internal Vector2 velocity = new Vector2(0,0);
        internal float airDrag = 0.005f;
        internal float groundFriction = 0.01f;
        internal float horizontalAcceleration = 0;
        internal float verticalAcceleration = 0;
        internal float maxVerticalSpeed = 2000f;
        internal float maxHorzizontalSpeed = 2000f;
        internal bool isInAir = true;

        public Entity(Screen screen) : base(screen)
        {
        }

        public Entity(Screen screen, Vector2 position) : base(screen, position)
        {
        }
    }
}
