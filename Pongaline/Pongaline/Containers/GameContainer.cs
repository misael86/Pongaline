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

        public List<GameEntity> _gameEntities = new List<GameEntity>();
        public List<GameEntity> gameEntities { get { return _gameEntities; } set { _gameEntities = value; } }

        public GameContainer() 
        { 
        }


        internal void Update()
        {
            foreach (GameEntity entity in gameEntities)
            {
                entity.Update();
            }
        }

        public void AddEntity(GameEntity entity)
        {
            gameEntities.Add(entity);
            entity.Paint();
        }
    }
}
