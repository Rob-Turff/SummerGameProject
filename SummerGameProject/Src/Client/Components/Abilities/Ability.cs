using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components
{
    public abstract class Ability : Entity
    {
        internal PlayerAttributes playerAttributes;
        internal MouseState mouseState;

        public Ability(Screen screen, PlayerAttributes playerAttributes, MouseState mouseState) : base(screen)
        {
            this.playerAttributes = playerAttributes;
            this.mouseState = mouseState;
            CalculatePosition();
        }

        private void CalculatePosition()
        {
            Position = playerAttributes.position;
        }
    }
}
