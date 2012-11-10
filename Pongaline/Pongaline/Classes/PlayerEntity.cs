using Pongaline.Containers;
using System;
using System.Collections.Generic;
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

            GameContainer.mainGrid.Children.Add(this.image);
            GameContainer.mainGrid.Children.Add(this.ellipse);
        }

        void ellipse_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            if (translateTransform.X + e.Delta.Translation.X < (GameContainer.mainGrid.ActualWidth - 200) / 2 &&
                translateTransform.X + e.Delta.Translation.X > -(GameContainer.mainGrid.ActualWidth - 200) / 2)
            {
                translateTransform.X += e.Delta.Translation.X;
            }

            if (translateTransform.Y + e.Delta.Translation.Y < (GameContainer.mainGrid.ActualHeight - 200) / 2 &&
                translateTransform.Y + e.Delta.Translation.Y > -(GameContainer.mainGrid.ActualHeight - 200) / 2)
            {
                translateTransform.Y += e.Delta.Translation.Y;
            }
        }

        public override void Move()
        {
            
        }
    }
}
