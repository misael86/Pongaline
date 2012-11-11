﻿using Pongaline.Common;
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
        }

        void lifeTimer_Tick(object o, object e)
        {
            GameContainer.RemoveEntity(this);
        }

        public override void Paint()
        {
            base.Paint();
        }

        /*
         * Maybe I should count loops and randomize in some way?
         */


    }



}
