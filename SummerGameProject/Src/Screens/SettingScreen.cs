using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SummerGameProject.Src.Screens
{
    class SettingScreen : Screen
    {
        public SettingScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {

        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }
    }
}
