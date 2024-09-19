To begin working in DOTS with Unity I began by downloading entities graphics and building my game using ECS. The Unity ECS is reliant on structs meaning that a lot of this game project will be allocated on the stack, the spawner will for example be a struct that rapidly spawns enemy prefabs(entity) with a SpawnerAuthoring telling it how to construct these entities through baking. This process is also replicated for spawning projectiles that the player shoots, in that case, adding data components like lifetime on instantiation or setting data components like transform for the projectile.

As for the player inputs; since we’re working in DOTS and using ECS we will also be opting out a monobehaviour player input script for one that uses SystemBase and Jobs.

Additions I made:
Randomizing spawn pos - as methods are stored on the stack I thought this should be fine and after analyzing the performance data I choose to keep this in.

Extra thing I learned:
As I made my own assets, the enemies had to be scaled but from my testing experience, scaling the prefab does not result in the scale applying for the spawned prefab. I instead had to use another method to be able to both randomize a spawn pos and scale the prefab at the same time: LocalTransform.FromPositionRotationScale(pos, rot, scale);

I decided however not to keep this method in the final version as the goal for this assignment was to make an optimized project.
