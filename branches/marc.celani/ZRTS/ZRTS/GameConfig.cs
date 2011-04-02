using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZRTS
{
    /// <summary>
    /// Any constant or game configuration value should be defined here.
    /// </summary>
    static class GameConfig
    {
        public static int SCREEN_WIDTH = 800;
        public static int SCREEN_HEIGHT = 600;

        public static int TILE_WIDTH = 20;
        public static int TILE_HEIGHT = 20;

        public static float DEFAULT_UNIT = 2.5f;
        public static int IMG_DRAGBOX = 1;


        public static int UNIT_WIDTH = 36;
        public static int UNIT_HEIGHT = 36;


        public static int ZOMBIE_START_X = 0;   // Absolute X location on the spritesheet
        public static int ZOMBIE_START_Y = 157;   // Absolute Y location on the spritehseet

        // Frame reference
        public static int ZOMBIE_ACTION_ATTACK= 0;
        public static int ZOMBIE_ACTION_MOVE= 1;
        public static int ZOMBIE_ACTION_DEAD = 2;
        public static int ZOMBIE_DIR_N = 0;
        public static int ZOMBIE_DIR_S = 4;
        public static int ZOMBIE_DIR_W = 12;
        public static int ZOMBIE_DIR_E = 8;
        public static int ZOMBIE_DIR_SE = 0;
        public static int ZOMBIE_DIR_SW = 0;
        public static int ZOMBIE_DIR_NE = 0;
        public static int ZOMBIE_DIR_NW = 0;
    }
}
