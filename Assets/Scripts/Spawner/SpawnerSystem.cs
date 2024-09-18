using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Collections;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct SpawnerSystem : ISystem
{
    public void OnUpdate(ref SystemState state) 
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        foreach(RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = ecb.Instantiate(spawner.ValueRO.Prefab);

                float3 pos = new float3(UnityEngine.Random.Range(-8.5f, 8.5f), 5f, 0);

                ecb.AddComponent(newEntity, new EnemyMoveSpeed { Value = spawner.ValueRO.EnemyMoveSpeed });
                ecb.AddComponent(newEntity, new EnemyTag());
                ecb.AddComponent(newEntity, new LifeTime { Value = spawner.ValueRO.EnemyLifeTime});
                ecb.SetComponent(newEntity, LocalTransform.FromPosition(pos));

                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
        // Prevent memory leak
        ecb.Playback(state.EntityManager);
        ecb.Dispose();


        // OLD VERSION
        //foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        //{
        //    if(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        //    {
        //        Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab); // What is ValueRO?

        //        float3 pos = new float3(UnityEngine.Random.Range(-8.5f, 8.5f), 3.5f, 0);

        //        state.EntityManager.AddComponentData(newEntity, new EnemyMoveSpeed { Value = 5f });
        //        state.EntityManager.AddComponentData(newEntity, new EnemyTag());

        //        state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));

        //        spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
        //    }
        //}
    }
}