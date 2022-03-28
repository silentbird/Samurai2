using Const;
using UnityEngine;

namespace UIFrame
{
    public abstract class UIBase : MonoBehaviour
    {
        public UILayer Layer { get; protected set; }
        private UIState _uiState = UIState.NORMAL;

        public UIState uiState
        {
            get { return _uiState; }
            set { HandleUIState(value); }
        }

        private void HandleUIState(UIState value)
        {
            switch (value)
            {
                case UIState.INIT:
                    if (_uiState == UIState.NORMAL)
                    {
                        Init();
                    }
                    break;
                case UIState.SHOW:
                    if (_uiState == UIState.NORMAL)
                    {
                        Init();
                        Show();
                    }
                    else
                    {
                        Show();
                    }
                    break;
                case UIState.HIDE:
                    Hide();
                    break;
                default:
                    return;
            }
        }

        protected virtual void Init()
        {
        }

        protected virtual void Show()
        {
            SetActive(true);
        }

        protected virtual void Hide()
        {
            SetActive(false);
        }

        protected virtual void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public abstract UIId GetUiId();

    }
}