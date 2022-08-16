using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Extensions
{
    public static class TweensExtension
    {
        public static void DOText(this TextMeshProUGUI text, string value)
        {
            text.SetText(value);
            text.transform.DOKill();
            text.transform.localScale = Vector3.one;
            text.transform.DOPunchScale(Vector3.one / 2, 0.3f, 2, 0.2f);
        }
    }
}