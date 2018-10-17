using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Coding.Exercise
{
    public class CombinationLock
    {
        private enum StatusEnum
        {
            Locked,
            Error,
            Open
        }

        private StatusEnum _status = StatusEnum.Locked;
        private readonly int[] _combination;
        private readonly List<int> _currentCombination = new List<int>();

        public CombinationLock(int[] combination)
        {
            _combination = combination;
        }

        private string currComb()
        {
            var sb = new StringBuilder();
            foreach (var digit in _currentCombination)
            {
                sb.Append(digit);
            }

            return sb.ToString();
        }

        public string Status => _currentCombination.Count == 0 || _status != StatusEnum.Locked
            ? _status.ToString().ToUpper() 
            : currComb();

        public void EnterDigit(int digit)
        {
            switch (_status)
            {
                case StatusEnum.Locked:
                    _currentCombination.Add(digit);
                    if (_currentCombination.SequenceEqual(_combination))
                        _status = StatusEnum.Open;
                    else if (digit != _combination[_currentCombination.Count - 1])
                        _status = StatusEnum.Error;
                    break;
                case StatusEnum.Error:
                    break;
                case StatusEnum.Open:
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");
        }
    }
}
