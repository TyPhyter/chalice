using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chalice_Android
{
    public class Card
    {

        static protected Vector2 __default_start_pos__ = new Vector2(-500, -500);

        public Vector2 _Pos;
        public Vector2 _Scale;
        public Vector2 _Vel;
        public Texture2D _Texture;
        public CardType _CardType;
        public CardSubType _CardSubType;

        public Card(Texture2D texture = null, CardType cardType = CardType.Minion, CardSubType cardSubType = CardSubType.None)
        {
            _Texture = texture;
            _CardType = cardType;
            _CardSubType = cardSubType;
            _Pos = __default_start_pos__;
        }
    }

    public enum CardType
    {
        Minion,
        Artifact
    }

    public enum CardSubType
    {
        Enchantment,
        Spell,
        Weapon,
        Armor,
        None
    }
}