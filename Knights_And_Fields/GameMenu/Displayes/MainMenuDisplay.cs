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
        SoundPlayer anotherClick;
        SoundPlayer selectedClick;

        Window win;
        Point MousePos;

        private bool selectedExit;
        private bool selectedStart;
        private bool selectedScorteBoard;
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

        private Brush backGroundBrush;

        public MainMenuDisplay()
        {
            anotherClick = new SoundPlayer();
            anotherClick.SoundLocation = "Sounds\\AnotherClick.wav";

            selectedClick = new SoundPlayer();
            selectedClick.SoundLocation = "Sounds\\SelectClick.wav";



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

                if (MousePos.X >= ((win.ActualWidth / 10) * 4)
                    && MousePos.X <= (((win.ActualWidth / 10) * 4) + 200))
                {
                    //1.
                    if (MousePos.Y >= ((win.ActualHeight / 10) * 1) && MousePos.Y <= (((win.ActualHeight / 10) * 1) + 50))
                    {
                        if (selectedStart == false){
                            selectedClick.Play();
                        }
                        selectedStart = true;
                        selectedExit = false;
                        selectedHelp = false;
                        selectedLoad = false;
                        selectedScorteBoard = false;
                        selectedAbout = false;
                    }
                    else
                    {
                        selectedStart = false;
                    }

                    //2.
                    if (MousePos.Y >= ((win.ActualHeight / 10) * 2) && MousePos.Y <= (((win.ActualHeight / 10) * 2) + 50))
                    {
                        if (selectedLoad == false)
                        {
                            selectedClick.Play();
                        }
                        selectedLoad = true;
                        selectedStart = false;
                        selectedExit = false;
                        selectedHelp = false;
                        selectedScorteBoard = false;
                        selectedAbout = false;
                    }
                    else
                    {
                        selectedLoad = false;
                    }

                    //4.
                    if (MousePos.Y >= ((win.ActualHeight / 10) * 4) && MousePos.Y <= (((win.ActualHeight / 10) * 4) + 50))
                    {
                        if (selectedHelp == false)
                        {
                            selectedClick.Play();
                        }
                        selectedHelp = true;
                        selectedLoad = false;
                        selectedStart = false;
                        selectedExit = false;
                        selectedScorteBoard = false;
                        selectedAbout = false;
                    }
                    else
                    {
                        selectedHelp = false;
                    }

                    //5.
                    if (MousePos.Y >= ((win.ActualHeight / 10) * 5) && MousePos.Y <= (((win.ActualHeight / 10) * 5) + 50))
                    {
                        if (selectedExit == false)
                        {
                            selectedClick.Play();
                        }
                        selectedExit = true;
                        selectedLoad = false;
                        selectedStart = false;
                        selectedHelp = false;
                        selectedScorteBoard = false;
                        selectedAbout = false;
                    }
                    else
                    {
                        selectedExit = false;
                    }
                }
                else
                {
                    selectedExit = false;
                    selectedLoad = false;
                    selectedStart = false;
                    selectedHelp = false;
                    selectedScorteBoard = false;
                    selectedAbout = false;
                }

                this.InvalidateVisual();


            }
        }


        private void MainMenuDisplay_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            anotherClick.Play();
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

        }

        private void DrawButtons(DrawingContext drawingContext)
        {

            DrawStartButton(drawingContext);
            DrawLoadButton(drawingContext);
            DrawExitButton(drawingContext);
            DrawHelpButton(drawingContext);
        }

        private void DrawStartButton(DrawingContext drawingContext)
        {

            Geometry g = new RectangleGeometry(new Rect((win.ActualWidth / 10) * 4, (win.ActualHeight / 10) * 1, 200, 50));

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
            Geometry g = new RectangleGeometry(new Rect((win.ActualWidth / 10) * 4, (win.ActualHeight / 10) * 2, 200, 50));

            if (this.selectedLoad)
            {
                drawingContext.DrawGeometry(this.loadButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.loadButtonBrush, null, g);
            }
        }

        private void DrawExitButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect((win.ActualWidth / 10) * 4, (win.ActualHeight / 10) * 5, 200, 50));

            if (this.selectedExit)
            {
                drawingContext.DrawGeometry(this.exitButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.exitButtonBrush, null, g);
            }
        }

        private void DrawHelpButton(DrawingContext drawingContext)
        {
            Geometry g = new RectangleGeometry(new Rect((win.ActualWidth / 10) * 4, (win.ActualHeight / 10) * 4, 200, 50));

            if (this.selectedHelp)
            {
                drawingContext.DrawGeometry(this.helpButtonSelectedBrush, null, g);
            }
            else
            {
                drawingContext.DrawGeometry(this.helpButtonBrush, null, g);
            }
        }
    }
}
