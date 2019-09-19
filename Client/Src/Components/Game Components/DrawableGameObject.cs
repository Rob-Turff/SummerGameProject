using Client.Src.Screens;
using Common.Src.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Components.Game_Components
{
    class DrawableGameObject
    {
        private readonly IDrawableEntity entity;
        private readonly Screen screen;

        public Vector2 Position => entity.Position;
        public float Scale => entity.Scale;

        public Texture2D Texture { get; }

        public DrawableGameObject(IDrawableEntity entity, Screen screen)
        {
            this.entity = entity;
            this.screen = screen;

            Texture = screen.Content.Load<Texture2D>("Game/" + entity.ImageName);

            Debug.Assert(Texture.Width == Convert.ToInt32(entity.Width / entity.Scale));
            Debug.Assert(Texture.Height == Convert.ToInt32(entity.Height / entity.Scale));
        }
    }
}
