using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using System.Globalization;
using System.Net.Mail;

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
        private bool __isLogin = false;
        private bool __isStart = false;
        private bool __isBreak = false;
        private string __player_last_username = "";
        private string __playerlist_cn;
        private string __playerlist_ea;
        private string __playerlist_qq;
        private string __playerlist_wc; 
        private string __player_id;
        private string __player_ldd;
        private string __start_time;
        private string __end_time;
        private string __get_time;
        List<string> __player_info = new List<string>();
        private bool __isInsert = false;
        private string __brand_code = "CL";
        private string __brand_color = "#2160AD";
        private int __count = 0;

        // Deposit
        private JObject __jo_deposit;
        private int __index_deposit = 0;
        private int __count_deposit = 0;
        List<string> __player_info_deposit = new List<string>();
        private string __player_ldd_deposit;
        private bool __isInsert_deposit = false;
        private bool __isInsertDetect_deposit = false;
        private bool __detectInsert_deposit = false;
        private int __send_email = 0;

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
            
            timer_landing.Start();
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
        private void label_player_last_registered_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_landing_MouseDown(object sender, MouseEventArgs e)
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
            DialogResult dr = MessageBox.Show("Exit the program?", "CL RTC Grab", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                DialogResult dr = MessageBox.Show("Exit the program?", "CL RTC Grab", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr h, out uint dwVolume);

        // Mute Sounds
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr h, uint dwVolume);

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            int NewVolume = ((ushort.MaxValue / 10) * 100);
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);

            webBrowser.Navigate("http://sn.gk001.gpk456.com/Account/Login");
        }

        static int LineNumber([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }

        // WebBrowser
        private async void webBrowser_DocumentCompletedAsync(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser.ReadyState == WebBrowserReadyState.Complete)
            {
                if (e.Url == webBrowser.Url)
                {
                    timer_fill.Start();

                    try
                    {                        
                        if (webBrowser.Url.ToString().Equals("http://sn.gk001.gpk456.com/Account/Login"))
                        {
                            if (__isStart)
                            {
                                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                                SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>The application have been logout, please re-login again.</b></body></html>");
                                SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>The application have been logout, please re-login again.</b></body></html>");
                                __send_email = 0;
                            }

                            __isLogin = false;
                            __isStart = false;
                            timer.Stop();
                            label_player_last_registered.Text = "-";
                            webBrowser.Document.Body.Style = "zoom:.8";
                            webBrowser.Visible = true;
                            label_brand.Visible = false;
                            pictureBox_loader.Visible = false;
                            label_player_last_registered.Visible = false;
                            webBrowser.WebBrowserShortcutsEnabled = true;
                        }

                        if (webBrowser.Url.ToString().Equals("http://sn.gk001.gpk456.com/"))
                        {
                            __isLogin = true;
                            
                            if (!__isStart)
                            {
                                __isStart = true;
                                webBrowser.Visible = false;
                                label_brand.Visible = true;
                                pictureBox_loader.Visible = true;
                                label_player_last_registered.Visible = true;
                                webBrowser.WebBrowserShortcutsEnabled = false;
                                ___PlayerLastRegistered();
                                await ___GetConnIDRequestAsync();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                }
            }
        }

        private async void timer_TickAsync(object sender, EventArgs e)
        {
            timer.Stop();
            await ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
            if (__isInsert_deposit)
            {
                __isInsert_deposit = false;
                __detectInsert_deposit = false;
                __isInsertDetect_deposit = false;
                ___GetPlayerListsRequestAsync_Deposit(__index_deposit.ToString(), __conn_id.ToString());
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
        private async Task ___GetConnIDRequestAsync()
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
                
                string result = await wc.DownloadStringTaskAsync("http://sn.gk001.gpk456.com/signalr/negotiate");
                var deserializeObject = JsonConvert.DeserializeObject(result);
                __jo = JObject.Parse(deserializeObject.ToString());
                __conn_id = __jo.SelectToken("$.ConnectionId");
                await ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
                ___GetPlayerListsRequestAsync_Deposit(__index_deposit.ToString(), __conn_id.ToString());
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    await ___GetConnIDRequestAsync();
                }
            }
        }

        private async Task ___GetPlayerListsRequestAsync(string index, string id)
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
                string responsebody = Encoding.UTF8.GetString(result).Replace("Date", "TestDate");
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                __jo = JObject.Parse(responsebody.ToString());
                await ___PlayerListAsync();
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    await ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
                }
            }
        }

        private async Task ___PlayerListAsync()
        {
            if (__index == 0)
            {
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
                    
                    JToken name = __jo.SelectToken("PageData[" + i + "].Name").ToString();
                    JToken date_time_register = __jo.SelectToken("PageData[" + i + "].JoinTime").ToString();

                    await ___PlayerGetDetailAsync(username.ToString());

                    try
                    {
                        date_time_register = date_time_register.ToString().Replace("/TestDate(", "");
                        date_time_register = date_time_register.ToString().Replace(")/", "");
                        DateTime date_time_register_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(date_time_register.ToString()) / 1000d)).ToLocalTime();
                        
                        __player_info.Add(username + "*|*" + name + "*|*" + date_time_register_replace.ToString("yyyy-MM-dd HH:mm:ss") + "*|*" + __player_ldd + "*|*" + __playerlist_cn + "*|*" + __playerlist_ea + "*|*" + __playerlist_qq + "*|*" + __playerlist_wc);

                        __playerlist_cn = "";
                        __playerlist_ea = "";
                        __player_ldd = "";
                        __playerlist_qq = "";
                        __playerlist_wc = "";

                        if (i == 9)
                        {
                            __index++;
                            await ___GetPlayerListsRequestAsync(__index.ToString(), __conn_id.ToString());
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Please call IT Support, thank you!");
                    }
                }
                else
                {
                    if (__player_info.Count != 0)
                    {
                        __player_info.Reverse();
                        string player_info_get = String.Join(",", __player_info);
                        string[] values = player_info_get.Split(',');
                        foreach (string value in values)
                        {
                            string[] values_inner = value.Split(new string[] { "*|*" }, StringSplitOptions.None);
                            int count = 0;
                            string _username = "";
                            string _name = "";
                            string _date_register = "";
                            string _date_deposit = "";
                            string _cn = "";
                            string _email = "";
                            string _agent = "";
                            string _qq = "";
                            string _wc = "";

                            foreach (string value_inner in values_inner)
                            {
                                count++;

                                // Username
                                if (count == 1)
                                {
                                    _username = value_inner;
                                }
                                // Name
                                else if (count == 2)
                                {
                                    _name = value_inner;
                                }
                                // Register Date
                                else if (count == 3)
                                {
                                    _date_register = value_inner;
                                }
                                // Last Deposit Date
                                else if (count == 4)
                                {
                                    _date_deposit = value_inner;
                                }
                                // Contact Number
                                else if (count == 5)
                                {
                                    _cn = value_inner;
                                }
                                // Email
                                else if (count == 6)
                                {
                                    _email = value_inner;
                                }
                                // QQ
                                else if (count == 7)
                                {
                                    _qq = value_inner;
                                }
                                // WeChat
                                else if (count == 8)
                                {
                                    _wc = value_inner;
                                }
                            }

                            // ----- Insert Data
                            //using (StreamWriter file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\rtcgrab_cl.txt", true, Encoding.UTF8))
                            //{
                            //    file.WriteLine(_username + "*|*" + _name + "*|*" + _date_register + "*|*" + _date_deposit + "*|*" + _cn + "*|*" + _email + "*|*" + _agent + "*|*" + __brand_code);
                            //}
                            using (StreamWriter file = new StreamWriter(Path.GetTempPath() + @"\rtcgrab_cl.txt", true, Encoding.UTF8))
                            {
                                file.WriteLine(_username + "*|*" + _name + "*|*" + _date_register + "*|*" + _date_deposit + "*|*" + _cn + "*|*" + _email + "*|*" + _agent + "*|*" + __brand_code);
                            }

                            Thread t = new Thread(delegate () { ___InsertData(_username, _name, _date_register, _date_deposit, _cn, _email, _agent, _qq, _wc, __brand_code); });
                            t.Start();
                            
                            __count = 0;
                        }
                        
                        __player_info.Clear();
                    }

                    if (!String.IsNullOrEmpty(__player_last_username.Trim()))
                    {
                        ___SavePlayerLastRegistered(__player_last_username);
                        label_player_last_registered.Text = "Last Registered: " + Properties.Settings.Default.______last_registered_player;
                    }
                    
                    __index = 0;
                    timer.Start();
                    break;

                }
            }
        }

        private void ___InsertData(string username, string name, string date_register, string date_deposit, string contact, string email, string agent, string qq, string wc, string brand_code)
        {
            try
            {
                string password = username.ToLower() + date_register + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["username"] = username,
                        ["name"] = name,
                        ["date_register"] = date_register,
                        ["date_deposit"] = date_deposit,
                        ["contact"] = contact,
                        ["email"] = email,
                        ["agent"] = agent,
                        ["brand_code"] = brand_code,
                        ["qq"] = qq,
                        ["wc"] = wc,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssimakati.com:8080/API/sendRTC", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __count++;
                    if (__count == 5)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ____InsertData2(username, name, date_register, date_deposit, contact, email, agent, qq, wc, brand_code);
                    }
                }
            }
        }

        private void ____InsertData2(string username, string name, string date_register, string date_deposit, string contact, string email, string agent, string qq, string wc, string brand_code)
        {
            try
            {
                string password = username.ToLower() + date_register + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["username"] = username,
                        ["name"] = name,
                        ["date_register"] = date_register,
                        ["date_deposit"] = date_deposit,
                        ["contact"] = contact,
                        ["email"] = email,
                        ["agent"] = agent,
                        ["brand_code"] = brand_code,
                        ["qq"] = qq,
                        ["wc"] = wc,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus2.ssitex.com:8080/API/sendRTC", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __count++;
                    if (__count == 5)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___InsertData(username, name, date_register, date_deposit, contact, email, agent, qq, wc, brand_code);
                    }
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
                __playerlist_qq = jo.SelectToken("Member.QQ").ToString();
                __playerlist_wc = jo.SelectToken("Member.IdNumber").ToString();
                await ___PlayerGetLastDepositCountAsync(id.ToString());
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    await ___PlayerGetDetailAsync(username);
                }
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
                if (__isLogin)
                {
                    await ___PlayerGetLastDepositCountAsync(id);
                }
            }
        }
        
        private void ___PlayerLastRegistered()
        {
            if (Properties.Settings.Default.______last_registered_player == "" && Properties.Settings.Default.______last_registered_player_deposit == "")
            {
                ___GetLastRegisteredPlayer();
            }

            label_player_last_registered.Text = "Last Registered: " + Properties.Settings.Default.______last_registered_player;
        }

        private void ___SavePlayerLastRegistered(string username)
        {
            Properties.Settings.Default.______last_registered_player = username;
            Properties.Settings.Default.Save();
        }

        private void timer_detect_Tick(object sender, EventArgs e)
        {
            __isInsert = true;
            timer_detect.Stop();
        }

        private void timer_landing_Tick(object sender, EventArgs e)
        {
            panel_landing.Visible = false;
            timer_landing.Stop();
        }

















        // Deposit
        private async void ___GetPlayerListsRequestAsync_Deposit(string index, string id)
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
                string responsebody = Encoding.UTF8.GetString(result).Replace("Date", "TestDate");
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                __jo_deposit = JObject.Parse(responsebody.ToString());
                ___PlayerListAsync_Deposit();
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    ___GetPlayerListsRequestAsync_Deposit(__index_deposit.ToString(), __conn_id.ToString());
                }
            }
        }

        private async void ___PlayerListAsync_Deposit()
        {
            string path = @"\rtcgrab_cl_deposit.txt";
            if (__index_deposit == 0)
            {
                __player_info_deposit.Clear();
            }

            for (int i = 0; i < 10; i++)
            {
                if (!File.Exists(Path.GetTempPath() + path))
                {
                    using (StreamWriter file = new StreamWriter(Path.GetTempPath() + path, true, Encoding.UTF8))
                    {
                        file.WriteLine("test123*|*");
                        file.Close();
                    }
                }

                JToken username = "";
                try
                {
                    username = __jo_deposit.SelectToken("PageData[" + i + "].Account").ToString();
                }
                catch (Exception err)
                {
                    string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                    SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                    SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                    __send_email = 0;

                    __isClose = false;
                    Environment.Exit(0);
                }
                

                if (username.ToString() == Properties.Settings.Default.______last_registered_player)
                {
                    __detectInsert_deposit = true;
                }

                bool isInsert = false;

                if (__detectInsert_deposit)
                {
                    using (StreamReader sr = File.OpenText(Path.GetTempPath() + path))
                    {
                        string s = String.Empty;
                        while ((s = sr.ReadLine()) != null)
                        {
                            Application.DoEvents();

                            if (s == username.ToString())
                            {
                                isInsert = true;
                                break;
                            }
                            else
                            {
                                isInsert = false;
                            }
                        }
                        sr.Close();
                    }
                }
                
                if (i == 9)
                {
                    __index_deposit++;

                    if (!__isInsertDetect_deposit)
                    {
                        __isInsertDetect_deposit = false;
                        ___GetPlayerListsRequestAsync_Deposit(__index_deposit.ToString(), __conn_id.ToString());
                    }
                }

                if (username.ToString() != Properties.Settings.Default.______last_registered_player_deposit)
                {
                    if (__detectInsert_deposit)
                    {
                        if (!isInsert)
                        {
                            await ___PlayerGetDetailAsync_Deposit(username.ToString());
                        }
                    }
                        
                    if (__player_ldd_deposit == "1")
                    {
                        JToken date_time_register = __jo_deposit.SelectToken("PageData[" + i + "].JoinTime").ToString();

                        try
                        {
                            date_time_register = date_time_register.ToString().Replace("/TestDate(", "");
                            date_time_register = date_time_register.ToString().Replace(")/", "");
                            DateTime date_time_register_replace = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToDouble(date_time_register.ToString()) / 1000d)).ToLocalTime();

                            if (__detectInsert_deposit)
                            {
                                if (!isInsert)
                                {
                                    __player_info_deposit.Add(username + "*|*" + __player_ldd_deposit);

                                    using (StreamWriter file = new StreamWriter(Path.GetTempPath() + path, true, Encoding.UTF8))
                                    {
                                        file.WriteLine(username.ToString());
                                        file.Close();
                                    }
                                }
                            }

                            __player_ldd_deposit = "";
                        }
                        catch (Exception err)
                        {
                            string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                            SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                            SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                            __send_email = 0;

                            __isClose = false;
                            Environment.Exit(0);
                        }
                    }
                }
                else
                {
                    if (__player_info_deposit.Count != 0)
                    {
                        __player_info_deposit.Reverse();
                        string player_info_get = String.Join(",", __player_info_deposit);
                        string[] values = player_info_get.Split(',');
                        foreach (string value in values)
                        {
                            string[] values_inner = value.Split(new string[] { "*|*" }, StringSplitOptions.None);
                            int count = 0;
                            string _username = "";
                            string _name = "";
                            string _date_register = "";
                            string _date_deposit = "";
                            string _cn = "";
                            string _email = "";
                            string _agent = "";
                            string _qq = "";
                            string _wc = "";

                            foreach (string value_inner in values_inner)
                            {
                                count++;

                                // Username
                                if (count == 1)
                                {
                                    _username = value_inner;
                                }
                                // Last Deposit Date
                                else if (count == 2)
                                {
                                    _date_deposit = value_inner;
                                }
                            }

                            Thread t = new Thread(delegate () { ___InsertData_Deposit(_username, _date_deposit, __brand_code); });
                            t.Start();
                            
                            __count_deposit = 0;
                        }
                    }
                    
                    __player_info_deposit.Clear();
                    __index_deposit = 0;
                    __isInsertDetect_deposit = true;

                    break;
                }
            }
            
            if (__isInsertDetect_deposit)
            {
                ___DepositLastRegistered();
                __isInsert_deposit = true;
            }
        }

        private async Task ___PlayerGetDetailAsync_Deposit(string username)
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
                JToken id = jo.SelectToken("Member.Id").ToString();
                await ___PlayerGetLastDepositCountAsync_Deposit(id.ToString());
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    await ___PlayerGetDetailAsync_Deposit(username);
                }
            }
        }

        private async Task ___PlayerGetLastDepositCountAsync_Deposit(string id)
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
                __player_ldd_deposit = jo.SelectToken("DepositTimes").ToString();
                if (Convert.ToInt16(__player_ldd_deposit.ToString()) > 0)
                {
                    __player_ldd_deposit = "1";
                }
                else
                {
                    __player_ldd_deposit = "0";
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    await ___PlayerGetLastDepositCountAsync_Deposit(id);
                }
            }
        }

        private void ___InsertData_Deposit(string username, string last_deposit_date, string brand)
        {
            try
            {
                string password = username.ToLower() + last_deposit_date + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["username"] = username,
                        ["date_deposit"] = last_deposit_date,
                        ["brand_code"] = brand,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssimakati.com:8080/API/sendRTCdep", "POST", data);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __count_deposit++;
                    if (__count_deposit == 5)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___InsertData2_Deposit(username, last_deposit_date, brand);
                    }
                }
            }
        }

        private void ___InsertData2_Deposit(string username, string last_deposit_date, string brand)
        {
            try
            {
                string password = username.ToLower() + last_deposit_date + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["username"] = username,
                        ["date_deposit"] = last_deposit_date,
                        ["brand_code"] = brand,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssimakati.com:8080/API/sendRTCdep", "POST", data);
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __count_deposit++;
                    if (__count_deposit == 5)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___InsertData_Deposit(username, last_deposit_date, brand);
                    }
                }
            }
        }

        private void ___DepositLastRegistered()
        {
            string path = Path.GetTempPath() + @"\rtcgrab_cl_deposit.txt";
            if (label_player_last_registered.Text != "-" && label_player_last_registered.Text.Trim() != "")
            {
                if (Properties.Settings.Default.______detect_deposit == "")
                {
                    DateTime today = DateTime.Now;
                    DateTime date = today.AddDays(1);
                    Properties.Settings.Default.______detect_deposit = date.ToString("yyyy-MM-dd 23");
                    Properties.Settings.Default.Save();
                }
                else
                {
                    DateTime today = DateTime.Now;
                    if (Properties.Settings.Default.______detect_deposit == today.ToString("yyyy-MM-dd HH"))
                    {
                        Properties.Settings.Default.______detect_deposit = "";
                        Properties.Settings.Default.______last_registered_player_deposit = label_player_last_registered.Text.Replace("Last Registered: ", "");
                        Properties.Settings.Default.Save();

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        string start_datetime = today.ToString("yyyy-MM-dd HH");
                        DateTime start = DateTime.ParseExact(start_datetime, "yyyy-MM-dd HH", CultureInfo.InvariantCulture);

                        string end_datetime = Properties.Settings.Default.______detect_deposit;
                        DateTime end = DateTime.ParseExact(end_datetime, "yyyy-MM-dd HH", CultureInfo.InvariantCulture);

                        if (start > end)
                        {
                            Properties.Settings.Default.______detect_deposit = "";
                            Properties.Settings.Default.______last_registered_player_deposit = label_player_last_registered.Text.Replace("Last Registered: ", "");
                            Properties.Settings.Default.Save();

                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                    }
                }
            }
        }

        private void SendEmail(string get_message)
        {
            try
            {
                int port = 587;
                string host = "smtp.gmail.com";
                string username = "drake@18tech.com";
                string password = "@ccess123418tech";
                string mailFrom = "noreply@mail.com";
                string mailTo = "drake@18tech.com";
                string mailTitle = "YB RTC Grab";
                string mailMessage = get_message;

                using (SmtpClient client = new SmtpClient())
                {
                    MailAddress from = new MailAddress(mailFrom);
                    MailMessage message = new MailMessage
                    {
                        From = from
                    };
                    message.To.Add(mailTo);
                    message.Subject = mailTitle;
                    message.Body = mailMessage;
                    message.IsBodyHtml = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential
                    {
                        UserName = username,
                        Password = password
                    };
                    client.Send(message);
                }
            }
            catch (Exception err)
            {
                __send_email++;
                if (__send_email <= 5)
                {
                    SendEmail(get_message);
                }
                else
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }

        private void SendEmail2(string get_message)
        {
            try
            {
                int port = 587;
                string host = "smtp.gmail.com";
                string username = "drake@18tech.com";
                string password = "@ccess123418tech";
                string mailFrom = "noreply@mail.com";
                string mailTo = "it@18tech.com";
                string mailTitle = "YB RTC Grab";
                string mailMessage = get_message;

                using (SmtpClient client = new SmtpClient())
                {
                    MailAddress from = new MailAddress(mailFrom);
                    MailMessage message = new MailMessage
                    {
                        From = from
                    };
                    message.To.Add(mailTo);
                    message.Subject = mailTitle;
                    message.Body = mailMessage;
                    message.IsBodyHtml = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential
                    {
                        UserName = username,
                        Password = password
                    };
                    client.Send(message);
                }
            }
            catch (Exception err)
            {
                __send_email++;
                if (__send_email <= 5)
                {
                    SendEmail2(get_message);
                }
                else
                {
                    MessageBox.Show(err.ToString());
                }
            }
        }
        private void ___GetLastRegisteredPlayer()
        {
            try
            {
                string password = __brand_code + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["token"] = token
                    };

                    var result = wb.UploadValues("http://zeus.ssimakati.com:8080/API/lastRTCrecord", "POST", data);
                    string responsebody = Encoding.UTF8.GetString(result);
                    var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                    JObject jo = JObject.Parse(deserializeObject.ToString());
                    JToken plr = jo.SelectToken("$.msg");

                    Properties.Settings.Default.______last_registered_player = plr.ToString();
                    Properties.Settings.Default.______last_registered_player_deposit = plr.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __count++;
                    if (__count == 5)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___GetLastRegisteredPlayer2();
                    }
                }
            }
        }

        private void ___GetLastRegisteredPlayer2()
        {
            try
            {
                string password = __brand_code + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["token"] = token
                    };

                    var result = wb.UploadValues("http://zeus2.ssitex.com:8080/API/lastRTCrecord", "POST", data);
                    string responsebody = Encoding.UTF8.GetString(result);
                    var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                    JObject jo = JObject.Parse(deserializeObject.ToString());
                    JToken plr = jo.SelectToken("$.msg");

                    Properties.Settings.Default.______last_registered_player = plr.ToString();
                    Properties.Settings.Default.______last_registered_player_deposit = plr.ToString();
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception err)
            {
                if (__isLogin)
                {
                    __count++;
                    if (__count == 5)
                    {
                        string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                        SendEmail("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Line Number: " + LineNumber() + "<br/>Message: <b>" + err.ToString() + "</b></body></html>");
                        SendEmail2("<html><body>Brand: <font color='" + __brand_color + "'>-----" + __brand_code + "-----</font><br/>IP: 192.168.10.252<br/>Location: Robinsons Summit Office<br/>Date and Time: [" + datetime + "]<br/>Message: <b>There's a problem to the server, please re-open the application.</b></body></html>");
                        __send_email = 0;

                        __isClose = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        ___GetLastRegisteredPlayer();
                    }
                }
            }
        }
    }
}