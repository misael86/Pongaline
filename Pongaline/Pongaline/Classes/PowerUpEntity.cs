using Pongaline.Common;
using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


namespace Pongaline.Classes
{

    enum PowerUps { FasterShot, BiggerSheild }

    class PowerUpEntity : GameEntity
    {

        

        Random r = new Random();

        public int whichPowerUp { get; set; }

        public PowerUpEntity()
        {

            DispatcherTimer lifeTimer = new DispatcherTimer();
            lifeTimer.Interval = new TimeSpan(0, 0, 0, 30, 0);
            lifeTimer.Tick += lifeTimer_Tick;
            lifeTimer.Start();

            PowerUps[] values = (PowerUps[])Enum.GetValues(typeof(PowerUps));
            PowerUps PowerUpRandom =  values[new Random().Next(0, values.Length)];

            switch (PowerUpRandom)
            {
                case PowerUps.FasterShot:
                    
                    break;

                case PowerUps.BiggerSheild:

                    break;
            }

        }

        public override void Update()
        {
            Move();
            CheckCollition();
        }

        public override void Move()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            //if (translateTransform.X > GlobalMethods.FromCornerXToMiddleXAxis((float)(GameContainer.mainGrid.ActualWidth - 30)) ||
            //    translateTransform.X < GlobalMethods.FromCornerXToMiddleXAxis(30))
            //{
            //    this.velocity.x *= -1;
            //}

            if (translateTransform.Y > GlobalMethods.FromCornerYToMiddleYAxis((float)(GameContainer.mainGrid.ActualHeight - 30)) ||
                translateTransform.Y < GlobalMethods.FromCornerYToMiddleYAxis(30))
            {
                this.velocity.y *= -1;
            }

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;
            this.position.x += this.velocity.x;
            this.position.y += this.velocity.y;
        }

        void lifeTimer_Tick(object o, object e)
        {
            GameContainer.RemoveEntity(this);
        }

        public override void Paint()
        {
            base.Paint();
        }

        private void CheckCollition()
        {
            var bullets = GameContainer.gameEntities.Where(ge => ge is BulletEntity);

            foreach (var bullet in bullets.ToList())
            {
                if (IsCollision(this, bullet))
                {
                    if ((bullet as BulletEntity).velocity.x > 0)
                    {
                        PlayerEntity player = GameContainer.gameEntities.First(ge => ge is PlayerEntity && (ge as PlayerEntity).isLeftSide) as PlayerEntity;
                        player.bulletSpeed = 4;
                    }
                    else
                    {
                        PlayerEntity player = GameContainer.gameEntities.First(ge => ge is PlayerEntity && !(ge as PlayerEntity).isLeftSide) as PlayerEntity;
                        player.bulletSpeed = 4;
                    }

                    GameContainer.RemoveEntity(this);
                    GameContainer.RemoveEntity(bullet);

                    //else if (this.velocity.x < 0 && (player as PlayerEntity).isLeftSide)
                    //{
                    //    GlobalMethods.GiveRightPlayerPoint();
                    //    GameContainer.RemoveEntity(this);
                    //}
                }
            }
        }

        private bool IsCollision(GameEntity ge1, GameEntity ge2)
        {

            var X1 = 0;
            var Y1 = ge1.position.y + (float)GameContainer.mainGrid.ActualHeight / 2;
            var X2 = GlobalMethods.FromCornerXToMiddleXAxis(ge2.position.x);
            var Y2 = GlobalMethods.FromCornerYToMiddleYAxis(ge2.position.y);

            var R1 = ge1.size.width / 2.0;
            var R2 = ge2.size.width / 2.0;
            var Radius = R1 + R2;

            var dX = X2 - X1;
            var dY = Y2 - Y1;

            return Math.Sqrt((dX * dX) + (dY * dY)) < Math.Sqrt(Radius * Radius);

        }

        /*
         * Maybe I should count loops and randomize in some way?
         */


    }



}
