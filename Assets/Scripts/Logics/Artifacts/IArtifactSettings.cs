using Game.Types;
using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public interface IArtifactSettings
    {
        void SetRare(EArtifactRare rare);
        void SetEntity(in EcsEntity entity);
    }
}