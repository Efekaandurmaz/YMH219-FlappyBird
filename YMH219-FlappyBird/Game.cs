namespace FlappyBird;

public enum GameState { Ready, Playing, GameOver }

/// <summary>Oyun kurallarını ve durumunu yönetir (arayüzden bağımsız).</summary>
public class Game
{
    private const int PipeCount = 3;
    private const int PipeSpacing = 220;
    private const int GroundHeight = 60;
    private const int PointsPerLevel = 5;

    private readonly Random _random = new Random();
    private readonly List<Pipe> _pipes = new List<Pipe>();

    public int Width { get; }
    public int Height { get; }
    public int GroundY => Height - GroundHeight;

    public Bird Bird { get; private set; }
    public IReadOnlyList<Pipe> Pipes => _pipes;
    public GameState State { get; private set; } = GameState.Ready;
    public int Score { get; private set; }
    public int Level => Score / PointsPerLevel + 1;
    public float Speed => 3f + (Level - 1) * 0.5f;   // seviye arttıkça hız artar

    public Game(int width, int height)
    {
        Width = width;
        Height = height;
        Bird = new Bird(100, height / 2f);
        Restart();
    }

    public void Restart()
    {
        Score = 0;
        State = GameState.Ready;
        Bird.Reset(Height / 2f);

        _pipes.Clear();
        for (int i = 0; i < PipeCount; i++)
            _pipes.Add(new Pipe(Width + i * PipeSpacing, RandomGapY(), GroundY));
    }

    /// <summary>Boşluk üst kenarı için rastgele ama makul bir yükseklik.</summary>
    private int RandomGapY() => _random.Next(60, GroundY - Pipe.GapHeight - 60);

    public void Flap()
    {
        if (State == GameState.Ready) State = GameState.Playing;
        if (State == GameState.Playing) Bird.Flap();
    }

    /// <summary>Timer her tick'inde çağrılır (oyun döngüsü).</summary>
    public void Update()
    {
        if (State != GameState.Playing) return;

        Bird.Update();

        foreach (Pipe pipe in _pipes)
        {
            pipe.Move(Speed);

            if (!pipe.Passed && pipe.X + Pipe.Width < Bird.X)
            {
                pipe.Passed = true;
                Score++;
            }

            if (pipe.IsOffScreen)
            {
                float lastX = _pipes.Max(p => p.X);
                pipe.Reposition(lastX + PipeSpacing, RandomGapY());
            }
        }

        if (CheckCollision()) State = GameState.GameOver;
    }

    private bool CheckCollision()
    {
        Rectangle b = Bird.Bounds;

        if (b.Top < 0 || b.Bottom >= GroundY) return true;

        return _pipes.Any(p => b.IntersectsWith(p.TopBounds) || b.IntersectsWith(p.BottomBounds));
    }
}