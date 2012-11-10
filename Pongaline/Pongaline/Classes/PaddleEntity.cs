using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Pongaline.Classes
{
    class PaddleEntity : GameEntity
    {


        public override void Update()
        {
            this.Move();
        }

        public override void Paint()
        {
            BitmapImage paddleBitmap = new BitmapImage 
            {
                UriSource = new Uri("ms-appx:///Assets/StoreLogo.png"),
            };

            TranslateTransform translateTransform = new TranslateTransform()
            {
                X = this.position.x,
                Y = this.position.y,
            };

            this.image = new Image()
            {
                Source = paddleBitmap,
                Width = this.size.width,
                Height = this.size.height,
                RenderTransform = translateTransform,
            };

            GameContainer.mainGrid.Children.Add(this.image);
        }

        public override void Move()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            this.velocity.x *= 0;

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
