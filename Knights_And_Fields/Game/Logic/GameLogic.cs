using Game.Models;
using Game.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic
{
    public class GameLogic : IGameLogic
    {
        ICreateNewKnightService service;
        static Random r = new Random();
        

        public List<Enemy> Enemies { get; set; }
        public List<Knight> Knights { get; set; }

        public GameLogic()
        {
            this.service = new CreateNewKnightService();
            Enemies = new List<Enemy>();
            Knights = new List<Knight>();
          
        }

       


        //Enemy things
        public void AddEnemy(double x, double y)
        {
            for (int i = 0; i < 5; i++)
            {
                if (y >= (i * Config.Height / 5) && y <= ((i + 1) * Config.Height / 5))
                {
                    y = i * Config.Height / 5;
                    break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (x >= (i * Config.Widht / 10) && x <= ((i + 1) * Config.Widht / 10))
                {
                    x = i * Config.Widht / 10;
                    break;
                }
            }

            if (EnemyBuilder(Enemies, x, y) != null)
            {
                Enemies.Add(EnemyBuilder(Enemies, x, y));
            }
            //RefreshScreen?.Invoke(this, EventArgs.Empty);
        }
        public Enemy EnemyBuilder(List<Enemy> others, double X, double Y)
        {
            Enemy enemy = null;
            enemy = new Enemy(X, Y);
            if (!(others.Exists(t => t.IsCollision(enemy) || EnemyKnightInSamePlace(enemy))))
            {
                return enemy;
            }
            return null;
        }
        private bool EnemyKnightInSamePlace(IGameItem g)
        {
            for (int i = 0; i < Knights.Count(); i++)
            {
                if (g.IsCollision(Knights[i]))
                {
                    return true;
                }
            }
            return false;
        }





        //Knights thins
        public void AddKnight(double x, double y)
        {
            for (int i = 0; i < Config.RowNumber; i++)
            {
                if (y >= (i * Config.Height / Config.RowNumber) && y <= ((i + 1) * Config.Height / Config.RowNumber))
                {
                    y = i * Config.Height / Config.RowNumber;
                    break;
                }
            }

            for (int i = 0; i < Config.ColumnNumber; i++)
            {
                if (x >= (i * Config.Widht / Config.ColumnNumber) && x <= ((i + 1) * Config.Widht / Config.ColumnNumber))
                {
                    x = i * Config.Widht / Config.ColumnNumber;
                    break;
                }
            }

            Knight knight = KnightBuilder(Knights, x, y);

            if (knight != null)
            {
                Knights.Add(knight);
            }

            
            //RefreshScreen?.Invoke(this, EventArgs.Empty);
        }
        public Knight KnightBuilder(List<Knight> others, double X, double Y)
        {
            Knight knight = new Knight(X, Y);

            service.OpenKnightCreatorWindoe(knight);

            if (knight == null) { return null; }
            else
            {
                knight.SetXY(X, Y);
                if (!(others.Exists(t => t.IsCollision(knight) || KnightEnemyInSamePlace(knight))))
                {
                    return knight;
                }
            }
            return null;
            //b = new PlayerItem(X, Y, Config.NegyzetWidth, Config.NegyzetHeight, );
            //if (!(others.Exists(t => t.IsCollisiob(b) || PlayerEnemyEgyHelyen(b))))
            //{
            //    return b;
            //}
            //return null;
        }
        private bool KnightEnemyInSamePlace(IGameItem g)
        {
            for (int i = 0; i < Knights.Count(); i++)
            {
                if (g.IsCollision(Knights[i]))
                {
                    return true;
                }
            }
            return false;
        }




        public bool ClickIsRightPosition(double X, double Y) {
            
            if (MouseClickPositionIsInTheWall(X) || MouseClickPositonIsEnemySpawnArea(X))
            {
                return false;
            }
            return true;
        }
        //first column is for wall, last column is for enemy's spawn area
        private bool MouseClickPositionIsInTheWall(double X){
            if (X >= (0 * Config.Widht / Config.ColumnNumber) && X <= (1 * Config.Widht / Config.ColumnNumber))
            {
                return true;
            }
            return false;
        }

        //the last column is the enemy's spawn area
        private bool MouseClickPositonIsEnemySpawnArea(double X)
        {
            if (X >= ( (Config.ColumnNumber-1) * Config.Widht / Config.ColumnNumber) && X <= (Config.ColumnNumber * Config.Widht / Config.ColumnNumber))
            {
                return true;
            }
            return false;
        }

        //other things
        public void TimeStep()
        {
            foreach (var item in Enemies)
            {
                //foreach (var other in Enemies)
                //{
                //    if (!item.Equals(other) && item.IsCollision(other))
                //    {
                //        item.Collision();
                //    }
                //}

                if (EnemyKnightInSamePlace(item))
                {
                    item.Collision();
                }
                item.Move();
            }
        }
    }
}
