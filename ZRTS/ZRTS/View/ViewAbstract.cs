using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZRTSModel.Entities;

namespace ZRTS.View
{
    /// <summary>
    /// ViewAbstract
    /// 
    /// This abstract class will provide common methods and properties for all inherited classes.
    /// </summary>
    public abstract class ViewAbstract
    {
        public int width, height;
        protected SpriteSheet sheet;


        /// <summary>
        /// Load spriteSheet
        /// </summary>
        public virtual void loadSheet(SpriteSheet sheet)
        {
            this.sheet = sheet;
        }

        /// <summary>
        /// Draw
        /// </summary>
        public virtual void Draw()
        {

        }

        /// <summary>
        /// Translate X screen to game X - location 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual float translateXScreen(float x)
        {
            return x;
        }

        /// <summary>
        ///  Translate Y screen to game Y - location 
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        protected virtual float translateYScreen(float y)
        {
            return y;
        }

        /// <summary>
        /// Update
        /// </summary>
        public virtual void update()
        {

        }
    }
}
