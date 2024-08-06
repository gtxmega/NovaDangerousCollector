using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public interface IArtifact
    {
        bool IsActive { get; }
        void ApplyPassiveBonusTo(in EcsEntity targetEntity);
        void ApplyActiveBonusTo(in EcsEntity targetEntity);
        void Deactivate(in EcsEntity targetEntity);
    }
}