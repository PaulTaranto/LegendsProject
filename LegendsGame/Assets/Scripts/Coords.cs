public struct Coords
{
    public Coords(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString() => $"({X}, {Y})";
    public void SetCoords(int x, int y) { X = x; Y = y; }

    public static bool operator ==(Coords lhs, Coords rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;
    public static bool operator !=(Coords lhs, Coords rhs) => lhs.X != rhs.X || lhs.Y != rhs.Y;
}