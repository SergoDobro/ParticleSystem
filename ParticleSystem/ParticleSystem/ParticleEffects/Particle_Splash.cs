using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem.ParticleEffects
{
    public class Splash_Particle : Particle
    {
        protected ParticleEffect particleEffect;
        protected Vector2 startPos;
        protected double startRandomFactor;
        protected float mainSize;
        public Splash_Particle(Vector2 pos, double randomFactor, Random random, ParticleEffect particleEffect) : base(pos, randomFactor, random, particleEffect)
        {
            this.particleEffect = particleEffect;
            startPos = pos;
            position = startPos;
            startRandomFactor = randomFactor;

            mainSize = (int)(random.NextDouble() * size);
        }
        public override void Update(GameTime gameTime)
        {

            float dt = MathHelper.Clamp(0.1f + 0.9f * (float)particleEffect.totalDuration / particleEffect.desiredDuration, 0, 0.99f);
            double dst = easeFunction(dt); //easeOutElastic(dt);

            double rndM = 2 * Math.PI;
            randomFactor *= rndM;


            size = (int)(mainSize * 6 * (Math.Pow(1 - dt, 5) - 0.0001f));
            float dist = (float)(1 * dst - size * 0.008);

            position.X = startPos.X + particleEffect.scale * (float)(
                dist *
                Math.Sin(randomFactor)
                );
            position.Y = startPos.Y + particleEffect.scale * (float)(
                dist *
                Math.Cos(randomFactor)
                );
            randomFactor /= rndM;

        }
        public virtual double easeFunction(double dt)
        {
            return 1 - Math.Pow(1 - dt, 6);
        } 
    }
    public class Splash_Particle_Elastic : Splash_Particle
    {
        public Splash_Particle_Elastic(Vector2 pos, double randomFactor, Random random, ParticleEffect particleEffect) : base(pos, randomFactor, random, particleEffect)
        {
            size *= 2;
        }

        public override double easeFunction(double x)
        {
            const double c4 = (2 * Math.PI) / 4.5;
            if (x > 0.5)
                return 1 - Math.Pow(2, -20 * x) * Math.Sin((x * 20 - 0.75) * c4);
            else
                return Math.Pow(2, -20 * x) * Math.Sin((x * 20 - 0.75) * c4) + 1;
        }
    }
}
