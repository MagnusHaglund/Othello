using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class SkyNet
    {
        private int[,] worthMatrix = new int[8,8];

        private const int HIGH_VAL_TARGET = 5;
        private const int HIGHMID_VAL_TARGET = 4;
        private const int MID_VAL_TARGET = 3;
        private const int LOWMID_VAL_TARGET = 2;
        private const int LOW_VAL_TARGET = 1;

        public SkyNet()
        {
            fillWorthMatrix();
        }

        public Tuple<int, int> NextMove(MainWindow mw)
        {
            OthelloImage toTry;
            int highestTargetValue = 0;
            Tuple<int, int> result = null;

            for (int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    toTry = mw.ImageMatrix[x, y];
                    if (toTry.Disc == OthelloDisc.none)
                    {
                        if (mw.allowedPlacement(toTry, false) == true)
                        {
                            if (worthMatrix[x, y] > highestTargetValue)
                            {
                                highestTargetValue = worthMatrix[x, y];
                                result = new Tuple<int, int>(x, y);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private void fillWorthMatrix()
        {
            worthMatrix[0, 0] = HIGH_VAL_TARGET;
            worthMatrix[0, 1] = LOW_VAL_TARGET;
            worthMatrix[0, 2] = HIGHMID_VAL_TARGET;
            worthMatrix[0, 3] = HIGHMID_VAL_TARGET;
            worthMatrix[0, 4] = HIGHMID_VAL_TARGET;
            worthMatrix[0, 5] = HIGHMID_VAL_TARGET;
            worthMatrix[0, 6] = LOW_VAL_TARGET;
            worthMatrix[0, 7] = HIGH_VAL_TARGET;
            worthMatrix[1, 0] = LOW_VAL_TARGET;
            worthMatrix[1, 1] = LOW_VAL_TARGET;
            worthMatrix[1, 2] = LOWMID_VAL_TARGET;
            worthMatrix[1, 3] = LOWMID_VAL_TARGET;
            worthMatrix[1, 4] = LOWMID_VAL_TARGET;
            worthMatrix[1, 5] = LOWMID_VAL_TARGET;
            worthMatrix[1, 6] = LOW_VAL_TARGET;
            worthMatrix[1, 7] = LOW_VAL_TARGET;
            worthMatrix[2, 0] = HIGHMID_VAL_TARGET;
            worthMatrix[2, 1] = LOWMID_VAL_TARGET;
            worthMatrix[2, 2] = MID_VAL_TARGET;
            worthMatrix[2, 3] = MID_VAL_TARGET;
            worthMatrix[2, 4] = MID_VAL_TARGET;
            worthMatrix[2, 5] = MID_VAL_TARGET;
            worthMatrix[2, 6] = LOWMID_VAL_TARGET;
            worthMatrix[2, 7] = HIGHMID_VAL_TARGET;
            worthMatrix[3, 0] = HIGHMID_VAL_TARGET;
            worthMatrix[3, 1] = LOWMID_VAL_TARGET;
            worthMatrix[3, 2] = MID_VAL_TARGET;
            worthMatrix[3, 3] = 0;
            worthMatrix[3, 4] = 0;
            worthMatrix[3, 5] = MID_VAL_TARGET;
            worthMatrix[3, 6] = LOWMID_VAL_TARGET;
            worthMatrix[3, 7] = HIGHMID_VAL_TARGET;
            worthMatrix[4, 0] = HIGHMID_VAL_TARGET;
            worthMatrix[4, 1] = LOWMID_VAL_TARGET;
            worthMatrix[4, 2] = MID_VAL_TARGET;
            worthMatrix[4, 3] = 0;
            worthMatrix[4, 4] = 0;
            worthMatrix[4, 5] = MID_VAL_TARGET;
            worthMatrix[4, 6] = LOWMID_VAL_TARGET;
            worthMatrix[4, 7] = HIGHMID_VAL_TARGET;
            worthMatrix[5, 0] = HIGHMID_VAL_TARGET;
            worthMatrix[5, 1] = LOWMID_VAL_TARGET;
            worthMatrix[5, 2] = MID_VAL_TARGET;
            worthMatrix[5, 3] = MID_VAL_TARGET;
            worthMatrix[5, 4] = MID_VAL_TARGET;
            worthMatrix[5, 5] = MID_VAL_TARGET;
            worthMatrix[5, 6] = LOWMID_VAL_TARGET;
            worthMatrix[5, 7] = HIGHMID_VAL_TARGET;
            worthMatrix[6, 0] = LOW_VAL_TARGET; 
            worthMatrix[6, 1] = LOW_VAL_TARGET;
            worthMatrix[6, 2] = LOWMID_VAL_TARGET;
            worthMatrix[6, 3] = LOWMID_VAL_TARGET;
            worthMatrix[6, 4] = LOWMID_VAL_TARGET;
            worthMatrix[6, 5] = LOWMID_VAL_TARGET;
            worthMatrix[6, 6] = LOW_VAL_TARGET;
            worthMatrix[6, 7] = LOW_VAL_TARGET;
            worthMatrix[7, 0] = HIGH_VAL_TARGET;
            worthMatrix[7, 1] = LOW_VAL_TARGET;
            worthMatrix[7, 2] = HIGHMID_VAL_TARGET;
            worthMatrix[7, 3] = HIGHMID_VAL_TARGET;
            worthMatrix[7, 4] = HIGHMID_VAL_TARGET;
            worthMatrix[7, 5] = HIGHMID_VAL_TARGET;
            worthMatrix[7, 6] = LOW_VAL_TARGET;
            worthMatrix[7, 7] = HIGH_VAL_TARGET;
        }
    }
}
