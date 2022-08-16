using UnityEditor;
using UnityEditor.SceneManagement;

namespace Sepjani.Helpers.Scripts.Editor
{
    public static class GameNavigationHelper
    {
        [MenuItem("Game/Open Boot Scene", priority = 11)]
        public static void OpenBoot()
        {
            EditorSceneManager.OpenScene("Assets/_Project/Scenes/Boot.unity");
        }
        
        [MenuItem("Game/Open Home Scene", priority = 12)]
        public static void OpenHome()
        {
            EditorSceneManager.OpenScene("Assets/_Project/Scenes/HomeMenu.unity");
        }

        [MenuItem("Game/Open Gameplay Scene", priority = 13)]
        public static void OpenFreestyle()
        {
            EditorSceneManager.OpenScene("Assets/_Project/Scenes/Gameplay.unity");
        }
    }
}