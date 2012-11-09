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
    class BallEntity : GameEntity
    {


        public override void Update(GameContainer gameContainer)
        {
            this.Move(gameContainer);
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

        }

        public override void Move(GameContainer gameContainer)
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            if (translateTransform.X > gameContainer.mainGrid.ActualWidth / 2 ||
                translateTransform.X < -gameContainer.mainGrid.ActualWidth / 2) 
            { 
                this.velocity.x *= -1; 
            }

            if (translateTransform.Y > gameContainer.mainGrid.ActualHeight / 2 || 
                translateTransform.Y < -gameContainer.mainGrid.ActualHeight / 2) 
            { 
                this.velocity.y *= -1; 
            }

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;
        }
    }
}
