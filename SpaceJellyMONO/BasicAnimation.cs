using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    class BasicAnimation : GameComponent
    {
        public Matrix position;

        Matrix scaleTransform;
        Matrix rotationTransform;
        Matrix translationTransform;
        Matrix keyFrameTransform;

        bool toggleRepeat = true;
        bool togglePlayBack = true;
        int numberOfKeyFrames = 20;
        int currentKeyFrame = 1;
        TimeSpan timeBetweenFrames = TimeSpan.FromMilliseconds(5);

        public BasicAnimation(Game game, Matrix initialPosition, Matrix scaleMatrix, Matrix rotationMatrix, Matrix translationMatrix) : base(game)
        {
            position = initialPosition;

            scaleTransform = scaleMatrix;
            rotationTransform = rotationMatrix;
            translationTransform = translationMatrix;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentKeyFrame != numberOfKeyFrames)
            {
                if (gameTime.ElapsedGameTime >= timeBetweenFrames)
                {
                    position = keyFrameTransform * position;
                    currentKeyFrame++;
                }
            }
            else if (togglePlayBack && toggleRepeat)
            {
                InvertTransforms();
                currentKeyFrame = 1;
            }
            else if(togglePlayBack)
            {
                InvertTransforms();
                togglePlayBack = false;
                currentKeyFrame = 1;
            }
            else if(toggleRepeat)
            {
                currentKeyFrame = 1;
            }

        }
        public override void Initialize()
        {
            base.Initialize();

            keyFrameTransform = Matrix.Lerp(Matrix.Identity, translationTransform * rotationTransform * scaleTransform, (float)1 / numberOfKeyFrames);

        }
        private void InvertTransforms()
        {
            keyFrameTransform = Matrix.Invert(keyFrameTransform);
        }
        
    }
}
