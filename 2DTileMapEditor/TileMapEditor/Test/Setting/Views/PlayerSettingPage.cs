using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TileMapEditor.Test.Setting
{
    public partial class PlayerSettingPage : UserControl,ISettingPage
    {
        private GameSetting gameSetting;
        private int currentItemIndex=-1;

        public PlayerSettingPage(GameSetting setting)
        {
            InitializeComponent();

            this.gameSetting = setting;

            Initialize();
        }

        private void Initialize()
        {
            lsvAnimations.View = View.Details;
            lsvAnimations.FullRowSelect = true;
            lsvAnimations.GridLines = true;
            lsvAnimations.HideSelection = false;
            lsvAnimations.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lsvAnimations.LabelEdit = false;

            lsvAnimations.Columns.Add("Animation", 80, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("Fps", 50, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("Loop", 50, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("OffsetX", 60, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("OffsetY", 60, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("Frames", 50, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("Width", 50, HorizontalAlignment.Center);
            lsvAnimations.Columns.Add("Height", 50, HorizontalAlignment.Center);

            lsvAnimations.Items.Clear();
            string[] attr = new string[] { "0", "True", "0", "0", "0", "0", "0"};
            ListViewItem walkDown = new ListViewItem(Sprite.Player.ANI_DN);
            walkDown.SubItems.AddRange(attr);
            ListViewItem walkLeft = new ListViewItem(Sprite.Player.ANI_LT);
            walkLeft.SubItems.AddRange(attr);
            ListViewItem walkRight = new ListViewItem(Sprite.Player.ANI_RT);
            walkRight.SubItems.AddRange(attr);
            ListViewItem walkUp = new ListViewItem(Sprite.Player.ANI_UP);
            walkUp.SubItems.AddRange(attr);
            lsvAnimations.Items.Add(walkDown);
            lsvAnimations.Items.Add(walkLeft);
            lsvAnimations.Items.Add(walkRight);
            lsvAnimations.Items.Add(walkUp);

            txtName.TextChanged += new EventHandler(txtName_TextChanged);
            Application.Idle += new EventHandler(Application_Idle);

            LoadPlayerSettings();
        }

        private void LoadPlayerSettings()
        {
            List<PlayerSetting> playerSettings = gameSetting.PlayerSettings;
            if (playerSettings.Count == 0)
                return;

            lsbPlayers.Items.Clear();
            foreach (PlayerSetting item in playerSettings)
            {
                lsbPlayers.Items.Add(item);
            }
            lsbPlayers.SelectedIndex = 0;
            currentItemIndex = 0;
            UpdateView(currentItemIndex);
        }

        private void UpdateView(int index)
        {
            PlayerSetting model = gameSetting.PlayerSettings[index];

            txtName.Text = model.Name;
            txtSpriteSheet.Text = model.SpriteSheet;
            txtWidth.Value = model.Width;
            txtHeight.Value = model.Height;
            txtASpeed.Value = (decimal)model.Accelerated;
            txtMaxSpeed.Value = (decimal)model.MaxVelocity;
            txtCollisionX.Value = model.CollisionBounds.X;
            txtCollisionY.Value = model.CollisionBounds.Y;
            txtCollisionWidth.Value = model.CollisionBounds.Width;
            txtCollisionHeight.Value = model.CollisionBounds.Height;

            cbRowSplit.Checked = model.SpliteOption == Sprite.SpriteSheetSplitOptions.FromRow;

            UpdateAnimationItem(model.WalkDownAttr, lsvAnimations.Items[0]);
            UpdateAnimationItem(model.WalkLeftAttr, lsvAnimations.Items[1]);
            UpdateAnimationItem(model.WalkRightAttr, lsvAnimations.Items[2]);
            UpdateAnimationItem(model.WalkUpAttr, lsvAnimations.Items[3]);
            UpdateSpriteSheet(model.SpriteSheet);
        }

        private void UpdateSpriteSheet(string charSet)
        {
            if (picSpriteSheet.Image != null)
            {
                picSpriteSheet.Image.Dispose();
                picSpriteSheet.Image = null;
            }

            string fullPath = string.Format("{0}\\{1}\\{2}",
                Application.StartupPath,
                ApplicationConsts.TILEMAP_GAME_CHARSET_DIR,
                charSet
            );

            if (!File.Exists(fullPath))
                return;

            picSpriteSheet.Image = Image.FromFile(fullPath);
            picSpriteSheet.Refresh();
        }

        private void UpdateAnimationItem(AnimationAttribute attr, ListViewItem item)
        {
            item.SubItems[0].Text = attr.Name;
            item.SubItems[1].Text = attr.Fps.ToString();
            item.SubItems[2].Text = attr.Loop.ToString();
            item.SubItems[3].Text = attr.OffsetX.ToString();
            item.SubItems[4].Text = attr.OffsetY.ToString();
            item.SubItems[5].Text = attr.Frames.ToString();
            item.SubItems[6].Text = attr.FrameWidth.ToString();
            item.SubItems[7].Text = attr.FrameHeight.ToString();        
        }

        private void UpdateAnimationAttribute(ref AnimationAttribute attr, ListViewItem item)
        {
            attr.Name = item.SubItems[0].Text;
            attr.Fps = float.Parse(item.SubItems[1].Text);
            attr.Loop = item.SubItems[2].Text == "True" ? true : false;
            attr.OffsetX = int.Parse(item.SubItems[3].Text);
            attr.OffsetY = int.Parse(item.SubItems[4].Text);
            attr.Frames = int.Parse(item.SubItems[5].Text);
            attr.FrameWidth = int.Parse(item.SubItems[6].Text);
            attr.FrameHeight = int.Parse(item.SubItems[7].Text);
        }

        private AnimationAttribute[] GetAllAnimationAttributes()
        {
            AnimationAttribute[] attrs = new AnimationAttribute[lsvAnimations.Items.Count];
            for (int i = 0; i < attrs.Length; i++)
            {
                AnimationAttribute atrr = new AnimationAttribute();
                UpdateAnimationAttribute(ref atrr, lsvAnimations.Items[i]);
                attrs[i] = atrr;
            }
            return attrs;
        }

        private void SaveCurrentItem()
        {
            if (currentItemIndex == -1)
                return;

            PlayerSetting model = gameSetting.PlayerSettings[currentItemIndex];
            model.Name = txtName.Text;
            model.SpriteSheet = txtSpriteSheet.Text;
            model.Width = (int)txtWidth.Value;
            model.Height = (int)txtHeight.Value;
            model.Accelerated = (float)txtASpeed.Value;
            model.MaxVelocity = (float)txtMaxSpeed.Value;
            model.CollisionBounds = new Rectangle(
                (int)txtCollisionX.Value,
                (int)txtCollisionY.Value,
                (int)txtCollisionWidth.Value,
                (int)txtCollisionHeight.Value
            );
            model.SpliteOption = cbRowSplit.Checked ? Sprite.SpriteSheetSplitOptions.FromRow :
                Sprite.SpriteSheetSplitOptions.FromColumn;
            UpdateAnimationAttribute(ref model.WalkDownAttr, lsvAnimations.Items[0]);
            UpdateAnimationAttribute(ref model.WalkLeftAttr, lsvAnimations.Items[1]);
            UpdateAnimationAttribute(ref model.WalkRightAttr, lsvAnimations.Items[2]);
            UpdateAnimationAttribute(ref model.WalkUpAttr, lsvAnimations.Items[3]);
            gameSetting.PlayerSettings[currentItemIndex] = model;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (lsbPlayers.SelectedIndex == -1)
                return;

            PlayerSetting item = (PlayerSetting)lsbPlayers.Items[lsbPlayers.SelectedIndex];
            item.Name = txtName.Text;
            lsbPlayers.Items[lsbPlayers.SelectedIndex] = item;
        }

        private void lsvAnimations_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lsvAnimations_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = lsvAnimations.Columns[e.ColumnIndex].Width;
        }

        private void btnSelectCharSet_Click(object sender, EventArgs e)
        {
            frm_CharSet charset = new frm_CharSet();
            charset.Initialize(txtSpriteSheet.Text);
            charset.Confirm += (obj, args) =>
            {
                txtSpriteSheet.Text = args.FileName;
                UpdateSpriteSheet(args.FileName);
            };
            charset.ShowDialog();
            charset.Dispose();
        }

        private void lsbPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbPlayers.SelectedIndex == -1) return;
            if (currentItemIndex == lsbPlayers.SelectedIndex) return;

            SaveCurrentItem();
            currentItemIndex = lsbPlayers.SelectedIndex;
            UpdateView(currentItemIndex);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PlayerSetting newItem = gameSetting.AddPlayer("Undefined");
                lsbPlayers.Items.Add(newItem);
                lsbPlayers.SelectedItem = newItem;
            }
            catch
            {
                MessageBox.Show("角色名称不能重复!");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int index = lsbPlayers.SelectedIndex;
            if (index == -1)
                return;

            gameSetting.RemovePlayer(index);
            lsbPlayers.Items.RemoveAt(index);

            if (lsbPlayers.Items.Count == 0)
            {
                currentItemIndex = -1;
                return; 
            }

            index = index > 0 ? index - 1 : 0;
            currentItemIndex = index;
            lsbPlayers.SelectedIndex = index;
            UpdateView(index);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frm_EditAnimation editForm = new frm_EditAnimation();

            AnimationAttribute attr = new AnimationAttribute();
            ListViewItem editItem = lsvAnimations.SelectedItems[0];
            UpdateAnimationAttribute(ref attr, editItem);

            editForm.Initialize(GetAllAnimationAttributes(), attr, picSpriteSheet.Image,
                cbRowSplit.Checked ? Sprite.SpriteSheetSplitOptions.FromRow :
                Sprite.SpriteSheetSplitOptions.FromColumn);

            editForm.EditConfirm += (obj, args) =>
            {
                UpdateAnimationItem(args.Attribute, editItem);
            };

            editForm.ShowDialog();
            editForm.Dispose();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            btnDel.Enabled = lsbPlayers.Items.Count > 0;
            btnEdit.Enabled = lsvAnimations.SelectedItems.Count > 0 && picSpriteSheet.Image != null;
        }

        #region ISettingPage interface

        public Control Owner
        {
            get { return this; }
        }

        public string PageName
        {
            get { return "角色设置"; }
        }

        public void HidePage()
        {
            this.Visible = false;
        }

        public void ShowPage()
        {
            this.Visible = true;
        }

        public void Save()
        {
            SaveCurrentItem();
        }

        #endregion
    }
}
