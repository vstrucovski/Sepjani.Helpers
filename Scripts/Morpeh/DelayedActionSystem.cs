using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Morpeh
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DelayedActionSystem))]
    public sealed class DelayedActionSystem : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter.With<DelayedAction>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var action = ref entity.GetComponent<DelayedAction>();
                action.delayTime -= deltaTime;
                if (action.delayTime > 0) continue;
                action.Action();
                entity.RemoveComponent<DelayedAction>();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            // Debug.Log("DelayedActionSystem Dispose");
            foreach (var entity in _filter)
            {
                World.Default.RemoveEntity(entity);
            }
        }
    }
}