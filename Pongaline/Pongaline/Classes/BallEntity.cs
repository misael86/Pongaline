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
    class BallEntity : GameEntity
    {
        public override void Update()
        {
            Move();
            CheckCollition();
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
                translateTransform.Y < GlobalMethods.FromCornerYToMiddleYAxis(30))
            {
                this.velocity.y *= -1;
            }

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;

            position.x += this.velocity.x;
            position.y += this.velocity.y;

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

        private void CheckCollition()
        {
            var players = GameContainer.gameEntities.Where(ge => ge is PlayerEntity);

            foreach (var player in players)
            {
                if (IsCollision(this, player))
                {
                    this.velocity.y += (float)(1.3 * (player as PlayerEntity).lastVelocity.y);

                    this.velocity.x = GlobalMethods.FromMiddleXToCornerXAxis(this.position.x) < GameContainer.mainGrid.ActualWidth / 2 ?
                        Math.Abs(velocity.x) + Math.Abs((player as PlayerEntity).lastVelocity.x) :
                        -Math.Abs(velocity.x) - Math.Abs((player as PlayerEntity).lastVelocity.x);
                }
            }

            var paddles = GameContainer.gameEntities.Where(ge => ge is PaddleEntity);

            foreach (var paddle in paddles)
            {
                if (IsCollision(this, paddle))
                {
                    this.velocity.x = GlobalMethods.FromMiddleXToCornerXAxis(this.position.x) < GameContainer.mainGrid.ActualWidth / 2 ?
                        Math.Abs(velocity.x) : -Math.Abs(velocity.x);
                }
            }

        }

        private bool IsCollision(GameEntity ge1, GameEntity ge2)
        {

            var X1 = ge1.position.x;
            var Y1 = ge1.position.y;
            var X2 = ge2.position.x;
            var Y2 = ge2.position.y;

            var R1 = ge1.size.width / 2.0;
            var R2 = ge2.size.width / 2.0;
            var Radius = R1 + R2;

            var dX = X2 - X1;
            var dY = Y2 - Y1;

            return Math.Sqrt((dX * dX) + (dY * dY)) <= Math.Sqrt(Radius * Radius);

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
