using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZRTSModel;
using ZRTSModel.EventHandlers;
using ZRTS.InputEngines;

namespace ZRTS.XnaCompositeView
{
    public class UnitUI : PictureBox
    {
        private UnitComponent unit;
		bool selected = false;

        private int currentFrame = 0;       // Current frame reference
        private float currentElapsedTime = 0f;      // Current time
		Texture2D pixel;

        public UnitComponent Unit
        {
            get { return unit; }
        }
        public UnitUI(Game game, UnitComponent unit, Rectangle sourceRect)
            : base(game, sourceRect)
        {
            this.unit = unit;
            this.OnClick += getAttacked;

			unit.SelectHandler += new ModelComponentSelectedHandler(onSelectChanged);
			unit.UnitStateChangedHandlers += new UnitStateChangedHanlder(onUnitStateChanged);

			pixel = new Texture2D(game.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
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
            if (unit.State == UnitComponent.UnitState.ATTACKING)
            {
                changePicture(0, GameConfig.ZOMBIE_START_Y);
            }
            else if (unit.State == UnitComponent.UnitState.MOVING)
            {
                changePicture(0, GameConfig.ZOMBIE_START_Y + GameConfig.ZOMBIE_ACTION_MOVE * GameConfig.UNIT_HEIGHT);
            }
            else if (unit.State == UnitComponent.UnitState.DEAD)
            {
                changePicture(0, GameConfig.ZOMBIE_START_Y + GameConfig.ZOMBIE_ACTION_DEAD * GameConfig.UNIT_HEIGHT);
            }

            if (unit.UnitOrient == UnitComponent.Orient.N)
            {
                changePicture(GameConfig.ZOMBIE_DIR_N, sourceRect.Y);
            }
			else if (unit.UnitOrient == UnitComponent.Orient.S)
            {
                changePicture(GameConfig.ZOMBIE_DIR_S * GameConfig.UNIT_WIDTH, sourceRect.Y);
            }
			else if (unit.UnitOrient == UnitComponent.Orient.E)
            {
                changePicture(GameConfig.ZOMBIE_DIR_E * GameConfig.UNIT_WIDTH, sourceRect.Y);
            }
			else if (unit.UnitOrient == UnitComponent.Orient.W)
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

		protected override void onDraw(XnaDrawArgs e)
		{
			base.onDraw(e);
			if (selected)
			{
				// Draw a healthbox for the Unit.
				Rectangle healthBG = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 8, e.Location.Width, 8);
				e.SpriteBatch.Draw(pixel, healthBG, Color.Black);

				int healthWidth = (int)( e.Location.Width * (1.0 * unit.CurrentHealth / unit.MaxHealth));

				Rectangle healthRect = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 8, healthWidth, 8);
				e.SpriteBatch.Draw(pixel, healthRect, Color.LimeGreen);

				Color neoGreen = new Color(111, 245, 30);

				// Draw a Rectangle around the unit to show that it is selected.
				Rectangle leftRect = new Rectangle(e.Location.X, e.Location.Y, 2, e.Location.Height);
				Rectangle rightRect = new Rectangle(e.Location.X + e.Location.Width -2, e.Location.Y, 2, e.Location.Height);
				Rectangle topRect = new Rectangle(e.Location.X, e.Location.Y, e.Location.Width, 2);
				Rectangle bottomRect = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height -2, e.Location.Width, 2);
				e.SpriteBatch.Draw(pixel, leftRect, neoGreen);
				e.SpriteBatch.Draw(pixel, topRect, neoGreen);
				e.SpriteBatch.Draw(pixel, rightRect, neoGreen);
				e.SpriteBatch.Draw(pixel, bottomRect, neoGreen);
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
            if (currentElapsedTime > 100)
            {
                updateAnimation();
                currentElapsedTime = 0;
            }
            base.Update(gameTime);
            
        }

		public void onSelectChanged(Object obj, bool selected)
		{
			this.selected = selected;
		}

		public void onUnitStateChanged(Object obj, UnitStateChangedEventArgs args)
		{
			currentFrame = 0;
			updateAnimation();
		}

		public void onUnitOrientationChange(Object obj, UnitOrientationChangedEventArgs args)
		{
			currentFrame = 0;
			updateAnimation();
		}
    }
}
