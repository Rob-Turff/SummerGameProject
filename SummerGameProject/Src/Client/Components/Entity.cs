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
        internal Vector2 maxVelocity;
        internal Vector2 velocity;

        internal float airDrag;
        internal float groundFriction;

        public Entity(Screen screen) : base(screen)
        {
        }

        public Entity(Screen screen, Vector2 position) : base(screen, position)
        {
        }
    }
}
