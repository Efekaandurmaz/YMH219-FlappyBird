namespace FlappyBird;


public class Bird
{
    public const int Size = 30;
    private const float Gravity = 0.5f;       // her karede hıza eklenir
    private const float FlapStrength = -8f;   // yukarı doğru ani hız

    public float X { get; }
    public float Y { get; private set; }
    public float VelocityY { get; private set; }

    public Rectangle Bounds => new Rectangle((int)X, (int)Y, Size, Size);

    public Bird(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void Flap() => VelocityY = FlapStrength;

    public void Update()
    {
        VelocityY += Gravity;
        Y += VelocityY;
    }

    public void Reset(float y)
    {
        Y = y;
        VelocityY = 0;
    }

    public void Draw(Graphics g)
    {
        g.FillEllipse(Brushes.Gold, Bounds);
        g.DrawEllipse(Pens.DarkOrange, Bounds);
        g.FillEllipse(Brushes.White, X + 18, Y + 6, 8, 8);   // göz
        g.FillEllipse(Brushes.Black, X + 22, Y + 9, 3, 3);
        g.FillRectangle(Brushes.OrangeRed, X + 26, Y + 15, 8, 5); // gaga
    }
}