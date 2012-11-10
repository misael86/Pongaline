using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Pongaline.Classes
{
    abstract class GameEntity
    {
        public Position position { get; set; }
        public Velocity velocity { get; set; }
        public Size size { get; set; }
        public Image image { get; set; }

        public abstract void Update();
        public abstract void Paint();
        public abstract void Move();
    }
}
