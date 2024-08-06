using ECS.Components;
using ECS.Components.Artifacts;
using ECS.Components.Attributes;
using Game.Types;
using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public class ArtifactDiademOfMadness : Artifact
    {
        public float CooldownTime { get; private set; }

        private float _passiveDecreaseHealth;
        private float _healthPerKill;

        private float _activeMoveSpeed;
        private float _activeAttackSpeed;
        private float _multiplierPassiveBonus;
        private float _duration;

        public void SetPassiveDecreaseHealth(float amount) => _passiveDecreaseHealth = amount;
        public void SetHealthPerKill(float amount) => _healthPerKill = amount;
        public void SetActiveMoveSpeed(float amount) => _activeMoveSpeed = amount;
        public void SetActiveAttackSpeed(float amount) => _activeAttackSpeed = amount;
        public void SetMultiplierPassiveBonus(float amount) => _multiplierPassiveBonus = amount;
        public void SetDuration(float duration) => _duration = duration;
        public void SetCooldownTime(float time) => CooldownTime = time;

        public override void Deactivate(in EcsEntity targetEntity)
        {
            ref var playerComponent = ref targetEntity.Get<PlayerComponent>();

            playerComponent.View.PlayerMovement.DecreaseMoveSpeed(_activeMoveSpeed);

            ref var diademOfMadness = ref _entity.Get<DiademOfMadnessComponent>();
            diademOfMadness.Multiplier = 0.0f;

            ref var generalAttributes = ref targetEntity.Get<GeneralAttributes>();
            generalAttributes.ReloadsSpeed -= _activeAttackSpeed;

            base.Deactivate(in targetEntity);
        }

        public override void ApplyPassiveBonusTo(in EcsEntity targetEntity)
        {
            ref var diademOfMandessComponent = ref _entity.Get<DiademOfMadnessComponent>();

            diademOfMandessComponent.Multiplier = 0.0f;
            diademOfMandessComponent.DecreaseHealth = _passiveDecreaseHealth;
            diademOfMandessComponent.HealthPerKill = _healthPerKill;
        }

        public override void ApplyActiveBonusTo(in EcsEntity targetEntity)
        {
            ref var playerView = ref targetEntity.Get<PlayerComponent>();
            playerView.View.PlayerMovement.IncreaseMoveSpeed(_activeMoveSpeed);

            ref var generalAttributes = ref targetEntity.Get<GeneralAttributes>();
            generalAttributes.ReloadsSpeed += _activeAttackSpeed;

            ref var diademOfMandessComponent = ref _entity.Get<DiademOfMadnessComponent>();
            diademOfMandessComponent.Duration = _duration;
            diademOfMandessComponent.Multiplier = _multiplierPassiveBonus;

            ref var artifactCooldownComponent = ref _entity.Get<ArtifactCooldownComponent>();
            artifactCooldownComponent.CooldownTimer = artifactCooldownComponent.CooldownTime;
            artifactCooldownComponent.IsReady = false;

            base.ApplyActiveBonusTo(in targetEntity);
        }
    }
}