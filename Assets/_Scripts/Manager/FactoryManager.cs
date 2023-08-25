using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FactoryManager : SingletonComponent<FactoryManager>
{
    [SerializeField] ParticleRecycleSystem particleFactory;
    public async Task Initialize()
    {
        await particleFactory.Initialize();
    }


    public FloatingTextParticle GetFloatingTextParticle(Vector3 pos)
    {
        return particleFactory.GetFloatingTextParticle(pos);
    }

    public void AllRestore()
    {
    }
}
