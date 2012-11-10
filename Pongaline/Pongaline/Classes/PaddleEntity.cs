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
