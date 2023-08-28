using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FactoryManager : SingletonComponent<FactoryManager>
{
    [SerializeField] ParticleRecycleSystem particleFactory;
    [SerializeField] EnemyRecycleSystem enemyFactory;
    public async Task Initialize()
    {
        await particleFactory.Initialize();
        await enemyFactory.Initialize();
    }


    public FloatingTextParticle GetFloatingTextParticle(Vector3 pos)
    {
        return particleFactory.GetFloatingTextParticle(pos);
    }
    public EnemyController GetEnemyController(string id, Vector3 pos, Vector3 lookAtTarget)
    {
        return enemyFactory.GetRecycleEnemy(id, pos, lookAtTarget);
    }

    public void AllRestore()
    {
    }
}
