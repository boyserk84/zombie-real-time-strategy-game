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

        // Absolute Y location on the spritehseet
        public static int TILE_START_Y = 468;
        public static int ZOMBIE_START_Y = 0;   
        public static int SOLDIER_START_Y = 108 ;
        public static int WORKER_START_Y = 108 + 36 * 3;

        public static int BIG_AVATAR_START_Y = 808;
        public static int SELECT_AVATAR_START_Y = 960;

        // Frame reference
        public static int ACTION_ATTACK= 0;
        public static int ACTION_MOVE= 1;
        public static int ACTION_DEAD = 2;
        public static int DIR_N = 0;
        public static int DIR_S = 4;
        public static int DIR_W = 12;
        public static int DIR_E = 8;
        public static int DIR_SE = 28;
        public static int DIR_SW = 24;
        public static int DIR_NE = 16;
        public static int DIR_NW = 20;


        public static int TILE_DIM = 36;        // Tile Dimension

        public static int BUTTON_STOP = 6;
        public static int BUTTON_MOVE = 0;
        public static int BUTTON_ATTACK = 12;
        public static int BUTTON_BUILD = 6;

        public static int BUTTON_START_Y_SECOND = 648+80 ;
        public static int BUTTON_START_Y = 648;
        public static int BUTTON_DIM = 80;
        public static int BUTTON_NORMAL = 0;
        public static int BUTTON_MOUSE_OVER = 1;
        public static int BUTTON_MOUSE_PRESS = 2;

        public static int TILE_GRASS = 0;
        public static int TILE_SAND = 1;

        
        



    }
}
