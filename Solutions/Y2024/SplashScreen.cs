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
        Write(0xcccccc, ".---'");
        Write(0xe3b585, ": ~ ");
        Write(0x00cc00, "'");
        Write(0x5555bb, "(~)");
        Write(0x00cc00, ", ");
        Write(0xe3b585, "~");
        Write(0xcccccc, "|        | ");
        Write(0x009900, ">");
        Write(0xff0000, "@");
        Write(0x009900, ">");
        Write(0x0066ff, "O");
        Write(0x009900, "< ");
        Write(0xff0000, "o");
        Write(0x886655, "-_/");
        Write(0xcccccc, ".()");
        Write(0x886655, "__------");
        Write(0xcccccc, "|  ");
        Write(0x888888, " 3 ");
        Write(0xffff66, "**\n           ");
        Write(0x888888, "|               |        |                      |   4\n                                              ");
        Write(0x888888, "                 5\n                                                               6\n                ");
        Write(0x888888, "                                               7\n                                                   ");
        Write(0x888888, "            8\n                                                               9\n                     ");
        Write(0x888888, "                                         10\n                                                        ");
        Write(0x888888, "      11\n                                                              12\n                          ");
        Write(0x888888, "                                    13\n                                                             ");
        Write(0x888888, " 14\n                                                              15\n                               ");
        Write(0x888888, "                               16\n                                                              17\n ");
        Write(0x888888, "                                                             18\n                                    ");
        Write(0x888888, "                          19\n                                                              20\n      ");
        Write(0x888888, "                                                        21\n                                         ");
        Write(0x888888, "                     22\n                                                              23\n           ");
        Write(0x888888, "                                                   24\n                                              ");
        Write(0x888888, "                25\n           \n");
        
        Console.WriteLine();
    }
}