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
        public Vector2 Rotation;
        public Vector3 Rotation3D;
        private Vector2 _origin;
        public Vector2 Origin
        {
            get { return Pos + new Vector2(Texture.Width * Scale.X / 2, Texture.Height * Scale.Y/ 2); }
            set { _origin = value; }
        }
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

        public void Render(SpriteBatch spriteBatch, Vector2 origin)
        {
            //spriteBatch.Draw(Texture, Pos, null, Color.White, Rotation3D.Z, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(Texture, Pos, null, Color.White, Rotation3D.Z, Origin, Scale, SpriteEffects.None, 0f);

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