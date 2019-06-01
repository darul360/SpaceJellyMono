using Microsoft.Xna.Framework;
using System;

namespace SpaceJellyMONO.FSM.States
{
    public class Animate : State
    {
        string clipName;
        int tempFrames;
        bool toggleRepeat;

        public Animate(string clipName, int tempFrames, bool toggleRepeat)
        {
            this.clipName = clipName ?? throw new ArgumentNullException(nameof(clipName));
            this.tempFrames = tempFrames;
            this.toggleRepeat = toggleRepeat;
        }

        public override void OnEnter(GameObject gameObject)
        {
            Console.WriteLine("Animation Start");
            gameObject.StartAnimationClip("Take 001", 20, true);
        }

        public override void OnExit(GameObject gameObject)
        {
        }

        public override void OnUpdate(GameTime gameTime, GameObject gameObject)
        {
        }
    }
}
