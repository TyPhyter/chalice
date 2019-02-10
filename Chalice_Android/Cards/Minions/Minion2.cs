using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Chalice_Android.Entities;
using Chalice_Android.Components;

namespace Chalice_Android.Cards
{
    class Minion2 : Minion
    {
        public Minion2(ContentManager cm) : base(/*cm.Load<Texture2D>("boar_purple")*/null, new AttackValues(1, 2, 5, 2), 10)
        {
            Name = "Minion2";
        }
    }
}