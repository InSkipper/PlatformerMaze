using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Platformer.Domain;

namespace Platformer
{
    public partial class PlatformerForm : Form
    {
        private readonly Game game = new Game(LoadLevels().First());
        private readonly Timer timer;
        private readonly Map[] levels = LoadLevels().ToArray();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            Text = "Test game build";
        }

        public PlatformerForm()
        {
            InitializeComponent();

            timer = new Timer { Interval = 50 };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromLines(Game.MapWithoutWalls);
            yield return Map.FromLines(Game.TestMap);
            yield return Map.FromLines(Game.TestMap2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            var map = game.CurrentMap;
            var player = game.Player;
            var tileSize = ClientSize.Width / map.Level.GetLength(0);
            for (var y = 0; y < map.Level.GetLength(1); y++)
                for (var x = 0; x < map.Level.GetLength(0); x++)
                    switch (map.Level[x, y])
                    {
                        case TileType.Wall:
                            graphics.FillRectangle(Brushes.DarkGray, x * tileSize, y * tileSize, tileSize, tileSize);
                            break;
                        case TileType.Ground:
                            graphics.FillRectangle(Brushes.CornflowerBlue, x * tileSize, y * tileSize, tileSize, tileSize);
                            break;
                    }
            graphics.FillRectangle(Brushes.DarkSeaGreen, player.PosX * tileSize,
                player.PosY * tileSize, tileSize, tileSize);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var speed = 6f;
            var player = game.Player;
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Right:
                    player.VelocityX = speed;
                    break;
                case Keys.Left:
                    player.VelocityX = -speed;
                    break;
                case Keys.Up:
                    player.VelocityY = -speed;
                    break;
                case Keys.Down:
                    player.VelocityY = speed;
                    break;
                case Keys.D1:
                    game.ChangeMap(levels[0]);
                    break;
                case Keys.D2:
                    game.ChangeMap(levels[1]);
                    break;
                case Keys.D3:
                    game.ChangeMap(levels[2]);
                    break;
            }

            player.MakeMove(player.VelocityX, player.VelocityY);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            var player = game.Player;
            base.OnKeyUp(e);
            switch (e.KeyCode)
            {
                case Keys.Right:
                    player.VelocityX = 0;
                    break;
                case Keys.Left:
                    player.VelocityX = 0;
                    break;
                case Keys.Up:
                    player.VelocityY = 0;
                    break;
                case Keys.Down:
                    player.VelocityY = 0;
                    break;
            }

            //player.MakeMove(player.VelocityX, player.VelocityY);
        }
    }
}
