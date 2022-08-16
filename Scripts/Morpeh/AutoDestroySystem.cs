using DG.Tweening;
using Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Morpeh
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AutoDestroySystem))]
    public sealed class AutoDestroySystem : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter.With<TransformRef>().With<Destroy>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var destroyable = ref entity.GetComponent<Destroy>();
                destroyable.time -= deltaTime;
                if (destroyable.time > 0) continue;
                ref var transform = ref entity.GetComponent<TransformRef>().Value;
                DOTween.Kill(transform);
                DOTween.Kill(transform.gameObject);
                if (transform.childCount > 0)
                {
                    DOTween.Kill(transform.GetChild(0));
                }

                Destroy(transform.gameObject);
                World.Default.RemoveEntity(entity);
            }
        }
    }
}