using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;

[System.Serializable]
public class ParticleRecycleSystem
{
    [System.Serializable]
    public struct ParticleComponent
    {
        public string id;
        public Factory factory;
        public float plusY;
        public void Initialize(RecycleParticle particle)
        {
            id = particle.id;
            plusY = particle.plusY;
            factory = new Factory(particle, particle.poolSize);
        }
        public void AllRestore()
        {
            int count = factory.AllObj.Count;
            for (int i = 0; i < count; i++)
            {
                factory.AllObj[i].Restore();
            }
        }
    }
    [SerializeField] ParticleComponent[] particles;
    ParticleComponent floatingParticleComponent;
    ParticleComponent criticalFloatingParticleComponent;
    FloatingTextParticle floatingParticle;
    Camera camera;
    public Task Initialize()
    {
        camera = Camera.main;
        List<ParticleComponent> tempParticles = new List<ParticleComponent>();


        AsyncOperationHandle<IList<GameObject>> handle = Addressables.LoadAssetsAsync<GameObject>("RecycleParticle", null);
        handle.Completed += (result) =>
       {
           var results = result.Result;
           for (int i = 0; i < results.Count; i++)
           {
               RecycleParticle recycleParticle = results[i].GetComponent<RecycleParticle>();
               ParticleComponent tempParticle = new ParticleComponent();
               tempParticle.Initialize(recycleParticle);
               tempParticles.Add(tempParticle);
           }
           particles = tempParticles.ToArray();
           for (int i = 0; i < particles.Length; i++)
           {
               if ("FloatingParticle" == particles[i].id)
               {
                   floatingParticleComponent = particles[i];
               }
               if ("CriticalFloatingText" == particles[i].id)
               {
                   criticalFloatingParticleComponent = particles[i];
               }
           }
           floatingParticleComponent.factory.CreatPool();
           criticalFloatingParticleComponent.factory.CreatPool();
       };

        return handle.Task;
    }
    public RecycleParticle GetRecycleParticle(string Id, Vector3 pos)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (Id == particles[i].id)
            {
                RecycleParticle particle = particles[i].factory.Get() as RecycleParticle;
                pos.y += particles[i].plusY;
                particle.transform.position = pos;

                if (particle.particleType == ParticleType.Basic || particle.particleType == ParticleType.Attack)
                {
                    particle.transform.rotation = Quaternion.identity;
                }
                else
                {
                    particle.transform.rotation = camera.transform.rotation;
                }

                return particle;
            }
        }
        Debug.LogError(string.Format($"Id 없음 Id = {Id}"));
        return null;
    }
    public FloatingTextParticle GetFloatingTextParticle(Vector3 pos)
    {
        floatingParticle = floatingParticleComponent.factory.Get() as FloatingTextParticle;
        pos.y += floatingParticleComponent.plusY;
        floatingParticle.transform.position = pos;
        return floatingParticle;
    }
    public FloatingTextParticle GetCritialFloatingTextParticle(Vector3 pos)
    {
        floatingParticle = criticalFloatingParticleComponent.factory.Get() as FloatingTextParticle;
        pos.y += criticalFloatingParticleComponent.plusY;
        floatingParticle.transform.position = pos;
        return floatingParticle;
    }
    public void AllRestore()
    {
        int count = particles.Length;
        for (int i = 0; i < count; i++)
        {
            particles[i].AllRestore();
        }
    }
}
