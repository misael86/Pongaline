﻿using Pongaline.Classes;
using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pongaline
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GameContainer gameContainer = new GameContainer();

        public MainPage()
        {
            this.InitializeComponent();

            GameContainer.mainGrid = this.MainGrid;

            DispatcherTimer runGameTimer = new DispatcherTimer();
            runGameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            runGameTimer.Tick += runGameTimer_Tick;
            runGameTimer.Start();

            InitiateData();
        }

        private void InitiateData()
        {
            #region BALL

            Random r = new Random();

            BallEntity ball = new BallEntity()
            {
                position = new Position()
                {
                    x = 0,
                    y = 0,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = r.Next(50),
                    width = r.Next(50),
                },

                velocity = new Velocity()
                {
                    x = 10,
                    y = 15,
                },
            };

            gameContainer.AddEntity(ball);

            #endregion

            #region PLAYER 1

            PlayerEntity player1 = new PlayerEntity()
            {
                position = new Position()
                {
                    x = 0,
                    y = 0,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = 15,
                    width = 15,
                },
            };

            #endregion

            #region PLAYER 2

            gameContainer.AddEntity(player1);

            PlayerEntity player2 = new PlayerEntity()
            {
                position = new Position()
                {
                    x = 0,
                    y = 0,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = 15,
                    width = 15,
                },
            };

            gameContainer.AddEntity(player2);

            #endregion

            #region init PaddlePlayerOne

            PaddleEntity paddlePlayerOne = new PaddleEntity()
            {
                position = new Position()
                {
                    x = -480,
                    y = 0,
                },

                size = new Pongaline.Classes.Size
                {
                    height = 100,
                    width = 30,
                },

                velocity = new Velocity()
                {
                    x = 0,
                    y = 5,
                },
            };
            gameContainer.AddEntity(paddlePlayerOne);
            #endregion

            #region Init paddlePLayerTwo

            PaddleEntity paddlePlayerTwo = new PaddleEntity()
            {
                position = new Position()
                {
                    x = 480,
                    y = 0,
                },

                size = new Pongaline.Classes.Size
                {
                    height = 100,
                    width = 30,
                },

                velocity = new Velocity()
                {
                    x = 0,
                    y = -5,
                },
            };

            gameContainer.AddEntity(paddlePlayerTwo);
            #endregion
        }

        void runGameTimer_Tick(object sender, object e)
        {
            gameContainer.Update();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}