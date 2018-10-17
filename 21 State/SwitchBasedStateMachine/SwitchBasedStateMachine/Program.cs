using System;
using System.Text;
using static System.Console;

namespace SwitchBasedStateMachine
{
    public enum State
    {
        Locked,
        Failed,
        Unlocked
    }

    class Program
    {
        static void Main(string[] args)
        {
            var code = "1234";
            var state = State.Locked;
            var entry = new StringBuilder();
            while (true)
            {
                switch (state)
                {
                    case State.Locked:
                        entry.Append(ReadKey().KeyChar);
                        if (entry.ToString() == code)
                            state = State.Unlocked;
                        else if (!code.StartsWith(entry.ToString()))
                            state = State.Failed;
                        break;
                    case State.Failed:
                        CursorLeft = 0;
                        WriteLine("FAILED");
                        entry.Clear();
                        state = State.Locked;
                        break;
                    case State.Unlocked:
                        CursorLeft = 0;
                        WriteLine("UNLOCKED");
                        return;
                }
            }
        }
    }
}

