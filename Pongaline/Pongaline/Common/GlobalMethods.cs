using Pongaline.Classes;
using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pongaline.Common
{
    class GlobalMethods
    {
        public static Position GetCornerPositionFromMiddlePosition(Position middlePos)
        {
            return new Position()
                {
                    x = (float)(middlePos.x + GameContainer.mainGrid.ActualWidth / 2.0),
                    y = (float)(middlePos.y + GameContainer.mainGrid.ActualHeight / 2.0),
                };
        }

        public static Position GetMiddlePositionFromCornerPosition(Position cornerPos)
        {
            return new Position()
            {
                x = (float)(cornerPos.x - GameContainer.mainGrid.ActualWidth / 2.0),
                y = (float)(cornerPos.y - GameContainer.mainGrid.ActualHeight / 2.0),
            };
        }

        public static float FromMiddleYToCornerYAxis(float y)
        {
            return (float)(y + GameContainer.mainGrid.ActualHeight / 2.0);
        }

        public static float FromMiddleXToCornerXAxis(float x)
        {
            return (float)(x + GameContainer.mainGrid.ActualWidth / 2.0);
        }

        public static float FromCornerYToMiddleYAxis(float y)
        {
            return (float)(y - GameContainer.mainGrid.ActualHeight / 2.0);
        }

        public static float FromCornerXToMiddleXAxis(float x)
        {
            return (float)(x - GameContainer.mainGrid.ActualWidth / 2.0);
        }
    }
}
