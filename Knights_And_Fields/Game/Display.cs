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
            
            this.renderer = new Renderer(this.model);
            this.win = Window.GetWindow(this);

            this.InvalidateVisual();
        }
    }
}
