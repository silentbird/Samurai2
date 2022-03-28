using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class OverlayUI : UIBase        
    {
        protected override void Init()
        {
            Layer = UILayer.TOP_UI;
        }
    }
}
