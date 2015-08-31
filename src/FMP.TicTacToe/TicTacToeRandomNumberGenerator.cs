using System;
using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class TicTacToeRandomNumberGenerator : IRandomNumberGenerator
    {
        public int Generate()
        {
            var random = new Random();
            return random.Next(1, 4);
        }
    }
}
