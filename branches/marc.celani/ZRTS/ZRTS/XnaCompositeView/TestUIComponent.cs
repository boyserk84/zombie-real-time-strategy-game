﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace ZRTS.XnaCompositeView
{
    public class TestUIComponent : XnaUIComponent
    {
        private Color color;

        public TestUIComponent(Game game, Color color)
            : base(game)
        {
            this.color = color;
        }

        protected override void onDraw(XnaDrawArgs e)
        {
            e.SpriteBatch.Draw(((XnaUITestGame)Game).SpriteSheet, e.Location, color);
        }
    }
}