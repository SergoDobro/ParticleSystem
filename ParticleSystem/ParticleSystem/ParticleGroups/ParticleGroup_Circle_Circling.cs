using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Reflection.Metadata;

namespace ParticleProgram.ParticleSystem.ParticleGroups
{
    internal class ParticleGroup_Circle_Circling : ParticleGroup
    {
        public Vector2 startPosition;
        Random random = new Random();
        double addFrequency = -1;
        double del = 0;
        public double circleScale = 100;
        public double circlingSpeed = 1;
        public ParticleGroup_Circle_Circling(object[] Parametrs) : base(Parametrs) { }

        public override int Update(GameTime gameTime)
        {
            if (addFrequency != -1)
            {
                del += gameTime.ElapsedGameTime.TotalSeconds;
                if (del > addFrequency)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        AddParticle(new ParticleG_Splash(GetPosOnCircleFromDt(), random.NextDouble(), random, this) { mainSize = 2 });
                    }
                    del = 0;
                }
            }
            return base.Update(gameTime);
        }
        public override void Instantiate(object[] Parametrs)
        {
            startPosition = (Vector2)Parametrs[0];
            scale = (int)Parametrs[1];
            selfDestructionOnEnd = (bool)Parametrs[2];
            addFrequency = (double)Parametrs[3];
            groupLifeDesiredDuration = (double)Convert.ToDouble(Parametrs[4]);
            circlingSpeed = (double)Parametrs[5];
            circleScale = (double)Convert.ToDouble(Parametrs[6]);

            base.Instantiate(Parametrs);
        }
        public Vector2 GetPosOnCircleFromDt()
        {
            return startPosition + (float)circleScale * new Vector2((float)Math.Cos(groupLifeDuration / (circlingSpeed) * Math.Tau), (float)Math.Sin(groupLifeDuration / (circlingSpeed) * Math.Tau));
        }
    }
    internal class ParticleGroup_Circle_Circling_Double : ParticleGroup
    {
        public Vector2 startPosition;
        Random random = new Random();
        double addFrequency = -1;
        double del = 0;
        public double circleScale = 100;
        public double circlingSpeed = 1;
        public ParticleGroup_Circle_Circling_Double(object[] Parametrs) : base(Parametrs) { }
         
        public override void Instantiate(object[] Parametrs)
        {
            startPosition = (Vector2)Parametrs[0];
            scale = (int)Parametrs[1];
            selfDestructionOnEnd = (bool)Parametrs[2];
            addFrequency = (double)Parametrs[3];
            groupLifeDesiredDuration = (double)Convert.ToDouble(Parametrs[4]);
            circlingSpeed = (double)Parametrs[5];
            circleScale = (double)Convert.ToDouble(Parametrs[6]);

            AddParticleGroup(new ParticleGroup_Circle_Circling(Parametrs));

            Parametrs[6] = (double)Convert.ToDouble(Parametrs[6]) + 30;
            AddParticleGroup(new ParticleGroup_Circle_Circling(Parametrs));

            particleGroups.Last().getDT = 0.5;
            base.Instantiate(Parametrs); 
        }
        public Vector2 GetPosOnCircleFromDt()
        {
            return startPosition + (float)circleScale * new Vector2((float)Math.Cos(groupLifeDuration / (circlingSpeed) * Math.Tau), (float)Math.Sin(groupLifeDuration / (circlingSpeed) * Math.Tau));
        }
    }
}
