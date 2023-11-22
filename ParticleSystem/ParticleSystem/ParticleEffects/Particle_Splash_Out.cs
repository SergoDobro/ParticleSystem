using Microsoft.Xna.Framework;
using MonoWithParticleSystem_test1.MonoWithParticleSystem_test1;
using MonoWithParticleSystem_test1.MonoWithParticleSystem_test1.ParticleEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace MonoWithParticleSystem_test1.MonoWithParticleSystem_test1.ParticleEffects
{
    public class Particle_Splash_Out : Splash_Particle
    {
        public Particle_Splash_Out(Vector2 pos, double randomFactor, Random random, ParticleEffect particleEffect) : base(pos, randomFactor, random, particleEffect)
        {
            double rndM = 2 * Math.PI;
            this.randomFactor *= rndM;
        }
        public override void Update(GameTime gameTime)
        {

            double dt = MathHelper.Clamp((float)particleEffect.totalDuration / particleEffect.desiredDuration, 0, 0.99f);
            double dst = 1 - Math.Pow(1 - dt, 6);

            //size = (int)Math.Ceiling(mainSize * 6 * (-Math.Pow(1 - dt, 5)));
            size = (int)Math.Ceiling(mainSize * 6 * (Math.Pow(1 - dt, 5) - 0.6f)); 
            float dist = (float)(1 * dst - size * 0.08);

            position.X = startPos.X + particleEffect.scale * (float)(
                dist *
                Math.Sin(randomFactor)
                );
            position.Y = startPos.Y + particleEffect.scale * (float)(
                dist *
                Math.Cos(randomFactor)
                );
        }
    }
    public class Particle_Splash_In : Splash_Particle
    {
        public Particle_Splash_In(Vector2 pos, double randomFactor, Random random, ParticleEffect particleEffect) : base(pos, randomFactor, random, particleEffect)
        {
            double rndM = 2 * Math.PI;
            this.randomFactor *= rndM;
        }
        public override void Update(GameTime gameTime)
        {

            double dt = MathHelper.Clamp((float)particleEffect.totalDuration / particleEffect.desiredDuration, 0, 0.99f);
            double dst = 1 - Math.Pow(1 - dt, 6);

            float dist = (float)(1 * dst - size * 0.08);

            position.X = startPos.X + particleEffect.scale * (float)(
                dist *
                Math.Sin(randomFactor)
                );
            position.Y = startPos.Y + particleEffect.scale * (float)(
                dist *
                Math.Cos(randomFactor)
                );
            size = (int)Math.Ceiling(mainSize * 1 * (Math.Pow(1 - dt, 5) - 1f));
        }
    }
}
