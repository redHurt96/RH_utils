using RH.Utilities.Hacks;
using UnityEngine;

namespace RH.Utilities.FPSCounting
{
    [RequireComponent(typeof(FpsDisplay))]
    public partial class FpsPanel : DebugPanel
    {
        private FpsDisplay _display;

        protected override void InitContent()
        {
            _display = GetComponent<FpsDisplay>();
            _display.Init();
        }

        protected override void OpenContent()
        {
            _display.ShowFps();
        }

        protected override void CloseContent()
        {
            _display.HideFps();
        }
    }
}