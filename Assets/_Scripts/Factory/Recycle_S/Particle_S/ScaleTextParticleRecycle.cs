using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ScaleTextParticleRecycle : RecycleParticle
{
    public override ParticleType particleType => ParticleType.Floating;
    [SerializeField] float scaleDuration;
    [SerializeField] float fadeDuration;
    [SerializeField] float scale;
    [SerializeField] AnimationCurve upEase;
    [SerializeField] AnimationCurve fadeOutEase;
    TextMeshPro textMesh;
    Vector3 initialScale;
    public override void Initialize(System.Action<RecycleObject> _restoreAction)
    {
        base.Initialize(_restoreAction);
        textMesh = GetComponentInChildren<TextMeshPro>();
        initialScale = textMesh.transform.localScale;
    }
    public override void Play()
    {
        textMesh.transform.localScale = initialScale;
        textMesh.alpha = 1f;
        Sequence a = DOTween.Sequence();
        a.Append(transform.DOScale(scale, scaleDuration).SetEase(upEase));
        a.Append(textMesh.DOFade(0, fadeDuration).SetEase(fadeOutEase))
        .OnComplete(() =>
        {
            Restore();
        });
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }
}
