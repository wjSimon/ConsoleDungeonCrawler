
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsolePixel
{
    public ConsolePixel()
    {
        this.symbol = ' ';
        this.background = ConsoleColor.Black;
        this.foreground = ConsoleColor.Gray;
    }
    public ConsolePixel(char symbol)
    {
        this.symbol = symbol;
        this.background = ConsoleColor.Black;
        this.foreground = ConsoleColor.Gray;
    }
    public ConsolePixel(ConsoleColor f, ConsoleColor b)
    {
        foreground = f;
        background = b;
    }
    public ConsolePixel(char symbol, ConsoleColor f)
    {
        this.symbol = symbol;
        foreground = f;
    }

    public ConsolePixel(char symbol, ConsoleColor f, ConsoleColor b)
    {
        this.symbol = symbol;
        foreground = f;
        background = b;
    }

    public char symbol;
    public ConsoleColor foreground;
    public ConsoleColor background;
}