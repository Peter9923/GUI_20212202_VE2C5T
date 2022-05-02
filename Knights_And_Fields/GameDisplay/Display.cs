using GameLogic.Interfaces;
using GameModel;
using GameModel.Interfaces;
using GameModel.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GameDisplay
{
    public class Display : FrameworkElement{

        private Window win;
        IModel model;
        IDisplayLogic displayLogic;
        IButtonLogic buttonLogic;
        IAlliedLogic alliedLogic;
        IEnemyLogic enemyLogic;

        MyBrush BRUSHES;

        Stopwatch _alliedAnimationStopWatch;
        Stopwatch _bulletsMoveStopWatch;
        Stopwatch _alliedMeleeAttackStopWatch;

        Stopwatch _enemySpawnerStopWatch;
        Stopwatch _enemyMoveStopWatch;
        Stopwatch _enemyAnimationStopWatch;

        Stopwatch _attackStopwatch;

        Stopwatch _dyingStopwatch;
        Stopwatch _giveGoldStopwatch;


        Stopwatch _waveDrawAnimation;


        Stopwatch _backGroundMusicStopwatch;

        List<Geometry> ButtonsGeometry;

        bool EXIT = false;
        bool shouldDrawWave = false;
        int waweIndex = 1;
        bool clickedSave;

        Point MovedMouseTilePos;
        Point MovedUnitPrevPos;

        public Display(IModel model, IDisplayLogic displayLogic, IButtonLogic buttonLogic, IAlliedLogic alliedLogic, IEnemyLogic enemyLogic)
        {
            this.model = model;
            this.displayLogic = displayLogic;
            this.buttonLogic = buttonLogic;
            this.alliedLogic = alliedLogic;
            this.enemyLogic = enemyLogic;


            BRUSHES = new MyBrush();
            this.SetStopWatches();
            ButtonsGeometry = new List<Geometry>();
            CreatDrawButtonsGeometries();

            this.Loaded += Display_Loaded;
        }

        //LOAD
        private void Display_Loaded(object sender, RoutedEventArgs e)
        {
            this.win = Window.GetWindow(this);

            if (this.win != null)
            {
                this.MouseDown += Display_MouseDown;
                win.Closed += Win_Closed;
                CompositionTarget.Rendering += CompositionTarget_Rendering;
            }

            this.model.SOUNDS.BackgroundMusics[0].Open(new Uri("Sounds\\backgroundMusic.mp3", UriKind.RelativeOrAbsolute));
            this.model.SOUNDS.BackgroundMusics[0].Play();
            this.InvalidateVisual();
        }

        private void Win_Closed(object? sender, EventArgs e)
        {
            foreach (var item in this.model.SOUNDS.BackgroundMusics)
            {
                item.Stop();
            }
            EXIT = true;
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e){
            if (EXIT == false){
                MovedMouseTilePos = this.displayLogic.GetTilePos(this.PointToScreen(Mouse.GetPosition(this)));
            }


            this.MeleeAnimation();
            this.AlliedAnimation();
            this.BulletsMoving();
            this.SpawnAnEnemy();
            this.EnemyMove();
            this.EnemyAnimation();
            this.DyingItemsAnimation();
            this.GetGold();

            if (_backGroundMusicStopwatch.ElapsedMilliseconds >= 114000){
                this.model.SOUNDS.BackgroundMusics[0].Open(new Uri("Sounds\\backgroundMusic.mp3", UriKind.RelativeOrAbsolute));
                this.model.SOUNDS.BackgroundMusics[0].Play();

                _backGroundMusicStopwatch.Stop();
                _backGroundMusicStopwatch.Reset();
                _backGroundMusicStopwatch.Start();
            }

            this.InvalidateVisual();
        }

        private void SetStopWatches(){
            _attackStopwatch = new Stopwatch();
            _alliedAnimationStopWatch = new Stopwatch();
            _bulletsMoveStopWatch = new Stopwatch();
            _enemyMoveStopWatch = new Stopwatch();
            _enemySpawnerStopWatch = new Stopwatch();
            _enemyAnimationStopWatch = new Stopwatch();
            _dyingStopwatch = new Stopwatch();
            _giveGoldStopwatch = new Stopwatch();
            _backGroundMusicStopwatch = new Stopwatch();
            _waveDrawAnimation = new Stopwatch();
            _alliedMeleeAttackStopWatch = new Stopwatch();

            _attackStopwatch.Start();
            _alliedAnimationStopWatch.Start();
            _bulletsMoveStopWatch.Start();
            _enemyMoveStopWatch.Start();
            _enemySpawnerStopWatch.Start();
            _enemyAnimationStopWatch.Start();
            _dyingStopwatch.Start();
            _giveGoldStopwatch.Start();
            _backGroundMusicStopwatch.Start();
            _alliedMeleeAttackStopWatch.Start();
        }


        private void Display_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e){

            Point mousePos = e.GetPosition(this);
            Point tilePos = this.displayLogic.GetTilePos(mousePos);
            bool nowCLicked = false;
            //mouseMoved = false;

            //which button clicked if clicked
            if (tilePos.X == this.model.Map[0].Length)
            {
                nowCLicked = true;
                if (tilePos.Y == 0)
                {
                    if (this.model.DeployKnight)
                    {
                        this.model.DeployKnight = false;
                    }
                    else
                    {
                        this.model.DeployKnight = true;
                        this.model.RemoveUnit = false;
                        this.model.MoveUnit = false;
                        this.model.UpgradeUnit = false;
                        this.model.DeployArcher = false;
                        this.model.SelectedSave = false;
                        this.model.DeployWall = false;
                    }
                }
                if (tilePos.Y == 1)
                {
                    if (this.model.DeployArcher)
                    {
                        this.model.DeployArcher = false;
                    }
                    else
                    {
                        this.model.DeployArcher = true;
                        this.model.RemoveUnit = false;
                        this.model.MoveUnit = false;
                        this.model.UpgradeUnit = false;
                        this.model.DeployKnight = false;
                        this.model.SelectedSave = false;
                        this.model.DeployWall = false;
                    }
                }
                if (tilePos.Y == 2)
                {
                    if (this.model.DeployWall)
                    {
                        this.model.DeployWall = false;
                    }
                    else
                    {
                        this.model.DeployWall = true;
                        this.model.RemoveUnit = false;
                        this.model.MoveUnit = false;
                        this.model.UpgradeUnit = false;
                        this.model.DeployKnight = false;
                        this.model.DeployArcher = false;
                        this.model.SelectedSave = false;
                    }
                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 2 && tilePos.Y == 5)
            {
                nowCLicked = true;
                if (this.model.MoveUnit)
                {
                    this.model.MoveUnit = false;
                }
                else
                {
                    MovedUnitPrevPos = new Point(-1, -1);

                    this.model.MoveUnit = true;
                    this.model.DeployKnight = false;
                    this.model.RemoveUnit = false;
                    this.model.UpgradeUnit = false;
                    this.model.DeployArcher = false;
                    this.model.SelectedSave = false;
                    this.model.DeployWall = false;
                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 1 && tilePos.Y == 5)
            {
                nowCLicked = true;
                if (this.model.RemoveUnit)
                {
                    this.model.RemoveUnit = false;
                }
                else
                {
                    this.model.RemoveUnit = true;
                    this.model.DeployKnight = false;
                    this.model.MoveUnit = false;
                    this.model.UpgradeUnit = false;
                    this.model.DeployArcher = false;
                    this.model.SelectedSave = false;
                    this.model.DeployWall = false;
                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 3 && tilePos.Y == 5)
            {
                nowCLicked = true;
                if (this.model.UpgradeUnit)
                {
                    this.model.UpgradeUnit = false;
                }
                else
                {
                    this.model.UpgradeUnit = true;
                    this.model.DeployKnight = false;
                    this.model.MoveUnit = false;
                    this.model.RemoveUnit = false;
                    this.model.DeployArcher = false;
                    this.model.SelectedSave = false;
                    this.model.DeployWall = false;
                }
            }
            else if (tilePos.X == this.model.Map[0].Length + 1 && tilePos.Y == 1) {
                nowCLicked = true;
                if (this.model.SelectedSave)
                {
                    this.model.SelectedSave = false;
                }
                else
                {
                    this.model.SelectedSave = true;
                    this.model.UpgradeUnit = false;
                    this.model.DeployKnight = false;
                    this.model.MoveUnit = false;
                    this.model.RemoveUnit = false;
                    this.model.DeployArcher = false;
                    this.model.DeployWall = false;
                }
            }


            //click sound

            if (nowCLicked){
                this.model.SOUNDS.selectedClick.Play();
            }
            else
            {
                this.model.SOUNDS.anotherClick.Play();
            }


           
            if (!nowCLicked && this.model.DeployKnight)
            {
                this.buttonLogic.DeployUnit((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployKnight = false;
            }
            else if (!nowCLicked && this.model.DeployArcher)
            {
                this.buttonLogic.DeployUnit((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployArcher = false;
            }
            else if (!nowCLicked && this.model.DeployWall)
            {
                this.buttonLogic.DeployUnit((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployWall = false;
            }
            else if (!nowCLicked && this.model.RemoveUnit)
            {
                this.buttonLogic.RemoveUnit((int)tilePos.X, (int)tilePos.Y);
                this.model.RemoveUnit = false;
            }
            else if (!nowCLicked && this.model.UpgradeUnit)
            {
                this.buttonLogic.UpgradeUnit((int)tilePos.X, (int)tilePos.Y);
                this.model.UpgradeUnit = false;
            }
            else if (!nowCLicked && this.model.MoveUnit)
            {
                if (MovedUnitPrevPos.X != -1)
                {
                    this.buttonLogic.MoveUnit((int)tilePos.X, (int)tilePos.Y, (int)MovedUnitPrevPos.X, (int)MovedUnitPrevPos.Y);
            
                    this.model.MoveUnit = false;
                    MovedUnitPrevPos = new Point(-1, -1);
                }
                else
                { // == -1
                    MovedUnitPrevPos = this.displayLogic.GetTilePos(mousePos);
                }
            }
            else if (nowCLicked && this.model.SelectedSave)
            {
                clickedSave = true;
                string jsonData = JsonConvert.SerializeObject(this.model);
                var a = Directory.GetCurrentDirectory() + "\\Saves\\";
                var path = Path.Combine(a, $"{this.model.PlayerName}_{DateTime.Now.ToShortDateString()}_{this.model.Score}.json");
                File.WriteAllText(path, jsonData);
                this.model.SelectedSave = false;
            }


        }




        private void MeleeAnimation() {
            if (_alliedMeleeAttackStopWatch.ElapsedMilliseconds >= 80)
            {
                this.alliedLogic.CheckWhichAlliedShouldAttack();

                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    for (int x = 0; x < this.model.Map[y].Length; x++)
                    {
                        if (this.model.Map[y][x] is Knight knight){
                            if (knight.ShouldAttack){
                                knight.AttackAnimationIndex++;

                                if (knight.AttackAnimationIndex == BRUSHES.KnightBrushes.Count){
                                    this.alliedLogic.AlliedAttackAnEnemy(knight);
                                    knight.AttackAnimationIndex = 0;
                                }
                            }
                        }
                    }
                }

                _alliedMeleeAttackStopWatch.Stop();
                _alliedMeleeAttackStopWatch.Reset();
                _alliedMeleeAttackStopWatch.Start();
            }

        }

        private void AlliedAnimation() {
            if (_alliedAnimationStopWatch.ElapsedMilliseconds >= 200)
            {
                for (int y = 0; y < this.model.Map.Length; y++)
                {
                    for (int x = 0; x < this.model.Map[y].Length; x++)
                    {
                        if (this.model.Map[y][x] is Archer archer)
                        {
                            if (archer.AttackAnimationIndex == this.BRUSHES.ArcherBrushes.Count - 1)
                            {
                                int arrowCount = archer.Arrows.Count;
                                archer.AttackAnimationIndex = 0;
                                this.alliedLogic.GenerateArrow(x, y);
                                if (arrowCount < 2)
                                {
                                    this.model.SOUNDS.ArrowSound.Open(new Uri("Sounds\\arrowSound.wav", UriKind.RelativeOrAbsolute));
                                    this.model.SOUNDS.ArrowSound.Play();
                                }
                            }
                            else
                            {
                                archer.AttackAnimationIndex++;
                            }
                        }
                    }
                }
                _alliedAnimationStopWatch.Stop();
                _alliedAnimationStopWatch.Reset();
                _alliedAnimationStopWatch.Start();
            }
        }

        private void BulletsMoving() {
            if (_bulletsMoveStopWatch.ElapsedMilliseconds >= 33)
            {
                this.alliedLogic.BulletsMoving();

                _bulletsMoveStopWatch.Stop();
                _bulletsMoveStopWatch.Reset();
                _bulletsMoveStopWatch.Start();
            }
        }

        private void SpawnAnEnemy() {
            if (_enemySpawnerStopWatch.ElapsedMilliseconds >= 3000)
            {
                shouldDrawWave = this.enemyLogic.SpawnAnEnemy();

                if (shouldDrawWave && waweIndex == 1){
                    _waveDrawAnimation.Start();
                }

                _enemySpawnerStopWatch.Stop();
                _enemySpawnerStopWatch.Reset();
                _enemySpawnerStopWatch.Start();
            }
        }

        private void EnemyMove() {
            if (_enemyMoveStopWatch.ElapsedMilliseconds >= 33)
            {
                this.enemyLogic.EnemyMove();

                _enemyMoveStopWatch.Stop();
                _enemyMoveStopWatch.Reset();
                _enemyMoveStopWatch.Start();
            }
        }

        private void EnemyAnimation() {
            if (_enemyAnimationStopWatch.ElapsedMilliseconds >= 33){

                for (int i = 0; i < this.model.SpawnedEnemies.Count; i++){

                    if (this.model.SpawnedEnemies[i] is EnemyGhost)
                    {
                        if (this.model.SpawnedEnemies[i].ShouldAttack)
                        {
                            this.model.SpawnedEnemies[i].AttackAnimationIndex++;

                            if (this.model.SpawnedEnemies[i].AttackAnimationIndex == BRUSHES.GhostAttackBrushes.Count)
                            {
                                this.enemyLogic.AttackAlliedUnits(this.model.SpawnedEnemies[i]);
                                this.model.SpawnedEnemies[i].AttackAnimationIndex = 0;
                            }
                        }
                        else if (this.model.SpawnedEnemies[i].ShouldWalk)
                        {
                            this.model.SpawnedEnemies[i].WalkingIndex++;

                            if (this.model.SpawnedEnemies[i].WalkingIndex == BRUSHES.GhostWalkingBrushes.Count)
                            {
                                this.model.SpawnedEnemies[i].WalkingIndex = 0;
                            }
                        }
                    }
                    else if (this.model.SpawnedEnemies[i] is EnemyGhost2)
                    {
                        if (this.model.SpawnedEnemies[i].ShouldAttack)
                        {
                            this.model.SpawnedEnemies[i].AttackAnimationIndex++;

                            if (this.model.SpawnedEnemies[i].AttackAnimationIndex == BRUSHES.Ghost2AttackBrushes.Count)
                            {
                                this.enemyLogic.AttackAlliedUnits(this.model.SpawnedEnemies[i]);
                                this.model.SpawnedEnemies[i].AttackAnimationIndex = 0;
                            }
                        }
                        else if (this.model.SpawnedEnemies[i].ShouldWalk)
                        {
                            this.model.SpawnedEnemies[i].WalkingIndex++;

                            if (this.model.SpawnedEnemies[i].WalkingIndex == BRUSHES.Ghost2WalkingBrushes.Count)
                            {
                                this.model.SpawnedEnemies[i].WalkingIndex = 0;
                            }
                        }
                    }
                    else if (this.model.SpawnedEnemies[i] is EnemyGhost3)
                    {
                        if (this.model.SpawnedEnemies[i].ShouldAttack)
                        {
                            this.model.SpawnedEnemies[i].AttackAnimationIndex++;

                            if (this.model.SpawnedEnemies[i].AttackAnimationIndex == BRUSHES.Ghost3AttackBrushes.Count)
                            {
                                this.enemyLogic.AttackAlliedUnits(this.model.SpawnedEnemies[i]);
                                this.model.SpawnedEnemies[i].AttackAnimationIndex = 0;
                            }
                        }
                        else if (this.model.SpawnedEnemies[i].ShouldWalk)
                        {
                            this.model.SpawnedEnemies[i].WalkingIndex++;

                            if (this.model.SpawnedEnemies[i].WalkingIndex == BRUSHES.Ghost3WalkingBrushes.Count)
                            {
                                this.model.SpawnedEnemies[i].WalkingIndex = 0;
                            }
                        }
                    }
                    else if (this.model.SpawnedEnemies[i] is EnemyOrc1)
                    {
                        if (this.model.SpawnedEnemies[i].ShouldAttack)
                        {
                            this.model.SpawnedEnemies[i].AttackAnimationIndex++;

                            if (this.model.SpawnedEnemies[i].AttackAnimationIndex == BRUSHES.Orc_1_AttackBrushes.Count)
                            {
                                this.enemyLogic.AttackAlliedUnits(this.model.SpawnedEnemies[i]);
                                this.model.SpawnedEnemies[i].AttackAnimationIndex = 0;
                            }
                        }
                        else if (this.model.SpawnedEnemies[i].ShouldWalk)
                        {
                            this.model.SpawnedEnemies[i].WalkingIndex++;

                            if (this.model.SpawnedEnemies[i].WalkingIndex == BRUSHES.Orc_1_WalkingBrushes.Count)
                            {
                                this.model.SpawnedEnemies[i].WalkingIndex = 0;
                            }
                        }
                    }
                    else if (this.model.SpawnedEnemies[i] is EnemyOrc2)
                    {
                        if (this.model.SpawnedEnemies[i].ShouldAttack)
                        {
                            this.model.SpawnedEnemies[i].AttackAnimationIndex++;

                            if (this.model.SpawnedEnemies[i].AttackAnimationIndex == BRUSHES.Orc_2_AttackBrushes.Count)
                            {
                                this.enemyLogic.AttackAlliedUnits(this.model.SpawnedEnemies[i]);
                                this.model.SpawnedEnemies[i].AttackAnimationIndex = 0;
                            }
                        }
                        else if (this.model.SpawnedEnemies[i].ShouldWalk)
                        {
                            this.model.SpawnedEnemies[i].WalkingIndex++;

                            if (this.model.SpawnedEnemies[i].WalkingIndex == BRUSHES.Orc_2_WalkingBrushes.Count)
                            {
                                this.model.SpawnedEnemies[i].WalkingIndex = 0;
                            }
                        }
                    }
                    else if (this.model.SpawnedEnemies[i] is EnemyOrc3)
                    {
                        if (this.model.SpawnedEnemies[i].ShouldAttack)
                        {
                            this.model.SpawnedEnemies[i].AttackAnimationIndex++;

                            if (this.model.SpawnedEnemies[i].AttackAnimationIndex == BRUSHES.Orc_3_AttackBrushes.Count)
                            {
                                this.enemyLogic.AttackAlliedUnits(this.model.SpawnedEnemies[i]);
                                this.model.SpawnedEnemies[i].AttackAnimationIndex = 0;
                            }
                        }
                        else if (this.model.SpawnedEnemies[i].ShouldWalk)
                        {
                            this.model.SpawnedEnemies[i].WalkingIndex++;

                            if (this.model.SpawnedEnemies[i].WalkingIndex == BRUSHES.Orc_3_WalkingBrushes.Count)
                            {
                                this.model.SpawnedEnemies[i].WalkingIndex = 0;
                            }
                        }
                    }


                }


                _enemyAnimationStopWatch.Stop();
                _enemyAnimationStopWatch.Reset();
                _enemyAnimationStopWatch.Start();
            }
        }

        private void DyingItemsAnimation() {


            this.model.DiedItems = this.model.DiedItems.Where(x => x != null).ToList();
            //_dyingStopwatch
            if (_dyingStopwatch.ElapsedMilliseconds >= 60)
            {
                for (int i = 0; i < this.model.DiedItems.Count; i++){

                    this.model.DiedItems[i].DieIndex++;

                    if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Ghost){
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.GhostDyingBrushes.Count){
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Ghost2){
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.Ghost2DyingBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Ghost3)
                    {
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.Ghost3DyingBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Orc1)
                    {
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.Orc_1_DyingBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Orc2)
                    {
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.Orc_2_DyingBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Orc3)
                    {
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.Orc_3_DyingBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Archer)
                    {
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.ArcherDieBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }
                    else if (this.model.DiedItems[i].WhoDied == UnitsWhatCanDie.Knight)
                    {
                        if (this.model.DiedItems[i].DieIndex == BRUSHES.KnightDieBrushes.Count)
                        {
                            this.model.DiedItems[i] = null;
                        }
                    }

                }

                _dyingStopwatch.Stop();
                _dyingStopwatch.Reset();
                _dyingStopwatch.Start();
            }
        }

        private void GetGold() {
            if (_giveGoldStopwatch.ElapsedMilliseconds >= 1000) { 
                this.model.Gold += 10;

                _giveGoldStopwatch.Stop();
                _giveGoldStopwatch.Reset();
                _giveGoldStopwatch.Start();
            }
        }













        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (drawingContext != null){
                if (this.model != null){

                    DrawBackgroundElements(drawingContext);
                    DrawButtons(drawingContext);
                    DrawKnights(drawingContext);
                    DrawBorder(drawingContext);
                    DrawEnemies(drawingContext);
                    DrawForegroundElements(drawingContext);
                    DrawArrows(drawingContext);
                    DrawDiyingItems(drawingContext);

                    DrawActualWave(drawingContext);
                    DrawActualCost(drawingContext);

                }
            }

        }

        #region Not changing elements
        private void DrawBackgroundElements(DrawingContext drawingContext){
            DrawButtonsBackground(drawingContext);
            DrawCellBackground(drawingContext);
            DrawEnemyCellBackground(drawingContext);
            DrawCastleWall(drawingContext);
            DrawCastleHpBoxBackground(drawingContext);
            DrawCastleHpText(drawingContext);
            DrawGoldText(drawingContext);
        }
        
        private void DrawGoldText(DrawingContext drawingContext){
            FormattedText text1 = new FormattedText("Gold: ", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 2);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text1.BuildGeometry(new Point(Config.TileSize/2 + 465, (Config.TileSize * Config.RowNumbers) + 60))); //520
        }

        private void DrawCastleHpText(DrawingContext drawingContext){
            FormattedText text = new FormattedText("Castle HP: ", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(Config.TileSize/2, (Config.TileSize * Config.RowNumbers) + 60)));
        }

        private void DrawCastleHpBoxBackground(DrawingContext drawingContext){
            Geometry rect1 = new RectangleGeometry(new Rect(Config.TileSize/2 + 145, (Config.TileSize * Config.RowNumbers) + 60, 300, 30));
            drawingContext.DrawGeometry(Brushes.WhiteSmoke, null, rect1);
        }

        private void DrawCastleWall(DrawingContext drawingContext){
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                Geometry box = new RectangleGeometry(new Rect(0 * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                drawingContext.DrawGeometry(this.BRUSHES.CastleWallBrush, null, box);
            }
        }

        private void DrawEnemyCellBackground(DrawingContext drawingContext){
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                Geometry box = new RectangleGeometry(new Rect(10 * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                drawingContext.DrawGeometry(this.BRUSHES.EnemyGrassBrush, null, box);
            }
        }

        private void DrawCellBackground(DrawingContext drawingContext){
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length - 1; x++)
                {
                    Geometry box = new RectangleGeometry(new Rect((x + 1) * Config.TileSize, y * Config.TileSize, Config.TileSize, Config.TileSize));
                    drawingContext.DrawGeometry(this.BRUSHES.GrassBrush, null, box);
                }
            }
        }

        private void DrawButtonsBackground(DrawingContext drawingContext) {
            Geometry rect1 = new RectangleGeometry(new Rect(0, Config.TileSize * (Config.RowNumbers), Config.TileSize*13, Config.TileSize * 2));
            drawingContext.DrawGeometry(this.BRUSHES.ButtonBackgroundBrush, null, rect1);

            Geometry rect2 = new RectangleGeometry(new Rect(Config.TileSize * (Config.ColumnNumbers+1), 0, Config.TileSize*2, Config.TileSize*Config.RowNumbers));
            drawingContext.DrawGeometry(this.BRUSHES.ButtonBackgroundTopBrush, null, rect2);
        }

        #endregion

        #region DrawButtons

        private void CreatDrawButtonsGeometries()
        {
            ButtonsGeometry.Add(new RectangleGeometry(new Rect(
                   (this.model.Map[0].Length + 1) * Config.TileSize, 0 * Config.TileSize, Config.TileSize, Config.TileSize)));

            ButtonsGeometry.Add(new RectangleGeometry(new Rect(
                   (this.model.Map[0].Length + 1) * Config.TileSize, 1 * Config.TileSize, Config.TileSize, Config.TileSize)));

            ButtonsGeometry.Add(new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length - 1) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize)));

            ButtonsGeometry.Add(new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize)));

            ButtonsGeometry.Add(new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length - 2) * Config.TileSize, 5 * Config.TileSize, Config.TileSize, Config.TileSize)));

            ButtonsGeometry.Add(new RectangleGeometry(new Rect(
                    (this.model.Map[0].Length + 2) * Config.TileSize, 1 * Config.TileSize, Config.TileSize, Config.TileSize)));
        }
        private void DrawButtons(DrawingContext drawingContext)
        {
            DeployKnightButton(drawingContext);
            DeployArcherButton(drawingContext);
            DeployWallButton(drawingContext);

            MoveButton(drawingContext);
            RemoveButton(drawingContext);
            UpgradeButton(drawingContext);

            SaveButton(drawingContext);
        }
        private void DeployKnightButton(DrawingContext drawingContext)
        {
            if (this.model.DeployKnight)
            {
                drawingContext.DrawGeometry(this.BRUSHES.DeployKnightSelectedBrush, null, ButtonsGeometry[0]);
            }
            else
            {
                drawingContext.DrawGeometry(this.BRUSHES.DeployKnightBrush, null, ButtonsGeometry[0]);
            }
        }
        private void DeployArcherButton(DrawingContext drawingContext)
        {
            if (this.model.DeployArcher)
            {
                drawingContext.DrawGeometry(this.BRUSHES.DeployArcherSelectedBrush, null, ButtonsGeometry[1]);
            }
            else
            {
                drawingContext.DrawGeometry(this.BRUSHES.DeployArcherBrush, null, ButtonsGeometry[1]);
            }
        }

        private void MoveButton(DrawingContext drawingContext)
        {
            if (this.model.MoveUnit)
            {
                drawingContext.DrawGeometry(this.BRUSHES.MoveButtonSelectedBrush, null, ButtonsGeometry[2]);
            }
            else
            {
                drawingContext.DrawGeometry(this.BRUSHES.MoveButtonBrush, null, ButtonsGeometry[2]);
            }
        }
        private void RemoveButton(DrawingContext drawingContext)
        {
            if (this.model.RemoveUnit)
            {
                drawingContext.DrawGeometry(this.BRUSHES.RemoveButtonSelectedBrush, null, ButtonsGeometry[3]);
            }
            else
            {
                drawingContext.DrawGeometry(this.BRUSHES.RemoveButtonBrush, null, ButtonsGeometry[3]);
            }
        }
        private void UpgradeButton(DrawingContext drawingContext)
        {
            if (this.model.UpgradeUnit)
            {
                drawingContext.DrawGeometry(this.BRUSHES.UpgradeButtonSelectedBrush, null, ButtonsGeometry[4]);
            }
            else
            {
                drawingContext.DrawGeometry(this.BRUSHES.UpgradeButtonBrush, null, ButtonsGeometry[4]);
            }
        }

        private async void SaveButton(DrawingContext drawingContext)
        {
            if (this.model.SelectedSave || clickedSave)
            {
                drawingContext.DrawGeometry(this.BRUSHES.SaveButtonSelectedBrush, null, ButtonsGeometry[5]);
                await Task.Delay(1500);
                clickedSave = false;
            }
            else
            {
                drawingContext.DrawGeometry(this.BRUSHES.SaveButtondBrush, null, ButtonsGeometry[5]);
            }
        }
        #endregion

        #region Foreground Elements

        private void DrawForegroundElements(DrawingContext drawingContext)
        {
            DrawCastleHpBoxForeground(drawingContext);
            DrawCurrentCastleHp(drawingContext);
            DrawCurrentGold(drawingContext);
            DrawWave(drawingContext);
            DrawScore(drawingContext);
            DrawAliedLevelsAndHps(drawingContext);
        }
        private void DrawCastleHpBoxForeground(DrawingContext drawingContext)
        {
            Geometry rect2 = new RectangleGeometry(new Rect(Config.TileSize / 2 + 145, (Config.TileSize * Config.RowNumbers) + 60, ((300 * this.model.CastleActualHP) / this.model.CastleMaxHP), 30));
            drawingContext.DrawGeometry(Brushes.DarkRed, null, rect2);
        }
        private void DrawCurrentCastleHp(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText(this.model.CastleActualHP.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen((this.model.CastleActualHP >= 150 ? Brushes.DarkGreen : Brushes.DarkRed), 2), text.BuildGeometry(new Point(Config.TileSize / 2 + 285, (Config.TileSize * Config.RowNumbers) + 60)));
        }

        private void DrawCurrentGold(DrawingContext drawingContext)
        {
            FormattedText text1 = new FormattedText(this.model.Gold.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Yellow, 2);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Yellow, 2), text1.BuildGeometry(new Point(Config.TileSize / 2 + 550, (Config.TileSize * Config.RowNumbers) + 60)));
        }
        private void DrawWave(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText("Wave: " + this.model.Wave.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(Config.TileSize / 2, (Config.TileSize * Config.RowNumbers) + 100)));
        }

        private void DrawScore(DrawingContext drawingContext)
        {
            FormattedText text = new FormattedText($"{this.model.PlayerName}'s score: " + this.model.Score.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 30, Brushes.Black, 1);
            drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), text.BuildGeometry(new Point(Config.TileSize / 2 + 465, (Config.TileSize * Config.RowNumbers) + 100)));
        }

        private void DrawAliedLevelsAndHps(DrawingContext drawingContext)
        {
            FormattedText text;
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is IAllied actual)
                    {
                        Geometry rect1 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, 80, 15));
                        drawingContext.DrawGeometry(Brushes.WhiteSmoke, null, rect1);

                        Geometry rect2 = new RectangleGeometry(new Rect((x + 1) * Config.TileSize + 50, y * Config.TileSize, ((80 * actual.ActualLife) / actual.MaxLife), 15));
                        drawingContext.DrawGeometry(Brushes.DarkRed, null, rect2);

                        text = new FormattedText(this.model.Map[y][x].Level.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 45, Brushes.Black, 1);
                        drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.WhiteSmoke, 1), text.BuildGeometry(new Point((x + 1) * Config.TileSize, y * Config.TileSize)));
                    }
                }
            }
        }

        #endregion


        private void DrawKnights(DrawingContext drawingContext)
        {
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    if (this.model.Map[y][x] is Knight knight)
                    {
                        if (knight.ShouldWalk){
                            drawingContext.DrawGeometry(this.BRUSHES.KnightBrushes[0], null, knight.RealArea);
                        }
                        else if (knight.ShouldAttack){
                            drawingContext.DrawGeometry(this.BRUSHES.KnightBrushes[knight.AttackAnimationIndex], null, knight.RealArea);
                        }


                    }
                    else if (this.model.Map[y][x] is Archer archer)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.ArcherBrushes[archer.AttackAnimationIndex], null, archer.RealArea);
                    }
                }
            }
        }
        private void DrawArrows(DrawingContext drawingContext){
            for (int y = 0; y < this.model.Map.Length; y++)
            {
                for (int x = 0; x < this.model.Map[y].Length; x++)
                {
                    this.alliedLogic.CheckWhichArrowShouldDraw(this.model.Map[y][x]);

                    if (this.model.Map[y][x] is Archer archer)
                    {
                        foreach (var arrow in archer.Arrows)
                        {
                            drawingContext.DrawGeometry(this.BRUSHES.ArrowBrush, null, arrow.RealArea);
                        }
                    }
                }
            }

        }

        private void DrawEnemies(DrawingContext drawingContext){
            FormattedText text;

            this.enemyLogic.DeleteNullEnemies();

            foreach (var enemy in this.model.SpawnedEnemies)
            {
                if (enemy is EnemyGhost) {
                    if (enemy.ShouldWalk)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.GhostWalkingBrushes[enemy.WalkingIndex], null, enemy.RealArea);
                    }
                    else if (enemy.ShouldAttack)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.GhostAttackBrushes[enemy.AttackAnimationIndex], null, enemy.RealArea);
                    }
                }
                else if (enemy is EnemyGhost2)
                {
                    if (enemy.ShouldWalk)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Ghost2WalkingBrushes[enemy.WalkingIndex], null, enemy.RealArea);
                    }
                    else if (enemy.ShouldAttack)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Ghost2AttackBrushes[enemy.AttackAnimationIndex], null, enemy.RealArea);
                    }
                }
                else if (enemy is EnemyGhost3)
                {
                    if (enemy.ShouldWalk)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Ghost3WalkingBrushes[enemy.WalkingIndex], null, enemy.RealArea);
                    }
                    else if (enemy.ShouldAttack)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Ghost3AttackBrushes[enemy.AttackAnimationIndex], null, enemy.RealArea);
                    }
                }
                else if (enemy is EnemyOrc1)
                {
                    if (enemy.ShouldWalk)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_1_WalkingBrushes[enemy.WalkingIndex], null, enemy.RealArea);
                    }
                    else if (enemy.ShouldAttack)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_1_AttackBrushes[enemy.AttackAnimationIndex], null, enemy.RealArea);
                    }
                }
                else if (enemy is EnemyOrc2)
                {
                    if (enemy.ShouldWalk)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_2_WalkingBrushes[enemy.WalkingIndex], null, enemy.RealArea);
                    }
                    else if (enemy.ShouldAttack)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_2_AttackBrushes[enemy.AttackAnimationIndex], null, enemy.RealArea);
                    }
                }
                else if (enemy is EnemyOrc3)
                {
                    if (enemy.ShouldWalk)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_3_WalkingBrushes[enemy.WalkingIndex], null, enemy.RealArea);
                    }
                    else if (enemy.ShouldAttack)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_3_AttackBrushes[enemy.AttackAnimationIndex], null, enemy.RealArea);
                    }
                }


                Geometry rect1 = new RectangleGeometry(new Rect(enemy.Position.X + Config.TileSize / 2, enemy.Position.Y, Config.TileSize/2, 15));
                drawingContext.DrawGeometry(Brushes.LightGray, null, rect1);

                Geometry rect2 = new RectangleGeometry(new Rect(enemy.Position.X + Config.TileSize / 2, enemy.Position.Y, (((Config.TileSize / 2) * enemy.ActualLife) / enemy.MaxLife), 15));
                drawingContext.DrawGeometry(Brushes.DarkRed, null, rect2);

                text = new FormattedText(enemy.Level.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 45, Brushes.WhiteSmoke, 3);
                drawingContext.DrawGeometry(Brushes.Black, new Pen(Brushes.WhiteSmoke, 1), text.BuildGeometry(new Point(enemy.Position.X + 20, enemy.Position.Y)));
            }
        }

        private void DrawBorder(DrawingContext drawingContext)
        {
            Pen actual = new Pen();

            if (MovedMouseTilePos.X >= 0 && MovedMouseTilePos.X < Config.ColumnNumbers-1 && MovedMouseTilePos.Y >= 0 && MovedMouseTilePos.Y < Config.RowNumbers){
                if (this.model.Map[(int)MovedMouseTilePos.Y][(int)MovedMouseTilePos.X] == null){
                    if (this.model.DeployKnight){
                        drawingContext.DrawGeometry(this.BRUSHES.TemporaryKnightBrush, new Pen(Brushes.Gray, 8), new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));

                    }
                    else if (this.model.DeployArcher){
                        drawingContext.DrawGeometry(this.BRUSHES.TemporaryArcherBrush, new Pen(Brushes.Gray, 8), new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                    }
                    else if (this.model.MoveUnit && MovedUnitPrevPos.X != -1 && MovedUnitPrevPos.X < Config.ColumnNumbers && MovedUnitPrevPos.Y < Config.RowNumbers){

                        if (this.model.Map[(int)MovedUnitPrevPos.Y][(int)MovedUnitPrevPos.X] is Knight){
                            drawingContext.DrawGeometry(this.BRUSHES.TemporaryKnightBrush, new Pen(Brushes.Aqua, 8), new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                        }
                        else if (this.model.Map[(int)MovedUnitPrevPos.Y][(int)MovedUnitPrevPos.X] is Archer){
                            drawingContext.DrawGeometry(this.BRUSHES.TemporaryArcherBrush, new Pen(Brushes.Aqua, 8), new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                        }
                        else
                        {
                            actual = new Pen(Brushes.Aqua, 8);
                            drawingContext.DrawGeometry(null, actual, new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                        }
                    }
                    else
                    {
                        if (this.model.RemoveUnit){
                            actual = new Pen(Brushes.Red, 8);
                        }
                        else if (this.model.MoveUnit){
                            actual = new Pen(Brushes.Aqua, 8);

                        }
                        else if (this.model.UpgradeUnit){
                            actual = new Pen(Brushes.Purple, 8);

                        }
                        else{
                            actual = new Pen(Brushes.Yellow, 8);
                        }
                        drawingContext.DrawGeometry(null, actual, new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                    }
                }
                else
                {
                    if (this.model.RemoveUnit){
                        actual = new Pen(Brushes.Red, 8);
                    }
                    else if (this.model.MoveUnit){
                        actual = new Pen(Brushes.Aqua, 8);

                    }
                    else if (this.model.UpgradeUnit){
                        actual = new Pen(Brushes.Purple, 8);

                    }
                    else{
                        actual = new Pen(Brushes.Yellow, 8);
                    }
                    drawingContext.DrawGeometry(null, actual, new RectangleGeometry(new Rect((MovedMouseTilePos.X + 1) * Config.TileSize, MovedMouseTilePos.Y * Config.TileSize, Config.TileSize, Config.TileSize)));
                }


            }
        }

        private void DrawDiyingItems(DrawingContext drawingContext) {

            //delete nulls

            foreach (var diedItem in this.model.DiedItems){

                if (diedItem != null){
                    if (diedItem.WhoDied == UnitsWhatCanDie.Ghost)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.GhostDyingBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Ghost2)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Ghost2DyingBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Ghost3)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Ghost3DyingBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Orc1)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_1_DyingBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Orc2)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_2_DyingBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Orc3)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.Orc_3_DyingBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Archer)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.ArcherDieBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                    else if (diedItem.WhoDied == UnitsWhatCanDie.Knight)
                    {
                        drawingContext.DrawGeometry(this.BRUSHES.KnightDieBrushes[diedItem.DieIndex], null, diedItem.RealArea);
                    }
                }
                

            }

        }

        [Obsolete]
        private void DrawActualWave(DrawingContext drawingContext) {

            if (waweIndex <= 301 && shouldDrawWave){
                FormattedText formattedText = new FormattedText("WAAAVE" + this.model.Wave.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                            new Typeface(
                                new FontFamily("Arial"),
                                FontStyles.Italic,
                                FontWeights.Bold,
                                FontStretches.Normal), (waweIndex + 1), Brushes.LightYellow);

                drawingContext.DrawGeometry(null, new Pen(Brushes.LightYellow, 3), formattedText.BuildGeometry(new Point(Config.TileSize, Config.TileSize)));
                waweIndex++;
            }
            else if (waweIndex == 1){

            }
            else if (waweIndex <= 301){
                FormattedText formattedText = new FormattedText("WAAAVE" + this.model.Wave.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                            new Typeface(
                                new FontFamily("Arial"),
                                FontStyles.Italic,
                                FontWeights.Bold,
                                FontStretches.Normal), (waweIndex + 1), Brushes.LightYellow);

                drawingContext.DrawGeometry(null, new Pen(Brushes.LightYellow, 3), formattedText.BuildGeometry(new Point(Config.TileSize, Config.TileSize)));
                waweIndex++;
            }
            if (waweIndex == 301){
                waweIndex = 1;
                _waveDrawAnimation.Stop();
                _waveDrawAnimation.Reset();
                shouldDrawWave = false;
            }
        }

        [Obsolete]
        private void DrawActualCost(DrawingContext drawingContext) {

            if (MovedMouseTilePos.X >= 0 && MovedMouseTilePos.Y >= 0 && MovedMouseTilePos.X < Config.ColumnNumbers && MovedMouseTilePos.Y < Config.RowNumbers)
            {
                FormattedText formattedText = new FormattedText("", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                           new Typeface(
                               new FontFamily("Arial"),
                               FontStyles.Italic,
                               FontWeights.Bold,
                               FontStretches.Normal), 40, Brushes.Black);
                if (this.model.Map[(int)MovedMouseTilePos.Y][(int)MovedMouseTilePos.X] != null &&
                    this.model.Map[(int)MovedMouseTilePos.Y][(int)MovedMouseTilePos.X] is IAllied allied)
                {

                    if (this.model.MoveUnit)
                    {
                        formattedText = new FormattedText($"COST: {allied.UpgradeCost / 2}.",
                                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     new Typeface(
                                         new FontFamily("Arial"),
                                         FontStyles.Italic,
                                         FontWeights.Bold,
                                         FontStretches.Normal), 40, Brushes.Black);
                    }
                    else if (this.model.UpgradeUnit)
                    {
                        formattedText = new FormattedText($"COST: {allied.UpgradeCost}.",
                                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     new Typeface(
                                         new FontFamily("Arial"),
                                         FontStyles.Italic,
                                         FontWeights.Bold,
                                         FontStretches.Normal), 40, Brushes.Black);
                    }
                    else if (this.model.RemoveUnit)
                    {
                        formattedText = new FormattedText($"GET: {allied.UpgradeCost / 2}.",
                                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     new Typeface(
                                         new FontFamily("Arial"),
                                         FontStyles.Italic,
                                         FontWeights.Bold,
                                         FontStretches.Normal), 40, Brushes.Black);
                    }
                }
                else if (this.model.Map[(int)MovedMouseTilePos.Y][(int)MovedMouseTilePos.X] == null)
                {
                    if (this.model.DeployArcher){
                        formattedText = new FormattedText($"COST: " + 250,
                                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     new Typeface(
                                         new FontFamily("Arial"),
                                         FontStyles.Italic,
                                         FontWeights.Bold,
                                         FontStretches.Normal), 40, Brushes.Black);
                    }
                    else if (this.model.DeployKnight)
                    {
                        formattedText = new FormattedText($"COST: " + 150,
                                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                     new Typeface(
                                         new FontFamily("Arial"),
                                         FontStyles.Italic,
                                         FontWeights.Bold,
                                         FontStretches.Normal), 40, Brushes.Black);
                    }
                    

                }

                

                drawingContext.DrawGeometry(null, new Pen(Brushes.Black, 2), formattedText.BuildGeometry(new Point(Config.TileSize * (Config.ColumnNumbers + 1), Config.TileSize * (Config.RowNumbers - 1))));
            }  
        }

    }
}
