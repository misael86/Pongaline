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

    class PowerUpEntity : GameEntity
    {

        enum PowerUps { FasterShot, BiggerSheild }

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
                    this.imageURI = new Uri("ms-appx:///Assets/DontSueUs/powRed.png");
                    break;

                case PowerUps.BiggerSheild:
                    this.imageURI = new Uri("ms-appx:///Assets/DontSueUs/powGreen.png");
                    break;
            }

        }

        public override void Update()
        {
            Move();
        }

        public override void Move()
        {
            TranslateTransform translateTransform = this.image.RenderTransform as TranslateTransform;

            if (translateTransform.Y < GlobalMethods.FromCornerYToMiddleYAxis(0) ||
                translateTransform.Y > GlobalMethods.FromCornerYToMiddleYAxis(GlobalVariables.fieldHeight))
            {
                this.velocity.y *= -1;
            }

            translateTransform.X += this.velocity.x;
            translateTransform.Y += this.velocity.y;
        }

        void lifeTimer_Tick(object o, object e)
        {
            GameContainer.RemoveEntity(this);
        }

        public override void Paint()
        {
            base.Paint();
        }

    }



}
