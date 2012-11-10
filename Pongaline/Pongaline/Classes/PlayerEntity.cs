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
        }

        public override void Paint()
        {
            BitmapImage bitMapImage = new BitmapImage()
            {
                UriSource = new Uri("ms-appx:///Assets/SmallLogo.png"),
            };

            TranslateTransform translateTransform = new TranslateTransform()
            {
                X = this.position.x,
                Y = this.position.y,
            };

            this.image = new Image()
            {
                Source = bitMapImage,
                Width = this.size.width,
                Height = this.size.height,
                RenderTransform = translateTransform,
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

            GameContainer.mainGrid.Children.Add(this.image);
            GameContainer.mainGrid.Children.Add(this.ellipse);
        }

        void ellipse_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Shoot();
        }

        void ellipse_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            if (translateTransform.X + e.Delta.Translation.X < (GameContainer.mainGrid.ActualWidth - 60) / 2 &&
                translateTransform.X + e.Delta.Translation.X > -(GameContainer.mainGrid.ActualWidth - 60) / 2)
            {
                translateTransform.X += e.Delta.Translation.X;
            }
            else
            {
                translateTransform.X = e.Delta.Translation.X / Math.Abs(e.Delta.Translation.X) * (GameContainer.mainGrid.ActualWidth - 60) / 2;
            }

            if (translateTransform.Y + e.Delta.Translation.Y < (GameContainer.mainGrid.ActualHeight - 60) / 2 &&
                translateTransform.Y + e.Delta.Translation.Y > -(GameContainer.mainGrid.ActualHeight - 60) / 2)
            {
                translateTransform.Y += e.Delta.Translation.Y;
            }
            else
            {
                translateTransform.Y = e.Delta.Translation.Y / Math.Abs(e.Delta.Translation.Y) * (GameContainer.mainGrid.ActualHeight - 60) / 2;
            }

            this.position.x = (float)translateTransform.X;
            this.position.y = (float)translateTransform.Y;

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
