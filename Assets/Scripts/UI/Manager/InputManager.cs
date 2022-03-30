using System;
using UnityEngine;

namespace UIFrame
{
    public class InputManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                RootManager.Instance.Back();
            }
        }
    }
}