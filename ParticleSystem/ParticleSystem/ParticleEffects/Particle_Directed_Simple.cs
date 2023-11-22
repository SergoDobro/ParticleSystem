using Microsoft.Xna.Framework;
using MonoWithParticleSystem_test1.MonoWithParticleSystem_test1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWithParticleSystem_test1.MonoWithParticleSystem_test1.ParticleEffects
{
    internal class Particle_Directed_Simple : Particle_Directed
    {
        double mainsize = 16;
        public Particle_Directed_Simple(Vector2 postion, Vector2 endpostion, double randomFactor, Random random, ParticleEffect particleEffect) : base(postion, endpostion, randomFactor, random, particleEffect)
        {
            mainsize = 16;
            desiredDuration = 5;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            float dt = MathHelper.Clamp(-0.00f + 1.00f * (float)(totalDuration / desiredDuration), 0, 0.99f);
            double dst = 1 - Math.Pow(1 - dt, 6);

            position = startPos + (endPos - startPos)*(float)dst;

            size = (int)(mainsize * Math.Exp(0.5+0.5*Math.Sin(3*gameTime.TotalGameTime.TotalSeconds - randomFactor*Math.PI)));
        }
    }
}
