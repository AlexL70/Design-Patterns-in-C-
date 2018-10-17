using System;
using Stateless;
using static System.Console;

namespace StateMachineWithStateless
{
    public enum Health
    {
        NonReproductive,
        Pregnant,
        Reproductive
    }

    public enum Activity
    {
        GiveBirth,
        ReachPuberty,
        HaveAbortion,
        HaveUnprotectedSex,
        Historectomy
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new StateMachine<Health, Activity>(Health.NonReproductive);
            machine.Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            machine.Configure(Health.Reproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant, () => ParentsNotWatching)
                .Permit(Activity.Historectomy, Health.NonReproductive);
            machine.Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);

            WriteLine($"Current state is {machine.State}");
            WriteLine($"Do next: {Activity.ReachPuberty}");
            machine.Fire(Activity.ReachPuberty);
            WriteLine($"Current state is {machine.State}");
            WriteLine($"Do next: {Activity.HaveUnprotectedSex}");
            try
            {
                machine.Fire(Activity.HaveUnprotectedSex);
            }
            catch (Exception e)
            {
                WriteLine(e);
                WriteLine($"Current state is {machine.State}");
                WriteLine("Distract parents");
                ParentsNotWatching = true;
                WriteLine($"Do next: {Activity.HaveUnprotectedSex}");
                machine.Fire(Activity.HaveUnprotectedSex);
            }
            WriteLine($"Current state is {machine.State}");
            WriteLine($"Do next: {Activity.GiveBirth}");
            machine.Fire(Activity.GiveBirth);
            WriteLine($"Current state is {machine.State}");
        }

        public static bool ParentsNotWatching { get; set; }
    }
}
