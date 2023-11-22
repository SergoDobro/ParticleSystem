using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWithParticleSystem_test1.MonoWithParticleSystem_test1
{
    public class Particle_Directed : Particle
    {  
        protected double totalDuration;
        protected double desiredDuration;

        protected Vector2 startPos;
        protected Vector2 endPos;
        public Particle_Directed(Vector2 postion, Vector2 endpostion, double randomFactor, Random random, ParticleEffect particleEffect) : base(postion, randomFactor, random, particleEffect)
        {
            this.randomFactor = randomFactor;
            this.particleEffect = particleEffect;
            startPos = postion;
            endPos = endpostion;
        }
        public override void Update(GameTime gameTime)
        {
            totalDuration += gameTime.ElapsedGameTime.TotalSeconds;
        } 
    }
}
