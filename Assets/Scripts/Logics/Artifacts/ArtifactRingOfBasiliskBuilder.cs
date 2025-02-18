using ECS.Mark;
using Leopotam.Ecs;
using System.Collections;
using UnityEngine;

namespace Logics.Artifacts
{
    public class ArtifactRingOfBasiliskBuilder : ArtifactBuilder
    {
        private readonly ArtifactRingOfBaisiliskConfig _config;

        public ArtifactRingOfBasiliskBuilder(ArtifactRingOfBaisiliskConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            base.Make();

            if(_artifact is ArtifactRingOfBasilisk ringOfBasilisk)
            {

            }

            _entity.Get<SpawnMark>();
        }
    }
}