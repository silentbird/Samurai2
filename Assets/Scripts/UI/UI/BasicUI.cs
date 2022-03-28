using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class BasicUI : UIBase
    {
        protected override void Init()
        {
            base.Init();
            Layer = UILayer.BASIC_UI;
        }

    }
}