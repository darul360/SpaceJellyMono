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
    class BasicAnimation
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

        public BasicAnimation(Matrix initialPosition, Matrix scaleMatrix, Matrix rotationMatrix, Matrix translationMatrix)
        {
            position = initialPosition;

            scaleTransform = scaleMatrix;
            rotationTransform = rotationMatrix;
            translationTransform = translationMatrix;

            keyFrameTransform = Matrix.Lerp(Matrix.Identity, translationTransform * rotationTransform * scaleTransform, (float)1 / numberOfKeyFrames);
        }
        public void Update(TimeSpan time)
        {

            if (currentKeyFrame != numberOfKeyFrames)
            {
                if (time >= timeBetweenFrames)
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
        private void InvertTransforms()
        {
            keyFrameTransform = Matrix.Invert(keyFrameTransform);
        }
        
    }
}
