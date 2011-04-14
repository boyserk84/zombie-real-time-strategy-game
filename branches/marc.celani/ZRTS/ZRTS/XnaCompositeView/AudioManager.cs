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
    /// <summary>
    /// Handle and manage all audio effect associated with the gameplay
    /// </summary>
    public static class AudioManager
    {
        /// <summary>
        /// Initializes all the audio content.
        /// </summary>
        /// <param name="Content">The ContentManager to use</param>
        public static void Initialize(ContentManager Content)
        {
            Soldier_Attack = Content.Load<SoundEffect>("audio/Soldier_Attack");
            Worker_Dying = Content.Load<SoundEffect>("audio/Worker_Dying");
            Zombie_Dying = Content.Load<SoundEffect>("audio/Zombie_Dying");
        }

        // Sound Effects
        public static SoundEffect Soldier_Attack;
        public static SoundEffect Worker_Dying;
        public static SoundEffect Zombie_Dying;

        /// <summary>
        /// Playing sound effect based on action and unit types
        /// </summary>
        /// <param name="type">Action Type</param>
        /// <param name="subtype">Unit type</param>
        public static void playSound(string type, string subtype)
        {
            if (type.Equals("dead"))
            {
                if (subtype.Equals("zombie"))
                {
                    Zombie_Dying.Play(0.10f, 0, 0);
                }
                else if (subtype.Equals("worker"))
                {
                    Worker_Dying.Play(0.10f, 0, 0);
                }
                else if (subtype.Equals("soldier"))
                {

                }
            }
            else if (type.Equals("attack"))
            {
                if (subtype.Equals("zombie"))
                {

                }
                else if (subtype.Equals("worker"))
                {

                }
                else if (subtype.Equals("soldier"))
                {
                    Soldier_Attack.Play(0.10f, 0, 0);
                }
            }
        }
        
    }
}
