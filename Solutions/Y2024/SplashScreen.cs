using AdventOfCode.Framework;
namespace AdventOfCode.Y2024;

[RegisterKeyedTransient("2024")] partial class SplashScreenImpl { }
partial class SplashScreenImpl : SplashScreen
{
    public override void Show()
    {
        WriteFiglet("Advent of code 2024", Spectre.Console.Color.Yellow);
        Write(0x888888, false, "                     .-----.          .------------------.         \n                  ");
        Write(0xcccccc, false, ".--'");
        Write(0xe3b585, false, "~ ~ ~");
        Write(0xcccccc, false, "|        .-' ");
        Write(0xffff66, true, "*       ");
        Write(0x886655, false, "\\  /     ");
        Write(0xcccccc, false, "'-.   1 ");
        Write(0xffff66, false, "**\n               ");
        Write(0xcccccc, false, ".--'");
        Write(0xe3b585, false, "~  ");
        Write(0x00cc00, false, ",");
        Write(0xffff66, true, "* ");
        Write(0xe3b585, false, "~ ");
        Write(0xcccccc, false, "|        |  ");
        Write(0x009900, false, ">");
        Write(0xff9900, true, "o");
        Write(0x009900, false, "<   ");
        Write(0x886655, false, "\\_\\_\\|_/__/   ");
        Write(0xcccccc, false, "|   2 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, ".---'");
        Write(0xe3b585, false, ": ~ ");
        Write(0x00cc00, false, "'");
        Write(0x5555bb, false, "(~)");
        Write(0x00cc00, false, ", ");
        Write(0xe3b585, false, "~");
        Write(0xcccccc, false, "|        | ");
        Write(0x009900, false, ">");
        Write(0xff0000, true, "@");
        Write(0x009900, false, ">");
        Write(0x0066ff, true, "O");
        Write(0x009900, false, "< ");
        Write(0xff0000, true, "o");
        Write(0x886655, false, "-_/");
        Write(0xcccccc, false, ".()");
        Write(0x886655, false, "__------");
        Write(0xcccccc, false, "|   3 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, "|");
        Write(0x488813, false, "@");
        Write(0x5eabb4, false, "..");
        Write(0x427322, false, "#");
        Write(0xe3b585, false, "'. ~ ");
        Write(0x00cc00, false, "\" ' ");
        Write(0xe3b585, false, "~ ");
        Write(0xcccccc, false, "|        |");
        Write(0x009900, false, ">");
        Write(0x0066ff, true, "O");
        Write(0x009900, false, ">");
        Write(0xff9900, true, "o");
        Write(0x009900, false, "<");
        Write(0xff0000, true, "@");
        Write(0x009900, false, "< ");
        Write(0x886655, false, "\\____       ");
        Write(0x00cc00, false, ".'");
        Write(0xcccccc, false, "|   4 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, "|");
        Write(0x01461f, false, "_");
        Write(0x5eabb4, false, ".~.");
        Write(0x427322, false, "_");
        Write(0x488813, false, "#");
        Write(0xe3b585, false, "'.. ~ ~ ");
        Write(0xffff66, true, "*");
        Write(0xcccccc, false, "|        | ");
        Write(0xaaaaaa, false, "_| |_    ");
        Write(0xcccccc, false, "..\\_");
        Write(0x886655, false, "\\_ ");
        Write(0x00cc00, false, "..'");
        Write(0xffff66, true, "* ");
        Write(0xcccccc, false, "|   5 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, "| ");
        Write(0xffffff, false, "||| ");
        Write(0x4d8b03, false, "@");
        Write(0x01461f, false, "#@");
        Write(0x7fbd39, false, "@");
        Write(0xe3b585, false, "'''...");
        Write(0xcccccc, false, "|        |");
        Write(0xa25151, false, "...     ");
        Write(0xcccccc, false, ".'  '.");
        Write(0x00cc00, false, "'''..");
        Write(0xd4dde4, false, "/");
        Write(0x00cc00, false, "..");
        Write(0xcccccc, false, "|   6 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, "|");
        Write(0x427322, false, "@");
        Write(0xffffff, false, "~~~");
        Write(0x4d8b03, false, "@@");
        Write(0x488813, false, "#");
        Write(0x7fbd39, false, "@");
        Write(0x427322, false, "@      ");
        Write(0xcccccc, false, "|        |");
        Write(0xa5a8af, false, "/\\ ");
        Write(0xa25151, false, "''.  ");
        Write(0xcccccc, false, "|    |   ");
        Write(0xccccff, false, "-");
        Write(0xd4dde4, false, "/  ");
        Write(0xffffff, false, ":");
        Write(0xcccccc, false, "|   7 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, "|");
        Write(0x5eabb4, false, "~~.");
        Write(0xcccccc, false, ".--.");
        Write(0x666666, false, " _____  ");
        Write(0xcccccc, false, "|        |");
        Write(0xffff66, true, "* ");
        Write(0xa5a8af, false, "/");
        Write(0xdf2308, true, "~");
        Write(0xa5a8af, false, "\\ ");
        Write(0xa25151, false, "'.");
        Write(0xcccccc, false, "|    | ");
        Write(0xccccff, false, "- ");
        Write(0xd4dde4, false, "/  ");
        Write(0xffffff, false, ".'");
        Write(0xcccccc, false, "|   8 ");
        Write(0xffff66, false, "**\n           ");
        Write(0xcccccc, false, "'---'  |");
        Write(0x666666, false, "|[][]_\\-");
        Write(0xcccccc, false, "|        |");
        Write(0xdf2308, true, "~");
        Write(0xa5a8af, false, "/ ");
        Write(0xffff66, true, "* ");
        Write(0xa5a8af, false, "\\ ");
        Write(0xa25151, false, ":");
        Write(0xcccccc, false, "|    |  ");
        Write(0xffff66, true, "*");
        Write(0xffffff, false, "..'  ");
        Write(0xcccccc, false, "|   9 ");
        Write(0xffff66, false, "**\n           ");
        Write(0x333333, false, "       |        |        |        |    |        |  ");
        Write(0x666666, false, "10\n                                                              11\n                                ");
        Write(0x666666, false, "                              12\n                                                              13\n  ");
        Write(0x666666, false, "                                                            14\n                                     ");
        Write(0x666666, false, "                         15\n                                                              16\n       ");
        Write(0x666666, false, "                                                       17\n                                          ");
        Write(0x666666, false, "                    18\n                                                              19\n            ");
        Write(0x666666, false, "                                                  20\n                                               ");
        Write(0x666666, false, "               21\n                                                              22\n                 ");
        Write(0x666666, false, "                                             23\n                                                    ");
        Write(0x666666, false, "          24\n                                                              25\n           \n");
        
        Console.WriteLine();
    }
}