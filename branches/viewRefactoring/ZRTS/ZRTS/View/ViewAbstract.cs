using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ZRTSModel.Entities;

namespace ZRTS.View
{
    public abstract class ViewAbstract
    {
        public int width, height;


        public virtual void loadSheet()
        {

        }

        public virtual void draw()
        {

        }

        public virtual float translateXScreen(float x)
        {
            return x;
        }

        public virtual float translateYScreen(float y)
        {
            return y;
        }

        public virtual void update()
        {

        }
    }
}
