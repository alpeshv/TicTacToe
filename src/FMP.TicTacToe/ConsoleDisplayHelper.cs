using System;
using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class ConsoleDisplayHelper : IDisplayHelper
    {
        public void Display(string str)
        {
            Console.WriteLine(str);
        }
    }
}
