using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Othello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OthelloImage[,] imageMatrix = new OthelloImage[8, 8];

        private OthelloDisc whoseTurn = OthelloDisc.white;
        private BitmapImage bmpimgCoinWhite = new BitmapImage(new Uri("pack://application:,,,/Images/DiscWhite.png"));
        private BitmapImage bmpimgCoinBlack = new BitmapImage(new Uri("pack://application:,,,/Images/DiscBlack.png"));

        public MainWindow()
        {
            InitializeComponent();
            AddImages();
            //SoundPlayer player = new SoundPlayer(new Uri("pack://application:,,,/Music/popcorn.wav"));
            SoundPlayer player = new SoundPlayer(Properties.Resources.popcorn);
            player.Load();
            player.PlayLooping();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Images/Icon.png"));
        }

        private void AddImages()
        {
            bool fubar;
            BitmapImage imageSource = null;
            OthelloImage othelloImage = null;
            Image discImage = null;
            for (int rowIndex = 0; rowIndex < MainBoardGrid.RowDefinitions.Count; rowIndex++)
            {
                if (rowIndex % 2 != 0)
                {
                    fubar = false;
                }
                else
                {
                    fubar = true;
                }

                for (int columnIndex = 0; columnIndex < MainBoardGrid.ColumnDefinitions.Count; columnIndex++)
                {
                    if (fubar)
                    {
                        imageSource = new BitmapImage(new Uri("pack://application:,,,/Images/DarkerGreen.jpg"));
                        fubar = false;
                    }
                    else
                    {
                        imageSource = new BitmapImage(new Uri("pack://application:,,,/Images/BrighterGreen.jpg"));
                        fubar = true;
                    }
                    discImage = new Image();
                    othelloImage = new OthelloImage(columnIndex, rowIndex, discImage) { Source = imageSource };
                    othelloImage.Stretch = Stretch.Fill;

                    Thickness margin = othelloImage.Margin;
                    margin.Top = 1;
                    margin.Right = 1;
                    margin.Bottom = 1;
                    margin.Left = 1;
                    othelloImage.Margin = margin;

                    othelloImage.MouseDown += Image_MouseDown;
                    imageMatrix[columnIndex, rowIndex] = othelloImage;

                    Grid.SetRow(othelloImage, rowIndex);
                    Grid.SetColumn(othelloImage, columnIndex);
                    MainBoardGrid.Children.Add(othelloImage);

                    Grid.SetRow(discImage, rowIndex);
                    Grid.SetColumn(discImage, columnIndex);
                    MainBoardGrid.Children.Add(discImage);

                    if ((rowIndex == 3 && columnIndex == 3) || (rowIndex == 4 && columnIndex == 4))
                    {
                        othelloImage.ChangeToWhite();
                    }

                    if ((rowIndex == 3 && columnIndex == 4) || (rowIndex == 4 && columnIndex == 3))
                    {
                        othelloImage.ChangeToBlack();
                    }
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var othelloImage = (OthelloImage)sender;
            //MessageBox.Show("Hello x=" + othelloImage.X + " " + imageMatrix[othelloImage.X, othelloImage.Y].X + " and y=" + othelloImage.Y + " " + imageMatrix[othelloImage.X, othelloImage.Y].Y);

            if(!allowedPlacement(othelloImage))
            {
                //MessageBox.Show("You are not allowed to place your disc here");

                /*
                System.Drawing.Color dog = System.Drawing.Color.DarkOliveGreen;
                System.Drawing.Color ired = System.Drawing.Color.IndianRed;
                ColorAnimation ca = new ColorAnimation();
                ca.To = Color.FromArgb(ired.A, ired.R, ired.G, ired.B);
                ca.From = Color.FromArgb(dog.A, dog.R, dog.G, dog.B);
                ca.Duration = TimeSpan.FromSeconds(0.2);
                ca.AutoReverse = true;
                Storyboard sb = new Storyboard();
                sb.Children.Add(ca);
                Storyboard.SetTarget(ca, GridBorder);
                Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));
                sb.Begin();
                */

                Storyboard sb = this.FindResource("FlashGridBorder") as Storyboard;
                Storyboard.SetTarget(sb, this.GridBorder);
                sb.Begin();

                return;
            }

            if (whoseTurn == OthelloDisc.white)
            {
                othelloImage.ChangeToWhite();
            }
            else
            {
                othelloImage.ChangeToBlack();
            }

            NextPlayer(true);
        }

        private bool allowedPlacement(OthelloImage othelloImage)
        {
            int offsetX;
            int offsetY;

            bool result = false;

            // check to N
            offsetX = 0;
            offsetY = -1;

            if(checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }

            // check to NE
            offsetX = 1;
            offsetY = -1;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }

            // check to E
            offsetX = 1;
            offsetY = 0;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }

            // check to SE
            offsetX = 1;
            offsetY = 1;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }

            // check to S
            offsetX = 0;
            offsetY = 1;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }

            // check to SW
            offsetX = -1;
            offsetY = 1;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }


            // check to W
            offsetX = -1;
            offsetY = 0;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }


            // check to NW
            offsetX = -1;
            offsetY = -1;

            if (checkIfValid(othelloImage, offsetX, offsetY))
            {
                result = true;
            }

            return result;
        }

        private bool checkIfValid(OthelloImage othelloImage, int offsetX, int offsetY)
        {
            OthelloImage nextOImg;
            bool result = false;
            int x = othelloImage.X;
            int y = othelloImage.Y;

            try
            {
                nextOImg = imageMatrix[x + offsetX, y + offsetY];
                if (nextOImg.Disc != OthelloDisc.none && nextOImg.Disc != whoseTurn)
                {
                    // Worth checking next disc then until IndexOutOfRangeException
                    while (true)
                    {
                        x += offsetX;
                        y += offsetY;

                        nextOImg = imageMatrix[x + offsetX, y + offsetY];
                        if (nextOImg.Disc != OthelloDisc.none && nextOImg.Disc == whoseTurn)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            if (result == true)
            {
                // flip discs
                x = othelloImage.X;
                y = othelloImage.Y;

                while (true)
                {
                    nextOImg = imageMatrix[x + offsetX, y + offsetY];
                    x += offsetX;
                    y += offsetY;

                    if (nextOImg.Disc == whoseTurn)
                    {
                        break;
                    }

                    if (nextOImg.Disc == OthelloDisc.black)
                    {
                        nextOImg.ChangeToWhite();
                    }
                    else
                    {
                        nextOImg.ChangeToBlack();
                    }
                }
            }

            return result;
        }

        private void NextPlayer(bool recalc)
        {
            int nrOfWhite = 0;
            int nrOfBlack = 0;

            if(whoseTurn == OthelloDisc.white)
            {
                whoseTurn = OthelloDisc.black;
                imgWhoseTurn.Source = bmpimgCoinBlack;
            }
            else
            {
                whoseTurn = OthelloDisc.white;
                imgWhoseTurn.Source = bmpimgCoinWhite;
            }

            if (recalc == true)
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if(imageMatrix[x,y].Disc == OthelloDisc.white)
                        {
                            nrOfWhite++;
                        }
                        else if(imageMatrix[x, y].Disc == OthelloDisc.black)
                        {
                            nrOfBlack++;
                        }
                    }
                }

                lblStatus.Content = "     Score: White = " + nrOfWhite + "  Black = " + nrOfBlack;

                if((nrOfWhite + nrOfBlack == 64) || (nrOfWhite == 0 || nrOfBlack == 0))
                {
                    if(nrOfWhite > nrOfBlack)
                    {
                        MessageBox.Show("White won with the score: White = " + nrOfWhite + "  Black = " + nrOfBlack + "!");
                    }
                    else if (nrOfWhite < nrOfBlack)
                    {
                        MessageBox.Show("Black won with the score: White = " + nrOfWhite + "  Black = " + nrOfBlack + "!");
                    }
                    else
                    {
                        MessageBox.Show("It's a tie!");
                    }
                }
            }
        }

        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            NextPlayer(false);
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if ((x == 3 && y == 3) || (x == 4 && y == 4))
                    {
                        imageMatrix[x, y].ChangeToWhite();
                    }
                    else if ((x == 3 && y == 4) || (x == 4 && y == 3))
                    {
                        imageMatrix[x, y].ChangeToBlack();
                    }
                    else
                    {
                        imageMatrix[x, y].Disc = OthelloDisc.none;
                        imageMatrix[x, y].DiscImage.Source = null;
                    }
                }
            }

            whoseTurn = OthelloDisc.white;
            imgWhoseTurn.Source = bmpimgCoinWhite;

            lblStatus.Content = "     Score: White = 2  Black = 2";
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            if (sizeInfo.WidthChanged) this.Width = sizeInfo.NewSize.Height * 1.1;
            else this.Height = sizeInfo.NewSize.Width / 1.1;
        }
    }
}
