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

        foreach (var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, MoveSpeed>().WithAll<EnemyTag>())
        {
            transform.ValueRW.Position += math.down() * moveSpeed.Value * deltaTime;
        }
    }
}

public struct MoveSpeed : IComponentData
{
    public float Value;
}

public struct EnemyTag : IComponentData { }