using UnityEngine;

namespace Logics.Views
{
    public class WeaponView : EntityView
    {
        [field: SerializeField] public LineRenderer TargetLine;

        [field: Header("Sockets")]
        [field: SerializeField] public Transform ProjectileSocket { get; private set; }
    }
}