using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameDisplay
{
    public class MyBrush{

        //Brushes
        public Brush KnightBrush;
        //public Brush EnemyKnightBrush;
        public List<Brush> ArcherBrushes;
        public Brush ArrowBrush;

        public List<Brush> GhostWalkingBrushes;
        public List<Brush> GhostAttackBrushes;
        public List<Brush> GhostDyingBrushes;

        public List<Brush> Ghost2WalkingBrushes;
        public List<Brush> Ghost2AttackBrushes;
        public List<Brush> Ghost2DyingBrushes;

        //
        public Brush TemporaryKnightBruesh;
        public Brush TemporaryArcherBrush;


        //Button Brushes
        public Brush DeployKnightBrush;
        public Brush DeployKnightSelectedBrush;
        public Brush DeployArcherBrush;
        public Brush DeployArcherSelectedBrush;

        public Brush MoveButtonBrush;
        public Brush MoveButtonSelectedBrush;
        public Brush RemoveButtonBrush;
        public Brush RemoveButtonSelectedBrush;
        public Brush UpgradeButtonBrush;
        public Brush UpgradeButtonSelectedBrush;

        //Backround Brushes
        public Brush GrassBrush;
        public Brush EnemyGrassBrush;
        public Brush CastleWallBrush;
        public Brush ButtonBackgroundBrush;

        public MyBrush(){
            SetBrushes();
        }

        public void SetBrushes() {
            KnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Knight.png", UriKind.RelativeOrAbsolute)));
            
            ArcherBrushes = new List<Brush>();
            for (int i = 0; i < 8; i++){
                ArcherBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\archer{i}.png", UriKind.RelativeOrAbsolute))));
            }
            ArrowBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Arrow.png", UriKind.RelativeOrAbsolute)));

            TemporaryKnightBruesh = new ImageBrush(new BitmapImage(new Uri("Images\\Knight.png", UriKind.RelativeOrAbsolute)));
            TemporaryKnightBruesh.Opacity = 0.5;
            TemporaryArcherBrush = new ImageBrush(new BitmapImage(new Uri($"Images\\archer0.png", UriKind.RelativeOrAbsolute)));
            TemporaryArcherBrush.Opacity = 0.5;

            DeployKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnight.png", UriKind.RelativeOrAbsolute)));
            DeployKnightSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnightSelected.png", UriKind.RelativeOrAbsolute)));

            DeployArcherBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployArcher.png", UriKind.RelativeOrAbsolute)));
            DeployArcherSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployArcherSelected.png", UriKind.RelativeOrAbsolute)));


            //ENemies
            //EnemyKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\EnemyKnight.png", UriKind.RelativeOrAbsolute)));
            GhostWalkingBrushes = new List<Brush>();
            for (int i = 0; i < 12; i++){
                GhostWalkingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\ghostWalk{i}.png", UriKind.RelativeOrAbsolute))));
            }

            GhostAttackBrushes = new List<Brush>();
            for (int i = 0; i < 12; i++){
                GhostAttackBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\ghostAttack{i}.png", UriKind.RelativeOrAbsolute))));
            }

            GhostDyingBrushes = new List<Brush>();
            for (int i = 0; i < 15; i++){
                GhostDyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\ghostDying{i}.png", UriKind.RelativeOrAbsolute))));
            }


            Ghost2WalkingBrushes = new List<Brush>();
            for (int i = 0; i < 11; i++)
            {
                Ghost2WalkingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\ghost2Walk{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Ghost2AttackBrushes = new List<Brush>();
            for (int i = 0; i < 11; i++)
            {
                Ghost2AttackBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\ghost2Attack{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Ghost2DyingBrushes = new List<Brush>();
            for (int i = 0; i < 15; i++){
                Ghost2DyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\ghost2Dying{i}.png", UriKind.RelativeOrAbsolute))));
            }


            MoveButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Move.png", UriKind.RelativeOrAbsolute)));
            MoveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\MoveSelected.png", UriKind.RelativeOrAbsolute)));
            RemoveButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Remove.png", UriKind.RelativeOrAbsolute)));
            RemoveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\RemoveSelected.png", UriKind.RelativeOrAbsolute)));
            UpgradeButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Upgrade.png", UriKind.RelativeOrAbsolute)));
            UpgradeButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\UpgradeSelected.png", UriKind.RelativeOrAbsolute)));

            GrassBrush = new ImageBrush(new BitmapImage(new Uri("Images\\grass.png", UriKind.RelativeOrAbsolute)));
            EnemyGrassBrush = new ImageBrush(new BitmapImage(new Uri("Images\\EnemyGrass.png", UriKind.RelativeOrAbsolute)));
            CastleWallBrush = new ImageBrush(new BitmapImage(new Uri("Images\\CastleWall.png", UriKind.RelativeOrAbsolute)));
            ButtonBackgroundBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\gallery.png", UriKind.RelativeOrAbsolute)));
        }
    }
}
