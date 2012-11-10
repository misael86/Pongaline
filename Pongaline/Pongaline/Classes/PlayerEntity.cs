using Pongaline.Common;
using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Pongaline.Classes
{
    class PlayerEntity : GameEntity
    {
        Ellipse ellipse = new Ellipse();
        bool isLeftSide { get; set; }

        public override void Update()
        {
            Move();
        }

        public override void Paint()
        {
            base.Paint();

            TranslateTransform translateTransform = new TranslateTransform()
            {
                X = this.position.x,
                Y = this.position.y,
            };

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(55, 255, 255, 0);

            this.ellipse.Fill = mySolidColorBrush;
            this.ellipse.Width = this.size.width;
            this.ellipse.Height = this.size.height;
            this.ellipse.RenderTransform = translateTransform;
            this.ellipse.ManipulationDelta += ellipse_ManipulationDelta;
            this.ellipse.ManipulationMode = Windows.UI.Xaml.Input.ManipulationModes.All;
            this.ellipse.Tapped += ellipse_Tapped;

            if (this.position.x < 0)
            {
                isLeftSide = true;
            }
            else
            {
                isLeftSide = false;
            }

            GameContainer.mainGrid.Children.Add(this.ellipse);
        }

        void ellipse_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Shoot();
        }

        void ellipse_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform imageTransform = this.image.RenderTransform as TranslateTransform;
            TranslateTransform ellipseTransform = this.ellipse.RenderTransform as TranslateTransform;

            float newX = GlobalMethods.FromMiddleXToCornerXAxis((float)(imageTransform.X + e.Delta.Translation.X));
            float newY = GlobalMethods.FromMiddleYToCornerYAxis((float)(imageTransform.Y + e.Delta.Translation.Y));




            if (newX > GlobalVariables.fieldMargin && newX < GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)
            {
                ellipseTransform.X = imageTransform.X = GlobalMethods.FromCornerXToMiddleXAxis(newX);
            }
            else
            {
                ellipseTransform.X = imageTransform.X = e.Delta.Translation.X > 0 ?
                    GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)) :
                    GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldMargin);
            }




            if (newY > GlobalVariables.fieldMargin && newY < GameContainer.mainGrid.ActualHeight - GlobalVariables.fieldMargin)
            {
                ellipseTransform.Y = imageTransform.Y = GlobalMethods.FromCornerYToMiddleYAxis(newY);
            }
            else
            {
                ellipseTransform.Y = imageTransform.Y = e.Delta.Translation.Y > 0 ?
                    GlobalMethods.FromCornerYToMiddleYAxis((float)(GameContainer.mainGrid.ActualHeight - GlobalVariables.fieldMargin)) :
                    GlobalMethods.FromCornerYToMiddleYAxis(GlobalVariables.fieldMargin);
            }



            if (!isLeftSide)
            {
                if (newX > GlobalVariables.playerFieldWidth && newX > GameContainer.mainGrid.ActualWidth - GlobalVariables.playerFieldWidth)
                {
                    ellipseTransform.X = imageTransform.X = GlobalMethods.FromCornerXToMiddleXAxis(newX);
                }
                else
                {
                    ellipseTransform.X = imageTransform.X = e.Delta.Translation.X < 0 ?
                        GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - GlobalVariables.playerFieldWidth)) :
                        GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.playerFieldWidth);
                }
            }
            else
            {
                if (newX < GlobalVariables.playerFieldWidth && newX < GameContainer.mainGrid.ActualWidth + GlobalVariables.playerFieldWidth)
                {
                    ellipseTransform.X = imageTransform.X = GlobalMethods.FromCornerXToMiddleXAxis(newX);
                }
                else
                {
                    ellipseTransform.X = imageTransform.X = e.Delta.Translation.X < 0 ?
                        GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth + GlobalVariables.playerFieldWidth)) :
                        GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.playerFieldWidth);
                }
            }


            this.position.x = (float)ellipseTransform.X;
            this.position.y = (float)ellipseTransform.Y;

        }

        public override void Move()
        {


        }

        public void Shoot()
        {
            int speedX;
            if(isLeftSide) 
            {
                speedX = 2;
            } else {
                speedX = -2;
            }

            BulletEntity bullet = new BulletEntity()
            {

                position = new Position()
                {
                    x = this.position.x,
                    y = this.position.y,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = 50,
                    width = 100,
                },

                velocity = new Velocity()
                {
                    x = speedX,
                    y = 0,
                }
            };

            GameContainer.AddEntity(bullet);

        }
    }
}
