using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel;
using ZRTSModel.EventHandlers;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class UnitUI : PictureBox
    {
        private UnitComponent unit;

        private int currentFrame = 0;       // Current frame reference
        private float currentElapsedTime = 0f;      // Current time



        public UnitComponent Unit
        {
            get { return unit; }
        }
        public UnitUI(Game game, UnitComponent unit, Rectangle sourceRect)
            : base(game, sourceRect)
        {
            this.unit = unit;
            this.OnClick += getAttacked;
        }

        /// <summary>
        /// Triggered if this unit is being attacked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void getAttacked(Object sender, XnaMouseEventArgs e)
        {
            if (e.Bubbled && !e.Handled && e.ButtonPressed == MouseButton.Right)
            {
                ((XnaUITestGame)Game).Controller.TellSelectedUnitsToAttack(unit);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Manually change picture by specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void changePicture(int x, int y)
        {
            this.sourceRect.X = x;
            this.sourceRect.Y = y;
        }

        /// <summary>
        /// Manually change picture by specified a pair of indices
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void changePictureByFrame(int col, int row)
        {
            this.sourceRect.X = col * this.sourceRect.Width ;
            this.sourceRect.Y = row * this.sourceRect.Height;
        }

        /// <summary>
        /// Change picture/image frame based on state and direction
        /// </summary>
        /// <param name="state"></param>
        /// <param name="direction"></param>
        private void changePicture(string state, string direction)
        {
            if (state.Equals("attack"))
            {
                changePicture(0, GameConfig.ZOMBIE_START_Y);
            }
            else if (state.Equals("move"))
            {
                changePicture(0, GameConfig.ZOMBIE_START_Y + GameConfig.ZOMBIE_ACTION_MOVE * GameConfig.UNIT_HEIGHT);
            }
            else if (state.Equals("dead"))
            {
                changePicture(0, GameConfig.ZOMBIE_START_Y + GameConfig.ZOMBIE_ACTION_DEAD * GameConfig.UNIT_HEIGHT);
            }

            if (direction.Equals("N"))
            {
                changePicture(GameConfig.ZOMBIE_DIR_N, sourceRect.Y);
            }
            else if (direction.Equals("S"))
            {
                changePicture(GameConfig.ZOMBIE_DIR_S * GameConfig.UNIT_WIDTH, sourceRect.Y);
            }
            else if (direction.Equals("E"))
            {
                changePicture(GameConfig.ZOMBIE_DIR_E * GameConfig.UNIT_WIDTH, sourceRect.Y);
            }
            else if (direction.Equals("W"))
            {
                changePicture(GameConfig.ZOMBIE_DIR_W * GameConfig.UNIT_WIDTH, sourceRect.Y);
            }
        }

        /// <summary>
        /// Update animation
        /// </summary>
        private void updateAnimation()
        {

            // NEED STATE AND DIRECTION INFO FROM UnitComponent

            if (currentFrame > 3)
            {
                currentFrame = 0;
            } else {
                changePicture("move","W");  // right now, just manually change
                sourceRect.X += GameConfig.UNIT_WIDTH * currentFrame;
                ++currentFrame;
            }
        }

        /// <summary>
        /// Update this unit's view
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            currentElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            //System.Console.Out.WriteLine("TICK" + currentElapsedTime);
            
            // Update animation every 1/4 second
            if (currentElapsedTime > 250)
            {
                updateAnimation();
                currentElapsedTime = 0;
            }
            base.Update(gameTime);
            
        }

        
    }
}
