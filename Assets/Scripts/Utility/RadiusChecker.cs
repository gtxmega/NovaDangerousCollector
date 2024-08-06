using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Utility
{
    public class RadiusChecker : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private DecalProjector _decalProjector;
        [SerializeField] private LayerMask _captureLayers;
        [SerializeField] private Color _sphereColor;

        private void Update()
        {
            _decalProjector.size = new Vector3(_radius * 2, _radius * 2, _radius * 2);
            Collider[] hit = Physics.OverlapSphere(transform.position, _radius, _captureLayers);
            Debug.Log("overlap counts: " + hit.Length);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = _sphereColor;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}