using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoWithParticleSystem_test1.MonoWithParticleSystem_test1;
using System;
using System.Collections.Generic;
using System.Threading;
using PartSys = MonoWithParticleSystem_test1.MonoWithParticleSystem_test1.ParticleManager;

namespace MonoWithParticleSystem_test1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
             

        }
        PartSys particleSystem;
        protected override void Initialize()
        {
            particleSystem = new PartSys();
            base.Initialize();


            //this.Window.AllowAltF4 = false;
            //IntPtr hWnd = Window.Handle;
            //System.Windows.Forms.Control ctrl = System.Windows.Forms.Control.FromHandle(hWnd);
            //System.Windows.Forms.Form form = ctrl.FindForm();
            //form.TransparencyKey = System.Drawing.Color.Black;

        }
        ParticleEffect ps;
        ParticleEffect_Directed ps2;
        float t = 0.7f;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GlobalHolder.GraphicsDevice = GraphicsDevice;
            particleSystem.LoadContent();
            
            ps = (new ParticleEffect(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            particleSystem.CreateEffect(ps);

            ps2 = (new ParticleEffect_Directed(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            particleSystem.CreateEffect(ps2);

            ps.StartEffect(typeof(MonoWithParticleSystem_test1.ParticleEffects.Splash_Fire), duration: 0.7f, particlesCount: 30, effectType: EffectType.OneWay,
                mainColor: Color.Red, scale:100);
            //new System.Threading.Thread(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep((int)(t * 1000));
            //        ps.StartEffect(typeof(MonoWithParticleSystem_test1.ParticleEffects.Splash_Particle), duration: t, particlesCount: 200);
            //    }

            //}).Start();
            IsMouseVisible = true;
        }


        protected override void UnloadContent()
        {
        }

        MouseState mouseState, prevmouseState;
        Vector2? positionFrom = new Vector2(0, 0); 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            particleSystem.Update(gameTime);

            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                ps.StartEffect(typeof(MonoWithParticleSystem_test1.ParticleEffects.Splash_Particle), duration: t, particlesCount: 200,
                    position: mouseState.Position.ToVector2(), mainColor: Color.Green);
                if (positionFrom == null)
                {
                    positionFrom = mouseState.Position.ToVector2();
                }
                else
                {
                    ps2.StartEffect(typeof(MonoWithParticleSystem_test1.ParticleEffects.Particle_Directed_Simple), startPosition: positionFrom, endPosition: mouseState.Position.ToVector2());
                    positionFrom = null;
                }
            }
            if (mouseState.RightButton == ButtonState.Pressed && prevmouseState.RightButton == ButtonState.Released)
            {
                ps.StartEffect(typeof(MonoWithParticleSystem_test1.ParticleEffects.Particle_Splash_Out), duration: t, particlesCount: 1000,
                    position: mouseState.Position.ToVector2(), mainColor: Color.LightGreen);
            }
            prevmouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            particleSystem.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
    public static class GlobalHolder
    {
        public static GraphicsDevice GraphicsDevice;
    }
}
