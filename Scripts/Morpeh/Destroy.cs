using System;
using Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Sepjani.Helpers.Scripts.Morpeh
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [Serializable]
    public struct Destroy : IComponent
    {
        public float time;
    }
}