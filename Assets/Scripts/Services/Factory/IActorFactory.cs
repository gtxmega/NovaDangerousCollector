﻿using Leopotam.Ecs;
using Logics.Artifacts;
using Services.Factory.Builders;
using UnityEngine;

namespace Services.Factory
{
    public interface IActorFactory
    {
        ref EcsEntity CreateArtifactEntity(ArtifactConfig config, in EcsEntity owner);
        ref EcsEntity CreateEntity(EntityConfig config, Vector3 location = default);
        ref EcsEntity CreateProjectileEntity(ProjectileConfig config, in EcsEntity owner, Transform socket);
        ref EcsEntity CreateWeaponEntity(WeaponConfig config, in EcsEntity owner, Transform socket);
    }
}