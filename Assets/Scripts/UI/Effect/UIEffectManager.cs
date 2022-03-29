using System;
using Const;
using UnityEngine;

namespace UIFrame
{
    public class UIEffectManager : MonoBehaviour
    {
        public void Show(Transform ui)
        {
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                effectBase.Enter();
            }
        }

        public void Hide(Transform ui)
        {
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                effectBase.Exit();
            }
        }

        public void AddViewEffectEnterListener(Transform ui, Action enterComplete)
        {
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                if (effectBase.GetEffectLevel() == UIEffect.VIEW_EFFECT)
                {
                    effectBase.OnEnterComplete(enterComplete);
                }
            }
        }
        
        public void AddViewEffectExitListener(Transform ui, Action exitComplete)
        {
            foreach (UIEffectBase effectBase in ui.GetComponentsInChildren<UIEffectBase>(true))
            {
                if (effectBase.GetEffectLevel() == UIEffect.VIEW_EFFECT)
                {
                    effectBase.OnExitComplete(exitComplete);
                }
            }
        }
    }
}