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
                return;
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
        }

        private void Hide()
        {
            if (_uiStack.Count != 0)
            {
                _uiStack.Peek().uiState = UIState.HIDE;
            }
        }

        public void Back()
        {
            if (_uiStack.Count > 1)
            {
                if (_uiStack.Peek().Layer == UILayer.BASIC_UI)
                {
                    _uiStack.Pop().uiState = UIState.HIDE;
                    _uiStack.Peek().uiState = UIState.SHOW;
                }
                else
                {
                    _uiStack.Pop().uiState = UIState.HIDE;
                }
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
                ui.localPosition = Vector3.zero;
            }
        }

        private GameObject GetPrefabObject(UIId id)
        {
            if (!_prefabDictionary.ContainsKey(id) || _prefabDictionary[id] == null)
            {
                _prefabDictionary[id] = LoadManager.Instance.Load<GameObject>(Path.UIPath, id.ToString());
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
            string scriptName = id + ConstValue.UI_SCRIPT_POSTFIX;
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