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
        private Game game = new Game(LoadLevels().First());
        private readonly Timer timer;
        private readonly Map[] levels = LoadLevels().ToArray();
        private readonly Dictionary<string, Bitmap> assets = Assets.LoadAssets();
        private Button nextLevelButton, restartButton;
        private int currentLevel = 1;
        private int deathCounter = 0;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            ClientSize = new Size(1920 / 2, 1080 / 2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;

            nextLevelButton = new Button
            {
                Size = new Size(ClientSize.Width, 60),
                Location = new Point(0, ClientSize.Height - 60),
                Text = "Next Level",
                Visible = false,
            };
            nextLevelButton.Click += NextLevelButton_Click;
            restartButton = new Button
            {
                Size = new Size(100, 30),
                Location = new Point(0, 0),
                Text = "Restart",
                Visible = false,
            };
            restartButton.Click += RestartButton_Click;
            Controls.Add(nextLevelButton);
            Controls.Add(restartButton);
            BackgroundImage = new Bitmap(@"Sprites\ButtonsTutor.png");
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
            yield return Map.FromLines(Game.Level1);
            yield return Map.FromLines(Game.Level2);
            yield return Map.FromLines(Game.Level3);
            yield return Map.FromLines(Game.Level4);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            game.Update();
            if (game.GameStage == GameStage.FinishedMap)
            {
                DrawMenu();
                return;
            }
            base.OnPaint(e);
            var graphics = e.Graphics;
            var map = game.CurrentMap;
            var player = game.Player;

            if (player.IsDead)
            {
                game.GameStage = GameStage.Waiting;
                DrawRestart();
                return;
            }

            for (var y = -1; y <= game.Camera.VisibleTilesY + 1; y++)
                for (var x = -1; x <= game.Camera.VisibleTilesX + 1; x++)
                    switch (map[x + game.Camera.OffsetX, y + game.Camera.OffsetY])
                    {
                        case TileType.Wall:
                            //DrawTile(Brushes.Gray, graphics, x, y);
                            DrawTile(assets["wall"], graphics, x, y);
                            break;
                        case TileType.Ground:
                            //DrawTile(Brushes.Green, graphics, x, y);
                            DrawTile(assets["grass"], graphics, x, y);
                            break;
                        case TileType.Exit:
                            DrawTile(assets["exit"], graphics, x, y);
                            break;
                    }
            DrawCreature(assets["player"], graphics, player.PosX, player.PosY);
            foreach (var enemy in map.Enemies)
                DrawCreature(assets["enemy"], graphics, enemy.PosX, enemy.PosY);
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

        private void DrawMenu()
        {
            nextLevelButton.Visible = true;
        }
        private void DrawRestart()
        {
            restartButton.Visible = true;
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
            ((Button)sender).Visible = false;
            if (currentLevel >= levels.Length)
                currentLevel = 0;
            game.ChangeMap(LoadLevels().Skip(currentLevel++).FirstOrDefault());
            game.GameStage = GameStage.Waiting;
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            if (deathCounter++ == 5)
            {
                deathCounter = 0;
                currentLevel = 1;
            }
            game = new Game(LoadLevels().Skip(currentLevel - 1).First());
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            game.GameStage = GameStage.Playing;
            var speed = 6f;
            var player = game.Player;
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Q:
                    Map.TileSize--;
                    break;
                case Keys.E:
                    Map.TileSize++;
                    break;
                case Keys.R:
                    if (deathCounter++ == 5)
                    {
                        deathCounter = 0;
                        currentLevel = 1;
                    }
                    game = new Game(LoadLevels().Skip(currentLevel - 1).First());
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
                    game.ChangeMap(LoadLevels().ToArray()[e.KeyValue - 49]);
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
