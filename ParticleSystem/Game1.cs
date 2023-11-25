using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleProgram.ParticleSystem;
using ParticleProgram.ParticleSystem.ParticleGroups;
using System;
using System.Collections.Generic;
using System.Threading; 

namespace ParticleProgram
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
        ParticleProgram.ParticleSystem.ParticleManager particleManager;
        protected override void Initialize()
        {
            particleManager = new ParticleProgram.ParticleSystem.ParticleManager();
            base.Initialize();


            //this.Window.AllowAltF4 = false;
            //IntPtr hWnd = Window.Handle;
            //System.Windows.Forms.Control ctrl = System.Windows.Forms.Control.FromHandle(hWnd);
            //System.Windows.Forms.Form form = ctrl.FindForm();
            //form.TransparencyKey = System.Drawing.Color.Black;

        }
        ParticleEffect ps;
        ParticleEffect_Groups ps2;
        float t = 0.7f;
        Random random = new Random();
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GlobalHolder.GraphicsDevice = GraphicsDevice;
            particleManager.LoadContent();
            
            ps = (new ParticleEffect(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            particleManager.CreateEffect(ps);

            ps2 = (new ParticleEffect_Groups(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));
            particleManager.CreateEffect(ps2);

            ps.StartEffect(typeof(ParticleSystem.ParticleEffects.Splash_Fire), duration: 0.7f, particlesCount: 30, effectType: EffectType.OneWay,
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
        Vector2? positionFrom = null; 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            particleManager.Update(gameTime);

            if (mouseState.LeftButton == ButtonState.Pressed && prevmouseState.LeftButton == ButtonState.Released)
            {
                //if (random.NextDouble()>0.5)
                //{
                //    ps.StartEffect(typeof(ParticleSystem.ParticleEffects.Splash_Particle), duration: t, particlesCount: 200,
                //        position: mouseState.Position.ToVector2(), mainColor: Color.Green);
                //}
                //else
                //{ 
                //    ps.StartEffect(typeof(ParticleSystem.ParticleEffects.Splash_Particle_Elastic), duration: t, particlesCount: 200,
                //        position: mouseState.Position.ToVector2(), mainColor: Color.Green);
                //}
                ps2.AddParticleGroup(new ParticleGroup_Circle_Circling_Double(new object[] { mouseState.Position.ToVector2(), 15, false, 0.001, 3, 0.3, 60 }) );
                if (positionFrom == null)
                {
                    positionFrom = mouseState.Position.ToVector2();
                }
                else
                {
                   // ps2.AddParticleGroup(new ParticleGroup_Circle_Circling_Double(new object[]{ positionFrom.Value, 10, false, 0.01 }) { groupLifeDesiredDuration = 3, circlingSpeed = 0.5, circleScale = 30});
                    //ps2.StartEffect(typeof(ParticleSystem.ParticleEffects.Particle_Directed_Generating), startPosition: positionFrom, endPosition: mouseState.Position.ToVector2());
                    positionFrom = null;
                }
            }
            if (mouseState.RightButton == ButtonState.Pressed && prevmouseState.RightButton == ButtonState.Released)
            {
                ps.StartEffect(typeof(ParticleSystem.ParticleEffects.Particle_Splash_Out), duration: t, particlesCount: 1000,
                    position: mouseState.Position.ToVector2(), mainColor: Color.LightGreen);
            }
            prevmouseState = mouseState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            particleManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
    public static class GlobalHolder
    {
        public static GraphicsDevice GraphicsDevice;
    }
}
