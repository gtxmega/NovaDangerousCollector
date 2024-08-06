using ECS.Components.Artifacts;
using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public class ArtifactEmperorEyeBuilder : ArtifactBuilder
    {
        private readonly ArtifactEmperorEyeConfig _config;

        public ArtifactEmperorEyeBuilder(ArtifactEmperorEyeConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            base.Make();

            if(_artifact is ArtifactEmperorEye emperorEye)
            {
                ref var emperorEyeComponent = ref _entity.Get<EmperorEyeComponent>();
                emperorEyeComponent.EnemyDamageReduce = _config.EnemyDamageReduce;
                emperorEyeComponent.EnemyMoveSpeedReduce = _config.EnemyMoveSpeedReduce;
                emperorEyeComponent.EnemyAllResistanceReduce = _config.EnemyAllResistanceReduce;
            }

        }
    }
}