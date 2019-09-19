using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Client.Src.Components;

namespace Client.Src.Screens
{
    internal class SettingScreen : Screen
    {
        public SettingScreen(Game1 game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;
        }
    }
}
