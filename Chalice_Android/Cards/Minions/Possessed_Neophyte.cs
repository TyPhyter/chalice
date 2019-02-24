using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Chalice_Android.Entities;
using Chalice_Android.Components;

namespace Chalice_Android.Cards
{
    class Possessed_Neophyte : Minion
    {
        public Possessed_Neophyte(ContentManager cm) : base(cm.Load<Texture2D>("Possessed_Neophyte"), new AttackValues(1, 1, 1, 1), 10)
        {
            Name = "Possessed_Neophyte";
        }
    }
}