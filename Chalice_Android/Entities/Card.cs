using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Chalice_Android.Components;
using Chalice_Android.Utils;
namespace Chalice_Android.Entities
{
    public class Card : IRenderable
    {
        static protected Vector2 __default_start_pos__ = new Vector2(-500, -500);

        public int _id;
        public int _InitialIndex;
        public string Name;
        //public Vector2 _pos;
        public Vector2 Pos;
        //{
        //    get { return _pos; }
        //    set { _pos = value; UpdateCollider(); } // is this the right order to do this in?
        //}
        public Vector2 Scale = new Vector2(0.25f, 0.25f);
        public Vector2 Vel;
        public Vector2 Rotation;
        public Vector3 Rotation3D;
        //private Vector2 _origin;
        public Vector2 Origin { get; set; }
        //{
        //    get { return Pos + new Vector2((Texture.Width * Scale.X) / 2, (Texture.Height * Scale.Y) / 2); }
        //    set { _origin = value; }
        //}
        public int ZIndex { get; set; } = 0;
        public Texture2D Texture;
        public Rectangle Collider = new Rectangle();
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
            Collider.Width = (int)(Texture.Width * Scale.X);
            Collider.Height = (int)(Texture.Height * Scale.Y);
            Origin = new Vector2((Texture.Width) / 2, (Texture.Height) / 2);
        }

        public void Render(SpriteBatch spriteBatch, Vector2 origin)
        {
            spriteBatch.Draw(Texture, Pos, null, Color.White, Rotation3D.Z, Origin, Scale, SpriteEffects.None, 0f);
            //spriteBatch.DrawRectangle(Collider, Color.Azure, 5f);
            //spriteBatch.FillRectangle(new Rectangle((Pos + Origin * Scale - (5 * Vector2.One)).ToPoint(), new Point(10,10)), Color.GreenYellow);
        }

        public void Update(float deltaT)
        {
            Collider.X = (int)(Pos.X - Origin.X * Scale.X);
            Collider.Y = (int)(Pos.Y - Origin.Y * Scale.Y);
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