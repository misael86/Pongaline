using Pongaline.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Pongaline.Containers
{
    class GameContainer
    {
        public static Grid mainGrid { get; set; }

        public static List<GameEntity> _gameEntities = new List<GameEntity>();
        public static List<GameEntity> gameEntities { get { return _gameEntities; } set { _gameEntities = value; } }

        public GameContainer() 
        { 
        }


        internal void Update()
        {
            foreach (GameEntity entity in gameEntities.ToList())
            {
                entity.Update();
            }
        }

        public static void AddEntity(GameEntity entity)
        {
            gameEntities.Add(entity);
            entity.Paint();
        }

        public static void RemoveEntity(GameEntity entity)
        {
            gameEntities.Remove(entity);
            mainGrid.Children.Remove(entity.image);
        }
    }
}
