using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ZRTSModel.Factories;
using System.Collections;
using ZRTS.InputEngines;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace ZRTS.XnaCompositeView
{
    public static class AudioManager
    {
        /// <summary>
        /// Initializes all the audio content.
        /// </summary>
        /// <param name="Content">The ContentManager to use</param>
        public static void Initialize(ContentManager Content)
        {
            Soldier_Attack = Content.Load<SoundEffect>("Audio/Soldier_Attack");
            Worker_Dying = Content.Load<SoundEffect>("Audio/Worker_Dying");
            Zombie_Dying = Content.Load<SoundEffect>("Audio/Zombie_Dying");
        }

        // Sound Effects
        public static SoundEffect Soldier_Attack;
        public static SoundEffect Worker_Dying;
        public static SoundEffect Zombie_Dying;
    }
}
