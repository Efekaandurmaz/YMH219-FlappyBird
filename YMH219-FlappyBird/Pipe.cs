namespace FlappyBird;


public class Pipe
{
    public const int Width = 60;
    public const int GapHeight = 150;

    public float X { get; private set; }
    public int GapY { get; private set; }     
    public bool Passed { get; set; }          

    private readonly int _groundY;

    public Pipe(float x, int gapY, int groundY)
    {
        X = x;
        GapY = gapY;
        _groundY = groundY;
    }

    public Rectangle TopBounds => new Rectangle((int)X, 0, Width, GapY);
    public Rectangle BottomBounds =>
        new Rectangle((int)X, GapY + GapHeight, Width, _groundY - (GapY + GapHeight));

    public void Move(float speed) => X -= speed;

    public bool IsOffScreen => X + Width < 0;

    
    public void Reposition(float newX, int newGapY)
    {
        X = newX;
        GapY = newGapY;
        Passed = false;
    }

    public void Draw(Graphics g)
    {
        g.FillRectangle(Brushes.ForestGreen, TopBounds);
        g.FillRectangle(Brushes.ForestGreen, BottomBounds);
        g.DrawRectangle(Pens.DarkGreen, TopBounds);
        g.DrawRectangle(Pens.DarkGreen, BottomBounds);
    }
}