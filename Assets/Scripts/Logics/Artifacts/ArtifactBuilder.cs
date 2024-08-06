using ECS.Components.Artifacts;
using Game.Types;
using Leopotam.Ecs;

namespace Logics.Artifacts
{
    public class ArtifactBuilder
    {
        private readonly ArtifactConfig _config;

        protected Artifact _artifact;
        protected EcsEntity _entity;
        protected EcsWorld _world;
        protected EcsEntity _owner;

        public ArtifactBuilder(ArtifactConfig config) => _config = config;

        public void SetWorld(EcsWorld world) => _world = world;
        public void SetOwner(in EcsEntity owner) => _owner = owner;

        public virtual void Make()
        {
            _entity = _world.NewEntity();
            _artifact = _config.GetArtifactInstance();

            _artifact.SetDescription(new ArtifactDescription()
            {
                ArtifactName = _config.ArtifactName,
                Description = _config.ArtifactDescription,
                DisplayImage = _config.DisplayImage
            });

            _artifact.SetRare(_config.Rare);

            ref var artifactComponent = ref _entity.Get<ArtifactComponent>();
            artifactComponent.Owner = _owner;
            artifactComponent.Artifact = _artifact;

            _artifact.SetEntity(in _entity);
        }

        public ref EcsEntity GetResult() => ref _entity;
    }
}