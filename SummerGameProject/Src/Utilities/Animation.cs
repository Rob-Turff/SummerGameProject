using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Utilities
{
    public class Animation
    {
        public Animation(int numFrames, float frameSpeed, Texture2D texture)
        {
            this.NumFrames = numFrames;
            this.FrameSpeed = frameSpeed;
            this.Texture = texture;
            IsLooping = true;
            CurrentFrame = 0;
        }

        public int CurrentFrame { get; set; }

        public int NumFrames { get; set; }

        public float FrameSpeed { get; set; }

        public Texture2D Texture { get; private set; }

        public bool IsLooping { get; set; }

        public int FrameWidth { get { return Texture.Width / NumFrames; } }
    }
}
