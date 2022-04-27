using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameModel
{
    public class MySounds
    {
        public SoundPlayer selectedClick;
        public SoundPlayer anotherClick;

        public MediaPlayer ArrowSound { get; set; }
        public MediaPlayer GhostDied { get; set; }
        public MediaPlayer GhostKilledAlliedUnit { get; set; }
        public List<MediaPlayer> BackgroundMusics { get; set; }

        public MySounds()
        {
            SetSounds();
        }

        private void SetSounds()
        {
            selectedClick = new SoundPlayer();
            anotherClick = new SoundPlayer();
            selectedClick.SoundLocation = "Sounds\\SelectClick.wav";
            anotherClick.SoundLocation = "Sounds\\AnotherClick.wav";

            ArrowSound = new MediaPlayer();
            ArrowSound.Volume = 0.5f;

            GhostDied = new MediaPlayer();
            GhostDied.Volume = 0.8f;

            GhostKilledAlliedUnit = new MediaPlayer();
            GhostKilledAlliedUnit.Volume = 0.8f;

            BackgroundMusics = new List<MediaPlayer>();
            MediaPlayer background1 = new MediaPlayer();
            background1.Volume = 0.3f;
            BackgroundMusics.Add(background1);

        }
    }
}
