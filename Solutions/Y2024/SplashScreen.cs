using AdventOfCode.Framework;
namespace AdventOfCode.Y2024;

class SplashScreenImpl : SplashScreen
{
    public override void Show()
    {
        WriteFiglet("Advent of code 2024", Spectre.Console.Color.Yellow);
        Write(0xcccccc, "                  .--'");
        Write(0xe3b585, "~ ~ ~");
        Write(0xcccccc, "|        .-' ");
        Write(0xffff66, "*       ");
        Write(0x886655, "\\  /     ");
        Write(0xcccccc, "'-.  ");
        Write(0x888888, " 1 ");
        Write(0xffff66, "**\n           ");
        Write(0x888888, "    .--'        |        |                      |   2\n                                              ");
        Write(0x888888, "                 3\n                                                               4\n                ");
        Write(0x888888, "                                               5\n                                                   ");
        Write(0x888888, "            6\n                                                               7\n                     ");
        Write(0x888888, "                                          8\n                                                        ");
        Write(0x888888, "       9\n                                                              10\n                          ");
        Write(0x888888, "                                    11\n                                                             ");
        Write(0x888888, " 12\n                                                              13\n                               ");
        Write(0x888888, "                               14\n                                                              15\n ");
        Write(0x888888, "                                                             16\n                                    ");
        Write(0x888888, "                          17\n                                                              18\n      ");
        Write(0x888888, "                                                        19\n                                         ");
        Write(0x888888, "                     20\n                                                              21\n           ");
        Write(0x888888, "                                                   22\n                                              ");
        Write(0x888888, "                23\n                                                              24\n                ");
        Write(0x888888, "                                              25\n           \n");
        
        Console.WriteLine();
    }
}