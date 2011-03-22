using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTSModel;
using Microsoft.Xna.Framework;

namespace ZRTS.XnaCompositeView
{
    public class ZRTSCompositeViewUIFactory
    {
        private static ZRTSCompositeViewUIFactory instance;
        private Game game;

        private ZRTSCompositeViewUIFactory()
        {
            
        }

        private ZRTSCompositeViewUIFactory(Game game)
        {
            // TODO: Initialize based on the xml files.
            this.game = game;
        }

        public static void Initialize(Game game)
        {
            if (instance == null)
            {
                instance = new ZRTSCompositeViewUIFactory(game);
            }
        }
        public static ZRTSCompositeViewUIFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public SelectedEntityUI BuildSelectedEntityUI(UnitComponent unit)
        {
            SelectedEntityUI seui = new SelectedEntityUI(game, unit);
            seui.DrawBox = new Rectangle(0, 0, 50, 50);

            // Add the HP Bar to the UI.
            HPBar hpBar = new HPBar(game);
            hpBar.DrawBox = new Rectangle(5, 42, 40, 5);

            seui.AddChild(hpBar);
            return seui;
        }
    }
}
