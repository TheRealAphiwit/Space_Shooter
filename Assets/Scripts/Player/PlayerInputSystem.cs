using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerInputSystem : SystemBase // Using SystemBase to read inputs
{
    private GameInput InputActions;
    private Entity Player;

    protected override void OnCreate()
    {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
    }

    protected override void OnUpdate()
    {
        
    }
}
