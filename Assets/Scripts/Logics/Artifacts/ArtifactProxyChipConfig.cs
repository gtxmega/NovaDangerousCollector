using System.Collections;
using System.Text;
using UnityEngine;

namespace Logics.Artifacts
{
    [CreateAssetMenu(menuName = "Game/Artifacts/TheProxyChip", fileName = "ProxyChipConfig")]
    public class ArtifactProxyChipConfig : ArtifactConfig
    {
        private string _passiveBonusTextCached = string.Empty;
        private string _activeBonusTextCached = string.Empty;

        public override string GetPassiveBonusText()
        {
            if (_passiveBonusTextCached == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("  ");
                stringBuilder.AppendLine("  ");

                _passiveBonusTextCached = stringBuilder.ToString();
            }

            return _passiveBonusTextCached;
        }

        public override string GetActiveBonusText()
        {
            if (_activeBonusTextCached == string.Empty)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("  Attack speed: ");
                stringBuilder.AppendLine("  Move speed: ");
                stringBuilder.AppendLine("  <color=#B7B7B7>Duration: ");
                stringBuilder.AppendLine("  Cooldown: ");

                _activeBonusTextCached = stringBuilder.ToString();
            }

            return _activeBonusTextCached;
        }

        public override Artifact GetArtifactInstance()
        {
            return new ArtifactProxyChip();
        }

        public override ArtifactBuilder GetBuilder()
        {
            return new ArtifactProxyChipBuilder(this);
        }
    }
}