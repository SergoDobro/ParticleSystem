using Microsoft.Xna.Framework;
using System;

namespace ParticleProgram.ParticleSystem.ParticleGroups
{
    internal class ParticleGroup_Circle : ParticleGroup
    {
        public Vector2 startPosition;
        Random random = new Random();
        double addFrequency = -1;
        double del = 0;
        public ParticleGroup_Circle(object[] Parametrs) : base(Parametrs) { }

        public override int Update(GameTime gameTime)
        {
            if (addFrequency != -1)
            {
                del += gameTime.ElapsedGameTime.TotalSeconds;
                if (del > addFrequency)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        AddParticle(new ParticleG_Splash(startPosition, random.NextDouble(), random, this));
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

            base.Instantiate(Parametrs);
            for (int i = 0; i < 8; i++)
            {
                AddParticle(new ParticleG_Splash(startPosition, random.NextDouble(), random, this));
            }
        }
    }
}
