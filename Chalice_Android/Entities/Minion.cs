using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Chalice_Android.Components;

namespace Chalice_Android.Entities
{
    class Minion : Card, IMinion
    {
        public int _Health { get; set; }
        public AttackValues _AtkVals { get; set; }

        public Minion (Texture2D texture, AttackValues atkVals, int health) : base(texture, CardType.Minion)
        {
            _AtkVals = atkVals;
            _Health = health;
        }
    }
}