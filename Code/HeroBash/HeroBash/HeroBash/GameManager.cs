using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledLib;

namespace HeroBash
{
    public static class GameManager
    {
        public static Hero Hero;
        public static Map Map;
        public static Camera Camera;

        public static MinionManager MinionManager;
        public static ButtonManager ButtonManager;
        public static ProjectileManager ProjectileManager;

        public static Vector2 princessPosition;

        public static SpriteFont Font;

        public static int CurrentStage;
        public static int CurrentLevel;

        public static bool CameraFollowingHero = true;
        public static bool GameIsPaused = false;

        
    }
}
