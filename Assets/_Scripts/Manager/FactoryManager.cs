using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FactoryManager : SingletonComponent<FactoryManager>
{
    [SerializeField] ParticleRecycleSystem particleFactory;
    [SerializeField] EnemyRecycleSystem enemyFactory;
    [SerializeField] TowerRecycleSystem towerFactory;
    [SerializeField] AttackObjectRecycleSystem attackObjectFactory;
    public async Task Initialize()
    {
        await particleFactory.Initialize();
        await enemyFactory.Initialize();
        await towerFactory.Initialize();
        await attackObjectFactory.Initialize();
    }


    public FloatingTextParticle GetFloatingTextParticle(Vector3 pos)
    {
        return particleFactory.GetFloatingTextParticle(pos);
    }
    public RecycleParticle GetParticle(string id, Vector3 pos)
    {
        return particleFactory.GetRecycleParticle(id, pos);
    }
    public FloatingTextParticle GetCritialFloatingTextParticle(Vector3 pos)
    {
        return particleFactory.GetCritialFloatingTextParticle(pos);
    }
    public EnemyController GetEnemyController(string id, Vector3 pos, Vector3 lookAtTarget)
    {
        return enemyFactory.GetRecycleEnemy(id, pos, lookAtTarget);
    }
    public TowerController GetTower(string id, Vector3 pos)
    {
        return towerFactory.GetTower(id, pos);
    }
    public AttackObject GetAttackObject(string id, Vector3 pos)
    {
        return attackObjectFactory.GetAttackObject(id, pos);
    }

    public void AllRestore()
    {
    }
}
