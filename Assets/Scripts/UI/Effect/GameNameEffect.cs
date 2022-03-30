using Const;
using DG.Tweening;
using UnityEngine;
using Util;

namespace UIFrame
{
    public class GameNameEffect : UIEffectBase
    {
        public override void Enter()
        {
            base.Enter();
            float time = 1;
            transform.DOScale(Vector3.one, time);
            transform.RectTransform().DOAnchorPos(new Vector2(550f, 290f), time)
                .OnComplete(() => _onEnterComplete?.Invoke());
        }

        public override void Exit()
        {
            float time = 1;
            transform.DOScale(Vector3.one * 2, time);
            transform.RectTransform().DOAnchorPos(_defaultAncherPos, time).OnComplete(() => _onExitComplete?.Invoke());
        }

        public override UIEffect GetEffectLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}