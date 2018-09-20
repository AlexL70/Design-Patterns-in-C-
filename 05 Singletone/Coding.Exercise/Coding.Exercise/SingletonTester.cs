using System;

namespace Coding.Exercise
{
    class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var inst1 = func();
            var inst2 = func();
            return inst1 == inst2;
        }
    }
}
