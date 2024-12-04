namespace AdventOfCode.Utilities;

public readonly record struct Vec2(long X, long Y)
{
    public Vec2 Inverse => new Vec2(-X, -Y);

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}