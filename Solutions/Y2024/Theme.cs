namespace AdventOfCode.Y2024;

class Theme : ITheme
{
    public Dictionary<string, int> Override(Dictionary<string, int> themeColors)
    {
        themeColors["calendar-color-3g"] = 0x00cc00;
        themeColors["calendar-color-3s"] = 0xe3b585;
        themeColors["calendar-color-3y"] = 0xffff66;
        themeColors["calendar-color-6b"] = 0x009900;
        themeColors["calendar-color-6o"] = 0xff9900;
        themeColors["calendar-color-6y"] = 0xffff66;
        themeColors["calendar-color-8n"] = 0x886655;
        return themeColors;
    }
}