using Pongaline.Classes;
using Pongaline.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
            return (float)(y + GlobalVariables.fieldWidth / 2.0);
        }

        public static float FromMiddleXToCornerXAxis(float x)
        {
            return (float)(x + GlobalVariables.fieldWidth / 2.0);
        }

        public static float FromCornerYToMiddleYAxis(float y)
        {
            return (float)(y - GlobalVariables.fieldHeight / 2.0);
        }

        public static float FromCornerXToMiddleXAxis(float x)
        {
            return (float)(x - GlobalVariables.fieldWidth / 2.0);
        }


        public static void GiveRightPlayerPoint()
        {
            var border = GameContainer.mainGrid.Children.FirstOrDefault(ui => ui is Border) as Border;
            var grid = border.Child as Grid;
            var textblock = grid.Children.FirstOrDefault(ui => ui is TextBlock && ((TextBlock)ui).Name == "TextBlock_RightScore") as TextBlock;
            textblock.Text = (int.Parse(textblock.Text) + 1).ToString();
        }

        public static void GiveLeftPlayerPoint()
        {
            var border = GameContainer.mainGrid.Children.FirstOrDefault(ui => ui is Border) as Border;
            var grid = border.Child as Grid;
            var textblock = grid.Children.FirstOrDefault(ui => ui is TextBlock && ((TextBlock)ui).Name == "TextBlock_LeftScore") as TextBlock;
            textblock.Text = (int.Parse(textblock.Text) + 1).ToString();
            
        }
    }
}
