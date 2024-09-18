using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Mathematics;

[BurstCompile]
public partial struct EnemyMoveSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, enemyMoveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, EnemyMoveSpeed>().WithAll<EnemyTag>())
        {
            transform.ValueRW.Position += math.down() * enemyMoveSpeed.Value * deltaTime;
        }
    }
}