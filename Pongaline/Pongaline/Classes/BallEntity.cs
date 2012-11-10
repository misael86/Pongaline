using Pongaline.Common;
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

        public override void Move()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            if (translateTransform.X > GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - 30)) ||
                translateTransform.X < GlobalMethods.FromCornerXToMiddleXAxis(30)) 
            { 
                this.velocity.x *= -1; 
            }

            if (translateTransform.Y > GlobalMethods.FromCornerYToMiddleYAxis((float)(GameContainer.mainGrid.ActualHeight - 30)) ||
                translateTransform.Y < GlobalMethods.FromCornerYToMiddleYAxis(30) ) 
            { 
                this.velocity.y *= -1; 
            }

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;
        }
    }
}
