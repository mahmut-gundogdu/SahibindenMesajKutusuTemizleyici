using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SahibindenMesajSilici
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey anahtar = Registry.LocalMachine;
            anahtar = anahtar.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION");
            
            //bool karar = (anahtar != null);
            string exeName = System.AppDomain.CurrentDomain.FriendlyName;

            var keyDegeri = anahtar.GetValue(exeName);
            if (keyDegeri == null)//Bu bizim IE nin 9 MOdunda Calismasi için GErekli Hede.
            {
                try
                {
                    anahtar = anahtar.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                    anahtar.SetValue(exeName, 9999, RegistryValueKind.DWord);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tarayınız eski hatasını düzeltmek için ilk sefere mahsus yönetici olarak calıstırın (Caliştirmadan önce sağ tıklayıp run as administrator veya yönetici olarak calistir)");
                }
            }
       
            webBrowser1.Navigate("http://www.sahibinden.com/index.php?a=1004&b=8&page=1");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted2;
            var chx = webBrowser1.Document.GetElementById("chk_msg");
            if (chx != null)
            {
                chx.InvokeMember("click");
                Sil();
            }
            else
            {
                MessageBox.Show("Hata Mesaj Kutusu Sayfasında olduğunuza emin olun");
            }

        }

        private void webBrowser1_DocumentCompleted2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var chx = webBrowser1.Document.GetElementById("chk_msg");
            if (chx != null)
            {
                chx.InvokeMember("click");
                Sil();
            }
            else
            {
                webBrowser1.DocumentCompleted -= webBrowser1_DocumentCompleted2;
                MessageBox.Show("Hata Mesaj Kutusu Sayfasında olduğunuza emin olun");

            }
        }
        public void Sil()
        {
            //Thread.Sleep(1000);
            var btn = webBrowser1.Document.GetElementById("remove_msg_button");
            //btn.InvokeMember("click");
            var frm = webBrowser1.Document.GetElementById("formmsg");
            frm.InvokeMember("submit");
        }
    }
}
