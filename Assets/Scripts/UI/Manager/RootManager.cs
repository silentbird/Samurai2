using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class RootManager : MonoBehaviour
    {
        private UIManager _uiManager;
        private UIEffectManager _uiEffectManager;
        private UILayerManager _uiLayerManager;
        private InputManager _inputManager;
        public static RootManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _uiManager = gameObject.AddComponent<UIManager>();
            _uiEffectManager = gameObject.AddComponent<UIEffectManager>();
            _uiLayerManager = gameObject.AddComponent<UILayerManager>();
            _inputManager = gameObject.AddComponent<InputManager>();
            _uiManager.AddGetLayerObjectListener((layer => _uiLayerManager.GetLayerObject(layer)));
        }

        private void Start()
        {
            Show(UIId.MainMenu);
        }

        public void Show(UIId id)
        {
            var uiPara = _uiManager.Show(id);
            ExecuteEffect(uiPara);
        }

        public void Back()
        {
            var uiPara = _uiManager.Back();
            ExecuteEffect(uiPara);
        }

        private void ExecuteEffect(Tuple<Transform, Transform> uiPara)
        {
            _uiEffectManager.Show(uiPara.Item1);
            _uiEffectManager.Hide(uiPara.Item2);
        }
    }
}