using System;
using Const;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class MainMenuView : BasicUI
    {
        private void Start()
        {
            transform.Find("Buttons/StartGame").RectTransform().AddBtnListener(() =>
            {
                RootManager.Instance.Show(UIId.StartGame);
            });
            transform.Find("Buttons/DOJO").RectTransform().AddBtnListener((() => { }));
            transform.Find("Buttons/Help").RectTransform().AddBtnListener((() => { }));
            transform.Find("Buttons/ExitGame").RectTransform().AddBtnListener((() => { Application.Quit(); }));
        }

        public override UIId GetUiId()
        {
            return UIId.MainMenu;
        }
    }
}