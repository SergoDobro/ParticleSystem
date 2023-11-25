using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem.ParticleEffects
{
    public class TestParticle : Particle
    {
        ParticleEffect particleEffect;
        public TestParticle(Vector2 pos, double randomFactor, Random random, ParticleEffect particleEffect) : base(pos, randomFactor, random, particleEffect)
        {
            this.particleEffect = particleEffect;
            startPos = pos;
            position = startPos;
            startRandomFactor = randomFactor;
            randSpeed = Math.Sin(1000 * startRandomFactor);
        }
        Vector2 startPos;
        double startRandomFactor;
        double randSpeed;
        public override void Update(GameTime gameTime)
        {
            float dt = MathHelper.Clamp((float)particleEffect.totalDuration / particleEffect.desiredDuration, 0, 1);
            double dst = 1 - Math.Pow(1 - dt, 4);

            double rndM = 4;
            randomFactor *= rndM;
            float dist = (float)(particleEffect.scale * dst);

            position.X = startPos.X + (float)(
                dist *
                Math.Sign(Math.Sin(randomFactor)) * (Math.Sin(randomFactor) * Math.Sin(randomFactor))
                );
            position.Y = startPos.Y + (float)(
                dist *
                Math.Sign(Math.Cos(randomFactor)) * Math.Cos(randomFactor) * Math.Cos(randomFactor)
                );
            randomFactor /= rndM;
            float dm = particleEffect.desiredDuration/4;
            if (particleEffect.totalDuration > dm)
            {
                randomFactor += 0.03 * Math.Abs((Math.Exp(0.5 * Math.Sin(gameTime.TotalGameTime.TotalSeconds - dm+0.1)) - 0.25));
            }
        }
    }
}
