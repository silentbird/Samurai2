using System;
using System.Collections.Generic;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class UILayerManager : MonoBehaviour
    {
        private readonly Dictionary<UILayer, Transform> _layerDictionary = new Dictionary<UILayer, Transform>();

        private void Awake()
        {
            Transform tmp = null;
            foreach (UILayer item in Enum.GetValues(typeof(UILayer)))
            {
                tmp = transform.Find(item.ToString());
                if (tmp == null)
                {
                    Debug.LogError("can not find layer:" + item + " GameObject");
                    continue;
                }
                else
                {
                    _layerDictionary[item] = tmp;
                }
            } 
        }

        public Transform GetLayerObject(UILayer layer)
        {
            if (!_layerDictionary.ContainsKey(layer))
            {
                Debug.LogError("_layerDictionary did not contains lay:" + layer);
            }
            return _layerDictionary[layer];
        }
    }
}