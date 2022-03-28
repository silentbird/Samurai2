using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class TopUI : UIBase        
    {
        protected override void Init()
        {
            Layer = UILayer.TOP_UI;
        }
    }
}
