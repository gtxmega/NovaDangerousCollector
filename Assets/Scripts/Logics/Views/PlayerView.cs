using Logics.Movements;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Logics.Views
{
    public class PlayerView : ActorView
    {
        [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }

        [SerializeField] private DecalProjector _radiusDecal;

        public void SetRadiusDecal(float radiusDecal)
        {
            _radiusDecal.size = new Vector3(radiusDecal * 2, radiusDecal * 2, 5.0f);
        }
    }
}