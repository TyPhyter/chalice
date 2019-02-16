using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Chalice_Android.Components
{
    public interface IRenderable
    {
        bool isActive { get; set; }
        void Render(SpriteBatch spriteBatch);
    }
}