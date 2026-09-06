using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System;

namespace Project_The_Elect.source_code
{
    public class GameAudioManager
    {
        private float volume_bgm = 30f;
        private float volume_sfx = 100f;
        private float volume_fadeStep;
        private float volume_fadeDuration = 2f;

        private List<SoundEffect> _SFX;
        private Song bgm_home;

        private string prefix;

        public void LoadContent(ContentManager content)
        {
            _SFX = new List<SoundEffect>();
            //_SFX.Add(content.Load<SoundEffect>("audio/sound_effect"));
  
            prefix = "audio/01_bgm/";
            bgm_home = content.Load<Song>(prefix + "home_bgm");

        }
        public void PlayBGM(string bgmName)
        {

            if (bgmName == "home")
            {
                //MediaPlayer.Play(bgm_home);
                MediaPlayer.IsRepeating = true;
            }
        }

        public void PlayClick()
        {
            // Play sound effect based on the provided name
            // Example: if (sfxName == "jump") { _SFX[0].Play(volume_sfx, 0f, 0f); }
        }

        public void VolumeControl(float bgmVolume, float sfxVolume)
        {
            volume_bgm = bgmVolume;
            volume_sfx = sfxVolume;
            MediaPlayer.Volume = volume_bgm;
        }

    }
}
