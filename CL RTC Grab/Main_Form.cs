using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Threading.Tasks;

namespace CL_RTC_Grab
{
    public partial class Main_Form : Form
    {
        private bool m_aeroEnabled;
        private bool __isClose;
        private int __secho;
        private int __display_length = 20;
        private int __result_count_json;
        private int __total_page;
        private int __i = 0;
        private int __index = 0;
        private JObject __jo;
        private JToken __conn_id;
        private bool __isStart = false;
        private bool __isBreak = false;
        private string __player_last_username = "";
        private string __playerlist_cn;
        private string __playerlist_ea;
        private string __player_id;
        private string __player_ldd;
        private string __start_time;
        private string __end_time;
        private string __get_time;
        List<string> __player_info = new List<string>();
        private bool __isInsert = false;

        // Drag Header to Move
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        // ----- Drag Header to Move

        // Form Shadow
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        // ----- Form Shadow

        public Main_Form()
        {
            InitializeComponent();
        }

        // Drag to Move
        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_loader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_brand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_status_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_player_last_registered_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----- Drag to Move

        // Click Close
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the program?", "CL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                __isClose = true;
                Environment.Exit(0);
            }
        }

        // Click Minimize
        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // Form Closing
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!__isClose)
            {
                DialogResult dr = MessageBox.Show("Exit the program?", "CL", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            Environment.Exit(0);
        }

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://sn.gk001.gpk456.com/Account/Login");
        }

        // WebBrowser
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                if (e.Url == webBrowser.Url)
                {
                    try
                    {
                        if (webBrowser.Url.ToString().Equals("http://sn.gk001.gpk456.com/Account/Login"))
                        {
                            __isStart = false;
                            timer.Stop();
                            label_status.Text = "-";
                            label_player_last_registered.Text = "-";
                            webBrowser.Document.Body.Style = "zoom:.8";
                            webBrowser.Visible = true;
                            label_brand.Visible = false;
                            pictureBox_loader.Visible = false;
                            label_status.Visible = false;
                            label_player_last_registered.Visible = false;
                            webBrowser.WebBrowserShortcutsEnabled = true;
                        }

                        if (webBrowser.Url.ToString().Equals("http://sn.gk001.gpk456.com/"))
                        {
                            if (!__isStart)
                            {
                                __isStart = true;
                                webBrowser.Visible = false;
                                label_brand.Visible = true;
                                pictureBox_loader.Visible = true;
                                label_status.Visible = true;
                                label_player_last_registered.Visible = true;
                                label_status.Text = "...";
                                webBrowser.WebBrowserShortcutsEnabled = false;
                                ___PlayerLastRegistered();
                                ___GetConnIDRequestAsync();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("No internet connection detected. Please call IT Support, thank you!", "CL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        __isClose = false;
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            label_status.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            label_status.Location = new Point(0, 70);
            DateTime start = DateTime.ParseExact(__start_time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(__end_time, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            TimeSpan difference = end - start;
            int hrs = difference.Hours;
            int mins = difference.Minutes;
            int secs = difference.Seconds;

            TimeSpan spinTime = new TimeSpan(hrs, mins, secs);

            TimeSpan delta = DateTime.Now - start;
            TimeSpan timeRemaining = spinTime - delta;

            if (timeRemaining.Minutes != 0)
            {
                if (timeRemaining.Seconds == 0)
                {
                    label_status.Text = timeRemaining.Minutes + ":" + timeRemaining.Seconds + "0";
                }
                else
                {
                    if (timeRemaining.Seconds.ToString().Length == 1)
                    {
                        label_status.Text = timeRemaining.Minutes + ":0" + timeRemaining.Seconds;
                    }
                    else
                    {
                        label_status.Text = timeRemaining.Minutes + ":" + timeRemaining.Seconds;
                    }
                }

                label_status.Visible = true;
            }
            else
            {
                if (label_status.Text == "1")
                {
                    timer.Stop();
                    label_status.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                    label_status.Location = new Point(0, 65);
                    label_status.Text = "...";
                    ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
                }
                else
                {
                    label_status.Text = timeRemaining.Seconds.ToString();
                    label_status.Visible = true;
                }
            }
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        void ___CloseMessageBox()
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "Message from webpage");

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void timer_close_message_box_Tick(object sender, EventArgs e)
        {
            ___CloseMessageBox();
        }

        // ----- Functions
        private async void ___GetConnIDRequestAsync()
        {
            __isBreak = false;

            try
            {
                var cookie = Cookie.GetCookieInternal(webBrowser.Url, false);
                WebClient wc = new WebClient();

                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var reqparm = new NameValueCollection
                {
                    {"pageIndex", "1"},
                    {"connectionId", "9ca65a15-aa52-4767-b486-60800fb872db"},
                };

                string data = "pageIndex=1&connectionId=9ca65a15-aa52-4767-b486-60800fb872db";

                string result = await wc.DownloadStringTaskAsync("http://sn.gk001.gpk456.com/signalr/negotiate");
                var deserializeObject = JsonConvert.DeserializeObject(result);
                __jo = JObject.Parse(deserializeObject.ToString());
                __conn_id = __jo.SelectToken("$.ConnectionId");
                ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
            }
            catch (Exception err)
            {
                ___GetConnIDRequestAsync();
            }
        }

        private async void ___GetPlayerListsRequestAsync(string index, string id)
        {
            try
            {
                var cookie = Cookie.GetCookieInternal(webBrowser.Url, false);
                WebClient wc = new WebClient();

                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Headers["X-Requested-With"] = "XMLHttpRequest";

                var reqparm = new NameValueCollection
                {
                    {"pageIndex", index},
                    {"connectionId", id},
                };

                byte[] result = await wc.UploadValuesTaskAsync("http://sn.gk001.gpk456.com/Member/Search", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                __jo = JObject.Parse(deserializeObject.ToString());
                ___PlayerListAsync();
            }
            catch (Exception err)
            {
                ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
            }
        }

        private async void ___PlayerListAsync()
        {
            if (__index == 0)
            {
                //if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test_cl.txt"))
                //{
                //    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test_cl.txt");
                //}

                __player_info.Clear();
            }
            
            
            for (int i = 0; i < 10; i++)
            {
                JToken username = __jo.SelectToken("PageData[" + i + "].Account").ToString();
                if (username.ToString() != Properties.Settings.Default.______last_registered_player)
                {
                    if (i == 0 && __index == 0)
                    {
                        __player_last_username = username.ToString();
                        label_player_last_registered.Text = "Last Registered: " + __player_last_username;
                    }
                    
                    // if last register is not equal
                    // get username, name, datetime register, player ldd, player cn, player ea
                    JToken name = __jo.SelectToken("PageData[" + i + "].Name").ToString();
                    JToken date_time_register = __jo.SelectToken("PageData[" + i + "].JoinTime").ToString();
                    //MessageBox.Show(username.ToString());
                    //MessageBox.Show(name.ToString());

                    await ___PlayerGetDetailAsync(username.ToString());

                    try
                    {
                        DateTime date_time_register_replace = DateTime.ParseExact(date_time_register.ToString(), "MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                        using (StreamWriter file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\test_cl.txt", true, Encoding.UTF8))
                        {
                            file.WriteLine(username + "*|*" + name + "*|*" + date_time_register_replace.ToString("yyyy-MM-dd HH:mm:ss") + "*|*" + __player_ldd + "*|*" + __playerlist_cn + "*|*" + __playerlist_ea);
                        }
                        __player_info.Add(username + "*|*" + name + "*|*" + date_time_register_replace.ToString("yyyy-MM-dd HH:mm:ss") + "*|*" + __player_ldd + "*|*" + __playerlist_cn + "*|*" + __playerlist_ea);

                        __playerlist_cn = "";
                        __playerlist_ea = "";
                        __player_ldd = "";

                        if (i == 9)
                        {
                            __index++;
                            ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please call IT Support, thank you!");
                    }
                }
                else
                {
                    //__player_info.Reverse();
                    //MessageBox.Show(String.Join("," + Environment.NewLine, __player_info));
                    if (__isInsert)
                    {
                        __isInsert = false;
                        ___SavePlayerLastRegistered(__player_last_username);
                        // send to api by 11 pm
                        // get last register
                        // save last register
                    }
                    __index = 0;
                    timer.Start();
                    __start_time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    __end_time = DateTime.Now.AddSeconds(302).ToString("dd/MM/yyyy HH:mm:ss");
                    break;

                }
            }
        }

        private async Task ___PlayerGetDetailAsync(string username)
        {
            try
            {
                var cookie = Cookie.GetCookieInternal(webBrowser.Url, false);
                WebClient wc = new WebClient();

                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Headers["X-Requested-With"] = "XMLHttpRequest";

                var reqparm = new NameValueCollection
                {
                    {"account", username}
                };

                byte[] result = await wc.UploadValuesTaskAsync("http://sn.gk001.gpk456.com/Member/GetDetail", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                var jo = JObject.Parse(deserializeObject.ToString());
                __playerlist_ea = jo.SelectToken("Member.Email").ToString();
                JToken id = jo.SelectToken("Member.Id").ToString();
                __playerlist_cn = jo.SelectToken("Member.Mobile").ToString();
                await ___PlayerGetLastDepositCountAsync(id.ToString());
            }
            catch (Exception err)
            {
                await ___PlayerGetDetailAsync(username);
            }
        }

        private async Task ___PlayerGetLastDepositCountAsync(string id)
        {
            try
            {
                var cookie = Cookie.GetCookieInternal(webBrowser.Url, false);
                WebClient wc = new WebClient();

                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                wc.Headers["X-Requested-With"] = "XMLHttpRequest";

                var reqparm = new NameValueCollection
                {
                    {"id", id}
                };

                byte[] result = await wc.UploadValuesTaskAsync("http://sn.gk001.gpk456.com/Member/GetDepositWithdrawInfo", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                var jo = JObject.Parse(deserializeObject.ToString());
                __player_ldd = jo.SelectToken("DepositTimes").ToString();
                if (Convert.ToInt16(__player_ldd.ToString()) > 0)
                {
                    __player_ldd = "1";
                }
                else
                {
                    __player_ldd = "0";
                }
            }
            catch (Exception err)
            {
                await ___PlayerGetLastDepositCountAsync(id);
            }
        }


        private void ___PlayerLastRegistered()
        {
            // handle last registered player
            if (Properties.Settings.Default.______last_registered_player == "")
            {
                //MessageBox.Show("ghghg");
                Properties.Settings.Default.______last_registered_player = "a76847510";
                Properties.Settings.Default.Save();
                // handle request
            }

            //Properties.Settings.Default.______last_registered_player = "a76847510";
            //Properties.Settings.Default.Save();

            label_player_last_registered.Text = "Last Registered: " + Properties.Settings.Default.______last_registered_player;
            // todo
        }

        private void ___SavePlayerLastRegistered(string username)
        {
            Properties.Settings.Default.______last_registered_player = username;
            Properties.Settings.Default.Save();
        }

        private void timer_detect_Tick(object sender, EventArgs e)
        {
            __isInsert = true;
            //__get_time = DateTime.Now.ToString("HH:mm:ss");
            //if (__get_time == "23:00:00")
            //{
            //    __isInsert = true;
            //}
        }
    }
}