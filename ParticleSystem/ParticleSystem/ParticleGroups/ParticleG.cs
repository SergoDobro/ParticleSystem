using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleSystem.ParticleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem.ParticleGroups
{
    public class ParticleG
    {
        public Vector2 position;
        public double randomFactor;
        public int size = 6;
        protected ParticleGroup particleGroup;
        public ParticleG(Vector2 postion, double randomFactor, Random random, ParticleGroup particleGroup)
        {
            this.randomFactor = randomFactor;
            this.particleGroup = particleGroup;
        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch, Texture2D particleTexture)
        {
            spriteBatch.Draw(particleTexture, new Rectangle((int)position.X - size / 2, (int)position.Y - size / 2, size, size), particleGroup.mainColor);
        }
    }
}
