using Game.Types;
using UnityEngine;

namespace Logics.Artifacts
{
    public class ArtifactConfig : ScriptableObject
    {
        [field: SerializeField] public string ArtifactName;
        [field: SerializeField][field: TextArea(15, 35)] public string ArtifactDescription { get; private set; }
        [field: SerializeField] public string PlanetName { get; private set; }
        [field: SerializeField] public Sprite DisplayImage { get; private set; }
        [field: SerializeField] public EArtifactRare Rare { get; private set; }

        public virtual string GetPassiveBonusText() => string.Empty;
        public virtual string GetActiveBonusText() => string.Empty;
        public virtual Artifact GetArtifactInstance() => new Artifact();
        public virtual ArtifactBuilder GetBuilder()
        {
            return new ArtifactBuilder(this);
        }
    }
}