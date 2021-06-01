using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Platformer.Domain;
using Timer = System.Windows.Forms.Timer;

namespace Platformer
{
    public partial class PlatformerForm : Form
    {
        private readonly Game game = new Game(LoadLevels().Skip(1).First());
        private readonly Timer timer;
        private readonly Map[] levels = LoadLevels().ToArray();
        private DateTime lastUpdate = DateTime.MinValue;
        private readonly Dictionary<string, Bitmap> Assets = Domain.Assets.LoadAssets();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            BackColor = Color.SaddleBrown;
        }

        public PlatformerForm()
        {
            InitializeComponent();
            timer = new Timer { Interval = 16 };
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
            yield return Map.FromLines(Game.LargeMap);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            game.Update();
            base.OnPaint(e);
            var graphics = e.Graphics;
            var map = game.CurrentMap;
            var player = game.Player;

            for (var y = -1; y <= game.Camera.VisibleTilesY; y++)
                for (var x = -1; x <= game.Camera.VisibleTilesX + 1; x++)
                    switch (map[x + game.Camera.OffsetX, y + game.Camera.OffsetY])
                    {
                        case TileType.Wall:
                            //DrawTile(Brushes.Gray, graphics, x, y);
                            DrawTile(Assets["wall"], graphics, x, y);
                            break;
                        case TileType.Ground:
                            //DrawTile(Brushes.Green, graphics, x, y);
                            DrawTile(Assets["grass"], graphics, x, y);
                            break;
                        case TileType.Spike:
                            DrawTile(Brushes.Pink, graphics, x, y);
                            break;
                    }

            graphics.DrawLine(new Pen(Color.Black, 10), 1, 1, 50, 50f);
            //if (player.IsDead)
            //    Text = "Dead";
            //else Text = "Alive";
            DrawCreature(Assets["creature"], graphics, player.PosX, player.PosY);
            foreach (var enemy in map.Enemies)
                DrawCreature(Assets["creature"], graphics, enemy.PosX, enemy.PosY);
            Text = "EnemyX: " + map.Enemies.First().VelocityX + " EnemyY: " + map.Enemies.First().VelocityY;
        }

        private void DrawCreature(Bitmap bitmap, Graphics graphics, float x, float y)
        {
            graphics.DrawImage(bitmap, new RectangleF(
                (x - game.Camera.OffsetX) * Map.TileSize,
                (y - game.Camera.OffsetY) * Map.TileSize,
                Map.TileSize + 1,
                Map.TileSize + 1));
        }

        private void DrawTile(Bitmap bitmap, Graphics graphics, int x, int y)
        {
            graphics.DrawImage(bitmap, new RectangleF(
                x * Map.TileSize - game.Camera.TileOffsetX,
                y * Map.TileSize - game.Camera.TileOffsetY,
                Map.TileSize + 1,
                Map.TileSize + 1));
        }

        private void DrawTile(Brush brush, Graphics graphics, int x, int y)
        {
            graphics.FillRectangle(brush, new RectangleF(
                x * Map.TileSize - game.Camera.TileOffsetX,
                y * Map.TileSize - game.Camera.TileOffsetY,
                Map.TileSize,
                Map.TileSize));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var speed = 6f;
            var player = game.Player;
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Q:
                    Map.TileSize++;
                    break;
                case Keys.E:
                    Map.TileSize--;
                    break;
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
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                    game.ChangeMap(levels[e.KeyValue - 49]);
                    break;
            }
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
        }
    }
}
