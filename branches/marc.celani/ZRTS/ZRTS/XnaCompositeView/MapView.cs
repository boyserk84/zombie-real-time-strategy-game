using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZRTSModel.Entities;
using ZRTSModel;
using ZRTSModel.GameModel;
using ZRTS.XnaCompositeView;
using ZRTS.XnaCompositeView.MapViewVisitors;
using Microsoft.Xna.Framework.Input;


namespace ZRTS
{
    /// <summary>
    /// View class
    ///     This class will handle all view-aspects of the game. 
    ///     This class will extract each game object and translate each object's game location into a screen location
    ///     as well as interpret which frame (or animation) to be displayed (or played).
    /// 
    /// Author Nattapol Kemavaha
    /// </summary>
    public class MapView : XnaUIComponent
    {
        // Drag box UI reference (null means we are not dragging)
        private TestUIComponent dragBox = null;
        // First point in the dragging, used when the mouse moves to move the dragBox.
        private Point startSelectionBoxPoint;

        private static int cellDimension = 60;

        public static int CellDimension
        {
            get { return MapView.cellDimension; }
        }
        private static int SCROLL_SPEED = 10;

        public MapView(Game game)
            : base(game)
        {
        }
        /*
        /// <summary>
        /// Convert screen location of mouse click to gane location
        /// </summary>
        /// <param name="mouseX">X-mouse click</param>
        /// <param name="mouseY">y-mouse click</param>
        /// <returns>Return (X,Y) game location </returns>
        public Vector2 convertViewPointToGamePoint(Point viewPoint)
        {
            Vector2 gamePoint = new Vector2();
            gamePoint.X = (float)viewPoint.X / (float)CELL_DIMENSION;
            gamePoint.Y = (float)viewPoint.Y / (float)CELL_DIMENSION;
            return gamePoint;
        }
        /// <summary>
        /// Draw all game entities
        /// </summary>
        private void DrawEntities()
        {
            foreach (ZRTSModel.Entities.Unit u in AllUnits())
            {
                //if (isUnitBeingSelected(u))
                //{
                // Draw a highlight unit
                //  this.spriteUtil.drawAtIndex(0,0, new Vector2(translateXScreen(u.x), translateYScreen(u.y)));
                //}
                //this.spriteUnits.drawByAction(0, new Vector2(u.x, u.y));
                this.spriteUnits.drawAtIndex(0, 0, new Vector2(translateXScreen(u.x), translateYScreen(u.y)));

            }
        }

        
        /// <summary>
        /// Draw buildings
        /// </summary>
        public void DrawBuildings()
        {
            //System.Console.Out.WriteLine("Building in the world " + this.WorldMap.getBuildings().Count);
            if (this.WorldMap.getBuildings().Count > 0)
            {
                foreach (ZRTSModel.Entities.Building u in this.WorldMap.getBuildings())
                {
                    //System.Console.Out.WriteLine("Draw" + translateXScreen(u.x) + ":" + translateYScreen(u.y));
                    spriteBuildings.drawAtCurrentIndex(new Vector2(translateXScreenBuild(u.orginCell.Xcoord), translateYScreenBuild(u.orginCell.Ycoord)));
                }
            }
        }*/


        protected override void onDraw(XnaDrawArgs e)
        {
            // Determine all of the cells in view
            ZRTSModel.Map map = ((XnaUITestGame)Game).Model.GetScenario().GetGameWorld().GetMap();
            Point upperLeftCell = new Point(ScrollX / CellDimension, ScrollY / CellDimension);
            Point lowerRightCell = new Point(Math.Min((ScrollX + DrawBox.Width) / CellDimension, map.GetWidth() - 1), Math.Min((ScrollY + DrawBox.Height) / CellDimension, map.GetWidth() - 1));
            Point offset = new Point(ScrollX % CellDimension, ScrollY % CellDimension);
            
            // Draw all of the tiles
            for (int x = upperLeftCell.X; x <= lowerRightCell.X; x++)
            {
                for (int y = upperLeftCell.Y; y <= lowerRightCell.Y; y++)
                {
                    CellComponent cell = map.GetCellAt(x, y);
                    Tile tile = cell.GetTile();
                    DrawTileVisitor drawer = new DrawTileVisitor(e.SpriteBatch, ((XnaUITestGame)Game).SpriteSheet, new Rectangle((x - upperLeftCell.X) * CellDimension - offset.X, (y - upperLeftCell.Y) * CellDimension - offset.Y, CellDimension, CellDimension));
                    tile.Accept(drawer);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Map map = ((XnaUITestGame)Game).Model.GetScenario().GetGameWorld().GetMap();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ScrollX = Math.Max(ScrollX - SCROLL_SPEED, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                ScrollX = Math.Min(ScrollX + SCROLL_SPEED, map.GetWidth() * CellDimension - DrawBox.Width);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ScrollY = Math.Max(ScrollY - SCROLL_SPEED, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                ScrollY = Math.Min(ScrollY + SCROLL_SPEED, map.GetHeight() * CellDimension - DrawBox.Height);
            }
            base.Update(gameTime);
        }
    }
}
