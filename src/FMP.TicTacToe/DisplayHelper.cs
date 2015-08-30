using System;
using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class DisplayHelper:IDisplayHelper
    {
        public void Display(string str)
        {
            Console.WriteLine(str);
        }
    }
}
