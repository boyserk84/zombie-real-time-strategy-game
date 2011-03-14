using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZRTS;

namespace ZRTS
{
    public class ViewGamePlayMenu
    {

        private SpriteSheet gamePlaySprite;
        private SpriteSheet gamePlayIconSprite;

        protected ZRTSModel.Scenario.Scenario scenario;

        private int width, height;                  // Width and Height of the screen
        private Microsoft.Xna.Framework.Vector2 location;
        private Microsoft.Xna.Framework.Vector2 iconLocation;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">Screen width</param>
        /// <param name="height">Screen height</param>
        public ViewGamePlayMenu(int width, int height, SpriteSheet sheet)
        {
            this.width = width;
            this.height = height;
            location = Microsoft.Xna.Framework.Vector2.Zero;
            loadGamePlaySprite(sheet);
            location.X = width - gamePlaySprite.frameDimX;
            location.Y = height - gamePlaySprite.frameDimY;
            
        }


        /// <summary>
        /// Load sprite sheet of the menu
        /// </summary>
        /// <param name="sheet"></param>
        public void loadGamePlaySprite(SpriteSheet sheet)
        {
            gamePlaySprite = sheet;

            iconLocation = Microsoft.Xna.Framework.Vector2.Zero;           
            iconLocation.X = width - (.91f*gamePlaySprite.frameDimX);
            iconLocation.Y = height - (.82f*gamePlaySprite.frameDimY);
        }

        /// <summary>
        /// Load sprite sheet for all icons corresponding to all type of entities
        /// </summary>
        /// <param name="sheet"></param>
        public void loadIconSprite(SpriteSheet sheet)
        {
            gamePlayIconSprite = sheet;
        }

        /// <summary>
        /// Activate Entity Menu (based on entity type)
        /// NEED TO REIMPLEMENT FOR THE FUTURE TO ACCOMODATE ENTITY TYPE
        /// </summary>
        public void activateEntityMenu()
        {
            
        }

        public void deactivateEntityMenu()
        {

        }

        /// <summary>
        /// Loading game scenario object for process
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScenario(ZRTSModel.Scenario.Scenario scene)
        {
            this.scenario = scene;
        }


        /// <summary>
        /// Draw
        /// </summary>
        public void Draw()
        {
            gamePlaySprite.drawAtCurrentIndex(location);
            if(scenario.getPlayer().SelectedEntities.Count!=0)
                gamePlayIconSprite.drawAtCurrentIndex(iconLocation);
        }

    }
}
