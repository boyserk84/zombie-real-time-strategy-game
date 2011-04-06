﻿using System;
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
            this.SourceRect = new Rectangle(x, y, this.SourceRect.Width, this.SourceRect.Height);
        }

        /// <summary>
        /// Manually change picture by specified a pair of indices
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void changePictureByFrame(int col, int row)
        {
            this.SourceRect = new Rectangle(col * this.SourceRect.Width, row * this.SourceRect.Height, this.SourceRect.Width, this.SourceRect.Height);
            //this.SourceRect.X = col * this.SourceRect.Width ;
            //this.SourceRect.Y = row * this.SourceRect.Height;
        }

        /// <summary>
        /// Change picture/image frame based on state and direction
        /// </summary>
        /// <param name="state"></param>
        /// <param name="direction"></param>
        private void changePicture()
        {
            int unitType = GameConfig.SOLDIER_START_Y;
            if (unit.IsZombie)
            {
                unitType = GameConfig.ZOMBIE_START_Y;
            }
            

            if (unit.State == UnitComponent.UnitState.ATTACKING)
            {
                changePicture(0, unitType + GameConfig.ACTION_ATTACK);
            }
            else if (unit.State == UnitComponent.UnitState.MOVING || unit.State == UnitComponent.UnitState.IDLE)
            {
                changePicture(0, unitType + GameConfig.ACTION_MOVE * GameConfig.UNIT_HEIGHT);
            }
            else if (unit.State == UnitComponent.UnitState.DEAD)
            {
                changePicture(0, unitType + GameConfig.ACTION_DEAD * GameConfig.UNIT_HEIGHT);
            }


            if (unit.UnitOrient == UnitComponent.Orient.N)
            {
                changePicture((GameConfig.DIR_N + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
			else if (unit.UnitOrient == UnitComponent.Orient.S)
            {
                changePicture((GameConfig.DIR_S + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
			else if (unit.UnitOrient == UnitComponent.Orient.E)
            {
                changePicture((GameConfig.DIR_E + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
			else if (unit.UnitOrient == UnitComponent.Orient.W)
            {
                changePicture((GameConfig.DIR_W + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
            else if (unit.UnitOrient == UnitComponent.Orient.NE)
            {
                changePicture((GameConfig.DIR_NE+ currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
            else if (unit.UnitOrient == UnitComponent.Orient.SE)
            {
                changePicture((GameConfig.DIR_SE + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
            else if (unit.UnitOrient == UnitComponent.Orient.NW)
            {
                changePicture((GameConfig.DIR_NW + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
            else if (unit.UnitOrient == UnitComponent.Orient.SW)
            {
                changePicture((GameConfig.DIR_SW + currentFrame) * GameConfig.UNIT_WIDTH, SourceRect.Y);
            }
        }

        /// <summary>
        /// Update animation
        /// </summary>
        private void updateAnimation()
        {
            if (unit.IsZombie)
            {
                //System.Console.Out.WriteLine(unit.State + ":" + unit.UnitOrient);
            }
            if (currentFrame > 3)
            {
                currentFrame = 0;
            }
            else
            {
                changePicture();
                ++currentFrame;
            }
        }

		protected override void onDraw(XnaDrawArgs e)
		{
			base.onDraw(e);
			if (selected && !unit.IsZombie)
			{
				// Draw a healthbox for the Unit.
				Rectangle healthBG = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 8, e.Location.Width, 8);
				e.SpriteBatch.Draw(pixel, healthBG, Color.Black);

				int healthWidth = (int)( e.Location.Width * (1.0 * unit.CurrentHealth / unit.MaxHealth));
				Color healthColor = Color.LimeGreen;

				if (1.0 * unit.CurrentHealth / unit.MaxHealth < 0.25)
				{
					healthColor = Color.Red;
				}
				else if (1.0 * unit.CurrentHealth / unit.MaxHealth < 0.5)
				{
					healthColor = Color.Yellow;
				}
				Rectangle healthRect = new Rectangle(e.Location.X, e.Location.Y + e.Location.Height - 8, healthWidth, 8);
				e.SpriteBatch.Draw(pixel, healthRect, healthColor);

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