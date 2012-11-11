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
        public bool isLeftSide { get; set; }

        public Velocity lastVelocity { get; set; }

        MiniPlayerEntity miniPlayer = new MiniPlayerEntity();

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

            if (this.position.x < 0)
            {
                isLeftSide = true;
            }
            else
            {
                isLeftSide = false;
            }

            this.lastVelocity = new Velocity() { x = 0, y = 0 };

            TranslateTransform translateTransform = new TranslateTransform()
            {
                X = this.position.x,
                Y = this.position.y,
            };

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            if (isLeftSide)
            {
                mySolidColorBrush.Color = Color.FromArgb(50, 124, 252, 0);
            }
            else
            { 
                mySolidColorBrush.Color = Color.FromArgb(50, 255, 215, 0);
            }

            this.ellipse.Fill = mySolidColorBrush;
            this.ellipse.Width = this.size.width;
            this.ellipse.Height = this.size.height;
            this.ellipse.RenderTransform = translateTransform;
            this.ellipse.ManipulationDelta += ellipse_ManipulationDelta;
            this.ellipse.ManipulationMode = Windows.UI.Xaml.Input.ManipulationModes.All;
            this.ellipse.Tapped += ellipse_Tapped;

            GameContainer.mainGrid.Children.Add(this.ellipse);


            miniPlayer.position = new Position()
            {
                x = this.position.x + 30,
                y = this.position.y,
            };

            miniPlayer.size = new Pongaline.Classes.Size
                {
                    height = 100,
                    width = 30,
                };

            miniPlayer.imageURI = new Uri("ms-appx:///Assets/DontSueUs/brick.png");

            GameContainer.AddEntity(miniPlayer);
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

            var newX = imageTransform.X + e.Delta.Translation.X;
            var newY = imageTransform.Y + e.Delta.Translation.Y;

            if (isLeftSide && newX > GlobalMethods.FromCornerXToMiddleXAxis(0)
                           && newX < GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.playerFieldWidth))
            {
                ellipseTransform.X = imageTransform.X = newX;
            }
            else if (isLeftSide)
            {
                double distanceRight = Math.Abs(newX - GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.playerFieldWidth));
                double distanceLeft = Math.Abs(newX - GlobalMethods.FromCornerXToMiddleXAxis(0));

                ellipseTransform.X = imageTransform.X = distanceRight < distanceLeft ?
                    GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.playerFieldWidth) :
                    GlobalMethods.FromCornerXToMiddleXAxis(0);
            }

            if (!isLeftSide && newX > GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldWidth - GlobalVariables.playerFieldWidth) &&
                               newX < GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldWidth) )
            {
                ellipseTransform.X = imageTransform.X = newX;
            }
            else if (!isLeftSide)
            {
                double distanceRight = Math.Abs(newX - GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldWidth));
                double distanceLeft = Math.Abs(newX - GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldWidth - GlobalVariables.playerFieldWidth));

                ellipseTransform.X = imageTransform.X = distanceRight < distanceLeft ?
                    GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldWidth) :
                    GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldWidth - GlobalVariables.playerFieldWidth);
            }

            if (newY > GlobalMethods.FromCornerYToMiddleYAxis(0) && 
                newY < GlobalMethods.FromCornerYToMiddleYAxis(GlobalVariables.fieldHeight))
            {
                ellipseTransform.Y = imageTransform.Y = newY;
            }
            else
            {
                ellipseTransform.Y = imageTransform.Y = newY > 0 ?
                    GlobalMethods.FromCornerYToMiddleYAxis(GlobalVariables.fieldHeight) :
                    GlobalMethods.FromCornerYToMiddleYAxis(0);
            }

            this.position.x = (float)ellipseTransform.X;
            this.position.y = (float)ellipseTransform.Y;

        }

        public override void Move()
        {


        }

        public void Shoot()
        {
            int speedX = isLeftSide ? 8 : -8;

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
