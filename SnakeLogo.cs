using System;
class Sample
{
    protected static int origRow;
    protected static int origCol;

    protected static void WriteAt(string s, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(s);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
    }

    public static void Main()
    {
        // Clear the screen, then save the top and left coordinates.
        Console.Clear();
        origRow = Console.CursorTop;
        origCol = Console.CursorLeft;

        Console.ForegroundColor = System.ConsoleColor.Red;

        // Draw the left side of a 15x60 rectangle, from top to bottom.

        for (int i = 2; i < 17; i++)
        {
            WriteAt("|", 10, i);
        }

        // Draw the bottom side, from left to right.

        for (int i = 10; i < 70; i++)
        {
            WriteAt("-", i, 17);
        }

        // Draw the right side, from bottom to top.

        for (int i = 2; i < 17; i++)
        {
            WriteAt("|", 69, i);
        }

        // Draw the top side, from right to left.
        for (int i = 10; i < 70; i++)
        {
            WriteAt("-", i, 1);
        }

        Console.ForegroundColor = System.ConsoleColor.Yellow;

        WriteAt(@" # # #  #       #      #      #     #  # # # # ", 17, 7);
        Console.ForegroundColor = System.ConsoleColor.Yellow;
        WriteAt(@"#       # #     #     # #     #   #    #", 17, 8);
        Console.ForegroundColor = System.ConsoleColor.Yellow;
        WriteAt(@"   #    #   #   #    #   #    # #      # # #", 17, 9);
        Console.ForegroundColor = System.ConsoleColor.Green;
        WriteAt(@"     #  #     # #   # # # #   #   #    #", 17, 10);
        Console.ForegroundColor = System.ConsoleColor.Red;
        WriteAt(@"# # #   #       #  #       #  #     #  # # # #", 17, 11);

        Console.WriteLine();
        Console.ForegroundColor = System.ConsoleColor.White;

        Console.ForegroundColor = System.ConsoleColor.Red;
        WriteAt(@"#   #  ####  #    #      #   #  ####  #  #  #   #  ###", 13, 19);
        WriteAt(@"#####  ###   #    #      #####  #  #  #  #  # # #  #  #", 13, 20);
        WriteAt(@"#   #  ####  ###  ###    #   #  ####  ####  #   #  ###", 13, 21);

        WriteAt("Studio", 62, 23);

        Console.ForegroundColor = System.ConsoleColor.White;
        Console.WriteLine();
    }
}


