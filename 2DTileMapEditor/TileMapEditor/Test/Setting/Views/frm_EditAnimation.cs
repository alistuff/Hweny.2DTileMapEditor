using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TileMapEditor.Test.Sprite;
using System.Text.RegularExpressions;

namespace TileMapEditor.Test.Setting
{
    public partial class frm_EditAnimation : Form
    {
        private Animation animation;
        private SpriteSheetSplitOptions spliteOption;
        private AnimationAttribute attr;
        private Timer animationUpdate;
        private Bitmap bufferCanvas;
        public event EventHandler<AnimationEditEventArgs> EditConfirm;

        public frm_EditAnimation()
        {
            InitializeComponent();
        }

        public void Initialize(AnimationAttribute[] allAttrs, AnimationAttribute attr, Image spriteSheet,
            SpriteSheetSplitOptions spliteOption)
        {
            this.attr = attr;
            this.spliteOption = spliteOption;
            this.picSpriteSheet.Image = spriteSheet;
            this.bufferCanvas = new Bitmap(this.picAnimation.Width, this.picAnimation.Height);

            txtFps.Minimum = 0;
            txtFps.Maximum = 1000;
            txtOffsetX.Minimum = txtOffsetY.Minimum = 0;
            txtOffsetX.Maximum = spriteSheet.Width - 1;
            txtOffsetY.Maximum = spriteSheet.Height - 1;
            txtFrames.Minimum = 1;
            txtFrames.Maximum = 1000;
            txtWidth.Minimum = txtHeight.Minimum = 0;
            txtWidth.Maximum = spriteSheet.Width;
            txtHeight.Maximum = spriteSheet.Height;

            Text = attr.Name + " " + this.Text;
            UpdateView(attr);
            animation = new Animation(attr.Name);
            MakeAnimation();

            cbbAnimations.Items.Clear();
            for (int i = 0; i < allAttrs.Length; i++)
                cbbAnimations.Items.Add(allAttrs[i]);
            cbbAnimations.Text = attr.Name;

            animationUpdate = new Timer();
            animationUpdate.Interval = 1000 / 60;
            animationUpdate.Tick += new EventHandler(animationUpdate_Tick);
            Application.Idle += new EventHandler(Application_Idle);
            picSpriteSheet.Paint += new PaintEventHandler(picSpriteSheet_Paint);
        }

        private void UpdateView(AnimationAttribute attr)
        {
            txtFps.Value = (decimal)attr.Fps;
            txtOffsetX.Value = Math.Min(attr.OffsetX, txtOffsetX.Maximum);
            txtOffsetY.Value = Math.Min(attr.OffsetY, txtOffsetY.Maximum);
            txtFrames.Value = Math.Max(attr.Frames, txtFrames.Minimum);
            txtWidth.Value = Math.Min(attr.FrameWidth, txtWidth.Maximum);
            txtHeight.Value = Math.Min(attr.FrameHeight, txtHeight.Maximum);
            ckbLoop.Checked = attr.Loop;
        }

        private void UpdateModel()
        {
            attr.Fps = (float)txtFps.Value;
            attr.Loop = ckbLoop.Checked;
            attr.OffsetX = (int)txtOffsetX.Value;
            attr.OffsetY = (int)txtOffsetY.Value;
            attr.Frames = (int)txtFrames.Value;
            attr.FrameWidth = (int)txtWidth.Value;
            attr.FrameHeight = (int)txtHeight.Value;
        }

        private void MakeAnimation()
        {
            UpdateModel();

            animation.Fps = attr.Fps;
            animation.IsLooping = attr.Loop;
            animation.Clear();
            animation.AddKeyFrames(
                attr.OffsetX,
                attr.OffsetY,
                attr.Frames,
                attr.FrameWidth,
                attr.FrameHeight,
                spliteOption
            );

            picSpriteSheet.Invalidate();
        }

        private void PlayAnimation(float dt)
        {
            try
            {
                animation.Update(Environment.TickCount, dt);
                if (animation.IsCompleted)
                    animationUpdate.Enabled = false;

                AnimationKeyFrame frame = animation.CurrentFrame;
                if (frame == null)
                    return;

                Rectangle dest = new Rectangle(0, 0, bufferCanvas.Width, bufferCanvas.Height);
                using (Graphics g = Graphics.FromImage(bufferCanvas))
                {
                    using (Brush brush = new TextureBrush(Properties.Resources.transparent))
                    {
                        g.FillRectangle(brush, dest);
                        g.DrawImage(
                            picSpriteSheet.Image,
                            dest,
                            frame.Source,
                            GraphicsUnit.Pixel
                        );
                    }
                }
                picAnimation.Image = bufferCanvas;
            }
            catch { }
        }

        private void DrawFramesGrid(Graphics g)
        {
            using (Brush brush = new TextureBrush(Properties.Resources.transparent))
            {
                Rectangle dest = new Rectangle(0, 0, picSpriteSheet.Width, picSpriteSheet.Height);
                g.FillRectangle(brush, dest);
                g.DrawImage(picSpriteSheet.Image, dest);
                foreach (var frame in animation.Frames)
                {
                    g.DrawRectangle(Pens.Red, frame.Source);
                }
            }
        }

        private void Cleanup()
        {
            animationUpdate.Enabled = false;
            animationUpdate.Dispose();
            animationUpdate = null;
            bufferCanvas.Dispose();
            bufferCanvas = null;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var handler = EditConfirm;
            if (handler != null)
            {
                UpdateModel();
                handler(this, new AnimationEditEventArgs(attr));
            }
            Cleanup();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cleanup();
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!animationUpdate.Enabled)
            {
                MakeAnimation();
            }
            animationUpdate.Enabled = !animationUpdate.Enabled;
        }

        private void animationUpdate_Tick(object sender, EventArgs e)
        {
            PlayAnimation(animationUpdate.Interval / 1000f);
        }

        private void picSpriteSheet_Paint(object sender, PaintEventArgs e)
        {
            DrawFramesGrid(e.Graphics);
        }

        private void cbbAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAnimations.SelectedIndex == -1)
                return;

            UpdateView((AnimationAttribute)cbbAnimations.SelectedItem);
            MakeAnimation();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (animationUpdate != null && animationUpdate.Enabled)
                btnPlay.Text = "暂停";
            else
                btnPlay.Text = "播放";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MakeAnimation();
        }
    }

    public class AnimationEditEventArgs : EventArgs
    {
        public AnimationAttribute Attribute
        {
            get;
            private set;
        }
        public AnimationEditEventArgs(AnimationAttribute attr)
        {
            this.Attribute = attr;
        }
    }
}
