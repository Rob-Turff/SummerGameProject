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
        public SettingScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }
    }
}
