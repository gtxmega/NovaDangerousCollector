using Game.Types;
using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public class Artifact : IArtifact, IArtifactSettings, IArtifactDisplaySettings
    {
        public bool IsActive { get; private set; }
        public EArtifactRare ArtifactRare { get; private set; }
        public ArtifactDescription Description { get; private set; }
        public EArtifactSlotUI UISlotType { get; private set; }
        public ref EcsEntity GetEntity() => ref _entity;
        

        protected EcsEntity _entity;


        public void SetEntity(in EcsEntity entity) => _entity = entity;
        public void SetRare(EArtifactRare rare) => ArtifactRare = rare;
        public void SetDescription(ArtifactDescription description) => Description = description;
        public void SetUISlot(EArtifactSlotUI uiSlot) => UISlotType = uiSlot;


        public virtual void ApplyActiveBonusTo(in EcsEntity targetEntity)
        {
            IsActive = true;
        }

        public virtual void ApplyPassiveBonusTo(in EcsEntity targetEntity)
        {
            
        }

        public virtual void Deactivate(in EcsEntity targetEntity)
        {
            IsActive = false;
        }
    }
}