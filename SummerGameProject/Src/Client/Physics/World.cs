using Microsoft.Xna.Framework;
using SummerGameProject.Src.Components.Platforms;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Physics
{
    public class World
    {
        public World(GameScreen gameScreen)
        {
            Platform floor = new GrassPlatform(new Vector2(0, 880), new Vector2(1f, 1f), gameScreen);
            Platform platform1 = new GrassPlatform(new Vector2(300, 650), new Vector2(0.2f, 0.2f), gameScreen);
            Platform platform2 = new GrassPlatform(new Vector2(1200, 650), new Vector2(0.2f, 0.2f), gameScreen);
            Platform wallLeft = new StoneWallPlatform(new Vector2(0, 380), new Vector2(1f, 1f), gameScreen);
            Platform wallRight = new StoneWallPlatform(new Vector2(1870, 380), new Vector2(1f, 1f), gameScreen);

            gameScreen.components.Add(floor);
            gameScreen.components.Add(platform1);
            gameScreen.components.Add(platform2);
            gameScreen.components.Add(wallLeft);
            gameScreen.components.Add(wallRight);
        }
    }
}
