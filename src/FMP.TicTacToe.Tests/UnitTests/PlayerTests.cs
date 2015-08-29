using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FMP.TicTacToe.Tests.UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        [TestCase('O')]
        [TestCase('X')]
        public void CreatingNewPlayer_SetsPlayerCharacter(char c)
        {
            var player = new Player(c);

            Assert.That(player.PlayCharacter, Is.EqualTo(c));
        }
    }
}
