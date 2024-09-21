using ECS.Components.Artifacts;
using ECS.Mark;
using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace Logics.Artifacts
{
    public class ArtifactProxyChipBuilder : ArtifactBuilder
    {
        private readonly ArtifactProxyChipConfig _config;

        public ArtifactProxyChipBuilder(ArtifactProxyChipConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            base.Make();

            if (_artifact is ArtifactProxyChip proxyChip)
            {
                ref var proxyChipComponent = ref _entity.Get<ProxyChipComponent>();
            }

            _entity.Get<SpawnMark>();
        }
    }
}