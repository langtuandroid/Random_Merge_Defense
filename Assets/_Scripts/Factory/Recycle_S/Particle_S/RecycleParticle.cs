using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ParticleType { Basic, Floating, Scale, Attack }
public abstract class RecycleParticle : RecycleObject
{
    public float plusY;
    public abstract ParticleType particleType { get; }
    public abstract void Play();
    public abstract void Stop();
}
