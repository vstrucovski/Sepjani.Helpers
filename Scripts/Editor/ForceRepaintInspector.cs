using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Editor
{
    public class ForceRepaintInspector : MonoBehaviour
    {
        private void Update()
        {
            GUIHelper.RequestRepaint();
        }
    }
}