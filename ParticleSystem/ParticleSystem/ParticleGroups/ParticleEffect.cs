using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem.ParticleGroups
{
    public class ParticleEffect_Groups : ParticleEffect
    {
        List<ParticleGroup> particleGroups = new List<ParticleGroup>();
        public ParticleEffect_Groups(Vector2 position) : base(position)
        {
            this.position = position;
            LoadContent();
            isActive = true;
        }
        public override void Update(GameTime gameTime)
        {
            if (!isActive) return;
            totalDuration += (float)gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
            }
            for (int i = 0; i < particleGroups.Count; i++)
            {
                if (1 == particleGroups[i].Update(gameTime))
                {
                    particleGroups[i].OnRemoved();
                    particleGroups.RemoveAt(i);
                    i--;
                } 
            }
        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spritebatch, particleTexture);
            }
            for (int i = 0; i < particleGroups.Count; i++)
            {
                particleGroups[i].Draw(spritebatch, particleTexture);
            }
            spritebatch.End();
        }
        public void AddParticleGroup(ParticleGroup particleGroup)
        {
            particleGroups.Add(particleGroup);
        }

        public virtual void CreateParticle(Type partType, int particlesToCreateCount = 1, float duration = -1, Vector2? position = null,
            float scale = -1, EffectType? effectType = null, Color? mainColor = null)
        {

            if (position.HasValue)
                this.position = position.Value;
            if (mainColor.HasValue)
                this.mainColor = mainColor.Value;



            for (int i = 0; i < particlesToCreateCount; i++)
            {
                particles.Add((Particle)Activator.CreateInstance(partType, new object[] { this.position, random.NextDouble(), random, this }));
            }
        }
    }
}
