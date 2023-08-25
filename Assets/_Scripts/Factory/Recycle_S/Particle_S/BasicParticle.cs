using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicParticle : RecycleParticle
{
    public override ParticleType particleType => ParticleType.Basic;
    // public override RecycleObjectType recycleObjectType => RecycleObjectType.Particle;
    protected new ParticleSystem particleSystem;
    public override void Initialize(System.Action<RecycleObject> _restoreAction)
    {
        particleSystem = GetComponent<ParticleSystem>();
        base.Initialize(_restoreAction);
        var main = particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }
    public override void Play()
    {
        particleSystem.Play();
    }
    private void OnParticleSystemStopped()
    {
        Restore();
    }

    public override void Stop()
    {
        particleSystem.Stop();
    }

}
