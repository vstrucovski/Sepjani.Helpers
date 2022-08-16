using System;
using System.Runtime.CompilerServices;

namespace Sepjani.Helpers.Scripts.Utils
{
    public static class Service<T> where T : class
    {
        private static T _instance;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Register(T instance)
        {
            _instance = instance;
            return _instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get(bool createIfNotExists = false)
        {
            if (_instance != null) return _instance;

            if (createIfNotExists) _instance = (T) Activator.CreateInstance(typeof(T), true);

            return _instance;
        }
    }
}