using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace ParticleSystem
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        ParticleSystem particleSystem;
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            particleSystem = new ParticleSystem();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GlobalHolder.GraphicsDevice = GraphicsDevice;
            particleSystem.LoadContent();
            particleSystem.CreateEffect(new ParticleEffect(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2)));

        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            particleSystem.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            particleSystem.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
    public static class GlobalHolder
    {
        public static GraphicsDevice GraphicsDevice;
    }
    class ParticleSystem
    {
        List<ParticleEffect> particleEffects = new List<ParticleEffect>();
        public void LoadContent()
        {

        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < particleEffects.Count; i++)
            {
                particleEffects[i].Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < particleEffects.Count; i++)
            {
                particleEffects[i].Draw(spritebatch);
            }
        }
        public void CreateEffect(ParticleEffect particleEffect)
        {
            particleEffects.Add(particleEffect);
            particleEffect.StartEffect();
        }
    }
    public class ParticleEffect
    {
        public float totalDuration = 0;
        public float desiredDuration = 0;
        Vector2 position;
        int particlesCount = 0;
        List<Particle> particles = new List<Particle>();
        Texture2D particleTexture;
        bool isActive = false;
        public ParticleEffect(Vector2 position)
        {
            this.position = position;
        }
        public void SetDuration(float desiredDuration)
        {
            this.desiredDuration = desiredDuration;
        }
        public void LoadContent()
        {
            if (particleTexture is null)
            {
                particleTexture = new Texture2D(GlobalHolder.GraphicsDevice, 100, 100);
                Color[] colors = new Color[100 * 100];
                for (int i = 0; i < 100; i++)
                {
                    for (int ii = 0; ii < 100; ii++)
                    {
                        if (Math.Pow((i - (100 - 1) / 2f), 2) + Math.Pow((ii - (100 - 1) / 2f), 2) <= 2500)
                        {
                            colors[i + ii * 100] = Color.Red;
                        }
                        else
                            colors[i + ii * 100] = Color.Transparent;
                    }
                }
                particleTexture.SetData(colors);

            }
        }
        public void Update(GameTime gameTime)
        {
            if (!isActive) return;
            totalDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spritebatch, particleTexture);
            }
            spritebatch.End();
        }
        public void StartEffect()
        {
            isActive = true;
            Random random = new Random();
            LoadContent();
            totalDuration = 0;
            SetDuration(2);
            particlesCount = 50;

            particles = new List<Particle>();
            for (int i = 0; i < particlesCount; i++)
            {
                particles.Add(new TestParticle(position, 1 * random.NextDouble(), this));
            }
        }
    }

    public class Particle
    {
        public Vector2 position;
        public double randomFactor;
        public Particle(Vector2 postion, double randomFactor)
        {
            this.randomFactor = randomFactor;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D particleTexture)
        {
            int size = 6;
            spriteBatch.Draw(particleTexture, new Rectangle((int)position.X - size / 2, (int)position.Y - size / 2, size, size), Color.Red);
        }
    }
    public class TestParticle : Particle
    {
        ParticleEffect particleEffect;
        public TestParticle(Vector2 pos, double randomFactor, ParticleEffect particleEffect) : base(pos, randomFactor)
        {
            this.particleEffect = particleEffect;
            startPos = pos;
            startRandomFactor = randomFactor;
            randSpeed = Math.Sin(1000 * startRandomFactor);
        }
        Vector2 startPos;
        double startRandomFactor;
        double randSpeed;
        public override void Update(GameTime gameTime)
        {
            //position.X = startPos.X + 20 * (float)(Math.Cos((startRandomFactor)) * MathHelper.Clamp(particleEffect.totalDuration, 0, 8) * Math.Cos(randomFactor * 2 * Math.PI));
            //position.Y = startPos.Y + 20 * (float)(Math.Sin((startRandomFactor)) * MathHelper.Clamp(particleEffect.totalDuration, 0, 8) * Math.Sin(randomFactor * 2 * Math.PI));
            float dt = MathHelper.Clamp((float)particleEffect.totalDuration / particleEffect.desiredDuration, 0, 1);
            double dst = 1 - Math.Pow(1 - dt, 4);
            //              dt < 0.5 ? (1 - Math.Sqrt(1 - Math.Pow(2 * dt, 2))) / 2
            //: (Math.Sqrt(1 - Math.Pow(-2 * dt + 2, 2)) + 1) / 2;
            double rndM = 4;
            randomFactor *= rndM;
            float dist = (float)(100 * dst);// * 8 * MathHelper.Clamp((float)(Math.Pow(particleEffect.totalDuration / particleEffect.desiredDuration, 3)), 0, 1);
            position.X = startPos.X + (float)(
                dist *
                Math.Sign(Math.Sin(randomFactor)) * (Math.Sin(randomFactor) * Math.Sin(randomFactor))
                );
            position.Y = startPos.Y + (float)(
                dist *
                Math.Sign(Math.Cos(randomFactor)) * Math.Cos(randomFactor) * Math.Cos(randomFactor)
                );
            randomFactor /= rndM;
            float dm = 1;
            if (particleEffect.totalDuration > dm)
            {
                randomFactor += 0.03 * Math.Abs((Math.Exp(0.5 * Math.Sin(gameTime.TotalGameTime.TotalSeconds - dm)) - 0.25));
            }
        }
    }


}
