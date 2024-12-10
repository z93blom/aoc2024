namespace AdventOfCode.Y2024;

class Theme : ITheme
{
    public Dictionary<string, int> Override(Dictionary<string, int> themeColors)
    {
        return themeColors;
    }
}