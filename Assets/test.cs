using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class test : MonoBehaviour
    {
        public void Start()
        {
            Type t = Type.GetType("UIFrame.MainMenuView");
            
            GameObject prefab = LoadManager.Instance.Load<GameObject>(Path.UIPath, "MainMenu");

            var tmp = gameObject.AddComponent(t);
            // var tmp = gameObject.AddComponent<MainMenuView>();
            if (tmp == null)
            {
                Debug.LogError("null");
            }
        }
    }
}