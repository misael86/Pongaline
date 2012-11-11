using Pongaline.Classes;
using Pongaline.Common;
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

        DispatcherTimer runPowerUpGeneratorTimer;
        Random r = new Random();

        public MainPage()
        {
            this.InitializeComponent();

            GameContainer.mainGrid = this.MainGrid;

            DispatcherTimer runGameTimer = new DispatcherTimer();
            runGameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            runGameTimer.Tick += runGameTimer_Tick;
            runGameTimer.Start();
            
            runPowerUpGeneratorTimer = new DispatcherTimer();
            InitPowerUpGenerator();
            runPowerUpGeneratorTimer.Tick += RunPowerUpGenerator;
            runPowerUpGeneratorTimer.Start();

            InitiateData();
        }

        private void InitiateData()
        {
            #region BALL

            BallEntity ball = new BallEntity()
            {
                position = new Position()
                {
                    x = 0,
                    y = 0,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = 30,
                    width = 30,
                },

                velocity = new Velocity()
                {
                    x = 10,
                    y = 15,
                },

                imageURI = new Uri("ms-appx:///Assets/SmallLogo.png"),
            };

            GameContainer.AddEntity(ball);

            #endregion

            #region PLAYER 1

            PlayerEntity player1 = new PlayerEntity()
            {
                position = new Position()
                {
                    x = GlobalMethods.FromCornerXToMiddleXAxis((float)GameContainer.mainGrid.ActualWidth - 600),
                    y = 0,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = 80,
                    width = 80,
                },

                imageURI = new Uri("ms-appx:///Assets/SmallLogo.png"),
            };

            GameContainer.AddEntity(player1);

            #endregion

            #region PLAYER 2

            

            PlayerEntity player2 = new PlayerEntity()
            {
                position = new Position()
                {
                    x = GlobalMethods.FromCornerXToMiddleXAxis((float)GameContainer.mainGrid.ActualWidth + 600),
                    y = 0,
                },

                size = new Pongaline.Classes.Size()
                {
                    height = 80,
                    width = 80,
                },

                imageURI = new Uri("ms-appx:///Assets/SmallLogo.png"),
            };

            GameContainer.AddEntity(player2);

            #endregion

            #region PADDLE 1

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

                imageURI = new Uri("ms-appx:///Assets/SmallLogo.png"),
            };
            GameContainer.AddEntity(paddlePlayerOne);
            #endregion

            #region PADDLE 2

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

                imageURI = new Uri("ms-appx:///Assets/SmallLogo.png"),
            };

            GameContainer.AddEntity(paddlePlayerTwo);
            #endregion



        }

        Random randomSec = new Random();

        private void InitPowerUpGenerator()
        {
            TimeSpan timeSpanPowerUp;
            timeSpanPowerUp = new TimeSpan(0, 0, 0, r.Next(60), 0);
            runPowerUpGeneratorTimer.Interval = timeSpanPowerUp;
            
            
        }

        void runGameTimer_Tick(object sender, object e)
        {
            gameContainer.Update();
        }

        void RunPowerUpGenerator(object sender, object e)
        {
            PowerUpEntity powerUP = new PowerUpEntity()
            {
                position = new Position()
                {
                    x = -(float)GameContainer.mainGrid.ActualWidth / 2,
                    y = -(float)GameContainer.mainGrid.ActualHeight / 2,
                },

                size = new Pongaline.Classes.Size
                {
                    height = 40,
                    width = 40,
                },

                velocity = new Velocity()
                {
                    x = 0,
                    y = r.Next(40),
                },

                imageURI = new Uri("ms-appx:///Assets/SplashScreen.png"),
            };


            GameContainer.AddEntity(powerUP);
            InitPowerUpGenerator();
            //randomSeconds = randomSec.Next(10);
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
