using Const;
using UnityEngine;

namespace UIFrame
{
    public interface IUIManager
    {

        void Show(UIId id);
        void Hide();
        void Back();
    }
}