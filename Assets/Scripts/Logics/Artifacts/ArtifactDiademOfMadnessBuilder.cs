using ECS.Components.Artifacts;
using ECS.Mark;
using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public class ArtifactDiademOfMadnessBuilder : ArtifactBuilder
    {
        private readonly ArtifactDiademOfMadnessConfig _config;

        public ArtifactDiademOfMadnessBuilder(ArtifactDiademOfMadnessConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            base.Make();

            if(_artifact is ArtifactDiademOfMadness diademOfMadness)
            {
                diademOfMadness.SetPassiveDecreaseHealth(_config.PassiveDecreaseHealth);
                diademOfMadness.SetHealthPerKill(_config.RestoreHealthPerKill);
                diademOfMadness.SetActiveAttackSpeed(_config.BonusAttackSpeed);
                diademOfMadness.SetActiveMoveSpeed(_config.BonusMoveSpeed);
                diademOfMadness.SetDuration(_config.Duration);
                diademOfMadness.SetMultiplierPassiveBonus(_config.MultiplierPassiveBonus);
                diademOfMadness.SetCooldownTime(_config.CooldownTime);

                ref var artifactCooldownComponent = ref _entity.Get<ArtifactCooldownComponent>();
                artifactCooldownComponent.IsReady = true;
                artifactCooldownComponent.CooldownTime = _config.CooldownTime;
            }

            _entity.Get<SpawnMark>();
        }
    }
}