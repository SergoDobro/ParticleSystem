using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWithParticleSystem_test1.MonoWithParticleSystem_test1
{
    public class Particle
    {
        public Vector2 position;
        public double randomFactor;
        public int size = 6;
        protected ParticleEffect particleEffect;
        public Particle(Vector2 postion, double randomFactor, Random random, ParticleEffect particleEffect)
        {
            this.randomFactor = randomFactor;
            this.particleEffect = particleEffect;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D particleTexture)
        {
            spriteBatch.Draw(particleTexture, new Rectangle((int)position.X - size / 2, (int)position.Y - size / 2, size, size), particleEffect.mainColor);
        }
    }
}
