using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleSystem.ParticleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem
{
    public enum EffectType { Singular, Loop, OneWay}
    public class ParticleEffect : DTimeHolder
    {
        public Random random = new Random(100);
        public float totalDuration = 0;
        public float desiredDuration = -1; //-1 = cycled
        public float scale = 100;
        public EffectType effectType = EffectType.Singular;
        public Color mainColor = Color.Red;
        public Vector2 position;
        public int particlesCount = 50;

        public bool isActive = false;

        protected List<Particle> particles = new List<Particle>();
        public Texture2D particleTexture;

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
                            colors[i + ii * 100] = Color.White;
                        }
                        else
                            colors[i + ii * 100] = Color.Transparent;
                    }
                }
                particleTexture.SetData(colors);

            }
        }
        public virtual void Update(GameTime gameTime)
        {
            if (!isActive) return;
            totalDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
            }
            if (totalDuration >= desiredDuration)
            {
                switch (effectType)
                {
                    case EffectType.Singular:
                        isActive = false;
                        break;
                    case EffectType.Loop:
                        totalDuration = 0;
                        break;
                    case EffectType.OneWay:
                        break;
                    default:
                        break;
                } 
            }
        }
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spritebatch, particleTexture);
            }
            spritebatch.End();
        }
        List<Particle> particleToDelete = new List<Particle>();

        public double getDT { get => totalDuration/desiredDuration; set => totalDuration = (float)value *desiredDuration; }

        public virtual void StartEffect(Type partType, int particlesCount = -1, float duration = -1, bool load = true, Vector2? position = null,
            float scale = -1, EffectType? effectType = null, Color? mainColor = null)
        {
            if (load)
                LoadContent();
            if (duration < 0)
                SetDuration(-1);
            else
                SetDuration(duration);

            if (position.HasValue)
                this.position = position.Value;
            if (effectType.HasValue)
                this.effectType = effectType.Value; 
            if (mainColor.HasValue)
                this.mainColor = mainColor.Value;

            if (scale != -1) 
                this.scale = scale;
            if (particlesCount != -1)
                this.particlesCount = particlesCount;



            isActive = true;
            totalDuration = 0;

            particles.Clear();
            particles = new List<Particle>();
            for (int i = 0; i < particlesCount; i++)
            {
                //particles.Add(new TestParticle(position, random.NextDouble(), this));
                particles.Add((Particle)Activator.CreateInstance(partType, new object[] { this.position, random.NextDouble(), random, this }));
                if (effectType.HasValue)
                {
                    if (this.effectType == EffectType.OneWay)
                    {
                        particleToDelete.Add(particles.Last());
                    }
                }
            }
        } 
    }
}
