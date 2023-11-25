using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem.ParticleEffects
{
    public class Splash_Fire : Particle
    {
        ParticleEffect particleEffect;
        Vector2 startPos;
        double startRandomFactor;
        double secstartRandomFactor;
        protected float mainSize;
        protected float particleSize = 1;
        public Splash_Fire(Vector2 pos, double randomFactor, Random random, ParticleEffect particleEffect) : base(pos, randomFactor, random, particleEffect)
        {
            this.particleEffect = particleEffect;
            startPos = pos;
            position = startPos;
            startRandomFactor = randomFactor;
            secstartRandomFactor = random.NextDouble();

            mainSize = (int)((0.5+random.NextDouble()+0.5) * 10);
        }
        public override void Update(GameTime gameTime)
        {
            float dt = MathHelper.Clamp((float)particleEffect.totalDuration / particleEffect.desiredDuration, 0, 1);

            double dst = 1 + 0.5 - 0.5 * Math.Pow(1 - dt, 6);
            size = (int)(5 * mainSize + mainSize * 6 * (Math.Pow(1 - dt, 3)));


            double rndM = 2 * Math.PI;
            randomFactor *= rndM;
            float dist = (float)(1 * dst - mainSize  * 0.008);

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
        public override void Draw(SpriteBatch spriteBatch, Texture2D particleTexture)
        {
            //spriteBatch.Draw(particleTexture, new Rectangle((int)position.X - size / 2, (int)position.Y - size / 2, size, size), particleEffect.mainColor);
            //double c = 0;
            float fluctspeed = 4;
            for (int i = 0; i < 8; i++)
            {
                //float devaluer = (1f / (1 + 0.3f * i));
                //spriteBatch.Draw(particleTexture, new Rectangle(
                //    (int)(position.X - devaluer*size / 2 + c),
                //    (int)(position.Y - devaluer * size / 2 - i * 10),
                //    (int)(devaluer * size),
                //    (int)(devaluer * size)
                //    ), particleEffect.mainColor);
                //c+= devaluer  * i * 10 * Math.Sin(Math.PI*i/3 +2 * Math.PI * particleEffect.totalDuration);

                double devaluer = Math.Clamp(0.6 - (i / 7f) * (i / 7f),0,1)
                    *
                    Math.Exp(Math.Sin((secstartRandomFactor * 2 * Math.PI + fluctspeed * particleEffect.totalDuration - i * Math.PI / 4))) / (2 * Math.E + i);
                spriteBatch.Draw(particleTexture, new Rectangle(
                    (int)(position.X - devaluer * size / 2 + 0),
                    (int)(position.Y - devaluer * size / 2 - i * 10* particleSize),
                    (int)(devaluer * size),
                    (int)(devaluer * size)
                    ), new Color(particleEffect.mainColor, 0.1f)); 
            }
            for (int i = 0; i < 8; i++)
            {
                double devaluer = Math.Clamp(0.6 - (i / 7f) * (i / 7f), 0, 1)
                    *
                    Math.Exp(Math.Sin((secstartRandomFactor * 2*Math.PI+ fluctspeed * particleEffect.totalDuration - i * Math.PI / 4))) / (2*Math.E+i);
                devaluer *= 0.75;
                float colav = (particleEffect.mainColor.R + particleEffect.mainColor.G + particleEffect.mainColor.B) / 3;
                spriteBatch.Draw(particleTexture, new Rectangle(
                    (int)(position.X - devaluer * size / 2 + 0),
                    (int)(position.Y - devaluer * size / 2 - i * 10 * particleSize),
                    (int)(devaluer * size),
                    (int)(devaluer * size)
                    ), new Color(Color.Yellow, 0.1f));
            }
        }
    }
}
