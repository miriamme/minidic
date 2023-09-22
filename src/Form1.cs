using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniDic
{
	public partial class Form1 : Form
	{
		private const string SETTING_NAME = "DictionarySettings";
		private Dictionary<string, string> tabs = new Dictionary<string, string>();
		public Form1()
		{
			InitializeComponent();
			InitDictionary();
			InitTabControl();
			InitTabMinus();
		}

		private void InitDictionary()
		{
			var settings = ConfigurationManager.GetSection(SETTING_NAME) as NameValueCollection;
			if (settings.Count == 0)
				throw new Exception("app.config의 설정값이 올바르지 않습니다.");
			else
			{
				tabs.Add("empty", "");
				foreach (var key in settings.AllKeys)
				{
					tabs.Add(key, settings[key]);
				}
			}
		}

		private void InitTabControl()
		{
			if (tabs.Count == 0)
				throw new NotImplementedException();

			foreach (var item in tabs)
			{
                CefSharp.WinForms.ChromiumWebBrowser url = new CefSharp.WinForms.ChromiumWebBrowser(item.Value) {
                    Dock = DockStyle.Fill
                };

                TabPage tab = new TabPage {
                    Text = item.Key,
                    UseVisualStyleBackColor = true
                };

                tab.Controls.Add(url);
				tabCtrl.Controls.Add(tab);
			}
		}

		private void InitTabMinus()
        {
            tabCtrl.Click += TabCtrl_Click;
		}

        private void TabCtrl_Click(object sender, EventArgs e)
        {
			if (tabCtrl.Controls.Count == 5)
				tabCtrl.Controls.RemoveAt(0);
		}

    }

}
