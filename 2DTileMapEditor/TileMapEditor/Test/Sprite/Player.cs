using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using TileMapEditor.Test.Input;
using TileMapLib;
using TileMapEditor.Test.Main;
using TileMapEditor.Test.Setting;

namespace TileMapEditor.Test.Sprite
{
    public class Player : IKeyListener,IMouseListener
    {
        public const string ANI_LT = "WalkLeft";
        public const string ANI_RT = "WalkRight";
        public const string ANI_UP = "WalkUp";
        public const string ANI_DN = "WalkDown";

        public float X { get; set; }
        public float Y { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public PointF CenterPosition
        {
            get
            {
                return new PointF(X + (width >> 1), Y + (height >> 1));
            }
            set
            {
                X = value.X - (width >> 1);
                Y = value.Y - (height >> 1);
            }
        }
        public bool ShowBoundingBox
        {
            get;
            set;
        }
        public Rectangle CollisionBox
        {
            get
            {
                Rectangle temp = collisionBounds;
                temp.X = (int)X + temp.X;
                temp.Y = (int)Y + temp.Y;
                return temp;
            }
        }

        //map
        private Map tileMap;
        private Camera camera;

        //states
        private bool left;
        private bool right;
        private bool up;
        private bool down;
        private bool moving;

        private bool topLeft;
        private bool bottomLeft;
        private bool topRight;
        private bool bottomRight;

        //animation
        private Animator animator;
        private Image spriteSheet;
        private int width;
        private int height;
        private float accelerated = float.MaxValue;
        private float maxSpeed = 150;
        private Rectangle collisionBounds;

        public Player(Point centerPos)
        {
            this.CenterPosition = centerPos;
        }

        public void Initialize(Map map,Camera camera,AssetsLoader assetsLoader, PlayerSetting setting)
        {
            this.tileMap = map;
            this.camera = camera;

            spriteSheet = assetsLoader.GetImage(MyAssetsLoader.IM_PLAYER);

            animator = new Animator();
            Animation ani_dn, ani_lt, ani_rt, ani_up;

            if (setting != null)
            {
                width = setting.Width;
                height = setting.Height;
                accelerated = setting.Accelerated;
                maxSpeed = setting.MaxVelocity;
                ani_dn = MakeAnimation(setting.WalkDownAttr, setting.SpliteOption);
                ani_lt = MakeAnimation(setting.WalkLeftAttr, setting.SpliteOption);
                ani_rt = MakeAnimation(setting.WalkRightAttr, setting.SpliteOption);
                ani_up = MakeAnimation(setting.WalkUpAttr, setting.SpliteOption);

                collisionBounds = setting.CollisionBounds;
            }
            else
            {
                width = 34;
                height = 48;
                ani_dn = new Animation(ANI_DN, 5f, true);
                ani_lt = new Animation(ANI_LT, 5f, true);
                ani_rt = new Animation(ANI_RT, 5f, true);
                ani_up = new Animation(ANI_UP, 5f, true);
                ani_dn.AddKeyFrames(0, 48 * 0, 4, width, height);
                ani_lt.AddKeyFrames(0, 48 * 1, 4, width, height);
                ani_rt.AddKeyFrames(0, 48 * 2, 4, width, height);
                ani_up.AddKeyFrames(0, 48 * 3, 4, width, height);

                collisionBounds = new Rectangle(0, height / 2, width, height / 2);
                collisionBounds.Inflate(-6, -1);
            }

            animator.AddAnimation(ani_dn);
            animator.AddAnimation(ani_lt);
            animator.AddAnimation(ani_rt);
            animator.AddAnimation(ani_up);

            animator.Play(ANI_DN);
        }

        public void Update(float gameTime, float dt)
        {
            SetVelocity(accelerated * dt, accelerated * dt);
            Move(dt);
            CheckTileMapCollision();
            UpdateAnimation(gameTime, dt);
        }

        private void SetVelocity(float ax, float ay)
        {
            moving = true;
            if (left)
            {
                VelocityX -= ax;
                if (VelocityX < -maxSpeed)
                    VelocityX = -maxSpeed;
            }
            else if (right)
            {
                VelocityX += ax;
                if (VelocityX > maxSpeed)
                    VelocityX = maxSpeed;
            }
            else if (up)
            {
                VelocityY -= ay;
                if (VelocityY < -maxSpeed)
                    VelocityY = -maxSpeed;
            }
            else if (down)
            {
                VelocityY += ax;
                if (VelocityY > maxSpeed)
                    VelocityY = maxSpeed;
            }
            else
            {
                moving = false;
            }

            if ((!left && !right) || VelocityY != 0)
                VelocityX = 0f;
            if ((!up && !down) || VelocityX != 0)
                VelocityY = 0f;
        }
        private void Move(float dt)
        {
            X += VelocityX * dt;
            Y += VelocityY * dt;
        }
        private void Collide()
        {
            int leftTile = CollisionBox.X / tileMap.TileWidth;
            int rightTile = (CollisionBox.X + CollisionBox.Width) / tileMap.TileWidth;
            int topTile = CollisionBox.Y / tileMap.TileHeight;
            int bottomTile = (CollisionBox.Y + CollisionBox.Height) / tileMap.TileHeight;

            if ((leftTile < 0 || rightTile > tileMap.Width - 1) ||
               (topTile < 0 || bottomTile > tileMap.Height - 1))
            {
                topLeft = bottomLeft = topRight = bottomRight = false;
                return;
            }

            topLeft = tileMap.BlockLayer[leftTile, topTile];
            bottomLeft = tileMap.BlockLayer[leftTile, bottomTile];
            topRight = tileMap.BlockLayer[rightTile, topTile];
            bottomRight = tileMap.BlockLayer[rightTile, bottomTile];
        }
        private void CheckTileMapCollision()
        {
            if (CollisionBox.IsEmpty)
                return;

            int tempX = 0;
            int tempY = 0;

            Collide();
            if (VelocityX < 0)
            {
                if (topLeft || bottomLeft)
                {
                    VelocityX = 0;
                    tempX = CollisionBox.X / tileMap.TileWidth * tileMap.TileWidth + tileMap.TileWidth;
                    X = tempX - (CollisionBox.X - X);
                }
            }
            else if (VelocityX > 0)
            {
                if (topRight || bottomRight)
                {
                    VelocityX = 0;
                    tempX = (CollisionBox.X + CollisionBox.Width) / tileMap.TileWidth * tileMap.TileWidth - CollisionBox.Width;
                    X = tempX - (CollisionBox.X - X) - 1;
                }
            }
            else if (VelocityY < 0)
            {
                if (topLeft || topRight)
                {
                    VelocityY = 0;
                    tempY = CollisionBox.Y / tileMap.TileHeight * tileMap.TileHeight + tileMap.TileHeight;
                    Y = tempY - (CollisionBox.Y - Y);
                }
            }
            else if (VelocityY > 0)
            {
                if (bottomLeft || bottomRight)
                {
                    VelocityY = 0;
                    tempY = (CollisionBox.Y + CollisionBox.Height) / tileMap.TileHeight * tileMap.TileHeight - CollisionBox.Height;
                    Y = tempY - (CollisionBox.Y - Y) - 1;
                }
            }
        }
        private void UpdateAnimation(float gameTime, float dt)
        {
            if (moving)
            {
                if (VelocityX < 0) animator.Play(ANI_LT);
                if (VelocityX > 0) animator.Play(ANI_RT);
                if (VelocityY < 0) animator.Play(ANI_UP);
                if (VelocityY > 0) animator.Play(ANI_DN);

                animator.Update(gameTime, dt);
            }
            else
            {
                animator.CurrentAnimation.Reset();
            }
        }
        private Animation MakeAnimation(AnimationAttribute attr, SpriteSheetSplitOptions option)
        {
            Animation ani = new Animation(attr.Name, attr.Fps, attr.Loop);
            ani.AddKeyFrames(attr.OffsetX, attr.OffsetY, attr.Frames, attr.FrameWidth, attr.FrameHeight, option);
            return ani;
        }

        public void Render(Graphics g)
        {
            var currentFrame = animator.CurrentAnimation.CurrentFrame;

            if (spriteSheet == null || currentFrame==null)
                return;

            g.DrawImage(
                spriteSheet,
                new RectangleF(X - camera.X, Y - camera.Y, currentFrame.Width, currentFrame.Height),
                currentFrame.Source,
                GraphicsUnit.Pixel
            );

            if (!ShowBoundingBox)
                return;

            g.DrawRectangle(
                Pens.Red,
                CollisionBox.X - camera.X,
                CollisionBox.Y - camera.Y,
                CollisionBox.Width,
                CollisionBox.Height
            );

            Rectangle tempBounds = CollisionBox;
            tempBounds.Inflate(-2, -2);

            g.DrawRectangle(
                Pens.Red,
                tempBounds.X - camera.X,
                tempBounds.Y - camera.Y,
                tempBounds.Width,
                tempBounds.Height
             );

            g.DrawLine(Pens.Red, tempBounds.X - camera.X, tempBounds.Y - camera.Y, tempBounds.Right - camera.X, tempBounds.Bottom - camera.Y);
            g.DrawLine(Pens.Red, tempBounds.X - camera.X, tempBounds.Bottom - camera.Y, tempBounds.Right - camera.X, tempBounds.Y - camera.Y);
        }

        #region InputHandler

        public void KeyPressed(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                up = true;
            else if (e.KeyCode == Keys.Down)
                down = true;
            else if (e.KeyCode == Keys.Left)
                left = true;
            else if (e.KeyCode == Keys.Right)
                right = true;
            else if (e.KeyCode == Keys.B)
                ShowBoundingBox = !ShowBoundingBox;

        }
        public void KeyReleased(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                up = false;
            else if (e.KeyCode == Keys.Down)
                down = false;
            else if (e.KeyCode == Keys.Left)
                left = false;
            else if (e.KeyCode == Keys.Right)
                right = false;
        }

        public void MousePressed(MouseEventArgs e)
        {
            X = ((e.X+camera.X) / tileMap.TileWidth) * tileMap.TileWidth;
            Y = ((e.Y+camera.Y) / tileMap.TileHeight) * tileMap.TileHeight;
        }
        public void MouseMoved(MouseEventArgs e)
        {
            
        }
        public void MouseReleased(MouseEventArgs e)
        {

        }

        #endregion
    }
}
