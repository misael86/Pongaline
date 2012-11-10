using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Pongaline.Classes
{
    class BallEntity : GameEntity
    {
        public override void Update()
        {
            this.Move();
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

            GameContainer.mainGrid.Children.Add(this.image);
        }

        public override void Move()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            if (translateTransform.X > GameContainer.mainGrid.ActualWidth / 2 ||
                translateTransform.X < -GameContainer.mainGrid.ActualWidth / 2) 
            { 
                this.velocity.x *= -1; 
            }

            if (translateTransform.Y > GameContainer.mainGrid.ActualHeight / 2 ||
                translateTransform.Y < -GameContainer.mainGrid.ActualHeight / 2) 
            { 
                this.velocity.y *= -1; 
            }

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;
        }
    }
}
