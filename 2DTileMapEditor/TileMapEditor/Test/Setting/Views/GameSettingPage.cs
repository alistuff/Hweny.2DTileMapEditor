using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapEditor.Test.Main;

namespace TileMapEditor.Test.Setting
{
    public partial class GameSettingPage : UserControl,ISettingPage
    {
        private GameSetting gameSetting;
        public GameSettingPage(GameSetting setting)
        {
            InitializeComponent();

            gameSetting = setting;
            Initialize();
        }

        private void Initialize()
        {
            txtScreenWidth.Minimum = txtScreenHeight.Minimum = 1;
            txtScreenWidth.Maximum = txtScreenHeight.Maximum = 1024;
            txtTargetFps.Minimum = 10;
            txtTargetFps.Maximum = 10000;

            UpdateView();
            this.VisibleChanged += new EventHandler(GameSettingPage_VisibleChanged);
        }

        private void UpdateView()
        {
            cbbGameType.Items.Clear();
            cbbGameType.Items.AddRange(Enum.GetNames(typeof(GameType)));
            cbbPlayer.Items.Clear();
            cbbPlayer.Items.AddRange(gameSetting.PlayerSettings.ToArray());

            txtTitle.Text = gameSetting.Title;
            txtScreenWidth.Value = gameSetting.Width;
            txtScreenHeight.Value = gameSetting.Height;
            txtTargetFps.Value = gameSetting.TargetFps;
            cbbGameType.SelectedItem = gameSetting.GameType.ToString();
            cbbPlayer.SelectedItem = gameSetting.GetCurrentPlayerSetting();
        }

        private void UpdateModel()
        {
            gameSetting.Title = txtTitle.Text;
            gameSetting.Width = (int)txtScreenWidth.Value;
            gameSetting.Height = (int)txtScreenHeight.Value;
            gameSetting.TargetFps = (int)txtTargetFps.Value;
            gameSetting.GameType = (GameType)Enum.Parse(typeof(GameType), cbbGameType.SelectedItem.ToString());
            gameSetting.CurrentPlayer = cbbPlayer.SelectedItem != null ? cbbPlayer.SelectedItem.ToString() : string.Empty;
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            gameSetting.SetDefault();
            UpdateView();
        }

        private void GameSettingPage_VisibleChanged(object sender, EventArgs e)
        {
            cbbPlayer.Items.Clear();
            cbbPlayer.Items.AddRange(gameSetting.PlayerSettings.ToArray());

            PlayerSetting currentItem = gameSetting.GetCurrentPlayerSetting();
            if (currentItem != null)
                cbbPlayer.SelectedItem = gameSetting.GetCurrentPlayerSetting();
            else
                cbbPlayer.Text = "";
        }

        #region ISettingPage interface

        public Control Owner
        {
            get { return this; }
        }

        public string PageName
        {
            get { return "常规"; }
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
            UpdateModel();
        }

        #endregion
    }
}
