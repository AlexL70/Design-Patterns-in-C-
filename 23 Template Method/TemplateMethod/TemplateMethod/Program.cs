using static System.Console;

namespace TemplateMethod
{
    public abstract class Game
    {
        public void Run()
        {
            Start();
            while (!HaveWinner)
                TakeTurn();
            WriteLine($"Player {WinningPlayer} wins!");
        }

        protected int currentPlayer;
        protected readonly int numberOfPlayers;

        protected Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }

        protected abstract void Start();
        protected abstract void TakeTurn();
        protected abstract bool HaveWinner { get; }
        protected abstract int WinningPlayer { get; }
    }

    public class Chess : Game
    {
        public Chess() : base(2)
        {
        }

        protected override void Start()
        {
            WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
        }

        protected override void TakeTurn()
        {
            WriteLine($"Turn {_turn++} taken by player #{currentPlayer}.");
            currentPlayer = (++currentPlayer) % numberOfPlayers;
        }

        protected override bool HaveWinner => _turn == MaxTurns;
        protected override int WinningPlayer => currentPlayer;

        private int _turn = 1;
        private const int MaxTurns = 10;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ch = new Chess();
            ch.Run();
        }
    }
}
