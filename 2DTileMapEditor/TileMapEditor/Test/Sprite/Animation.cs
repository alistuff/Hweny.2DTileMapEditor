using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapEditor.Test.Sprite
{
    public class Animation
    {
        private IList<AnimationKeyFrame> keyFrames;
        private int currentFrame;
        private float frameTime;
        private float accumulatedFrameTime;
        private bool isCompleted;
        private float fps;

        public string Name
        { 
            get; 
            set; 
        }
        public bool IsLooping
        {
            get; 
            set; 
        }
        public float Fps
        {
            get
            {
                return fps;
            }
            set
            {
                fps = value;
                if (fps <= 0)
                    fps = 1f;
                frameTime = 1.0f / fps;
            }
        }
        public string TransitionKey
        { 
            get; 
            set;
        }
        public int TotalFrames
        { 
            get 
            { 
                return keyFrames.Count;
            }
        }
        public bool IsCompleted 
        { 
            get 
            { 
                return isCompleted;
            } 
        }
        public AnimationKeyFrame CurrentFrame
        {
            get
            {
                if (keyFrames.Count == 0) return null;
                return keyFrames[currentFrame];
            }
        }
        public AnimationKeyFrame this[int index]
        {
            get
            {
                if (index < 0 || index > keyFrames.Count - 1)
                    throw new ArgumentOutOfRangeException("index");
                return keyFrames[index];
            }
        }
        public IList<AnimationKeyFrame> Frames
        {
            get
            {
                return keyFrames;
            }
        }

        public Animation()
        {
            Initialize("Undefined", 1f, true, "");
        }

        public Animation(string name, float fps = 1f, bool loop = true, string transitionKey = "")
        {
            Initialize(name, fps, loop, transitionKey);
        }

        private void Initialize(string name, float fps, bool loop, string transitionKey)
        {
            keyFrames = new List<AnimationKeyFrame>();
            currentFrame = 0;
            isCompleted = false;
            Name = name.Trim();
            IsLooping = loop;
            Fps = fps;
            TransitionKey = transitionKey;
            accumulatedFrameTime = 0f;
        }

        public void Update(float gameTime, float elapsedSeconds)
        {
            if (TotalFrames == 0)
                return;

            accumulatedFrameTime += elapsedSeconds;
            if (accumulatedFrameTime > frameTime)
            {
                accumulatedFrameTime -= frameTime;
                if (IsLooping)
                {
                    currentFrame = (currentFrame + 1) % TotalFrames;
                    OnAnimationChanged(new AnimationEventArgs(CurrentFrame));
                    isCompleted = false;
                }
                else
                {
                    if (!isCompleted)
                    {
                        currentFrame = Math.Min(currentFrame + 1, TotalFrames - 1);
                        if (currentFrame == TotalFrames - 1)
                        {
                            OnAnimationCompleted(new AnimationEventArgs(CurrentFrame));
                            isCompleted = true;
                        }
                        else
                        {
                            OnAnimationChanged(new AnimationEventArgs(CurrentFrame));
                        }
                    }
                }
            }
        }

        public void AddKeyFrame(int x, int y, int width, int height)
        {
            AnimationKeyFrame frame = new AnimationKeyFrame(x, y, width, height);
            keyFrames.Add(frame);
        }

        public void AddKeyFrame(AnimationKeyFrame frame)
        {
            keyFrames.Add(frame);
        }

        public void AddKeyFrames(int offsetX, int offsetY, int frames, int width, int height, 
            SpriteSheetSplitOptions option = SpriteSheetSplitOptions.FromRow)
        {
            if (option == SpriteSheetSplitOptions.FromRow)
            {
                for (int i = 0; i < frames; i++)
                {
                    AddKeyFrame(offsetX + width * i, offsetY, width, height);
                }
            }
            else
            {
                for (int i = 0; i < frames; i++)
                {
                    AddKeyFrame(offsetX, offsetY + height * i, width, height);
                }
            }
        }

        public void Clear()
        {
            keyFrames.Clear();
            Reset();
        }

        public void Reset()
        {
            currentFrame = 0;
            accumulatedFrameTime = 0f;
            isCompleted = false;
        }

        private void OnAnimationChanged(AnimationEventArgs e)
        {
            EventHandler<AnimationEventArgs> temp = AnimationChanged;
            if (temp != null) temp(this, e);
        }
        private void OnAnimationCompleted(AnimationEventArgs e)
        {
            EventHandler<AnimationEventArgs> temp = AnimationCompleted;
            if (temp != null) temp(this, e);
        }
        public event EventHandler<AnimationEventArgs> AnimationChanged;
        public event EventHandler<AnimationEventArgs> AnimationCompleted;
    }
}
