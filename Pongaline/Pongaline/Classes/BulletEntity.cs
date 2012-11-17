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
    class BulletEntity : GameEntity
    {
        public bool isSpeedUp { get; set; }

        public override void Update()
        {
            Move();
            CheckCollition();
        }

        private void CheckCollition()
        {
            var players = GameContainer.gameEntities.Where(ge => ge is PlayerEntity);

            foreach (var player in players.ToList())
            {
                if (IsCollision(this, player))
                {
                    if (this.velocity.x > 0 && !(player as PlayerEntity).isLeftSide)
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(55, 255, 0, 144);

                        TranslateTransform translateTransform = new TranslateTransform()
                        {
                            X = this.position.x,
                            Y = this.position.y,
                        };

                        GlobalMethods.GiveLeftPlayerPoint();
                        GameContainer.RemoveEntity(this);
                    }
                    else if (this.velocity.x < 0 && (player as PlayerEntity).isLeftSide)
                    {
                        GlobalMethods.GiveRightPlayerPoint();
                        GameContainer.RemoveEntity(this);
                    }
                }
            }  
 
            var bricks = GameContainer.gameEntities.Where(ge => ge is PaddleEntity);

            foreach (var brick in bricks.ToList())
            {
                if (IsCollision(this, brick))
                {
                    if (this.velocity.x > 0 && brick.position.x > 0)
                    {
                        GameContainer.RemoveEntity(this);
                    }
                    else if (this.velocity.x < 0 && brick.position.x < 0)
                    {
                        GameContainer.RemoveEntity(this);
                    }
                }
            }
        }

        private bool IsCollision(GameEntity ge1, GameEntity ge2)
        {

            var X1 = GlobalMethods.FromCornerXToMiddleXAxis(ge1.position.x);
            var Y1 = GlobalMethods.FromCornerYToMiddleYAxis(ge1.position.y);
            var X2 = GlobalMethods.FromCornerXToMiddleXAxis(ge2.position.x);
            var Y2 = GlobalMethods.FromCornerYToMiddleYAxis(ge2.position.y);

            var R1 = ge1.size.width / 2.0;
            var R2 = ge2.size.width / 2.0;
            var Radius = R1 + R2;

            var dX = X2 - X1;
            var dY = Y2 - Y1;

            return Math.Sqrt((dX * dX) + (dY * dY)) < Math.Sqrt(Radius * Radius);

        }

        public override void Paint()
        {
            BitmapImage bulletEntity = new BitmapImage();

            if (this.velocity.x > 0) { bulletEntity.UriSource = new Uri("ms-appx:///Assets/DontSueUs/bulletRight.png"); }
            else { bulletEntity.UriSource = new Uri("ms-appx:///Assets/DontSueUs/bulletLeft.png"); }

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

            this.position.x += this.velocity.x;
            this.position.y += this.velocity.y;

            if (translateTransform.X > GameContainer.mainGrid.ActualWidth / 2 ||
                translateTransform.X < -GameContainer.mainGrid.ActualWidth / 2)
            {
                GameContainer.RemoveEntity(this);
            }
        }
    }


}
