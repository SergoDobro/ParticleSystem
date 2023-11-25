using Microsoft.Xna.Framework;
using System;

namespace ParticleProgram.ParticleSystem.ParticleEffects
{
    internal class Particle_Directed_Generating : Particle_Directed
    {
        double mainsize = 16;
        public Particle_Directed_Generating(Vector2 postion, Vector2 endpostion, double randomFactor, Random random, ParticleEffect particleEffect) : base(postion, endpostion, randomFactor, random, particleEffect)
        {
            mainsize = 16;
            desiredDuration = 5;
        }
        int created = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float dt = MathHelper.Clamp(-0.00f + 1.00f * (float)(totalDuration / desiredDuration), 0, 0.99f); 
            double dst = easeOutElastic(dt);// 1 - Math.Pow(1 - dt, 6);

            if (dt > 0.5 && created == 0)
            {
                created = 1;
                particleEffect.StartEffect(typeof(ParticleSystem.ParticleEffects.Splash_Particle), duration: 0.2f, particlesCount: 200, 
                    position: startPos + (endPos - startPos) * (float)dst, mainColor: Color.Green, effectType: EffectType.OneWay);
            }


            position = startPos + (endPos - startPos) * (float)dst;

            size = (int)(mainsize * Math.Exp(0.5 + 0.5 * Math.Sin(3 * gameTime.TotalGameTime.TotalSeconds - randomFactor * Math.PI)));
        }
        public double easeOutElastic(double x)
        {
            const double c4 = (2 * Math.PI) / 4.5;
            if (x > 0.5)
                return 1 - Math.Pow(2, -20 * x) * Math.Sin((x * 20 - 0.75) * c4);
            else
                return Math.Pow(2, -20 * x) * Math.Sin((x * 20 - 0.75) * c4) + 1;
        }
    }
}
