using System.Text;
using UnityEngine;

namespace Logics.Artifacts
{
    [CreateAssetMenu(menuName = "Game/Artifacts/DiademOfMadness", fileName = "DiademOfMadnessConfig")]
    public class ArtifactDiademOfMadnessConfig : ArtifactConfig
    {
        [field: Header("Passive bonus")]
        [field: SerializeField] public float PassiveDecreaseHealth { get; private set; }
        [field: SerializeField][field: Tooltip("Percent value")] public float RestoreHealthPerKill { get; private set; }

        [field: Header("Active bonus")]
        [field: SerializeField] public float BonusAttackSpeed { get; private set; }
        [field: SerializeField] public float BonusMoveSpeed { get; private set; }
        [field: SerializeField] public float MultiplierPassiveBonus { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }

        [field: Header("Cooldown")]
        [field: SerializeField] public float CooldownTime { get; private set; }

        private string _passiveBonusTextCached = string.Empty;
        private string _activeBonusTextCached = string.Empty;


        public override string GetPassiveBonusText()
        {
            if(_passiveBonusTextCached == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("  Passively reduces health \n  by " + PassiveDecreaseHealth + " per second.");
                stringBuilder.AppendLine("  When killing an enemy \n  6 health points are restored.");

                _passiveBonusTextCached = stringBuilder.ToString();
            }

            return _passiveBonusTextCached;
        }

        public override string GetActiveBonusText()
        {
            if (_activeBonusTextCached == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("  Attack speed: " + BonusAttackSpeed);
                stringBuilder.AppendLine("  Move speed: " + BonusMoveSpeed);
                stringBuilder.AppendLine("  <color=#B7B7B7>Duration: " + Duration);
                stringBuilder.AppendLine("  Cooldown: " + CooldownTime);

                _activeBonusTextCached = stringBuilder.ToString();
            }

            return _activeBonusTextCached;
        }

        public override Artifact GetArtifactInstance()
        {
            return new ArtifactDiademOfMadness();
        }
        
        public override ArtifactBuilder GetBuilder()
        {
            return new ArtifactDiademOfMadnessBuilder(this);
        }
    }
}