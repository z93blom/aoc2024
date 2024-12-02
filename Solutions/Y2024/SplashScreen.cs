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
        Write(0xffff66, "**\n               ");
        Write(0xcccccc, ".--'");
        Write(0xe3b585, "~  ");
        Write(0x00cc00, ",");
        Write(0xffff66, "* ");
        Write(0xe3b585, "~ ");
        Write(0xcccccc, "|        |  ");
        Write(0x009900, ">");
        Write(0xff9900, "o");
        Write(0x009900, "<   ");
        Write(0x886655, "\\_\\_\\|_/__/   ");
        Write(0xcccccc, "|  ");
        Write(0x888888, " 2 ");
        Write(0xffff66, "**\n           ");
        Write(0x888888, ".---'           |        |                      |   3\n                                              ");
        Write(0x888888, "                 4\n                                                               5\n                ");
        Write(0x888888, "                                               6\n                                                   ");
        Write(0x888888, "            7\n                                                               8\n                     ");
        Write(0x888888, "                                          9\n                                                        ");
        Write(0x888888, "      10\n                                                              11\n                          ");
        Write(0x888888, "                                    12\n                                                             ");
        Write(0x888888, " 13\n                                                              14\n                               ");
        Write(0x888888, "                               15\n                                                              16\n ");
        Write(0x888888, "                                                             17\n                                    ");
        Write(0x888888, "                          18\n                                                              19\n      ");
        Write(0x888888, "                                                        20\n                                         ");
        Write(0x888888, "                     21\n                                                              22\n           ");
        Write(0x888888, "                                                   23\n                                              ");
        Write(0x888888, "                24\n                                                              25\n           \n");
        
        Console.WriteLine();
    }
}