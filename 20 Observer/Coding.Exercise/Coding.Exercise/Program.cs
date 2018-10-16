using System.Collections.Generic;

namespace Coding.Exercise
{
    using System;

    namespace Coding.Exercise
    {
        public class Game
        {
            private List<Rat> _rats = new List<Rat>();

            public void AddRat(Rat rat)
            {
                _rats.Add(rat);
                RatsCountChanged?.Invoke(this, _rats.Count);
            }

            public void RemoveRat(Rat rat)
            {
                _rats.Remove(rat);
                RatsCountChanged?.Invoke(this, _rats.Count);
            }

            public event EventHandler<int> RatsCountChanged;
        }

        public class Rat : IDisposable
        {
            private readonly Game _game;
            public int Attack = 1;

            public Rat(Game game)
            {
                _game = game;
                _game.RatsCountChanged += GameOnRatsCountChanged;
                _game.AddRat(this);
            }

            private void GameOnRatsCountChanged(object sender, int count)
            {
                this.Attack = count;
            }


            public void Dispose()
            {
                _game.RatsCountChanged -= GameOnRatsCountChanged;
                _game.RemoveRat(this);
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                var game = new Game();
                var rat1 = new Rat(game);
                Console.WriteLine($"Attack: {rat1.Attack}");
                var rat2 = new Rat(game);
                Console.WriteLine($"Attack: {rat1.Attack}");
                var rat3 = new Rat(game);
                Console.WriteLine($"Attack: {rat3.Attack}");
                rat3.Dispose();
                Console.WriteLine($"Attack: {rat1.Attack}");
            }
        }
    }
}
