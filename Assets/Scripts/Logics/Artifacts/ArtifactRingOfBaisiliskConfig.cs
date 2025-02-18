using System.Collections;
using System.Text;
using UnityEngine;

namespace Logics.Artifacts
{
    [CreateAssetMenu(menuName = "Game/Artifacts/RingOfBasilisk", fileName = "RingOfBasiliskConfig")]
    public class ArtifactRingOfBaisiliskConfig : ArtifactConfig
    {
        private string _passiveBonusTextCached = string.Empty;
        private string _activeBonusTextCached = string.Empty;


        public override string GetPassiveBonusText()
        {
            if (_passiveBonusTextCached == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                //stringBuilder.AppendLine("  Passively reduces health \n  by " + PassiveDecreaseHealth + " per second.");
                //stringBuilder.AppendLine("  When killing an enemy \n  6 health points are restored.");

                _passiveBonusTextCached = stringBuilder.ToString();
            }

            return _passiveBonusTextCached;
        }

        public override string GetActiveBonusText()
        {
            if (_activeBonusTextCached == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                //stringBuilder.AppendLine("  Attack speed: " + BonusAttackSpeed);
                //stringBuilder.AppendLine("  Move speed: " + BonusMoveSpeed);
                //stringBuilder.AppendLine("  <color=#B7B7B7>Duration: " + Duration);
                //stringBuilder.AppendLine("  Cooldown: " + CooldownTime);

                _activeBonusTextCached = stringBuilder.ToString();
            }

            return _activeBonusTextCached;
        }

        public override Artifact GetArtifactInstance()
        {
            return new ArtifactRingOfBasilisk();
        }

        public override ArtifactBuilder GetBuilder()
        {
            return new ArtifactRingOfBasiliskBuilder(this);
        }
    }
}