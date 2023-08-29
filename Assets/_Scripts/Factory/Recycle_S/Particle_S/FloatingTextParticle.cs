using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class FloatingTextParticle : RecycleParticle
{
    public override ParticleType particleType => ParticleType.Floating;
    // [SerializeField] float time;
    // [SerializeField] float UptoY;
    // [SerializeField] AnimationCurve upEase;
    // [SerializeField] AnimationCurve fadeOutEase;
    TextMeshPro textMesh;
    // Sequence textSequnce;

    public override void Initialize(System.Action<RecycleObject> _restoreAction)
    {
        base.Initialize(_restoreAction);
        textMesh = GetComponentInChildren<TextMeshPro>();
    }
    public override void Play()
    {

    }
    public void Play(string content)
    {
        transform.rotation = CameraManager.Instance.transform.rotation;
        textMesh.text = content;

    }

    public override void Stop()
    {
    }
}
