using GameLogic;
using GameModel;
using GameRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game
{
    public class Display : FrameworkElement
    {
        private IModel model;
        private ILogic logic;
        private Renderer renderer;
        private Window win;


        private bool mouseMoved = false;
        Point MovedMouseTilePos;

        Point MovedUnitPrevPos;


        public Display()
        {
            this.Loaded += this.CastleDefendersControl_Loaded;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (drawingContext is not null)
            {
                if (this.renderer != null)
                {
                    drawingContext.DrawDrawing(this.renderer.BuildDrawing());
                }

                if (this.model != null && mouseMoved == true
                    && (int)MovedMouseTilePos.Y <= this.model.Map.Length - 1
                    && (int)MovedMouseTilePos.X <= this.model.Map[0].Length - 1)
                {
                    if (this.model.Map[(int)MovedMouseTilePos.Y][(int)MovedMouseTilePos.X] == null){
                        if (this.model.DeployKnight)
                        {
                            drawingContext.DrawDrawing(this.renderer.GetKnightIfMouseMove(MovedMouseTilePos.X, MovedMouseTilePos.Y));
                        }
                    }
                    else{ //mouse is an exist object.
                        //maybe are is another color??
                        drawingContext.DrawDrawing(this.renderer.GetBorderIfMouseMove(MovedMouseTilePos.X, MovedMouseTilePos.Y));
                    }
                    
                    
                }
            }
        }

        private void CastleDefendersControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.win = Window.GetWindow(this);
            this.model = new Model(this.ActualHeight, this.ActualWidth);
            this.logic = new Logic(this.model);
            this.renderer = new Renderer(this.model);
            this.win = Window.GetWindow(this);

            MovedUnitPrevPos = new Point(-1, -1);

            if (this.win != null)
            {
                this.MouseDown += Display_MouseDown; ;
                this.MouseMove += Display_MouseMove;
            }

            this.InvalidateVisual();
        }

        private void Display_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(this);
            MovedMouseTilePos = this.logic.GetTilePos(mousePos);
            if (MovedMouseTilePos.X != this.model.Map[0].Length)
            {
                mouseMoved = true;
                this.InvalidateVisual();
            }
        }

        private void Display_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(this);
            Point tilePos = this.logic.GetTilePos(mousePos);
            bool nowCLicked = false;
            mouseMoved = false;

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
                    this.model.MoveUnit = true;
                    this.model.DeployKnight = false;
                    this.model.RemoveUnit = false;
                    this.model.UpgradeUnit = false;
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

                }
            }


            //what will happan if Knight button clicked and clicked another cell.
            if (!nowCLicked && this.model.DeployKnight)
            {
                this.logic.DeployKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.DeployKnight = false;
            }
            else if (!nowCLicked && this.model.RemoveUnit)
            {
                this.logic.RemoveKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.RemoveUnit = false;
            }
            else if (!nowCLicked && this.model.UpgradeUnit)
            {
                this.logic.UpgradeKnight((int)tilePos.X, (int)tilePos.Y);
                this.model.UpgradeUnit = false;
            }
            else if (!nowCLicked && this.model.MoveUnit)
            {
                if (MovedUnitPrevPos.X != -1){
                    this.logic.MoveKnight((int)tilePos.X, (int)tilePos.Y, (int)MovedUnitPrevPos.X, (int)MovedUnitPrevPos.Y);

                    this.model.MoveUnit = false;
                    MovedUnitPrevPos = new Point(-1, -1);
                }
                else{ // == -1
                    MovedUnitPrevPos = this.logic.GetTilePos(mousePos);
                }
            }


            this.InvalidateVisual();
        }
    }
}
