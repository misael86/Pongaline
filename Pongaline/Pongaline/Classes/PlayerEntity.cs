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

        public Velocity lastVelocity { get; set; } 

        public Ellipse getEllipse()
        {
            return this.ellipse;
        }

        public override void Update()
        {
            Move();
        }

        public override void Paint()
        {
            base.Paint();

            this.lastVelocity = new Velocity() { x = 0, y = 0 };

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

            this.lastVelocity = new Velocity()
            {
                x = (float)e.Delta.Translation.X,
                y = (float)e.Delta.Translation.Y,
            };

            float newX = GlobalMethods.FromMiddleXToCornerXAxis((float)(imageTransform.X + e.Delta.Translation.X));
            float newY = GlobalMethods.FromMiddleYToCornerYAxis((float)(imageTransform.Y + e.Delta.Translation.Y));

            if (isLeftSide && newX > GlobalVariables.fieldMargin && newX < GlobalVariables.fieldMargin + GlobalVariables.playerFieldWidth)
            {
                ellipseTransform.X = imageTransform.X = GlobalMethods.FromCornerXToMiddleXAxis(newX);
            }
            else if (isLeftSide)
            {
                double distanceRight = Math.Abs(GlobalMethods.FromMiddleXToCornerXAxis((float)imageTransform.X) - GlobalVariables.fieldMargin - GlobalVariables.playerFieldWidth);
                double distanceLeft = Math.Abs(GlobalMethods.FromMiddleXToCornerXAxis((float)imageTransform.X) - GlobalVariables.fieldMargin);

                ellipseTransform.X = imageTransform.X = distanceRight < distanceLeft ?
                    GlobalMethods.FromCornerXToMiddleXAxis((float)(GlobalVariables.fieldMargin + GlobalVariables.playerFieldWidth)) :
                    GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldMargin);
            }

            if (!isLeftSide && newX > GameContainer.mainGrid.ActualWidth - GlobalVariables.playerFieldWidth - GlobalVariables.fieldMargin &&
                               newX < GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)
            {
                ellipseTransform.X = imageTransform.X = GlobalMethods.FromCornerXToMiddleXAxis(newX);
            }
            else if (!isLeftSide)
            {
                double distanceRight = Math.Abs(GlobalMethods.FromMiddleXToCornerXAxis((float)ellipseTransform.X) - (GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin));
                double distanceLeft = Math.Abs(GlobalMethods.FromMiddleXToCornerXAxis((float)ellipseTransform.X) - (GameContainer.mainGrid.ActualWidth - GlobalVariables.playerFieldWidth - GlobalVariables.fieldMargin));

                ellipseTransform.X = imageTransform.X = distanceRight < distanceLeft ?
                    GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)) :
                    GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - GlobalVariables.playerFieldWidth - GlobalVariables.fieldMargin));
            }

            if (newY > GlobalVariables.fieldMargin && newY < GameContainer.mainGrid.ActualHeight - GlobalVariables.fieldMargin)
            {
                ellipseTransform.Y = imageTransform.Y = GlobalMethods.FromCornerYToMiddleYAxis(newY);
            }
            else
            {
                ellipseTransform.Y = imageTransform.Y = GlobalMethods.FromMiddleYToCornerYAxis((float)imageTransform.Y) > GameContainer.mainGrid.ActualHeight / 2  ?
                    GlobalMethods.FromCornerYToMiddleYAxis((float)(GameContainer.mainGrid.ActualHeight - GlobalVariables.fieldMargin)) :
                    GlobalMethods.FromCornerYToMiddleYAxis(GlobalVariables.fieldMargin);
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
            if (isLeftSide)
            {
                speedX = 2;
            }
            else
            {
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
