using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //arr of tiles
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/tetris/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/TileRed.png", UriKind.Relative))
        };
        // arr nxt and hold char
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/tetris/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/tetris/Block-Z.png", UriKind.Relative))
        };
        //2D arr control
        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 70;
        private readonly int delayDecrease = 50;

        private Stat gameState = new Stat();

        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private Image[,] SetupGameCanvas(Grid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns]; //var for ea cell
            int cellSize = 25; //px

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 20); //-2 pushes top hidden rows up
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl; //returns outsides the loop
                }
            }
            return imageControls;
        }
        //loop and update
        private void DrawGrid(Grid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }
        private void DrawBlock(Block block)
        {
            foreach (BlockMove p in block.TileBlockMoves())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }
        //displays the next char
        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }
        //displays the held char in an empty block
        private void DrawHeldBlock(Block heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }
        //feature to display where char will drop 
        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();

            foreach (BlockMove p in block.TileBlockMoves())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25; //shadow effect
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(Stat gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            ScoreText.Text = $"Score: {gameState.Score}";
        } 
        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease)); //when scores increases char moves faster
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }

            GameOverTab.Visibility = Visibility.Visible;
            FinalScoreTxt.Text = $"Score: {gameState.Score}";
        }
        //key functions 
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.V:
                    gameState.RotateBlockCW();
                    break;
                case Key.C:
                    gameState.RotateBlockCCW();
                    break;
                case Key.X:
                    gameState.HoldBlock();
                    break;
                case Key.Space:
                    gameState.DropBlock();
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }
        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }
        //button function
        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new Stat();
            GameOverTab.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}
