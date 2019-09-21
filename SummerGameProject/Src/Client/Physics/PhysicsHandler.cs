using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Client.Physics
{
    public class PhysicsHandler
    {
        private readonly Screen screen;

        public PhysicsHandler(Screen screen)
        {
            this.screen = screen;
        }

        internal void Update(GameTime gameTime)
        {
            HandleMovement();
            CheckCollisons();
        }

        private void HandleMovement()
        {
            foreach (var entity in screen.entities)
            {
                
            }
        }

        private void CheckCollisons()
        {
            throw new NotImplementedException();
        }
    }
}
