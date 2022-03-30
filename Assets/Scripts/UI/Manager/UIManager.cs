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
        private Func<UILayer, Transform> GetLayerObject;

        private void Start()
        {
            Show(UIId.MainMenu);
        }

        public Tuple<Transform, Transform> Show(UIId id)
        {
            GameObject ui = GetPrefabObject(id);
            if (ui == null)
            {
                Debug.LogError("can not find prefab:" + id + "!");
                return null;
            }

            UIBase uiScript = GetUIScript(ui, id);
            if (uiScript == null)
            {
                Debug.LogError("uiScript is null");
                return null;
            }

            InitUI(uiScript);

            Transform hideUI = null;
            if (uiScript.Layer == UILayer.BASIC_UI)
            {
                uiScript.uiState = UIState.SHOW;
                hideUI = Hide();
            }
            else
            {
                uiScript.uiState = UIState.SHOW;
            }

            _uiStack.Push(uiScript);

            return new Tuple<Transform, Transform>(ui.transform, hideUI);
        }

        private Transform Hide()
        {
            if (_uiStack.Count != 0)
            {
                var hideUI = _uiStack.Peek();
                hideUI.uiState = UIState.HIDE;
                return hideUI.transform;
            }

            return null;
        }

        public Tuple<Transform, Transform> Back()
        {
            UIBase showUI = null;
            UIBase hideUI = null;
            if (_uiStack.Count > 1)
            {
                hideUI = _uiStack.Pop();
                showUI = _uiStack.Peek();
                if (showUI.Layer == UILayer.BASIC_UI)
                {
                    showUI.uiState = UIState.SHOW;
                }

                hideUI.uiState = UIState.HIDE;
            }
            else
            {
                Debug.LogError("uiStack has one or no element");
            }

            return new Tuple<Transform, Transform>(showUI?.transform, hideUI?.transform);
        }

        private void InitUI(UIBase uiScript)
        {
            if (uiScript.uiState == UIState.NORMAL)
            {
                Transform ui = uiScript.transform;
                //根据层级添加到对应父物体下
                ui.SetParent(GetLayerObject?.Invoke(uiScript.Layer));
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

        public void AddGetLayerObjectListener(Func<UILayer, Transform> fun)
        {
            if (fun == null)
            {
                Debug.LogError("GetLayerObject function can not be null");
                return;
            }
            GetLayerObject = fun;
        }
    }
}