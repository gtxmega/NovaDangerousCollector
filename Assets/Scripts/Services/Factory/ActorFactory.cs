using Leopotam.Ecs;
using Logics.Artifacts;
using Logics.Views;
using Services.Events;
using Services.Factory.Builders;
using Services.locator;
using UnityEngine;

namespace Services.Factory
{
    public class ActorFactory : IActorFactory
    {
        private readonly EcsWorld _world;
        private readonly IServicesLocator _locator;
        private readonly ILevelEventsExec _levelEventsExec;

        public ActorFactory(EcsWorld world, IServicesLocator locator)
        {
            _world = world;
            _locator = locator;
        }

        public ref EcsEntity CreateEntity(EntityConfig config, Vector3 location = default)
        {
            EntityBuilder builder = config.GetBuilder();

            builder.SetWorld(_world);
            builder.SetLocation(location);
            builder.Make();

            ActorView entityView = builder.GetView();

            InjectServices(entityView.gameObject);

            return ref builder.GetResult();
        }

        public ref EcsEntity CreateWeaponEntity(WeaponConfig config, in EcsEntity owner, Transform socket)
        {
            WeaponBuilder builder = config.GetBuilder();

            builder.SetWorld(_world);
            builder.SetOwner(in owner);
            builder.SetWeaponSocket(socket);
            builder.Make();

            return ref builder.GetResult();
        }

        public ref EcsEntity CreateProjectileEntity(ProjectileConfig config, in EcsEntity owner, Transform socket)
        {
            ProjectileBuilder builder = config.GetBuilder();
            builder.SetWorld(_world);
            builder.SetOwner(owner);
            builder.SetLocation(socket.position);
            builder.SetRotation(socket.rotation);
            builder.Make();

            return ref builder.GetResult();
        }

        public ref EcsEntity CreateArtifactEntity(ArtifactConfig config, in EcsEntity owner)
        {
            ArtifactBuilder builder = config.GetBuilder();
            builder.SetWorld(_world);
            builder.SetOwner(owner);
            builder.Make();

            return ref builder.GetResult();
        }

        private void InjectServices(GameObject gameObject)
        {
            foreach (var item in gameObject.GetComponentsInChildren<InjectDependency>())
            {
                item.Inject(_locator);
            }
        }
    }
}