#region Using Statements
using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

using Nez;
using Nez.Sprites;

#endregion

namespace Chalice_Shared
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Core
    {
        Entity entityOne;

        public GameMain() : base(width: 450, height: 800, isFullScreen: false, enableEntitySystems: true)
        {
            Nez.Debug.log("Desktop Constructed");
            Core.defaultSamplerState = SamplerState.AnisotropicWrap;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.AllowUserResizing = false;

            // create our Scene with the DefaultRenderer and a clear color of CornflowerBlue
            var myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);

            var texture = myScene.content.Load<Texture2D>("boar_blue");

            // setup our Scene by adding some Entities
            entityOne = myScene.createEntity("entity-one");
            entityOne.addComponent(new Sprite(texture));
            entityOne.scale = new Vector2(0.5f, 0.5f);
            entityOne.position = new Vector2(225, 400);
            entityOne.rotation = 0;

            // set the scene so Nez can take over
            scene = myScene;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //entityOne.rotation += 0.01f;
        }
    }
}
