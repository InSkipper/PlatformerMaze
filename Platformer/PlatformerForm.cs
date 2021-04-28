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
        private Map map = LoadLevels().First();
        private readonly Timer timer;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
        }

        public PlatformerForm()
        {
            InitializeComponent();
            var levels = LoadLevels();

            var button = new Button
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width, 30),
                Text = "This is test button"
            };

            //Controls.Add(button);
            button.Click += Button_Click;

            timer = new Timer { Interval = 50 };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Why did you just click on this test button";
            ((Button)sender).Font = new Font(FontFamily.GenericMonospace, 16);
        }

        private static IEnumerable<Map> LoadLevels()
        {
            yield return Map.FromLines(Game.TestMap);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game.CurrentMap = Map.FromLines(Game.TestMap);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            var tileSize = 30;
            for (var y = 0; y < map.Level.GetLength(1); y++)
                for (var x = 0; x < map.Level.GetLength(0); x++)
                    if (map.IsSolid(x, y))
                        graphics.FillRectangle(Brushes.DarkGray, x * tileSize, y * tileSize, tileSize, tileSize);
                    else
                        graphics.FillRectangle(Brushes.CornflowerBlue, x * tileSize, y * tileSize, tileSize, tileSize);
            graphics.FillRectangle(Brushes.DarkSeaGreen, map.Player.PosX * tileSize, map.Player.PosY * tileSize, tileSize, tileSize);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var speed = 6;
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Right:
                    map.Player.MoveHorizontally(speed);
                    break;
                case Keys.Left:
                    map.Player.MoveHorizontally(-speed);
                    break;
                case Keys.Up:
                    map.Player.MoveVertically(-speed);
                    break;
                case Keys.Down:
                    map.Player.MoveVertically(speed);
                    break;
                case Keys.Enter:
                    Game.CurrentMap = Map.FromLines(Game.TestMap2);
                    map = Map.FromLines(Game.TestMap2);
                    break;
            }
        }
    }
}
