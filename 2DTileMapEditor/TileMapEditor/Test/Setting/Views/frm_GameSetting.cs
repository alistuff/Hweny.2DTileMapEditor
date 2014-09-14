using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TileMapEditor.Test.Setting
{
    public partial class frm_GameSetting : Form
    {
        private SettingPageManager settingPageManager;
        private GameSetting setting;
        private string savePath = string.Empty;

        public frm_GameSetting()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            savePath = Application.StartupPath + "//" + ApplicationConsts.TILEMAP_GAME_SETTING_PATH;
            setting = GameSetting.FromFile(savePath);

            settingPageManager = new SettingPageManager();
            settingPageManager.AddPage(new GameSettingPage(setting));
            settingPageManager.AddPage(new PlayerSettingPage(setting));

            viewPanel.Controls.AddRange(settingPageManager.ArrayOfPageOwner);

            lsbConfig.Items.AddRange(settingPageManager.ArrayOfPageName);
            lsbConfig.SelectedIndex = 0;
            lsbConfig_SelectedIndexChanged(lsbConfig, null);
        }

        private void lsbConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbConfig.SelectedItem != null)
            {
                settingPageManager.SetPage(lsbConfig.SelectedItem.ToString());
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                settingPageManager.Save();
                setting.Save(savePath);
                this.Close();
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
