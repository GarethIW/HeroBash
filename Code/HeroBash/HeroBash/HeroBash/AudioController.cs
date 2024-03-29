using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBash
{
    static class AudioController
    {
        static Dictionary<string, SoundEffect> SFX = new Dictionary<string, SoundEffect>();

        public static Random randomNumber = new Random();

        public static void LoadContent(ContentManager content)
        {
            SFX.Add("jump", content.Load<SoundEffect>("sfx/jump"));
            SFX.Add("fireball", content.Load<SoundEffect>("sfx/fireball"));
            SFX.Add("herohurt", content.Load<SoundEffect>("sfx/herohurt"));
            SFX.Add("crush", content.Load<SoundEffect>("sfx/crush"));
            SFX.Add("fall", content.Load<SoundEffect>("sfx/fall"));
            SFX.Add("minionswordhit", content.Load<SoundEffect>("sfx/minionswordhit"));
            SFX.Add("swordstuck", content.Load<SoundEffect>("sfx/swordstuck"));
            SFX.Add("swordthrow", content.Load<SoundEffect>("sfx/swordthrow"));
            SFX.Add("win", content.Load<SoundEffect>("sfx/win"));
            SFX.Add("lose", content.Load<SoundEffect>("sfx/lose"));
            SFX.Add("levelup", content.Load<SoundEffect>("sfx/levelup"));
            SFX.Add("freeze", content.Load<SoundEffect>("sfx/freeze"));
        }

        public static void PlaySFX(string key)
        {
            PlaySFX(key, 0f, 1f, 0f);
        }
        public static void PlaySFX(string key, float pitch)
        {
            PlaySFX(key, pitch, 1f, 0f);
        }
        public static void PlaySFX(string key, float pitch, float volume)
        {
            PlaySFX(key, pitch, volume, 0f);
        }
        public static void PlaySFX(string key, float pitch, float volume, float pan)
        {
            SFX[key].Play(volume, pitch, pan);
        }
    }
}
