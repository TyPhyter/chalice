using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Chalice_Android.Entities;
using Chalice_Android.Components;

namespace Chalice_Android.Cards
{
    class Boar : Minion
    {
        public Boar(ContentManager cm) : base(cm.Load<Texture2D>("boar_purple"), new AttackValues(1,2,5,2), 10)
        {
            Name = "Boar";
        }
    }
}