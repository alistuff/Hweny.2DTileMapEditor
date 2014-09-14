using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TileMapEditor.Test.Setting
{
    public class SettingPageManager
    {
        private IList<ISettingPage> pageList;
        private Dictionary<string, ISettingPage> pages;
        private ISettingPage currentPage;

        public SettingPageManager()
        {
            pages = new Dictionary<string, ISettingPage>();
            pageList = new List<ISettingPage>();
        }

        public void AddPage(ISettingPage page)
        {
            if (pages.ContainsKey(page.PageName))
                throw new ArgumentException("key");
            if (page == null)
                throw new ArgumentNullException("page");

            pages.Add(page.PageName, page);
            page.HidePage();

            pageList.Add(page);
        }

        public void SetPage(string key)
        {
            if (!pages.ContainsKey(key))
                return;

            if (pages[key] == currentPage)
                return;

            if (currentPage != null)
                currentPage.HidePage();

            currentPage = pages[key];
            currentPage.ShowPage();
        }

        public void Save()
        {
            for (int i = 0; i < pageList.Count; i++)
                pageList[i].Save();
        }

        public string[] ArrayOfPageName
        {
            get
            {
                string[] names = new string[pages.Count];
                pages.Keys.CopyTo(names,0);
                return names;
            }
        }

        public Control[] ArrayOfPageOwner
        {
            get
            {
                Control[] array = new Control[pages.Count];
                for (int i = 0; i < pageList.Count; i++)
                    array[i] = pageList[i].Owner;
                return array;
            }
        }
    }
}
