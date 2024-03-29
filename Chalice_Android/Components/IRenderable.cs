﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chalice_Android.Components
{
    public interface IRenderable
    {
        bool isActive { get; set; }
        int ZIndex { get; set; }
        Vector2 Origin { get; set; }
        void Render(SpriteBatch spriteBatch, Vector2 origin);
    }
}