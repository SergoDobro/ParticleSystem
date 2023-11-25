using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem
{ 
    public enum ParticleAdditionType { AddEach, ReAddAll }
    public class ParticleEffect_Directed : ParticleEffect
    {
        ParticleAdditionType particleAdditionType = ParticleAdditionType.AddEach;
        public ParticleEffect_Directed(Vector2 position) : base(position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (!isActive) return;
            totalDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
            }
        }
        public void StartEffect(Type partType, int particlesCount = -1, float duration = -1, bool load = true, Vector2? startPosition = null,
            float scale = -1, EffectType? effectType = null, Color? mainColor = null, Vector2? endPosition = null)
        {
            if (load)
                LoadContent();
            if (duration < 0)
                SetDuration(-1);
            else
                SetDuration(duration);

            if (startPosition.HasValue)
                this.position = startPosition.Value;
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
             
            switch (particleAdditionType)
            {
                case ParticleAdditionType.AddEach:
                    particles.Add((Particle_Directed)Activator.CreateInstance(partType, new object[] { startPosition, endPosition, random.NextDouble(), random, this }));
                    break;
                case ParticleAdditionType.ReAddAll:
                    particles.Clear();
                    particles = new List<Particle>();
                    for (int i = 0; i < particlesCount; i++)
                    {
                        //particles.Add(new TestParticle(position, random.NextDouble(), this));
                        particles.Add((Particle)Activator.CreateInstance(partType, new object[] { this.position, random.NextDouble(), random, this }));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
