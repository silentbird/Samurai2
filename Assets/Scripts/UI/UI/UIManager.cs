using System;
using System.Collections.Generic;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class UIManager : MonoBehaviour
    {
        private readonly Dictionary<UIId, GameObject> _prefabDictionary = new Dictionary<UIId, GameObject>();
        private readonly Stack<UIBase> _uiStack = new Stack<UIBase>();
        private UILayerManager _layerManager;
        private UIEffectManager _effectManager;

        private void Awake()
        {
            _layerManager = GetComponent<UILayerManager>();
            _effectManager = GetComponent<UIEffectManager>();
            if (_layerManager == null) Debug.LogError("can not find UILayerManager");
            if (_effectManager == null) Debug.LogError("can not find UIEffectManager");
        }

        private void Start()
        {
            Show(UIId.MainMenu);
        }

        public void Show(UIId id)
        {
            GameObject ui = GetPrefabObject(id);
            if (ui == null)
            {
                Debug.LogError("can not find prefab:" + id + "!");
                return;
            }

            UIBase uiScript = GetUIScript(ui, id);
            if (uiScript == null)
            {
                Debug.LogError("uiScript is null");
                return;
            }

            InitUI(uiScript);

            if (uiScript.Layer == UILayer.BASIC_UI)
            {
                uiScript.uiState = UIState.SHOW;
                Hide();
            }
            else
            {
                uiScript.uiState = UIState.SHOW;
            }

            _uiStack.Push(uiScript);
            _effectManager.Show(ui.transform);
        }

        private void Hide()
        {
            if (_uiStack.Count != 0)
            {
                _uiStack.Peek().uiState = UIState.HIDE;
                _effectManager.Hide(_uiStack.Peek().transform);
            }
        }

        public void Back()
        {
            if (_uiStack.Count > 1)
            {
                UIBase hideUI = _uiStack.Pop();
                if (_uiStack.Peek().Layer == UILayer.BASIC_UI)
                {
                    _uiStack.Peek().uiState = UIState.SHOW;
                }

                hideUI.uiState = UIState.HIDE;
                _effectManager.Hide(hideUI.transform);
            }
            else
            {
                Debug.LogError("uiStack has one or no element");
            }
        }

        private void InitUI(UIBase uiScript)
        {
            if (uiScript.uiState == UIState.NORMAL)
            {
                Transform ui = uiScript.transform;
                //根据层级添加到对应父物体下
                ui.SetParent(_layerManager.GetLayerObject(uiScript.Layer));
                ui.localPosition = Vector3.zero;
                ui.localScale = Vector3.one;
            }
        }

        private GameObject GetPrefabObject(UIId id)
        {
            if (!_prefabDictionary.ContainsKey(id) || _prefabDictionary[id] == null)
            {
                GameObject prefab = LoadManager.Instance.Load<GameObject>(Path.UIPath, id.ToString());
                if (prefab != null)
                {
                    _prefabDictionary[id] = Instantiate(prefab);
                }
                else
                {
                    Debug.LogError("can not find prefab:" + id);
                }
            }

            return _prefabDictionary[id];
        }

        private UIBase GetUIScript(GameObject prefab, UIId id)
        {
            UIBase ui = prefab.GetComponent<UIBase>();
            if (ui == null)
            {
                return AddUIScript(prefab, id);
            }
            else
            {
                return ui;
            }
        }

        private UIBase AddUIScript(GameObject prefab, UIId id)
        {
            string scriptName = $"{ConstValue.NAMESPCAE_NAME}.{id}{ConstValue.UI_SCRIPT_POSTFIX}";
            Type ui = Type.GetType(scriptName);
            if (ui == null)
            {
                Debug.LogError("can not find script:" + scriptName);
                return null;
            }

            return prefab.AddComponent(ui) as UIBase;
        }
    }
}