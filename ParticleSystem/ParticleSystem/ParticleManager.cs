using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleProgram.ParticleSystem
{
    internal class ParticleManager
    {
        List<ParticleEffect> particleEffects = new List<ParticleEffect>();
        public void LoadContent()
        {

        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < particleEffects.Count; i++)
            {
                particleEffects[i].Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            for (int i = 0; i < particleEffects.Count; i++)
            {
                particleEffects[i].Draw(spritebatch);
            }
        }
        public void CreateEffect(ParticleEffect particleEffect)
        {
            particleEffects.Add(particleEffect);
        }
    }


}
