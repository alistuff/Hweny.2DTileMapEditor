using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapEditor.Test.Sprite
{
    public class Animator
    {
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;

        public Animation CurrentAnimation
        {
            get
            {
                return currentAnimation;
            }
        }

        public Animator()
        {
            this.animations = new Dictionary<string, Animation>();
            this.currentAnimation = null;
        }

        public void AddAnimation(Animation animation)
        {
            if (animations.ContainsKey(animation.Name))
            {
                throw new ArgumentException("error,repeat key!");
            }
            animations.Add(animation.Name, animation);
        }

        public void Play(string name)
        {
            if (!animations.ContainsKey(name)) return;
            if (currentAnimation != null)
            {
                if (currentAnimation.Name == name) return;
            }
            currentAnimation = animations[name];
            currentAnimation.Reset();
        }

        public void Update(float gameTime, float elapsedSeconds)
        {
            if (currentAnimation != null)
            {
                currentAnimation.Update(gameTime, elapsedSeconds);
                if (currentAnimation.IsCompleted)
                {
                    if (!string.IsNullOrEmpty(currentAnimation.TransitionKey))
                    {
                        Play(currentAnimation.TransitionKey);
                    }
                }
            }
        }
    }
}
