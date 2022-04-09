using Game.Logic;
using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Display
{
    public class Display : FrameworkElement
    {
        IGameLogic logic;
        private string PlayerName;
        private int PlayerID;
        private int Wave;

        public void SetUpLogic(IGameLogic logic) {
            this.logic = logic;

            MouseLeftButtonDown += Display_MouseLeftButtonDown;
        }

        private void Display_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point mousePos = e.GetPosition(this);

            if (logic.ClickIsRightPosition(mousePos.X, mousePos.Y) == true){
                logic.AddKnight(mousePos.X, mousePos.Y);
            }
            else{
                // user click the wall or enemy spawn area..
            }
            e.Handled = true;

           
           
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (logic != null)
            {
                GeometryDrawing background = new GeometryDrawing(Config.BgBrush,
                new Pen(Config.BorderBrush, Config.BorderSize),
                new RectangleGeometry(new Rect(0, 0, Config.Widht, Config.Height)));

                drawingContext.DrawDrawing(background);

                foreach (var item in logic.Enemies)
                {
                    drawingContext.DrawGeometry(Config.EnemyBgBrush, new Pen(Config.EnemyLineBrush, 3), item.Area);
                }

                foreach (var item in logic.Knights)
                {
                    if (item is Knight)
                    {
                        drawingContext.DrawGeometry(Config.Player_KatonaBgBrush, new Pen(Config.Player_KatonaLineBrush, 3), item.Area);
                    }
                    

                }

            }
        }
    }
}
