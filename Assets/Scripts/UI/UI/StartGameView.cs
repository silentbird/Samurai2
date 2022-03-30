using System;
using Const;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class StartGameView : BasicUI
    {
        public override UIId GetUiId()
        {
            return UIId.StartGame;
        }

        private void Start()
        {
            transform.Find("Buttons/Continue").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Easy").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Normal").RectTransform().AddBtnListener(() => { });
            transform.Find("Buttons/Hard").RectTransform().AddBtnListener(() => { });
        }
    }
}