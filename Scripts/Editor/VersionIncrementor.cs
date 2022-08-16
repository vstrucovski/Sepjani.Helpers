using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Sepjani.Helpers.Scripts.Editor
{
    [InitializeOnLoad]
    class VersionIncrementor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        [MenuItem("Game/Build/Increase Both Versions &v")]
        static void IncreaseBothVersions()
        {
            IncreaseBuild();
            IncreasePlatformVersion();
        }

        [MenuItem("Game/Build/Increase Current Build Version")]
        static void IncreaseBuild()
        {
            IncrementVersion(new[] {0, 0, 1});
        }

        [MenuItem("Game/Build/Increase Minor Version")]
        static void IncreaseMinor()
        {
            IncrementVersion(new[] {0, 1, 0});
        }

        [MenuItem("Game/Build/Increase Major Version")]
        static void IncreaseMajor()
        {
            IncrementVersion(new[] {1, 0, 0});
        }

        [MenuItem("Game/Build/Increase Platform Version")]
        static void IncreasePlatformVersion()
        {
            PlayerSettings.Android.bundleVersionCode += 1;
            PlayerSettings.iOS.buildNumber = (int.Parse(PlayerSettings.iOS.buildNumber) + 1).ToString();
        }

        static void IncrementVersion(int[] version)
        {
            var lines = PlayerSettings.bundleVersion.Split('.');

            for (var i = lines.Length - 1; i >= 0; i--)
            {
                var isNumber = int.TryParse(lines[i], out int numberValue);

                if (!isNumber || version.Length - 1 < i) continue;
                if (i > 0 && version[i] + numberValue > 9)
                {
                    version[i - 1]++;

                    version[i] = 0;
                }
                else
                {
                    version[i] += numberValue;
                }
            }

            PlayerSettings.bundleVersion = $"{version[0]}.{version[1]}.{version[2]}";
        }

        //Show dialog about version increment
        public void OnPreprocessBuild(BuildReport report)
        {
            // bool shouldIncrement = EditorUtility.DisplayDialog(
            //     "Increment Version?",
            //     $"Current: {PlayerSettings.bundleVersion}",
            //     "Yes",
            //     "No"
            // );
            //
            // if (shouldIncrement) IncreaseBothVersions();
        }
    }
}