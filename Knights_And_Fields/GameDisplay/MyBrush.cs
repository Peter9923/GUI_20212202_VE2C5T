using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameDisplay
{
    public class MyBrush{

        //Brushes
        //Allieds
        public List<Brush> KnightBrushes;
        //public List<Brush> KnightWaitingBrushes;
        public List<Brush> KnightDieBrushes;
        public Brush TemporaryKnightBrush;


        public List<Brush> ArcherBrushes;
        public Brush ArrowBrush;
        public List<Brush> ArcherDieBrushes;
        public Brush TemporaryArcherBrush;

        public List<Brush> WallBrushes;
        public Brush TemporaryWallBrush;


        //enemies
        public List<Brush> GhostWalkingBrushes;
        public List<Brush> GhostAttackBrushes;
        public List<Brush> GhostDyingBrushes;

        public List<Brush> Ghost2WalkingBrushes;
        public List<Brush> Ghost2AttackBrushes;
        public List<Brush> Ghost2DyingBrushes;

        public List<Brush> Ghost3WalkingBrushes;
        public List<Brush> Ghost3AttackBrushes;
        public List<Brush> Ghost3DyingBrushes;

        public List<Brush> Orc_1_WalkingBrushes;
        public List<Brush> Orc_1_AttackBrushes;
        public List<Brush> Orc_1_DyingBrushes;

        public List<Brush> Orc_2_WalkingBrushes;
        public List<Brush> Orc_2_AttackBrushes;
        public List<Brush> Orc_2_DyingBrushes;

        public List<Brush> Orc_3_WalkingBrushes;
        public List<Brush> Orc_3_AttackBrushes;
        public List<Brush> Orc_3_DyingBrushes;


        //Button Brushes
        public Brush DeployKnightBrush;
        public Brush DeployKnightSelectedBrush;
        public Brush DeployArcherBrush;
        public Brush DeployArcherSelectedBrush;
        public Brush DeployWallBrush;
        public Brush DeployWallSelectedBrush;

        public Brush MoveButtonBrush;
        public Brush MoveButtonSelectedBrush;
        public Brush RemoveButtonBrush;
        public Brush RemoveButtonSelectedBrush;
        public Brush UpgradeButtonBrush;
        public Brush UpgradeButtonSelectedBrush;

        public Brush SaveButtondBrush;
        public Brush SaveButtonSelectedBrush;

        //Backround Brushes
        public Brush GrassBrush;
        public Brush EnemyGrassBrush;
        public Brush CastleWallBrush;
        public Brush ButtonBackgroundBrush;
        public Brush ButtonBackgroundTopBrush;

        public MyBrush(){
            SetBrushes();
        }

        public void SetBrushes() {
            this.SetAlliedBrushes();
            this.SetEnemyBrushes();
            this.AnotherBrushes();
        }


        private void SetAlliedBrushes() {
            KnightBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                KnightBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Knight3\\Knight_03__ATTACK_00{i}_prev_ui.png", UriKind.RelativeOrAbsolute))));
            }
            //KnightWaitingBrushes = new List<Brush>();
            //for (int i = 0; i < 5; i++){
            //    KnightWaitingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Knight3\\Knight_03__IDLE_00{i}_prev_ui.png", UriKind.RelativeOrAbsolute))));
            //}
            KnightDieBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                KnightDieBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Knight3\\Knight_03__DIE_00{i}_prev_ui.png", UriKind.RelativeOrAbsolute))));
            }

            WallBrushes = new List<Brush>();
            for (int i = 0; i < 1; i++)
            {
                WallBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Wall\\Wall{i}.png", UriKind.RelativeOrAbsolute))));
            }

            ArcherBrushes = new List<Brush>();
            for (int i = 0; i < 6; i++)
            {
                ArcherBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Archer\\Warrior_03__ATTACK_00{i}.png", UriKind.RelativeOrAbsolute))));
            }
            ArrowBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Arrow.png", UriKind.RelativeOrAbsolute)));

            ArcherDieBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                ArcherDieBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Archer\\Warrior_03__DIE_00{i}.png", UriKind.RelativeOrAbsolute))));
            }


            TemporaryKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Allied\\Knight3\\Knight_03__ATTACK_000_prev_ui.png", UriKind.RelativeOrAbsolute)));
            TemporaryKnightBrush.Opacity = 0.5;

            TemporaryArcherBrush = new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Archer\\Warrior_03__ATTACK_000.png", UriKind.RelativeOrAbsolute)));
            TemporaryArcherBrush.Opacity = 0.5;

            TemporaryWallBrush = new ImageBrush(new BitmapImage(new Uri($"Images\\Allied\\Wall\\Wall.png", UriKind.RelativeOrAbsolute)));
            TemporaryWallBrush.Opacity = 0.5;

            DeployKnightBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnight.png", UriKind.RelativeOrAbsolute)));
            DeployKnightBrush.Opacity = 0.7;
            DeployKnightSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployKnightSelected.png", UriKind.RelativeOrAbsolute)));

            DeployArcherBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployArcher.png", UriKind.RelativeOrAbsolute)));
            DeployArcherBrush.Opacity = 0.7;
            DeployArcherSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployArcherSelected.png", UriKind.RelativeOrAbsolute)));

            DeployWallBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployWall.png", UriKind.RelativeOrAbsolute)));
            DeployWallBrush.Opacity = 0.7;
            DeployWallSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\DeployWallSelected.png", UriKind.RelativeOrAbsolute)));

        }

        private void SetEnemyBrushes() {
            GhostWalkingBrushes = new List<Brush>();
            for (int i = 0; i < 12; i++)
            {
                GhostWalkingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost1\\ghostWalk{i}.png", UriKind.RelativeOrAbsolute))));
            }

            GhostAttackBrushes = new List<Brush>();
            for (int i = 0; i < 12; i++)
            {
                GhostAttackBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost1\\ghostAttack{i}.png", UriKind.RelativeOrAbsolute))));
            }

            GhostDyingBrushes = new List<Brush>();
            for (int i = 0; i < 15; i++)
            {
                GhostDyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost1\\ghostDying{i}.png", UriKind.RelativeOrAbsolute))));
            }


            Ghost2WalkingBrushes = new List<Brush>();
            for (int i = 0; i < 11; i++)
            {
                Ghost2WalkingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost2\\ghost2Walk{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Ghost2AttackBrushes = new List<Brush>();
            for (int i = 0; i < 11; i++)
            {
                Ghost2AttackBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost2\\ghost2Attack{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Ghost2DyingBrushes = new List<Brush>();
            for (int i = 0; i < 15; i++)
            {
                Ghost2DyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost2\\ghost2Dying{i}.png", UriKind.RelativeOrAbsolute))));
            }


            Ghost3WalkingBrushes = new List<Brush>();
            for (int i = 0; i < 12; i++)
            {
                Ghost3WalkingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost3\\ghost3Walk{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Ghost3AttackBrushes = new List<Brush>();
            for (int i = 0; i < 12; i++)
            {
                Ghost3AttackBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost3\\Wraith_03_Attack_{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Ghost3DyingBrushes = new List<Brush>();
            for (int i = 0; i < 15; i++)
            {
                Ghost3DyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Ghost3\\Wraith_03_Dying_{i}.png", UriKind.RelativeOrAbsolute))));
            }


            Orc_1_AttackBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++){
                Orc_1_AttackBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Orc1\\ORK_01_ATTAK_00{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Orc_1_DyingBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++){
                Orc_1_DyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Orc1\\ORK_01_DIE_00{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Orc_1_WalkingBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++){
                Orc_1_WalkingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Orc1\\ORK_01_WALK_00{i}.png", UriKind.RelativeOrAbsolute))));
            }


            Orc_2_AttackBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                var fullBitmap = new BitmapImage(new Uri($"Images\\Enemy\\Orc2\\ORK_02_ATTAK_00{i}.png", UriKind.RelativeOrAbsolute));
                CroppedBitmap cb = new CroppedBitmap(fullBitmap, new Int32Rect(571, 359, 779, 779));
                Orc_2_AttackBrushes.Add(new ImageBrush(cb));
            }

            Orc_2_DyingBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                //var fullBitmap = new BitmapImage(new Uri($"Images\\Enemy\\Orc2\\ORK_02_DIE_00{i}.png", UriKind.RelativeOrAbsolute));
                //CroppedBitmap cb = new CroppedBitmap(fullBitmap, new Int32Rect(279, 301, 949 , 949));
                //Orc_2_DyingBrushes.Add(new ImageBrush(cb));
                Orc_2_DyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Orc2\\ORK_02_DIE_00{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Orc_2_WalkingBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                var fullBitmap = new BitmapImage(new Uri($"Images\\Enemy\\Orc2\\ORK_02_WALK_00{i}.png", UriKind.RelativeOrAbsolute));
                CroppedBitmap cb = new CroppedBitmap(fullBitmap, new Int32Rect(571, 359, 779, 779));
                Orc_2_WalkingBrushes.Add(new ImageBrush(cb));
                
            }

            Orc_3_AttackBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                var fullBitmap = new BitmapImage(new Uri($"Images\\Enemy\\Orc3\\ORK_03_ATTAK_00{i}.png", UriKind.RelativeOrAbsolute));
                CroppedBitmap cb = new CroppedBitmap(fullBitmap, new Int32Rect(571, 359, 779, 779));
                Orc_3_AttackBrushes.Add(new ImageBrush(cb));
            }

            Orc_3_DyingBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                //var fullBitmap = new BitmapImage(new Uri($"Images\\Enemy\\Orc2\\ORK_02_DIE_00{i}.png", UriKind.RelativeOrAbsolute));
                //CroppedBitmap cb = new CroppedBitmap(fullBitmap, new Int32Rect(279, 301, 949 , 949));
                //Orc_2_DyingBrushes.Add(new ImageBrush(cb));
                Orc_3_DyingBrushes.Add(new ImageBrush(new BitmapImage(new Uri($"Images\\Enemy\\Orc3\\ORK_03_DIE_00{i}.png", UriKind.RelativeOrAbsolute))));
            }

            Orc_3_WalkingBrushes = new List<Brush>();
            for (int i = 0; i < 10; i++)
            {
                var fullBitmap = new BitmapImage(new Uri($"Images\\Enemy\\Orc3\\ORK_03_WALK_00{i}.png", UriKind.RelativeOrAbsolute));
                CroppedBitmap cb = new CroppedBitmap(fullBitmap, new Int32Rect(571, 359, 779, 779));
                Orc_3_WalkingBrushes.Add(new ImageBrush(cb));

            }
        }

        private void AnotherBrushes() {
            MoveButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Move.png", UriKind.RelativeOrAbsolute)));
            MoveButtonBrush.Opacity = 0.7;
            MoveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\MoveSelected.png", UriKind.RelativeOrAbsolute)));
            RemoveButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Remove.png", UriKind.RelativeOrAbsolute)));
            RemoveButtonBrush.Opacity = 0.7;
            RemoveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\RemoveSelected.png", UriKind.RelativeOrAbsolute)));
            UpgradeButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Upgrade.png", UriKind.RelativeOrAbsolute)));
            UpgradeButtonBrush.Opacity = 0.7;
            UpgradeButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\UpgradeSelected.png", UriKind.RelativeOrAbsolute)));

            GrassBrush = new ImageBrush(new BitmapImage(new Uri("Images\\grass.png", UriKind.RelativeOrAbsolute)));
            EnemyGrassBrush = new ImageBrush(new BitmapImage(new Uri("Images\\EnemyGrass.png", UriKind.RelativeOrAbsolute)));
            CastleWallBrush = new ImageBrush(new BitmapImage(new Uri("Images\\CastleWall.png", UriKind.RelativeOrAbsolute)));
            ButtonBackgroundBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\gallery.png", UriKind.RelativeOrAbsolute)));
            ButtonBackgroundTopBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\galleryForTopButtons.png", UriKind.RelativeOrAbsolute)));

            SaveButtondBrush = new ImageBrush(new BitmapImage(new Uri("Images\\saveButton.png", UriKind.RelativeOrAbsolute)));
            SaveButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\saveButtonSelected.png", UriKind.RelativeOrAbsolute)));
        }


    }
}
