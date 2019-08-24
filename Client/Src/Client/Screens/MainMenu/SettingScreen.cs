using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Client.Src.Components;

namespace Client.Src.Screens
{
    public class SettingScreen : Screen
    {
        public SettingScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;
        }
    }
}
