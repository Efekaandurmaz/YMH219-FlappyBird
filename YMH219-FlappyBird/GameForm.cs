namespace FlappyBird;


public class GameForm : Form
{
    private readonly Game _game;
    private readonly System.Windows.Forms.Timer _timer;
    private readonly Button _btnRestart;

    public GameForm()
    {
        Text = "Flappy Bird - YMH219";
        ClientSize = new Size(480, 640);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        DoubleBuffered = true;   
        KeyPreview = true;       

        _game = new Game(ClientSize.Width, ClientSize.Height);

        _btnRestart = new Button
        {
            Text = "Yeni Oyun (R)",
            Location = new Point(10, 10),
            Size = new Size(110, 30),
            TabStop = false
        };
        _btnRestart.Click += (s, e) => RestartGame();
        Controls.Add(_btnRestart);

        _timer = new System.Windows.Forms.Timer { Interval = 20 };   
        _timer.Tick += (s, e) =>
        {
            _game.Update();
            Invalidate();
        };
        _timer.Start();
    }

    private void RestartGame()
    {
        _game.Restart();
        Focus();   
        Invalidate();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
        {
            _game.Flap();
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.R)
        {
            RestartGame();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;

        g.Clear(Color.SkyBlue);

        foreach (Pipe pipe in _game.Pipes) pipe.Draw(g);

        g.FillRectangle(Brushes.SandyBrown, 0, _game.GroundY, _game.Width, _game.Height - _game.GroundY);
        g.DrawLine(Pens.SaddleBrown, 0, _game.GroundY, _game.Width, _game.GroundY);

        _game.Bird.Draw(g);

        using var scoreFont = new Font("Segoe UI", 28, FontStyle.Bold);
        using var infoFont = new Font("Segoe UI", 11, FontStyle.Bold);
        using var bigFont = new Font("Segoe UI", 30, FontStyle.Bold);

        g.DrawString(_game.Score.ToString(), scoreFont, Brushes.White, _game.Width / 2f - 20, 20);
        g.DrawString($"Seviye: {_game.Level}   Hız: {_game.Speed:0.0}", infoFont, Brushes.White, 10, _game.GroundY + 20);

        var center = new StringFormat { Alignment = StringAlignment.Center };

        if (_game.State == GameState.Ready)
        {
            g.DrawString("Başlamak için SPACE", infoFont, Brushes.White,
                new RectangleF(0, 280, _game.Width, 30), center);
        }
        else if (_game.State == GameState.GameOver)
        {
            g.DrawString("GAME OVER", bigFont, Brushes.Firebrick,
                new RectangleF(0, 230, _game.Width, 60), center);
            g.DrawString($"Skor: {_game.Score}   —   R ile yeniden başla", infoFont, Brushes.White,
                new RectangleF(0, 295, _game.Width, 30), center);
        }
    }
}