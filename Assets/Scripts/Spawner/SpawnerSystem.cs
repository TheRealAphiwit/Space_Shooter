using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    public void OnDestroy(ref SystemState state) { }
    public void OnUpdate(ref SystemState state) 
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach(RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if(spawner.ValueRW.NextSpawnTime < (float)SystemAPI.Time.ElapsedTime)
            {
                Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab); // What is ValueRO?

                float3 pos = new float3(UnityEngine.Random.Range(-8.5f, 8.5f), 3.5f, 0);

                state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPositionRotationScale(pos, quaternion.identity, 5));
                state.EntityManager.AddComponentData(newEntity, new MoveSpeed { Value = 5f });

                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}