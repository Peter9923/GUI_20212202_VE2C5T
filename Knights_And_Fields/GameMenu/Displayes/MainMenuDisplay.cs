using GameMenu.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameMenu.Displayes
{
    internal class MainMenuDisplay : FrameworkElement{
        MediaPlayer anotherClick;

        Window win;
        Point MousePos;

        private bool selectedExit;
        private bool selectedStart;
        private bool selectedScoreBoard;
        private bool selectedLoad;
        private bool selectedHelp;
        private bool selectedAbout;

        private Brush startButtonBrush;
        private Brush startButtonSelectedBrush;

        private Brush loadButtonBrush;
        private Brush loadButtonSelectedBrush;

        private Brush exitButtonBrush;
        private Brush exitButtonSelectedBrush;

        private Brush helpButtonBrush;
        private Brush helpButtonSelectedBrush;
        
        private Brush aboutButtonBrush;
        private Brush aboutButtonSelectedBrush;

        private Brush backGroundBrush;

        public MainMenuDisplay()
        {
            anotherClick = new MediaPlayer();
            anotherClick.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));



            this.SetBrushes();
            this.Loaded += MainMenuDisplay_Loaded;

        }

        private void MainMenuDisplay_Loaded(object sender, RoutedEventArgs e)
        {
            this.win = Window.GetWindow(this);

            if (this.win != null)
            {
                this.MouseDown += MainMenuDisplay_MouseDown;
                CompositionTarget.Rendering += CompositionTarget_Rendering; ;
            }

            this.InvalidateVisual();
        }

        private void CompositionTarget_Rendering(object? sender, EventArgs e)
        {
            if (win != null){

                MousePos = this.PointToScreen(Mouse.GetPosition(this));
                MousePos.X -= win.Left;
                MousePos.Y -= win.Top;
                //MousePos = this.PointFromScreen(Mouse.GetPosition(this));

                if (MousePos.X >= 209 && MousePos.X <= 590
                    && MousePos.Y >= 130 && MousePos.Y <= 223){
                    selectedStart = true;
                    selectedExit = false;
                    selectedHelp = false;
                    selectedLoad = false;
                    selectedScoreBoard = false;
                    selectedAbout = false;
                }
                else if (MousePos.X >= 293 && MousePos.X <= 538)
                {
                    if (MousePos.Y >= 231 && MousePos.Y <= 284){
                        selectedLoad = true;
                        selectedStart = false;
                        selectedExit = false;
                        selectedHelp = false;
                        selectedScoreBoard = false;
                        selectedAbout = false;
                    }
                    else if (MousePos.Y >= 293 && MousePos.Y <= 346){
                        selectedAbout = true;
                        selectedLoad = false;
                        selectedStart = false;
                        selectedExit = false;
                        selectedHelp = false;
                        selectedScoreBoard = false;
                    }
                    else if (MousePos.Y >= 351 && MousePos.Y <= 404){
                        selectedHelp = true;
                        selectedLoad = false;
                        selectedStart = false;
                        selectedExit = false;
                        selectedScoreBoard = false;
                        selectedAbout = false;
                    }
                    else if (MousePos.Y >= 411 && MousePos.Y <= 464){
                        selectedExit = true;
                        selectedLoad = false;
                        selectedStart = false;
                        selectedHelp = false;
                        selectedScoreBoard = false;
                        selectedAbout = false;
                    }
                }
                else {
                    selectedExit = false;
                    selectedLoad = false;
                    selectedStart = false;
                    selectedHelp = false;
                    selectedScoreBoard = false;
                    selectedAbout = false;
                }

                this.InvalidateVisual();


            }
        }


        private void MainMenuDisplay_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            anotherClick.Play();
            //MessageBox.Show(MousePos.X + "  |  " + MousePos.Y + " | " + (win.ActualHeight - this.ActualHeight + " | " + (win.ActualHeight + " | " + this.ActualHeight)));
            anotherClick.Open(new Uri("Sounds\\AnotherClick.wav", UriKind.RelativeOrAbsolute));
            if (selectedStart)
            {
                CreateNewGameWindow newGameWindow = new CreateNewGameWindow();
                newGameWindow.ShowDialog();
            }
            else if (selectedExit)
            {

                Environment.Exit(0);

            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (drawingContext != null && win != null)
            {

                drawingContext.DrawGeometry(backGroundBrush, null, new RectangleGeometry(new Rect(0, 0, win.ActualWidth, win.ActualHeight)));
                DrawButtons(drawingContext);

            }
        }

        private void SetBrushes()
        {
            backGroundBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\gallery.png", UriKind.RelativeOrAbsolute)));

            startButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\start_idle.png", UriKind.RelativeOrAbsolute)));
            startButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\start_hover.png", UriKind.RelativeOrAbsolute)));

            loadButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\load_idle.png", UriKind.RelativeOrAbsolute)));
            loadButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\load_hover.png", UriKind.RelativeOrAbsolute)));

            exitButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\quit_idle.png", UriKind.RelativeOrAbsolute)));
            exitButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\quit_hover.png", UriKind.RelativeOrAbsolute)));

            helpButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\help_idle.png", UriKind.RelativeOrAbsolute)));
            helpButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\help_hover.png", UriKind.RelativeOrAbsolute)));
            
            aboutButtonBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\about_idle.png", UriKind.RelativeOrAbsolute)));
            aboutButtonSelectedBrush = new ImageBrush(new BitmapImage(new Uri("Images\\Menu\\about_hover.png", UriKind.RelativeOrAbsolute)));

        }

        private void DrawButtons(DrawingContext drawingContext)
        {

            DrawStartButton(drawingContext);
            DrawLoadButton(drawingContext);
            DrawAboutButton(drawingContext);
            DrawExitButton(drawingContext);
            DrawHelpButton(drawingContext);
        }

        private void DrawStartButton(DrawingContext drawingContext)
        {

            Geometry g = new RectangleGeometry(new Rect(200 , 100, 383, 93));

            if (this.selectedStart)
            {
                drawingContext.DrawGeometry(this.startButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.startButtonBrush, null, g);
            }

        }

        private void DrawLoadButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(285, 200, 245, 53));

            if (this.selectedLoad)
            {
                drawingContext.DrawGeometry(this.loadButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.loadButtonBrush, null, g);
            }
        }

        private void DrawAboutButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(285, 260, 245, 53));

            if (this.selectedAbout)
            {
                drawingContext.DrawGeometry(this.aboutButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.aboutButtonBrush, null, g);
            }
        }        
        private void DrawHelpButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(285, 320, 245, 53));

            if (this.selectedHelp)
            {
                drawingContext.DrawGeometry(this.helpButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.helpButtonBrush, null, g);
            }
        }

        private void DrawExitButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect(285, 380, 245, 53));

            if (this.selectedExit)
            {
                drawingContext.DrawGeometry(this.exitButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.exitButtonBrush, null, g);
            }
        }


    }
}
