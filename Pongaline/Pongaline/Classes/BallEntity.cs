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

            if (translateTransform.X > GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - GlobalVariables.fieldMargin)))
            {
                var border = GameContainer.mainGrid.Children.FirstOrDefault(ui => ui is Border) as Border;
                var grid = border.Child as Grid;
                var textblock = grid.Children.FirstOrDefault(ui => ui is TextBlock && ((TextBlock)ui).Name == "TextBlock_LeftScore") as TextBlock;
                textblock.Text = (int.Parse(textblock.Text) + 1).ToString();
                Reset();
            }

            if (translateTransform.X < GlobalMethods.FromCornerXToMiddleXAxis(GlobalVariables.fieldMargin))
            {
                var border = GameContainer.mainGrid.Children.FirstOrDefault(ui => ui is Border) as Border;
                var grid = border.Child as Grid;
                var textblock = grid.Children.FirstOrDefault(ui => ui is TextBlock && ((TextBlock)ui).Name == "TextBlock_RightScore") as TextBlock;
                textblock.Text = (int.Parse(textblock.Text) + 1).ToString();
                Reset();
            }
        }

        public void Reset()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            translateTransform.X = 0;
            translateTransform.Y = 0;

            this.position.x = 0;
            this.position.y = 0;

            Random random = new Random();

            do
            {
                velocity.x = random.Next(-20, 20);
            } while (velocity.x == 0);

            do
            {
                velocity.y = random.Next(-20, 20);
            } while (velocity.y == 0);
        }
    }
}
