using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace Sepjani.Helpers.Scripts.Editor
{
    public static class PostBuildStep
    {
        // Set the IDFA request description:
        const string TrackingDescription =
            "Your data will be used to provide you a better and personalized ad experience.";

        [PostProcessBuild(0)]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                SetFlagsAboutFrameworks(pathToXcode);
                AddPListValues(pathToXcode);
            }
        }

        // Implement a function to read and write values to the plist file:
        private static void AddPListValues(string pathToXcode)
        {
#if UNITY_IOS
        // Retrieve the plist file from the Xcode project directory:
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();


        // Read the values from the plist file:
        plistObj.ReadFromString(File.ReadAllText(plistPath));

        // Set values from the root object:
        PlistElementDict plistRoot = plistObj.root;

        // Set the description key-value in the plist:
        plistRoot.SetString("NSUserTrackingUsageDescription", TrackingDescription);

        // Save changes to the plist:
        File.WriteAllText(plistPath, plistObj.WriteToString());
#endif
        }

        private static void SetFlagsAboutFrameworks(string path)
        {
#if UNITY_IOS
        string projPath = PBXProject.GetPBXProjectPath(path);
        PBXProject proj = new PBXProject();
        proj.ReadFromFile(projPath);

        string targetGuid = proj.GetUnityMainTargetGuid();

        foreach (var framework in new[] {targetGuid, proj.GetUnityFrameworkTargetGuid()})
        {
            // proj.SetBuildProperty(framework, "ENABLE_BITCODE", "NO");
            proj.SetBuildProperty(framework, "EMBEDDED_CONTENT_CONTAINS_SWIFT", "YES");
            proj.SetBuildProperty(framework, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
            proj.SetBuildProperty(framework, "SWIFT_VERSION", "5.0");
        }

        proj.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "YES");

        proj.WriteToFile(projPath);
#endif
        }
    }
}