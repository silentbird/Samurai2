using UnityEngine;

namespace Const
{
    /// <summary>
    /// UI层级
    /// </summary>
    public enum UILayer
    {
        BASIC_UI,
        OVERLAY_UI,
        TOP_UI
    }

    /// <summary>
    /// UI运行状态
    /// </summary>
    public enum UIState
    {
        NORMAL,
        INIT,
        SHOW,
        HIDE
    }

    public enum UIId
    {
        MainMenu,
        StartGame
    }
}