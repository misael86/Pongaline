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


        public override void Update(GameContainer gc)
        {
            this.Move(gc);
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
            
        }

        public override void Move(GameContainer gc)
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            this.velocity.x *= 0;

            if (translateTransform.Y > gc.mainGrid.ActualHeight / 2 || 
                translateTransform.Y < -gc.mainGrid.ActualHeight / 2) 
            { 
                this.velocity.y *= -1; 
            }


            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;

        }
    }
}
