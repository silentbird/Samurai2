using Const;
using DG.Tweening;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class MainViewButtonEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            transform.DOKill();
            transform.RectTransform().DOKill();
            transform.RectTransform().DOAnchorPos(new Vector2(0, -324f), 1)
                .OnComplete(() => _onEnterComplete?.Invoke());
        }

        public override void Exit()
        {
            transform.DOKill();
            transform.RectTransform().DOKill();
            transform.RectTransform().DOAnchorPos(_defaultAncherPos, 1)
                .OnComplete(() => _onExitComplete?.Invoke());
        }

        public override UIEffect GetEffectLevel()
        {
            return UIEffect.OTHERS_EFFECT;
        }
    }
}