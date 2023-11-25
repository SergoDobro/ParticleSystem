using Microsoft.Xna.Framework;
using ParticleProgram.ParticleSystem.ParticleGroups;
using ParticleSystem.ParticleSystem;
using System;

namespace ParticleProgram.ParticleSystem.ParticleGroups
{
    public class ParticleG_Splash : ParticleG, DTimeHolder
    {
        public Vector2 startPos;
        protected double startRandomFactor;
        public float mainSize;
        protected double lifeSpanDUration = 0;

        public double getDT { get => lifeSpanDUration / particleGroup.groupLifeDesiredDuration; set => lifeSpanDUration = particleGroup.groupLifeDesiredDuration*value; }

        public ParticleG_Splash(Vector2 pos, double randomFactor, Random random, ParticleGroup particleGroup) : base(pos, randomFactor, random, particleGroup)
        {
            startPos = pos;
            position = startPos;
            startRandomFactor = randomFactor;

            mainSize = (int)(random.NextDouble() * size);
        }
        public override void Update(GameTime gameTime)
        {
            lifeSpanDUration += gameTime.ElapsedGameTime.TotalSeconds;
            float dt = MathHelper.Clamp(0.1f + 0.9f * (float)(lifeSpanDUration / particleGroup.groupLifeDesiredDuration), 0, 0.99f);
            double dst = easeFunction(dt); //easeOutElastic(dt);

            double rndM = 2 * Math.PI;
            randomFactor *= rndM;


            size = (int)(mainSize * 3 * (Math.Pow(1 - dt, 5) - 0.0001f));
            float dist = (float)(1 * dst - size * 0.008);

            position.X = startPos.X + (float)particleGroup.scale * (float)(
                dist *
                Math.Sin(randomFactor)
                );
            position.Y = startPos.Y + (float)particleGroup.scale * (float)(
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
}
