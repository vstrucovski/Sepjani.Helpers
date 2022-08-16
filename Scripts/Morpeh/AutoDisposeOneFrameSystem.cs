using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Morpeh
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AutoDisposeOneFrameSystem))]
    public sealed class AutoDisposeOneFrameSystem : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter.With<OneFrameTag>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                World.RemoveEntity(entity);
            }
        }
    }
}