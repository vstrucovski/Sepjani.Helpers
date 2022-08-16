using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Editor
{
    [InitializeOnLoad]
    public static class PlayButtonEditor
    {
        static PlayButtonEditor()
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
            EditorSceneManager.playModeStartScene = sceneAsset;
            Debug.Log(pathOfFirstScene + " was set as default play mode scene");
        }
    }
}