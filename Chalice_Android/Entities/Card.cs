using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Chalice_Android.Components;

namespace Chalice_Android.Entities
{
    public class Card : IRenderable
    {
        static protected Vector2 __default_start_pos__ = new Vector2(-500, -500);

        public int _id;
        public int _InitialIndex;
        public string Name;
        public Vector2 Pos;
        public Vector2 Scale;
        public Vector2 Vel;
        public int ZIndex { get; set; } = 0;
        public Texture2D Texture;
        public CardType _CardType;
        public CardSubType _CardSubType;
        public bool wasPlayed;
        public bool isActive { get; set; }

        public Card(Texture2D texture = null, CardType cardType = CardType.Minion, CardSubType cardSubType = CardSubType.None)
        {
            Texture = texture;
            _CardType = cardType;
            _CardSubType = cardSubType;
            Pos = __default_start_pos__;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Pos, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
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