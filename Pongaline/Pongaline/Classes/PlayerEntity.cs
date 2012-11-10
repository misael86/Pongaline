using Pongaline.Common;
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
            base.Paint();

            TranslateTransform translateTransform = new TranslateTransform()
            {
                X = this.position.x,
                Y = this.position.y,
            };

            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(55, 255, 255, 0);

            this.ellipse.Fill = mySolidColorBrush;
            this.ellipse.Width = this.size.width;
            this.ellipse.Height = this.size.height;
            this.ellipse.RenderTransform = translateTransform;
            this.ellipse.ManipulationDelta += ellipse_ManipulationDelta;
            this.ellipse.ManipulationMode = Windows.UI.Xaml.Input.ManipulationModes.All;

            GameContainer.mainGrid.Children.Add(this.ellipse);
        }

        void ellipse_ManipulationDelta(object sender, Windows.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs e)
        {
            TranslateTransform imageTransform = this.image.RenderTransform as TranslateTransform;
            TranslateTransform ellipseTransform = this.ellipse.RenderTransform as TranslateTransform;

            float newX = GlobalMethods.FromMiddleXToCornerXAxis((float)(imageTransform.X + e.Delta.Translation.X));
            float newY = GlobalMethods.FromMiddleYToCornerYAxis((float)(imageTransform.Y + e.Delta.Translation.Y));

            if (newX > GlobalVariables.fieldMargin && newX < GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)
            {
                ellipseTransform.X = imageTransform.X = GlobalMethods.FromCornerXToMiddleXAxis(newX);
            }
            else
            {
                ellipseTransform.X = imageTransform.X = e.Delta.Translation.X > 0 ?
                    GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)) :
                    GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldMargin);
            }

            if (newY > GlobalVariables.fieldMargin && newY < GameContainer.mainGrid.ActualHeight - GlobalVariables.fieldMargin)
            {
                ellipseTransform.Y = imageTransform.Y = GlobalMethods.FromCornerYToMiddleYAxis(newY);
            }
            else
            {
                ellipseTransform.Y = imageTransform.Y = e.Delta.Translation.Y > 0 ?
                    GlobalMethods.FromCornerYToMiddleYAxis((float)(GameContainer.mainGrid.ActualHeight - GlobalVariables.fieldMargin)) :
                    GlobalMethods.FromCornerYToMiddleYAxis(GlobalVariables.fieldMargin);
            }
        }

        public override void Move()
        {

        }
    }
}
