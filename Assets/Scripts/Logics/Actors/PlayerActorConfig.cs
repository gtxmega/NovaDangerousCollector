using Services.Factory.Builders;
using System.Text;
using UnityEngine;

namespace Logics.Actors
{
    [CreateAssetMenu(menuName = "Game/Actors/PlayerActor", fileName = "PlayerActorConfig")]
    public class PlayerActorConfig : EntityConfig
    {
        [field: Header("Damage")]
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float CriticalChance { get; private set; }
        [field: SerializeField] public float CriticalDamage { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float Vampirism { get; private set; }
        [field: SerializeField] public float AttackInterval { get; private set; }

        [field: Header("Armor")]
        [field: SerializeField] public int Armor { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float PhysicResistance { get; private set; }
        [field: SerializeField][field: Range(0.0f, 1.0f)] public float MagicResistance { get; private set; }

        [field: Header("Movement")]
        [field: SerializeField] public float MoveSpeed { get; private set; }

        private string _attributeTextCached = string.Empty;

        public string GetAttributesText()
        {
            if (_attributeTextCached == string.Empty)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Health: " + MaxHealth);
                sb.AppendLine("Regeneration: " + RegenerationHealth + "/sec.");

                sb.AppendLine(string.Empty);
                sb.AppendLine("Damage: " + Damage);
                sb.AppendLine("Attack interval: " + AttackInterval);
                sb.AppendLine("CriticalChance: " + (CriticalChance * 100.0f) + "%");
                sb.AppendLine("CriticalDamage: " + CriticalDamage + "x");
                sb.AppendLine("Vampirism: " + (Vampirism * 100.0f) + "%");
                
                sb.AppendLine(string.Empty);
                sb.AppendLine("Physic resistance: " + (PhysicResistance * 100.0f) + "%");
                sb.AppendLine("Magic resistance: " + (MagicResistance * 100.0f) + "%");

                sb.AppendLine("Move speed: " + MoveSpeed + " m/s");

                _attributeTextCached = sb.ToString();
            }

            return _attributeTextCached;
        }

        public override EntityBuilder GetBuilder()
        {
            return new PlayerActorBuilder(this);
        }
    }
}