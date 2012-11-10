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
    class BulletEntity : GameEntity
    {


        public override void Update()
        {
            this.Move();
        }

        public override void Paint()
        {
            BitmapImage bulletEntity = new BitmapImage
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
                Source = bulletEntity,
                Width = this.size.width,
                Height = this.size.height,
                RenderTransform = translateTransform,
            };

            GameContainer.mainGrid.Children.Add(this.image);
        }

        public override void Move()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;

            if (translateTransform.X > GameContainer.mainGrid.ActualWidth / 2 ||
                translateTransform.X < -GameContainer.mainGrid.ActualWidth / 2)
            {
                GameContainer.RemoveEntity(this);
            }
        }
    }


}
