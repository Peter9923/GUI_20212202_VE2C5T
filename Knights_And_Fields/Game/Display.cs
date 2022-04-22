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

        public Display()
        {
            this.Loaded += this.CastleDefendersControl_Loaded;
        }

        protected override void OnRender(DrawingContext drawingContext){
            if (drawingContext is not null){
                if (this.renderer != null){
                    drawingContext.DrawDrawing(this.renderer.BuildDrawing());
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

            if (this.win != null)
            {
                this.MouseDown += Display_MouseDown; ;
            }

            this.InvalidateVisual();
        }

        private void Display_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e){
            Point mousePos = e.GetPosition(this);
            Point tilePos = this.logic.GetTilePos(mousePos);
            bool nowCLicked = false;

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
            else if (tilePos.X == this.model.Map[0].Length -2){
                nowCLicked = true;
                if (tilePos.Y == 5)
                {
                    if (this.model.MoveUnit)
                    {
                        this.model.MoveUnit = false;
                    }
                    else
                    {
                        this.model.MoveUnit = true;
                        this.model.DeployKnight = false;
                        this.model.RemoveUnit = false;
                    }
                }
            }
            else if (tilePos.X == this.model.Map[0].Length - 1){
                nowCLicked = true;
                if (tilePos.Y == 5)
                {
                    if (this.model.RemoveUnit)
                    {
                        this.model.RemoveUnit = false;
                    }
                    else
                    {
                        this.model.RemoveUnit = true;
                        this.model.DeployKnight = false;
                        this.model.MoveUnit = false;

                    }
                }
            }


            this.InvalidateVisual();
        }
    }
}
