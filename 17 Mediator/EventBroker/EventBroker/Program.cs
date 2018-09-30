using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Autofac;
using static System.Console;

namespace EventBroker
{
    class Program
    {
        public class Actor
        {
            protected EventBroker Broker;

            public Actor(EventBroker broker)
            {
                Broker = broker ?? throw new ArgumentNullException(nameof(broker));
            }
        }

        public class FootballPlayer : Actor
        {
            public FootballPlayer(EventBroker broker, string name) : base(broker)
            {
                Name = name;
                broker.OfType<PlayerScoredEvent>()
                    .Where(p => p.Name != Name).Subscribe( pe =>
                {
                    WriteLine($"{Name}: Nicely done, {pe.Name}! It's your {pe.GoalsScored} goal!");
                });
                broker.OfType<PlayerSentOffEvent>()
                    .Where(pe => pe.Name != Name).Subscribe(pe =>
                    {
                        WriteLine($"{Name}: see you in the lockers, {pe.Name}!");
                    });
            }

            public string Name { get; set; }
            public int GoalScored { get; set; } = 0;

            public void Score()
            {
                GoalScored++;
                Broker.Publish(new PlayerScoredEvent() {Name = Name, GoalsScored = GoalScored});
            }

            public void AssaultTheReferee()
            {
                Broker.Publish(new PlayerSentOffEvent() {Name = Name, Reason = "violence"});
            }
        }

        public class FootballCoach : Actor
        {
            public FootballCoach(EventBroker broker) : base(broker)
            {
                broker.OfType<PlayerScoredEvent>()
                    .Subscribe(pe =>
                    {
                        if (pe.GoalsScored < 3)
                        {
                            WriteLine($"Coach: Well done, {pe.Name}!");
                        }
                    });
                broker.OfType<PlayerSentOffEvent>()
                    .Subscribe(pe =>
                    {
                        if (pe.Reason == "violence")
                        {
                            WriteLine($"Coach: How could you do that, {pe.Name}?!");
                        }
                    });
            }
        }

        public class PlayerEvent
        {
            public string Name { get; set; }
        }

        public class PlayerScoredEvent : PlayerEvent
        {
            public int GoalsScored { get; set; }
        }

        public class PlayerSentOffEvent : PlayerEvent
        {
            public string Reason { get; set; }
        }

        public class EventBroker : IObservable<PlayerEvent>
        {
            readonly Subject<PlayerEvent> _subscriptions = new Subject<PlayerEvent>();

            public IDisposable Subscribe(IObserver<PlayerEvent> observer)
            {
                return _subscriptions.Subscribe(observer);
            }

            public void Publish(PlayerEvent pe)
            {
                _subscriptions.OnNext(pe);
            }
        }

        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<FootballCoach>();
            cb.Register((c, p) => new FootballPlayer(
                c.Resolve<EventBroker>(), p.Named<string>("name")
            ));
            using (var c = cb.Build())
            {
                var coach = c.Resolve<FootballCoach>();
                var player1 = c.Resolve<FootballPlayer>(new NamedParameter("name", "John"));
                var player2 = c.Resolve<FootballPlayer>(new NamedParameter("name", "Chris"));

                player1.Score();
                player1.Score();
                player1.Score();
                player1.AssaultTheReferee();
                player2.Score();
            }
        }
    }
}
