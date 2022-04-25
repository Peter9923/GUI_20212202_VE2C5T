﻿using GameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameLogic
{
    public class Logic : ILogic
    {
        private static Random rnd;
        public IModel Model { get; set; }
        public Logic(IModel model)
        {
            rnd = new Random();
            this.Model = model;
            this.CreateEnemies();
        }

        // Pixel position --> Tile position
        public Point GetTilePos(Point mousePos)
        {
            return new Point(
                (int)((mousePos.X / Config.TileSize) - 1),
                (int)(mousePos.Y / Config.TileSize));
        }

        public void DeployKnight(int x, int y)
        {
            if (this.Model.Map[y][x] == null
                && x < Config.ColumnNumbers - 1) {
                if (this.Model.DeployKnight && this.Model.Gold >= Config.KnightCost) {
                    this.Model.Map[y][x] = new Knight(x, y);
                    this.Model.Gold -= (this.Model.Map[y][x] as Knight).Cost;
                }
                else if (this.Model.DeployArcher && this.Model.Gold >= Config.ArcherCost) {
                    this.Model.Map[y][x] = new Archer(x, y);
                    this.Model.Gold -= (this.Model.Map[y][x] as Archer).Cost;
                }
            }

        }
        public void RemoveKnight(int x, int y)
        {
            if (this.Model.Map[y][x] != null && this.Model.Map[y][x] is IAllied actual)
            {
                this.Model.Gold += (int)((actual.Level * actual.Cost) / 1.25);
                this.Model.Map[y][x] = null;
            }
        }

        public void UpgradeKnight(int x, int y)
        {
            if (this.Model.Map[y][x] != null && this.Model.Map[y][x] is IAllied actual
                && this.Model.Gold >= actual.UpgradeCost) {
                this.Model.Gold -= actual.UpgradeCost;
                this.Model.Map[y][x].Level++;
                this.Model.Map[y][x].ActualLife = this.Model.Map[y][x].MaxLife;
            }
        }

        public void MoveKnight(int actualX, int actualY, int prevX, int prevY)
        {
            if (this.Model.Map[actualY][actualX] == null
                && this.Model.Map[prevY][prevX] != null && this.Model.Map[prevY][prevX] is IAllied actual
                && this.Model.Gold >= (actual.UpgradeCost / 2)) {

                this.Model.Gold -= actual.UpgradeCost / 2;

                this.Model.Map[actualY][actualX] = this.Model.Map[prevY][prevX];
                this.Model.Map[actualY][actualX].Position = new Point(actualX, actualY);
                this.Model.Map[prevY][prevX] = null;

            }
        }


        public void GenerateArrow(int x, int y) {
            if (this.Model.Map[y][x] != null &&
                this.Model.Map[y][x] is Archer archer)
            {
                Archer.Arrow arrow = new Archer.Arrow(((x + 1) * Config.TileSize), (y * Config.TileSize), rnd.Next(15, 25));
                archer.AddArrow(arrow);
            }
        }



        public bool EnemyAndAlliedUnitMetEachOther(EnemyKnight enemy, IAllied allied) {
            //Knight
            if (allied is Knight) {
                enemy.ActualLife -= allied.Damage;
                if (enemy.ActualLife <= 0)
                {
                    return true;
                }
                else
                {
                    allied.ActualLife -= enemy.Damage;
                    if (allied.ActualLife <= 0)
                    {
                        this.Model.Map[(int)allied.Position.Y][(int)allied.Position.X] = null;
                    }
                }
            }
            else //Wall, Archer, Cannon, they generate another item what can make damage...
            {
                allied.ActualLife -= enemy.Damage;
                if (allied.ActualLife <= 0)
                {
                    this.Model.Map[(int)allied.Position.Y][(int)allied.Position.X] = null;
                }
            }
            return false;
        }
        public void EnemyIsInTheCastle(EnemyKnight enemy) {
            this.Model.CastleActualHP -= 25;

            enemy.ShouldDie = true;

            if (Model.CastleActualHP <= 0) {
                //GAME OVER
            }
        }


        private void CreateEnemies() {
            int enemyCount = this.Model.Wave + 2 + rnd.Next(0, this.Model.Wave + 10);
            this.Model.Wave++;

            for (int i = 0; i < enemyCount; i++)
            {
                this.Model.ShouldSpawnEnemies.Add(new EnemyKnight((10 * Config.TileSize), (rnd.Next(0, 5)) * Config.TileSize));
                this.Model.ShouldSpawnEnemies[i].Level = rnd.Next(1, this.Model.Wave + 1);
            }
        }

        public int EnemySpawnTime() {
            for (int i = 0; i < this.Model.ShouldSpawnEnemies.Count; i++) {

                foreach (var spawned in this.Model.SpawnedEnemies) {
                    if (spawned.IsCollision(this.Model.ShouldSpawnEnemies[i]))
                    {
                        return -1;
                    }
                }
                //not collision, so can spawn.
                var a = this.Model.ShouldSpawnEnemies[i];
                this.Model.ShouldSpawnEnemies.Remove(this.Model.ShouldSpawnEnemies[i]);
                this.Model.SpawnedEnemies.Add(a);

                return this.Model.SpawnedEnemies.Count - 1;
            }

            if (this.Model.ShouldSpawnEnemies.Count == 0){
                CreateEnemies();
            }

            return -1;
        }

        public void CheckCollision(bool shouldAttack) {
            foreach (var enemy in this.Model.SpawnedEnemies)
            {
                bool wasCollision = false;
                for (int y = 0; y < this.Model.Map.Length; y++)
                {
                    for (int x = 0; x < this.Model.Map[y].Length; x++)
                    {
                        if (shouldAttack) {
                            //should attack

                            if (this.Model.Map[y][x] is IAllied allied && enemy.IsCollision(allied)
                                && this.EnemyAndAlliedUnitMetEachOther(enemy, allied))
                            {
                                enemy.ShouldDie = true;
                            }
                            if (this.Model.Map[y][x] is Archer archer)
                            {

                                for (int i = 0; i < archer.Arrows.Count; i++)
                                {

                                    if (enemy.IsCollision(archer.Arrows[i]))
                                    {

                                        enemy.ActualLife -= archer.Damage;
                                        archer.Arrows[i].ShouldDraw = false;

                                        //Check If Enemy Die
                                        if (enemy.ActualLife <= 0) {
                                            enemy.ShouldDie = true;
                                        }
                                    }

                                }

                            }

                        }
                        else { //just Move not attack
                            if (this.Model.Map[y][x] is IAllied allied)
                            {
                                if (enemy.IsCollision(allied))
                                {
                                    wasCollision = true;
                                }

                            }
                        }
                    }
                }
                if (wasCollision == false)
                {
                    enemy.Move();

                    if (enemy.Position.X <= Config.TileSize / 2) {
                        this.EnemyIsInTheCastle(enemy);
                        enemy.ShouldDie = true;
                    }
                }
            }
            this.CheckEnemyIsDied();
        }

        public void CheckWhichArrowShouldDraw(IUnit allied) {
            if (allied is Archer archer){
                for (int i = 0; i < archer.Arrows.Count; i++){
                    if (archer.Arrows[i].Position.X >= (Config.TileSize * Config.ColumnNumbers)){
                        archer.Arrows[i].ShouldDraw = false;
                    }
                }

                archer.Arrows = archer.Arrows.Where(x => x.ShouldDraw == true).ToList();
            }
        }

        public void CheckEnemyIsDied() {
            this.Model.SpawnedEnemies = this.Model.SpawnedEnemies.Where(x => x.ShouldDie == false).ToList();
        }
    }
}
