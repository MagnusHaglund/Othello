using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Othello
{
    public class OthelloImage : Image
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Image DiscImage { get; set; }
        public OthelloDisc Disc { get; set; }

        public OthelloImage(int x, int y, Image discImage)
        {
            this.X = x;
            this.Y = y;
            this.DiscImage = discImage;
            Disc = OthelloDisc.none;
        }

        public void ChangeToWhite()
        {
            DiscImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/DiscWhite.png"));
            Disc = OthelloDisc.white;
        }

        public void ChangeToBlack()
        {
            DiscImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/DiscBlack.png"));
            Disc = OthelloDisc.black;
        }
    }
}
