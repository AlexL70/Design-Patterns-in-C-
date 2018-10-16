using System;
using System.Collections.Generic;
using static System.Console;

namespace HandmadeStateMachine
{
    public enum State
    {
        OffHook,
        Connecting,
        Connected,
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        HangUp,
        CallConnected,
        PlacedOnHold,
        TakenOffHold,
        LeftMessage
    }

    class Program
    {
        private static Dictionary<State, List<(Trigger, State)>> _rules =
            new Dictionary<State, List<(Trigger, State)>>
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HangUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HangUp, State.OffHook),
                    (Trigger.PlacedOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakenOffHold, State.Connected),
                    (Trigger.HangUp, State.OffHook)
                }
            };

        static void Main(string[] args)
        {
            var state = State.OffHook;
            while (true)
            {
                WriteLine($"The phone is currently {state}");
                WriteLine($"Select the trigger: ");
                for (var i = 0; i < _rules[state].Count; i++)
                {
                    var (transition, _) = _rules[state][i];
                    WriteLine($"{i}. {transition}");
                }

                State s;
                try
                {
                    int input = int.Parse(ReadLine());
                    (_, s) = _rules[state][input];
                }
                catch (Exception e)
                {
                    //WriteLine(e);
                    WriteLine("Error parsing input. Try again.");
                    WriteLine();
                    continue;
                }

                state = s;
            }
        }
    }
}
