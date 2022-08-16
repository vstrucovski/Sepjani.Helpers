using Morpeh;
using Sepjani.Helpers.Scripts.Morpeh;

namespace Sepjani.Helpers.Scripts.Extensions
{
    public static class MorpehExtensions
    {
        //example World.Default.CreateOneFrame<SpawnEnemyEvent>();
        public static void CreateOneFrameEvent<T>(this World world) where T : struct, IComponent
        {
            var entity = world.CreateEntity();
            entity.AddComponent<OneFrameTag>();
            entity.AddComponent<T>();
        }
    }
}