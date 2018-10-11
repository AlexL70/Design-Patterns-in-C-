using System;
using System.Windows;
using static System.Console;

namespace WeakEventPattern
{
    public class Button
    {
        public event EventHandler Clicked;

        public void Fire()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Window
    {
        public Window(Button button)
        {
            WeakEventManager<Button, EventArgs>
                .AddHandler(button, nameof(Button.Clicked), ButtonOnClicked);
        }

        ~Window()
        {
            WriteLine("Window finalized");
        }

        private void ButtonOnClicked(object sender, EventArgs e)
        {
            WriteLine("Button clicked (Window handler)");
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var button = new Button();
            var window = new Window(button);
            //var winRef = new WeakReference(window);
            button.Fire();

            WriteLine("Setting window to null");
            // ReSharper disable once RedundantAssignment
            window = null;

            FireGC();

            //if (winRef.IsAlive)
            //{
            //    WriteLine("Windows is still alive!");
            //    button.Fire();
            //}
        }

        private static void FireGC()
        {
            WriteLine("Start GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            WriteLine("GC is done!");
        }
    }
}
