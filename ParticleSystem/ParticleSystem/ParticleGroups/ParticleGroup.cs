using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleProgram.ParticleSystem;
using ParticleProgram.ParticleSystem.ParticleGroups;
using ParticleSystem.ParticleSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem.ParticleGroups
{
    public enum ParticleGroupTypes { OneTimeForAll, PeronalTime }
    public class ParticleGroup : DTimeHolder
    {
        protected List<ParticleG> particles = new List<ParticleG>();
        protected List<ParticleGroup> particleGroups = new List<ParticleGroup>();
        protected ParticleGroup parent = null;

        public Color mainColor = Color.Red;
        public double groupLifeDuration;
        public double groupLifeDesiredDuration = 3;
        public double scale = 1;

        public bool selfDestructionOnEnd = false;
        public bool particleDestructionOnEnd = true;
        public double getDT { get => groupLifeDuration / groupLifeDesiredDuration; set => groupLifeDuration = value * groupLifeDesiredDuration; }

        public ParticleGroup(object[] Parametrs)
        {
            Instantiate(Parametrs);
        }
        public ParticleGroup(ParticleGroup parent, object[] Parametrs)
        {
            this.parent = parent;
            this.parent.AddParticleGroup(this);
            Instantiate(Parametrs);
        }
        public virtual int Update(GameTime gameTime)
        {
            groupLifeDuration += gameTime.ElapsedGameTime.TotalSeconds;
            for (int i = 0; i < particleGroups.Count; i++)
            {
                if (1 == particleGroups[i].Update(gameTime))
                {
                    particleGroups[i].OnRemoved();
                    particleGroups.RemoveAt(i);
                    i--;
                }   
            }
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
                if (particleDestructionOnEnd)
                {
                    if ((particles[i] as DTimeHolder).getDT>1)
                    { 
                        particles.RemoveAt(i);
                        i--;
                    }
                } 
            }
            if (selfDestructionOnEnd)
            {
                if (groupLifeDuration > groupLifeDesiredDuration)
                {
                    return 1;
                }
            }
            return 0;
        }
        public void AddParticle(ParticleG particle)
        {
            particles.Add(particle);
        }
        public void AddParticleGroup(ParticleGroup particleGroup)
        {
            particleGroups.Add(particleGroup);
        }
        public virtual void Instantiate(object[] Parametrs)
        {

        }
 
        public void Draw(SpriteBatch spritebatch, Texture2D particleTexture)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spritebatch, particleTexture);
            }
            for (int i = 0; i < particleGroups.Count; i++)
            {
                particleGroups[i].Draw(spritebatch, particleTexture);
            }
        }
        public void OnRemoved()
        {
            for (int i = 0; i < particleGroups.Count; i++)
            {
                particleGroups.RemoveAt(0);
                i--;
            }
            for (int i = 0; i < particles.Count; i++)
            {
                particles.RemoveAt(0);
                i--;
            }
        }
    }
}
