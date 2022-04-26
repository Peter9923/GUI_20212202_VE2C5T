using GameLogic.Interfaces;
using GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic.Classes
{
    public class DisplayLogic : IDisplayLogic{
        SoundPlayer selectedClick;
        SoundPlayer anotherClick;

        public DisplayLogic(){

            this.SetSoundPlayers();
        }

        private void SetSoundPlayers()
        {
            selectedClick = new SoundPlayer();
            anotherClick = new SoundPlayer();
            selectedClick.SoundLocation = "Sounds\\SelectClick.wav";
            anotherClick.SoundLocation = "Sounds\\AnotherClick.wav";
        }

        public Point GetTilePos(Point mousePos)
        {
            return new Point(
                (int)((mousePos.X / Config.TileSize) - 1),
                (int)(mousePos.Y / Config.TileSize));
        }

        public void ClickSound(bool selection)
        {
            if (selection){
                selectedClick.Play();
            }
            else{
                anotherClick.Play();
            }

        }

    }
}
