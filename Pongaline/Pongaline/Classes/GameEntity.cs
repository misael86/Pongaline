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

namespace Pongaline.Classes
{
    abstract class GameEntity
    {
        public Position position { get; set; }
        public Velocity velocity { get; set; }
        public Size size { get; set; }
        public Image image { get; set; }
        public Uri imageURI { get; set; }

        public abstract void Update();
        public virtual void Paint() 
        {
            BitmapImage bitMapImage = new BitmapImage()
            {
                UriSource = this.imageURI,
            };

            TranslateTransform translateTransform = new TranslateTransform()
            {
                X = GlobalMethods.GetCornerPositionFromMiddlePosition(this.position).x,
                Y = GlobalMethods.GetCornerPositionFromMiddlePosition(this.position).y,
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
        public abstract void Move();
    }
}
