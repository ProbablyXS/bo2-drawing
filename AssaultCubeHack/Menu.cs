using RL.game;
using RL.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VECRPROJECT.util;

namespace AssaultCubeHack
{
    public partial class Menu : Form
    {

        private Clsini INIConfig = new Clsini("profiles/" + Settings.Default.Profiles + "/config.ini");
        private Clsini INIBestProfiles = new Clsini("profiles/startup.ini");
        private bool started = false;
        private System.Drawing.ColorConverter ConvertColor = new ColorConverter();

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey); // Keys enumeration

        //threads for updating rendering
        private Thread windowPosThread;




        // 'MOUVE FORM WITH MOUSE'
        private bool drag;
        private int mousex;
        private int mousey;
        public object WantApplicationExit = false;
        public int access = 0;

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            mousex = Cursor.Position.X - this.Left;
            mousey = Cursor.Position.Y - this.Top;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                this.Top = Cursor.Position.Y - mousey;
                this.Left = Cursor.Position.X - mousex;
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
        // MOUVE FORM WITH MOUSE'




        public Menu()
        {
            InitializeComponent();
        }

        private void AssaultHack_Load(object sender, EventArgs e)
        {

            Security.CHANGENAME();

            //LOAD PARAMETERS
            InitializationCONTROLS();

            //try to attach to game
            AttachToGameProcess();

            Examples.Example.Menu_Showed = true;
            Examples.Example.Menu_Loading = false;

            //Show topmost first page
            this.Activate();
        }

        private void InitializeOverlayWindowAttributes()
        {
            TopMost = true; //set window on top of all others
            FormBorderStyle = FormBorderStyle.None; //remove form controls
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            ACB.UseAnimation = true;
            ACB.SelectedTextColor = Color.White;
            ACB.Font = new Font("Arial Black", 8, FontStyle.Bold);
        }

        public async Task refreshControllerIsConnectedAsync()
        {

            while (true)
            {
                await Task.Delay(1000);

                if (RL.game.Controller.controller.Connection.Value == true)
                {
                    label109.Text = "ON";
                    label109.ForeColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    label109.Text = "OFF";
                    label109.ForeColor = Settings.Default.Menu_Border_Color;
                }
            }
        }

        public void AttachToGameProcess()
        {
            bool success = false;
            do
            {
                //check if game is running
                if (Memory.GetProcessesByName(Examples.Example.processName, out Examples.Example.process))
                {
                    try
                    {
                        //success  
                        IntPtr handle = Memory.OpenProcess(Examples.Example.process.Id);
                        if (handle != IntPtr.Zero)
                        {
                            success = true;
                        }
                        else
                        {
                            Security.FAILACC();
                        }
                    }
                    catch (Exception)
                    {
                        Security.FAILACC();
                    }
                }
                else
                {
                    Security.FAILACC();
                }
            } while (!success);

            InitializeOverlayWindowAttributes();
            StartThreads();
        }

        public void resetConfigFile()
        {
            File.WriteAllBytes("profiles/" + Settings.Default.Profiles + "/config.ini", Resources.config);
            MessageBox.Show("ERROR: 8, Resetting config.ini", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            InitializationCONTROLS();
        }

        public void InitializationCONTROLS()
        {
            //--------PAGE--------
            ACB.SelectTab(0);

            AIMBOTpage.BaseColor = Color.FromArgb(30, 30, 30);
            MISCpage.BaseColor = Color.FromArgb(30, 30, 30);

            metroSetComboBox1.ForeColor = Settings.Default.Normal_Color;
            metroSetComboBox1.BorderColor = Settings.Default.Normal_Color;


            //HOME

            //REFRESH CONTROLLER INFORMATION
            refreshControllerIsConnectedAsync();

            label11.Text = "Welcome: " + Environment.MachineName;
            //label100.Text = "Expires: " + Security.infoDate;
            label13.Text = "Current version: " + Application.ProductVersion;
            //END --------PAGE--------


            //CHECK INI FILE EXIST
            try
            {

                if (File.Exists("profiles/startup.ini") == true)
                {
                    Settings.Default.Profiles = INIBestProfiles.Read("Best", "STARTUP");
                }
                else
                {
                    Directory.CreateDirectory("profiles/" + Settings.Default.Profiles);
                    File.WriteAllBytes("profiles/startup.ini", Resources.startup);
                }

                if (File.Exists("profiles/" + Settings.Default.Profiles + "/config.ini") == false)
                {
                    Directory.CreateDirectory("profiles/" + Settings.Default.Profiles);
                    File.WriteAllBytes("profiles/" + Settings.Default.Profiles + "/config.ini", Resources.config);
                }

                INIConfig = new Clsini("profiles/" + Settings.Default.Profiles + "/config.ini"); //get new config

                KeysConverter kc = new KeysConverter(); //Convert to int val to string val ex: 10 = SPACE

                //AIMBOT
                Settings.Default.AIMBOT = Convert.ToBoolean(INIConfig.Read("Enable_AIMBOT", "AIMBOT"));
                Settings.Default.AIMBOT_Acceleration = Convert.ToInt32(INIConfig.Read("Acceleration", "AIMBOT"));
                Settings.Default.Aim_FOV = Convert.ToInt32(INIConfig.Read("Aim_FOV", "AIMBOT"));
                Settings.Default.Draw_Aim_Fov = Convert.ToBoolean(INIConfig.Read("Draw_Aim_Fov", "AIMBOT"));
                Settings.Default.Draw_Enemy_Close = Convert.ToBoolean(INIConfig.Read("Draw_Enemy_Close", "AIMBOT"));
                Settings.Default.Aim_Filter = Convert.ToInt32(INIConfig.Read("Aim_Filter", "AIMBOT"));
                Settings.Default.AIMOT_Target = Convert.ToInt32(INIConfig.Read("Aim_Target", "AIMBOT"));
                Settings.Default.AIM_Key = (INIConfig.Read("AIM_Key", "AIMBOT"));
                Settings.Default.AIM_Auto_Fire = Convert.ToBoolean(INIConfig.Read("Auto_Fire", "AIMBOT"));
                Settings.Default.AUTO_FIRE_MS = Convert.ToInt32(INIConfig.Read("Auto_Fire_MS", "AIMBOT"));

                //ESP
                Settings.Default.ESP = Convert.ToBoolean(INIConfig.Read("Enable_ESP", "ESP"));
                Settings.Default.ESP_Form = Convert.ToInt32(INIConfig.Read("ESP_Form", "ESP"));
                Settings.Default.ESP_Size = Convert.ToInt32(INIConfig.Read("ESP_Size", "ESP"));
                Settings.Default.Show_Allies = Convert.ToBoolean(INIConfig.Read("Show_Allies", "ESP"));
                Settings.Default.Show_Name = Convert.ToBoolean(INIConfig.Read("Show_Name", "ESP"));
                Settings.Default.Show_DISTANCE = Convert.ToBoolean(INIConfig.Read("Show_Distance", "ESP"));
                Settings.Default.Show_PING = Convert.ToBoolean(INIConfig.Read("Show_Ping", "ESP"));
                Settings.Default.SNAPLINE = Convert.ToBoolean(INIConfig.Read("Enable_SNAPLINE", "ESP"));
                Settings.Default.SNAPLINE_Size = Convert.ToInt32(INIConfig.Read("Snapline_Size", "ESP"));
                Settings.Default.SNAPLINE_Starting_Point = Convert.ToInt32(INIConfig.Read("Snapline_Starting_Point", "ESP"));

                //MISC
                Settings.Default.CROSSHAIR = Convert.ToBoolean(INIConfig.Read("Enable_CROSSHAIR", "MISC"));
                Settings.Default.CROSSHAIR_Size = Convert.ToInt32(INIConfig.Read("Crosshair_Size", "MISC"));
                Settings.Default.CROSSHAIR_Thickness = Convert.ToInt32(INIConfig.Read("Crosshair_Thickness", "MISC"));
                Settings.Default.Show_Text_Border = Convert.ToBoolean(INIConfig.Read("Show_Text_Border", "MISC"));
                Settings.Default.ESP_SIZE_TEXT = Convert.ToInt32(INIConfig.Read("Text_Size", "MISC"));
                Settings.Default.FontText = Convert.ToString(INIConfig.Read("Font_Text", "MISC"));
                Settings.Default.VSAT = Convert.ToBoolean(INIConfig.Read("Enable_VSAT", "MISC"));

                //STYLES
                Settings.Default.ESP_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Enemies_Color", "STYLES"));
                Settings.Default.ESP_ALLIES_COLOR = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Allies_Color", "STYLES"));
                Settings.Default.ESP_FILLED_BORDER_COLOR = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Filled_Border_Color", "STYLES"));
                Settings.Default.SNAPLINE_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Snapline_Color", "STYLES"));
                Settings.Default.CROSSHAIR_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Crosshair_Color", "STYLES"));
                Settings.Default.AIMBOT_FOV_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("FOV_Color", "STYLES"));
                Settings.Default.Menu_Border_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Menu_Color", "STYLES"));
                Settings.Default.Player_Name_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Player_Name_Color", "STYLES"));
                Settings.Default.Player_Ping_Color = (Color)ConvertColor.ConvertFromString(INIConfig.Read("Player_Ping_Color", "STYLES"));

                //CONTROLLER
                Settings.Default.ControllerButtonAIMBOT = (INIConfig.Read("AIM_Key", "CONTROLLER"));
                Settings.Default.ControllerToggleAIMBOT = (INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER"));
                Settings.Default.ControllerToggleESP = (INIConfig.Read("TOGGLE_ESP", "CONTROLLER"));
                Settings.Default.ControllerToggleSNAPLINE = (INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER"));
                Settings.Default.ControllerToggleVSAT = (INIConfig.Read("TOGGLE_VSAT", "CONTROLLER"));
                Settings.Default.ControllerToggleCROSSHAIR = (INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER"));

                //SETTING
                Settings.Default.FPS = Convert.ToInt32(INIConfig.Read("FPS", "SETTINGS"));
                Settings.Default.Key_Show_MENU = (Keys)kc.ConvertFromString(INIConfig.Read("Show_Menu", "SETTINGS"));
                Settings.Default.OVERLAY = Convert.ToBoolean(INIConfig.Read("Show_Overlay", "SETTINGS"));
                Settings.Default.Show_FPS = Convert.ToBoolean(INIConfig.Read("Show_FPS", "SETTINGS"));
                Settings.Default.VSYNC = Convert.ToBoolean(INIConfig.Read("VSYNC", "SETTINGS"));
                Settings.Default.FPS_CORNER = Convert.ToInt32(INIConfig.Read("FPS_Corner", "SETTINGS"));
                Settings.Default.Menu_Opacity = Convert.ToDouble(INIConfig.Read("Menu_Opacity", "SETTINGS"));
                Settings.Default.TextAntiAliasing = Convert.ToBoolean(INIConfig.Read("Anti_Aliasing", "SETTINGS"));
            }
            catch
            {
                resetConfigFile();
            }

            //--------READ CONFIG--------

            //AIMBOT
            label43.Text = Settings.Default.AIM_Key.ToString();

            metroSetTrackBar4.Value = (int)Settings.Default.AIMBOT_Acceleration;
            label36.Text = Settings.Default.AIMBOT_Acceleration.ToString();

            if (Settings.Default.AIMOT_Target == 1) //HEAD
            {
                Aimbot.TARGET = 10F;
            }
            else if (Settings.Default.AIMOT_Target == 2) //BODY
            {
                Aimbot.TARGET = 20F;
            }
            else if (Settings.Default.AIMOT_Target == 3) //FOOT
            {
                Aimbot.TARGET = 55F;
            }

            metroSetTrackBar8.Value = (int)Settings.Default.AUTO_FIRE_MS;
            label105.Text = Settings.Default.AUTO_FIRE_MS.ToString() + " MS";

            if (Settings.Default.Aim_FOV <= metroSetTrackBar6.Maximum)
            {
                metroSetTrackBar6.Value = Settings.Default.Aim_FOV;
                label57.Text = Settings.Default.Aim_FOV.ToString();
            }

            if (Settings.Default.AIMBOT == true && (Security.pfpf == 1))
            {
                CheckBoxAIMBOT.Ovalchecked = true;
            }
            else
            {
                CheckBoxAIMBOT.Ovalchecked = false;
            }

            if (Settings.Default.Draw_Aim_Fov == true && (Security.pfpf == 1))
            {
                customCheckBox7.Ovalchecked = true;
                label59.Text = "Enabled";
            }
            else
            {
                customCheckBox7.Ovalchecked = false;
                label59.Text = "Disabled";
            }

            if (Settings.Default.Draw_Enemy_Close == true && (Security.pfpf == 1))
            {
                customCheckBox14.Ovalchecked = true;
                label23.Text = "Enabled";
            }
            else
            {
                customCheckBox14.Ovalchecked = false;
                label23.Text = "Disabled";
            }

            if (Settings.Default.AIM_Auto_Fire == true && (Security.pfpf == 1))
            {
                customCheckBox4.Ovalchecked = true;
                label31.Text = "Enabled";
            }
            else
            {
                customCheckBox4.Ovalchecked = false;
                label31.Text = "Disabled";
            }

            if (Settings.Default.Aim_Filter == 1 && (Security.pfpf == 1))
            {
                metroSetEllipse8.NormalColor = Settings.Default.Menu_Border_Color;
                metroSetEllipse7.NormalColor = Settings.Default.Default_Color;
                label12.Text = "CROSSHAIR";
            }
            else if (Settings.Default.Aim_Filter == 2)
            {
                metroSetEllipse7.NormalColor = Settings.Default.Menu_Border_Color;
                metroSetEllipse8.NormalColor = Settings.Default.Default_Color;
                label12.Text = "DISTANCE";
            }

            if (Settings.Default.AIMOT_Target == 1 && (Security.pfpf == 1))
            {
                metroSetEllipse4.NormalColor = Settings.Default.Menu_Border_Color;
                metroSetEllipse2.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse3.NormalColor = Settings.Default.Default_Color;
                label34.Text = "HEAD";
            }
            else if (Settings.Default.AIMOT_Target == 2)
            {
                metroSetEllipse2.NormalColor = Settings.Default.Menu_Border_Color;
                metroSetEllipse3.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse4.NormalColor = Settings.Default.Default_Color;
                label34.Text = "BODY";
            }
            else if (Settings.Default.AIMOT_Target == 3)
            {
                metroSetEllipse3.NormalColor = Settings.Default.Menu_Border_Color;
                metroSetEllipse2.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse4.NormalColor = Settings.Default.Default_Color;
                label34.Text = "FOOT";
            }

            if (Settings.Default.AIM_Auto_Fire == true && (Security.pfpf == 1) && Settings.Default.AIM_Key.ToString().ToLower() == "LButton".ToLower())
            {
                customCheckBox4.Ovalchecked = false;
                customCheckBox4.Enabled = false;
                label31.Text = "Disabled";
            }

            //ESP
            metroSetTrackBar1.Value = Settings.Default.ESP_Size;
            labelESPSIZE.Text = metroSetTrackBar1.Value.ToString();

            if (Settings.Default.ESP == true)
            {
                CheckBoxESP.Ovalchecked = true;
            }
            else
            {
                CheckBoxESP.Ovalchecked = false;
            }

            if (Settings.Default.Show_Allies == true)
            {
                customCheckBox10.Ovalchecked = true;
                label72.Text = "Showed";
            }
            else
            {
                customCheckBox10.Ovalchecked = false;
                label72.Text = "Hidden";
            }

            if (Settings.Default.ESP_Form == 1)
            {
                button7.BackgroundImage = Resources.Square;
                label10.Text = "Rectangle";
            }
            else if (Settings.Default.ESP_Form == 2)
            {
                button7.BackgroundImage = Resources.SquareFULL;
                label10.Text = "Filled Rect";
            }
            else if (Settings.Default.ESP_Form == 3)
            {
                button7.BackgroundImage = Resources.Ellipse;
                label10.Text = "Ellipse";
            }

            if (Settings.Default.Show_Name == true && Security.pfpf == 1)
            {
                customCheckBox11.Ovalchecked = true;
                label74.Text = "Showed";
            }
            else
            {
                customCheckBox11.Ovalchecked = false;
                label74.Text = "Hidden";
            }

            if (Settings.Default.Show_DISTANCE == true && Security.pfpf == 1)
            {
                customCheckBox2.Ovalchecked = true;
                label41.Text = "Showed";
            }
            else
            {
                customCheckBox2.Ovalchecked = false;
                label41.Text = "Hidden";
            }

            if (Settings.Default.Show_PING == true && Security.pfpf == 1)
            {
                customCheckBox5.Ovalchecked = true;
                label96.Text = "Showed";
            }
            else
            {
                customCheckBox5.Ovalchecked = false;
                label96.Text = "Hidden";
            }


            //SNAPLINE
            metroSetTrackBarSNAPLINESize.Value = Settings.Default.SNAPLINE_Size;

            labelSNAPLINESIZE.Text = metroSetTrackBarSNAPLINESize.Value.ToString();

            if (Settings.Default.SNAPLINE == true && (Security.pfpf == 1))
            {
                CheckBoxSnapLine.Ovalchecked = true;
                label21.Text = "Showed";
            }
            else
            {
                CheckBoxSnapLine.Ovalchecked = false;
                label21.Text = "Hidden";

            }

            if (Settings.Default.ESP_Form == 1)
            {
                button7.BackgroundImage = Resources.Square;
                label10.Text = "Rectangle";
            }
            else if (Settings.Default.ESP_Form == 2)
            {
                button7.BackgroundImage = Resources.SquareFULL;
                label10.Text = "Filled Rect";
            }
            else if (Settings.Default.ESP_Form == 3)
            {
                button7.BackgroundImage = Resources.Ellipse;
                label10.Text = "Ellipse";
            }

            if (Settings.Default.SNAPLINE_Starting_Point == 1 && (Security.pfpf == 1))
            {
                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "TOP LEFT";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 2)
            {
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "CENTER TOP";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 3)
            {
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "TOP RIGHT";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 4)
            {
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "CENTER L";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 5)
            {
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "CENTER";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 6)
            {
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "CENTER R";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 7)
            {
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "BOTTOM L";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 8)
            {
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "BOTTOM";
            }
            else if (Settings.Default.SNAPLINE_Starting_Point == 9)
            {
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                labelSNAPLINEStartingPoint.Text = "BOTTOM R";
            }

            //STYLES
            label3.Text = Settings.Default.ESP_Color.Name;
            label90.Text = Settings.Default.ESP_ALLIES_COLOR.Name;
            label88.Text = Settings.Default.ESP_FILLED_BORDER_COLOR.Name;
            label22.Text = Settings.Default.SNAPLINE_Color.Name;
            label47.Text = Settings.Default.CROSSHAIR_Color.Name;
            label78.Text = Settings.Default.AIMBOT_FOV_Color.Name;
            label103.Text = Settings.Default.Menu_Border_Color.Name;
            label66.Text = Settings.Default.Player_Name_Color.Name;
            label110.Text = Settings.Default.Player_Ping_Color.Name;

            metroSetEllipse1.NormalColor = Settings.Default.ESP_Color;
            metroSetEllipse1.HoverColor = Settings.Default.ESP_Color;

            metroSetEllipse11.NormalColor = Settings.Default.ESP_ALLIES_COLOR;
            metroSetEllipse11.HoverColor = Settings.Default.ESP_ALLIES_COLOR;

            metroSetEllipse10.NormalColor = Settings.Default.ESP_FILLED_BORDER_COLOR;
            metroSetEllipse10.HoverColor = Settings.Default.ESP_FILLED_BORDER_COLOR;

            metroSetEllipseSNAPLINEColor.NormalColor = Settings.Default.SNAPLINE_Color;
            metroSetEllipseSNAPLINEColor.HoverColor = Settings.Default.SNAPLINE_Color;

            metroSetEllipse5.NormalColor = Settings.Default.CROSSHAIR_Color;
            metroSetEllipse5.HoverColor = Settings.Default.CROSSHAIR_Color;

            metroSetEllipse6.NormalColor = Settings.Default.AIMBOT_FOV_Color;
            metroSetEllipse6.HoverColor = Settings.Default.AIMBOT_FOV_Color;

            metroSetEllipse15.NormalColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse15.HoverColor = Settings.Default.Menu_Border_Color;

            metroSetEllipse16.NormalColor = Settings.Default.Player_Name_Color;
            metroSetEllipse16.HoverColor = Settings.Default.Player_Name_Color;

            metroSetEllipse18.NormalColor = Settings.Default.Player_Ping_Color;
            metroSetEllipse18.HoverColor = Settings.Default.Player_Ping_Color;

            //MISC
            if (Settings.Default.CROSSHAIR == false)
            {
                customCheckBox6.Ovalchecked = false;
            }
            else
            {
                customCheckBox6.Ovalchecked = true;
            }

            if (Settings.Default.VSAT == false)
            {
                customCheckBox9.Ovalchecked = false;
            }
            else
            {
                customCheckBox9.Ovalchecked = true;
            }

            if (Settings.Default.Show_Text_Border == true && Security.pfpf == 1)
            {
                customCheckBox13.Ovalchecked = true;
                label98.Text = "Showed";
            }
            else
            {
                customCheckBox13.Ovalchecked = false;
                label98.Text = "Hidden";
            }

            metroSetTrackBar2.Value = Settings.Default.CROSSHAIR_Size;
            label52.Text = Settings.Default.CROSSHAIR_Size.ToString();

            metroSetTrackBar5.Value = Settings.Default.CROSSHAIR_Thickness;
            label55.Text = Settings.Default.CROSSHAIR_Thickness.ToString();

            metroSetTrackBar3.Value = Settings.Default.ESP_SIZE_TEXT;
            label82.Text = Settings.Default.ESP_SIZE_TEXT.ToString();

            label114.Text = Settings.Default.FontText;

            //CONTROLLER
            label117.Text = Settings.Default.ControllerButtonAIMBOT.ToString();
            label118.Text = Settings.Default.ControllerToggleAIMBOT.ToString();
            label95.Text = Settings.Default.ControllerToggleESP.ToString();
            label121.Text = Settings.Default.ControllerToggleSNAPLINE.ToString();
            label125.Text = Settings.Default.ControllerToggleVSAT.ToString();
            label129.Text = Settings.Default.ControllerToggleCROSSHAIR.ToString();

            //SETTING
            metroSetTrackBar7.Value = Settings.Default.FPS;
            label67.Text = metroSetTrackBar7.Value.ToString();
            if (metroSetTrackBar7.Value >= 999)
            {
                label67.Text = "Max";
            }
            label16.Text = Settings.Default.Key_Show_MENU.ToString();

            if (Settings.Default.OVERLAY == false)
            {
                customCheckBox3.Ovalchecked = false;
                label29.Text = "Hidden";
            }
            else
            {
                customCheckBox3.Ovalchecked = true;
                label29.Text = "Showed";
            }

            if (Settings.Default.Show_FPS == false)
            {
                customCheckBox8.Ovalchecked = false;
                label69.Text = "Hidden";
            }
            else
            {
                customCheckBox8.Ovalchecked = true;
                label69.Text = "Showed";
            }

            if (Settings.Default.VSYNC == false)
            {
                customCheckBox12.Ovalchecked = false;
                label76.Text = "Disabled";
            }
            else
            {
                customCheckBox12.Ovalchecked = true;
                label76.Text = "Enabled";
            }

            if (Settings.Default.FPS_CORNER == 1 && (Security.pfpf == 1))
            {
                metroSetEllipse9.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipse14.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse12.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse13.NormalColor = Settings.Default.Default_Color;
                label101.Text = "TOP L";
            }
            else if (Settings.Default.FPS_CORNER == 2 && (Security.pfpf == 1))
            {
                metroSetEllipse14.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipse9.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse12.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse13.NormalColor = Settings.Default.Default_Color;
                label101.Text = "TOP R";
            }
            else if (Settings.Default.FPS_CORNER == 3 && (Security.pfpf == 1))
            {
                metroSetEllipse12.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipse9.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse14.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse13.NormalColor = Settings.Default.Default_Color;
                label101.Text = "BOT L";
            }
            else if (Settings.Default.FPS_CORNER == 4 && (Security.pfpf == 1))
            {
                metroSetEllipse13.NormalColor = Settings.Default.Menu_Border_Color;

                metroSetEllipse9.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse14.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse12.NormalColor = Settings.Default.Default_Color;
                label101.Text = "BOT R";
            }

            metroSetTrackBar9.Value = (int)Settings.Default.Menu_Opacity;

            label112.Text = metroSetTrackBar9.Value.ToString();
            if (metroSetTrackBar9.Value >= 100)
            {
                label112.Text = "Max";
            }

            if (Settings.Default.TextAntiAliasing == false)
            {
                customCheckBox1.Ovalchecked = false;
                label92.Text = "Disabled";
            }
            else
            {
                customCheckBox1.Ovalchecked = true;
                label92.Text = "Enabled";
            }

            metroSetComboBox1.Items.Clear();
            foreach (string Dir in Directory.GetDirectories("profiles"))
            {
                FileInfo result = new FileInfo(Dir);

                if (result.Name.Length > 8)
                {
                    continue;
                }

                metroSetComboBox1.Items.Add(result.Name);
            }
            metroSetComboBox1.Items.Add("New..");
            metroSetComboBox1.Text = Settings.Default.Profiles;

            label81.Text = Settings.Default.Profiles;

            //END --------READ CONFIG--------


            //--------THEME--------
            BackColor = Settings.Default.Menu_Border_Color;

            //HOME
            label65.ForeColor = Settings.Default.Menu_Border_Color;
            label1.ForeColor = Settings.Default.Menu_Border_Color;
            label80.ForeColor = Settings.Default.Menu_Border_Color;

            button2.ForeColor = Settings.Default.Menu_Border_Color;
            button4.ForeColor = Settings.Default.Menu_Border_Color;

            //AIMBOT
            CheckBoxAIMBOT.LabelColor = Settings.Default.Menu_Border_Color;
            CheckBoxAIMBOT.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            CheckBoxAIMBOT.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            CheckBoxAIMBOT.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            metroSetTrackBar4.ValueColor = Settings.Default.Menu_Border_Color;
            metroSetTrackBar6.ValueColor = Settings.Default.Menu_Border_Color;
            metroSetTrackBar8.ValueColor = Settings.Default.Menu_Border_Color;

            button16.ForeColor = Settings.Default.Menu_Border_Color;
            button18.ForeColor = Settings.Default.Menu_Border_Color;
            button19.ForeColor = Settings.Default.Menu_Border_Color;
            button20.ForeColor = Settings.Default.Menu_Border_Color;

            metroSetEllipse2.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse3.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse4.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse7.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse8.HoverColor = Settings.Default.Menu_Border_Color;

            customCheckBox7.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox7.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox7.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox7.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            customCheckBox14.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox14.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox14.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox14.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            customCheckBox4.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox4.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox4.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox4.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            //ESP
            CheckBoxESP.LabelColor = Settings.Default.Menu_Border_Color;
            CheckBoxESP.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            CheckBoxESP.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            CheckBoxESP.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox10.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox10.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox10.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox10.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox11.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox11.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox11.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox11.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox2.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox2.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox2.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox2.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox5.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox5.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox5.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox5.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            metroSetTrackBar1.ValueColor = Settings.Default.Menu_Border_Color;

            button1.ForeColor = Settings.Default.Menu_Border_Color;
            button5.ForeColor = Settings.Default.Menu_Border_Color;
            button6.ForeColor = Settings.Default.Menu_Border_Color;

            //SNAPLINE
            CheckBoxSnapLine.LabelColor = Settings.Default.Menu_Border_Color;
            CheckBoxSnapLine.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            CheckBoxSnapLine.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            CheckBoxSnapLine.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            metroSetEllipseSNAPLINE_TOP_LEFT.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_CENTER_TOP.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_TOP_RIGHT.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_CENTER_LEFT.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_CENTER.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_CENTER_RIGHT.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_BOTTOM_LEFT.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_CENTER_BOT.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipseSNAPLINE_BOTTOM_RIGHT.HoverColor = Settings.Default.Menu_Border_Color;

            metroSetTrackBarSNAPLINESize.ValueColor = Settings.Default.Menu_Border_Color;

            //STYLE
            button27.ForeColor = Settings.Default.Menu_Border_Color;
            button28.ForeColor = Settings.Default.Menu_Border_Color;
            button29.ForeColor = Settings.Default.Menu_Border_Color;

            //MISC
            customCheckBox6.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox6.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox6.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox6.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox9.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox9.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox9.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox9.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox13.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox13.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox13.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox13.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            metroSetTrackBar2.ValueColor = Settings.Default.Menu_Border_Color;
            metroSetTrackBar5.ValueColor = Settings.Default.Menu_Border_Color;
            metroSetTrackBar3.ValueColor = Settings.Default.Menu_Border_Color;

            button22.ForeColor = Settings.Default.Menu_Border_Color;
            button23.ForeColor = Settings.Default.Menu_Border_Color;
            button24.ForeColor = Settings.Default.Menu_Border_Color;
            button37.ForeColor = Settings.Default.Menu_Border_Color;

            //CONTROLLER
            label109.ForeColor = Settings.Default.Menu_Border_Color;

            button44.ForeColor = Settings.Default.Menu_Border_Color;
            button41.ForeColor = Settings.Default.Menu_Border_Color;
            button42.ForeColor = Settings.Default.Menu_Border_Color;
            button43.ForeColor = Settings.Default.Menu_Border_Color;
            button50.ForeColor = Settings.Default.Menu_Border_Color;
            button53.ForeColor = Settings.Default.Menu_Border_Color;
            button48.ForeColor = Settings.Default.Menu_Border_Color;
            button45.ForeColor = Settings.Default.Menu_Border_Color;
            button46.ForeColor = Settings.Default.Menu_Border_Color;
            button47.ForeColor = Settings.Default.Menu_Border_Color;
            button49.ForeColor = Settings.Default.Menu_Border_Color;
            button51.ForeColor = Settings.Default.Menu_Border_Color;

            button35.ForeColor = Settings.Default.Menu_Border_Color;
            button38.ForeColor = Settings.Default.Menu_Border_Color;
            button39.ForeColor = Settings.Default.Menu_Border_Color;
            button40.ForeColor = Settings.Default.Menu_Border_Color;

            //SETTINGS
            customCheckBox8.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox8.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox8.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox8.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox12.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox12.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox12.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox12.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox3.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox3.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox3.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox3.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox1.LabelColor = Settings.Default.Menu_Border_Color;
            customCheckBox1.ovalcolorFalse = Settings.Default.Menu_Border_Color;
            customCheckBox1.ovalcolorTrue = Settings.Default.Menu_Border_Color;
            customCheckBox1.RectangleborderovalcolorTrue = Settings.Default.Menu_Border_Color;

            metroSetEllipse9.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse12.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse13.HoverColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse14.HoverColor = Settings.Default.Menu_Border_Color;

            metroSetTrackBar7.ValueColor = Settings.Default.Menu_Border_Color;
            metroSetTrackBar9.ValueColor = Settings.Default.Menu_Border_Color;

            button15.ForeColor = Settings.Default.Menu_Border_Color;
            button21.ForeColor = Settings.Default.Menu_Border_Color;
            button30.ForeColor = Settings.Default.Menu_Border_Color;
            button31.ForeColor = Settings.Default.Menu_Border_Color;
            button11.ForeColor = Settings.Default.Menu_Border_Color;
            button12.ForeColor = Settings.Default.Menu_Border_Color;
            button13.ForeColor = Settings.Default.Menu_Border_Color;
            button32.ForeColor = Settings.Default.Menu_Border_Color;
            button33.ForeColor = Settings.Default.Menu_Border_Color;
            button34.ForeColor = Settings.Default.Menu_Border_Color;

            metroSetComboBox1.ArrowColor = Settings.Default.Menu_Border_Color;
            metroSetComboBox1.BorderColor = Settings.Default.Menu_Border_Color;
            metroSetComboBox1.ForeColor = Settings.Default.Menu_Border_Color;
            metroSetComboBox1.DisabledBackColor = Settings.Default.Menu_Border_Color;
            metroSetComboBox1.DisabledBorderColor = Settings.Default.Menu_Border_Color;
            metroSetComboBox1.DisabledForeColor = Settings.Default.Menu_Border_Color;
            metroSetComboBox1.SelectedItemForeColor = Settings.Default.Menu_Border_Color;

            textBox1.ForeColor = Settings.Default.Menu_Border_Color;
            //END --------THEME--------

            //CHECK PRM
            if (Security.pfpf == 1)
            {
                ACB.Visible = true;
                label42.Visible = true;
                label42.Enabled = true;
                label75.Visible = true;
                label75.Enabled = true;
                customCheckBox2.Visible = true;
                customCheckBox2.Enabled = true;
                customCheckBox5.Visible = true;
                customCheckBox5.Enabled = true;
                customCheckBox11.Visible = true;
                customCheckBox11.Enabled = true;
                label41.Visible = true;
                label41.Enabled = true;
                label74.Visible = true;
                label74.Enabled = true;
                label96.Visible = true;
                label96.Enabled = true;
                label97.Visible = true;
                label97.Enabled = true;
                panel2.Visible = true;
                panel2.Enabled = true;
                metroSetComboBox1.Visible = true;
                return;
            }
            else
            {
                Security.FAILACC();
            }
        }

        private void StartThreads()
        {
            //start thread flag
            Examples.Example.isRunning = true;

            //start thread for positioning and sizing overlay on top of target process
            windowPosThread = new Thread(UpdateWindow);
            windowPosThread.IsBackground = false;
            windowPosThread.Start(Handle);
        }

        private void UpdateWindow(object handle)
        {
            //update flag, make sure game is still running
            while (Examples.Example.isRunning)
            {

                if (!Memory.IsProcessRunning(Examples.Example.process))
                {
                    Examples.Example.isRunning = false;
                    Security.FAILACC();
                    continue;
                }

                //ensure we are in focus and on top of game
                SetOverlayPosition((IntPtr)handle);

                //sleep for a bit, we don't need to move around constantly
                Thread.Sleep(Examples.Example.refreshWindows);
            }
        }

        private void SetOverlayPosition(IntPtr overlayHandle)
        {

            //get window handle
            IntPtr gameProcessHandle = Examples.Example.process.MainWindowHandle;
            if (gameProcessHandle == IntPtr.Zero)
                return;

            //get position and size of window
            NativeMethods.RECT targetWindowPosition, targetWindowSize;
            if (!NativeMethods.GetWindowRect(gameProcessHandle, out targetWindowPosition))
                return;
            if (!NativeMethods.GetClientRect(gameProcessHandle, out targetWindowSize))
                return;

            //calculate width and height of full target window
            int width = targetWindowPosition.Right - targetWindowPosition.Left;
            int height = targetWindowPosition.Bottom - targetWindowPosition.Top;

            //calculate inner window size without borders      
            int bWidth = targetWindowPosition.Right - targetWindowPosition.Left;
            int bHeight = targetWindowPosition.Bottom - targetWindowPosition.Top;

            //check if window has borders
            int dwStyle = NativeMethods.GetWindowLong(gameProcessHandle, NativeMethods.GWL_STYLE);
            if ((dwStyle & NativeMethods.WS_BORDER) != 0)
            {

                width = targetWindowSize.Right - targetWindowSize.Left;
                height = targetWindowSize.Bottom - targetWindowSize.Top;

                int borderWidth = (bWidth - targetWindowSize.Right) / 2;
                int borderHeight = (bHeight - targetWindowSize.Bottom);
                borderHeight -= borderWidth; //remove bottom

                targetWindowPosition.Left += borderWidth;
                targetWindowPosition.Top += borderHeight;

                //Return function only if windows border exceeds the limit of the game
                if ((Location.X > targetWindowPosition.Right - Size.Width - borderWidth)
                    || Location.X < targetWindowPosition.Left
                    || Location.Y < targetWindowPosition.Top
                    || Location.Y > targetWindowPosition.Bottom - Size.Height - borderWidth)
                {
                    //move and resize self window to match target window
                    NativeMethods.MoveWindow(overlayHandle, (width / 2 + targetWindowPosition.Left) - Width / 2, (height / 2 + targetWindowPosition.Top) - Height / 2, Size.Width, Size.Height, true);
                }

                //Return function only if the windows doesn't have moved
                if (targetWindowPosition.Left == Examples.Example._oldtargetWindowPositionLeft
                        && targetWindowPosition.Top == Examples.Example._oldtargetWindowPositionTop
                        && Examples.Example.gameWidth == width
                        && Examples.Example.gameHeight == height)
                {
                    return;
                }

                //move and resize self window to match target window
                NativeMethods.MoveWindow(overlayHandle, (width / 2 + targetWindowPosition.Left) - Width / 2, (height / 2 + targetWindowPosition.Top) - Height / 2, Size.Width, Size.Height, true);
            }
        }

        private void AssaultHack_FormClosing(object sender, FormClosingEventArgs e)
        {
            try

            {

                windowPosThread.Abort(200);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Dispose(true);
                Close();
            }
            catch { }
            //kill threads
        }

        private void CustomCheckBoxSnapLine_Ovalclick()
        {

            if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
            {
                Settings.Default.SNAPLINE = true;
                label21.Text = "Showed";
            }
            else
            {
                Settings.Default.SNAPLINE = false;
                label21.Text = "Hidden";
            }
            INIConfig.Write("Enable_SNAPLINE", Convert.ToString(Settings.Default.SNAPLINE), "ESP");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Security.FAILACC();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                windowPosThread.Abort(200);
                Examples.Example.Menu_Showed = false;
                Close();
            }
            catch { }
            //kill threads

        }

        private void metroSetTrackBar1_Scroll(object sender)
        {

            Settings.Default.ESP_Size = metroSetTrackBar1.Value;
            labelESPSIZE.Text = metroSetTrackBar1.Value.ToString();
            INIConfig.Write("ESP_Size", Convert.ToString(Settings.Default.ESP_Size), "ESP");
        }

        private void metroSetTrackBar2_Scroll(object sender)
        {

            Settings.Default.SNAPLINE_Size = metroSetTrackBarSNAPLINESize.Value;
            labelSNAPLINESIZE.Text = metroSetTrackBarSNAPLINESize.Value.ToString();

            INIConfig.Write("Snapline_Size", Convert.ToString(Settings.Default.SNAPLINE_Size), "ESP");
        }

        private void metroSetEllipse1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.ESP_Color = colorDialog1.Color;

            label3.Text = Settings.Default.ESP_Color.Name;

            metroSetEllipse1.NormalColor = colorDialog1.Color;
            metroSetEllipse1.HoverColor = colorDialog1.Color;

            INIConfig.Write("Enemies_Color", ConvertColor.ConvertToString(Settings.Default.ESP_Color.ToArgb()), "STYLES");
        }

        private void metroSetEllipse2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.SNAPLINE_Color = colorDialog1.Color;

            label22.Text = Settings.Default.SNAPLINE_Color.Name;

            metroSetEllipseSNAPLINEColor.NormalColor = colorDialog1.Color;
            metroSetEllipseSNAPLINEColor.HoverColor = colorDialog1.Color;

            INIConfig.Write("Snapline_Color", ConvertColor.ConvertToString(Settings.Default.SNAPLINE_Color.ToArgb()), "STYLES");
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            button.ForeColor = Color.FromArgb(65, 177, 225);

        }

        private void button_MouseLeave(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            button.ForeColor = Settings.Default.Menu_Border_Color;

        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (Settings.Default.ESP_Form == 1)
            {
                button7.BackgroundImage = Resources.SquareFULL;
                label10.Text = "Ellipse";
                Settings.Default.ESP_Form = 2;
            }
            else if (Settings.Default.ESP_Form == 2)
            {
                button7.BackgroundImage = Resources.Ellipse;
                label10.Text = "Filled Rect";
                Settings.Default.ESP_Form = 3;
            }
            else if (Settings.Default.ESP_Form == 3)
            {
                button7.BackgroundImage = Resources.Square;
                label10.Text = "Rectangle";
                Settings.Default.ESP_Form = 1;
            }
            INIConfig.Write("ESP_Form", Convert.ToString(Settings.Default.ESP_Form), "ESP");
        }

        private void customCheckBox1_Ovalclick()
        {

            //Security.CVP2();

            if (Settings.Default.ESP == false)
            {
                Settings.Default.ESP = true;
            }
            else
            {
                Settings.Default.ESP = false;
            }

            INIConfig.Write("Enable_ESP", Convert.ToString(Settings.Default.ESP), "ESP");

        }

        private void metroSetEllipseSNAPLINEStartingPoint_Click(object sender, EventArgs e)
        {

            MetroSet_UI.Controls.MetroSetEllipse metroSetEllipse = (MetroSet_UI.Controls.MetroSetEllipse)sender;

            if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_TOP_LEFT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 1;
                labelSNAPLINEStartingPoint.Text = "TOP LEFT";

                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_CENTER_TOP.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 2;
                labelSNAPLINEStartingPoint.Text = "CENTER T";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_TOP_RIGHT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 3;
                labelSNAPLINEStartingPoint.Text = "TOP RIGHT";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_CENTER_LEFT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 4;
                labelSNAPLINEStartingPoint.Text = "CENTER L";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;

                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_CENTER.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 5;
                labelSNAPLINEStartingPoint.Text = "CENTER";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;

                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_CENTER_RIGHT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 6;
                labelSNAPLINEStartingPoint.Text = "CENTER R";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;

                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_BOTTOM_LEFT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 7;
                labelSNAPLINEStartingPoint.Text = "BOTTOM L";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;

                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_CENTER_BOT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 8;
                labelSNAPLINEStartingPoint.Text = "CENTER B";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_RIGHT.NormalColor = Settings.Default.Default_Color;

                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }
            else if (metroSetEllipse.Name == metroSetEllipseSNAPLINE_BOTTOM_RIGHT.Name)
            {
                Settings.Default.SNAPLINE_Starting_Point = 9;
                labelSNAPLINEStartingPoint.Text = "BOTTOM R";

                metroSetEllipseSNAPLINE_TOP_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_TOP_RIGHT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_TOP.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_BOTTOM_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_BOT.NormalColor = Settings.Default.Default_Color;

                metroSetEllipseSNAPLINE_CENTER.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_LEFT.NormalColor = Settings.Default.Default_Color;
                metroSetEllipseSNAPLINE_CENTER_RIGHT.NormalColor = Settings.Default.Default_Color;
            }

            metroSetEllipse.NormalColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse.HoverColor = Settings.Default.Menu_Border_Color;

            INIConfig.Write("Snapline_Starting_Point", Convert.ToString(Settings.Default.SNAPLINE_Starting_Point), "ESP");
        }

        private async Task GetKeyController()
        {
            KeysConverter kc = new KeysConverter(); //Convert to int val to string val

            if (started == true)
            {
                return;
            }

            started = true;

            while (true)
            {
                await Task.Delay(1);

                //CONTROLLER AIMBOT
                if (Settings.Default.Active_Func_Key == 1)
                {
                    if (Controller.ActionControllerButtonClicked == true)
                    {
                        label117.Text = kc.ConvertToString(Settings.Default.ControllerButtonAIMBOT);
                        button44.Text = "Change";
                        INIConfig.Write("AIM_Key", kc.ConvertToString(Settings.Default.ControllerButtonAIMBOT), "CONTROLLER");

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        Controller.ActionControllerButtonClicked = false;
                        return;
                    }
                }
                //-----CONTROLLER AIMBOT END-----

                //CONTROLLER TOGGLE AIMBOT
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    if (Controller.ActionControllerButtonClicked == true)
                    {
                        label118.Text = kc.ConvertToString(Settings.Default.ControllerToggleAIMBOT);
                        button41.Text = "Change";
                        INIConfig.Write("TOGGLE_AIMBOT", kc.ConvertToString(Settings.Default.ControllerToggleAIMBOT), "CONTROLLER");

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        Controller.ActionControllerButtonClicked = false;
                        return;
                    }
                }
                //-----CONTROLLER TOGGLE AIMBOT END-----

                //CONTROLLER TOGGLE ESP
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    if (Controller.ActionControllerButtonClicked == true)
                    {
                        label95.Text = kc.ConvertToString(Settings.Default.ControllerToggleESP);
                        button42.Text = "Change";
                        INIConfig.Write("TOGGLE_ESP", kc.ConvertToString(Settings.Default.ControllerToggleESP), "CONTROLLER");

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        Controller.ActionControllerButtonClicked = false;
                        return;
                    }
                }
                //-----CONTROLLER TOGGLE ESP END-----

                //CONTROLLER TOGGLE SNAPLINE
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    if (Controller.ActionControllerButtonClicked == true)
                    {
                        label121.Text = kc.ConvertToString(Settings.Default.ControllerToggleSNAPLINE);
                        button43.Text = "Change";
                        INIConfig.Write("TOGGLE_SNAPLINE", kc.ConvertToString(Settings.Default.ControllerToggleSNAPLINE), "CONTROLLER");

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        Controller.ActionControllerButtonClicked = false;
                        return;
                    }
                }
                //-----CONTROLLER TOGGLE SNAPLINE END-----

                //CONTROLLER TOGGLE VSAT
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    if (Controller.ActionControllerButtonClicked == true)
                    {
                        label125.Text = kc.ConvertToString(Settings.Default.ControllerToggleVSAT);
                        button50.Text = "Change";
                        INIConfig.Write("TOGGLE_VSAT", kc.ConvertToString(Settings.Default.ControllerToggleVSAT), "CONTROLLER");

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        Controller.ActionControllerButtonClicked = false;
                        return;
                    }
                }
                //-----CONTROLLER TOGGLE VSAT END-----

                //CONTROLLER TOGGLE CROSSHAIR
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    if (Controller.ActionControllerButtonClicked == true)
                    {
                        label129.Text = kc.ConvertToString(Settings.Default.ControllerToggleCROSSHAIR);
                        button53.Text = "Change";
                        INIConfig.Write("TOGGLE_CROSSHAIR", kc.ConvertToString(Settings.Default.ControllerToggleCROSSHAIR), "CONTROLLER");

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        Controller.ActionControllerButtonClicked = false;
                        return;
                    }
                }
                //-----CONTROLLER TOGGLE CROSSHAIR END-----

            }
        }

        private async Task GetKeybinding()
        {

            KeysConverter kc = new KeysConverter(); //Convert to int val to string val

            if (started == true)

            {
                return;
            }

            started = true;
            while (true)
            {
                await Task.Delay(1);

                for (int i = 0; i <= 999; i++)
                {

                    int keypressed = GetAsyncKeyState((Keys)i);
                    if (keypressed == -32767)
                    {
                        if (Settings.Default.Active_Func_Key == 1)
                        {
                            Settings.Default.AIM_Key = kc.ConvertToString(i);
                            label43.Text = kc.ConvertToString(i);
                            button16.Text = "Change";
                            INIConfig.Write("AIM_Key", kc.ConvertToString(Settings.Default.AIM_Key), "AIMBOT");

                            if (customCheckBox4.Enabled == true && (Security.pfpf == 1) && Settings.Default.AIM_Key.ToString().ToLower() == "LButton".ToLower())
                            {
                                customCheckBox4.Ovalchecked = false;
                                customCheckBox4.Enabled = false;
                                label31.Text = "Disabled";
                            }
                            else
                            {
                                if (Settings.Default.AIM_Auto_Fire == true)
                                {
                                    customCheckBox4.Ovalchecked = true;
                                    customCheckBox4.Enabled = true;
                                    label31.Text = "Enabled";
                                }
                                else if (customCheckBox4.Enabled == false)
                                {
                                    customCheckBox4.Enabled = true;
                                    label31.Text = "Disabled";
                                }
                            }
                        }
                        else if (Settings.Default.Active_Func_Key == 2)
                        {
                            Settings.Default.Key_Show_MENU = (Keys)i;
                            label16.Text = kc.ConvertToString(i);
                            button15.Text = "Change";
                            INIConfig.Write("Show_Menu", kc.ConvertToString(Settings.Default.Key_Show_MENU), "SETTINGS");
                        }

                        if (keypressed == -1)
                        {
                            Settings.Default.Active_Func_Key = 0;
                            started = false;
                            return;
                        }

                        Settings.Default.Active_Func_Key = 0;
                        started = false;
                        return;
                    }
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 2;
            button15.Text = "Press...";
            GetKeybinding();
        }

        private void CustomCheckBox3_Click()
        {

            if (Settings.Default.OVERLAY == false)
            {
                Settings.Default.OVERLAY = true;
                label29.Text = "Showed";
            }
            else
            {
                Settings.Default.OVERLAY = false;
                label29.Text = "Hidden";
            }

            INIConfig.Write("Show_Overlay", Convert.ToString(Settings.Default.OVERLAY), "SETTINGS");

        }

        private void customCheckBox2_Ovalclick()
        {
            if (Settings.Default.Show_DISTANCE == false && Security.pfpf == 1)
            {
                Settings.Default.Show_DISTANCE = true;
                label41.Text = "Showed";
            }
            else
            {
                Settings.Default.Show_DISTANCE = false;
                label41.Text = "Hidden";
            }
            INIConfig.Write("Show_Distance", Convert.ToString(Settings.Default.Show_DISTANCE), "ESP");
        }

        private void customCheckBox1_Ovalclick_1()
        {

            //Security.CVP2();

            if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
            {
                Settings.Default.AIMBOT = true;
                INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
            }
            else
            {
                Settings.Default.AIMBOT = false;
                INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 1;
            button16.Text = "Press...";
            GetKeybinding();
        }

        private void customCheckBox4_Ovalclick()
        {

            if (Settings.Default.AIM_Auto_Fire == false && (Security.pfpf == 1) && Settings.Default.AIM_Key.ToString().ToLower() != "LButton".ToLower())
            {
                Settings.Default.AIM_Auto_Fire = true;
                label31.Text = "Enabled";
            }
            else
            {
                Settings.Default.AIM_Auto_Fire = false;
                label31.Text = "Disabled";
            }

            INIConfig.Write("Auto_Fire", Convert.ToString(Settings.Default.AIM_Auto_Fire), "AIMBOT");

        }

        private void customCheckBox6_Ovalclick()
        {

            if (Settings.Default.CROSSHAIR == false)
            {
                Settings.Default.CROSSHAIR = true;
            }
            else
            {
                Settings.Default.CROSSHAIR = false;
            }

            INIConfig.Write("Enable_CROSSHAIR", Convert.ToString(Settings.Default.CROSSHAIR), "MISC");

        }

        private void metroSetTrackBar2_Scroll_1(object sender)
        {
            Settings.Default.CROSSHAIR_Size = metroSetTrackBar2.Value;
            label52.Text = metroSetTrackBar2.Value.ToString();
            INIConfig.Write("Crosshair_Size", Convert.ToString(Settings.Default.CROSSHAIR_Size), "MISC");
        }

        private void metroSetTrackBar5_Scroll(object sender)
        {
            Settings.Default.CROSSHAIR_Thickness = metroSetTrackBar5.Value;
            label55.Text = metroSetTrackBar5.Value.ToString();
            INIConfig.Write("Crosshair_Thickness", Convert.ToString(Settings.Default.CROSSHAIR_Thickness), "MISC");
        }

        private void metroSetEllipse5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.CROSSHAIR_Color = colorDialog1.Color;

            label47.Text = Settings.Default.CROSSHAIR_Color.Name;

            metroSetEllipse5.NormalColor = colorDialog1.Color;
            metroSetEllipse5.HoverColor = colorDialog1.Color;

            INIConfig.Write("Crosshair_Color", ConvertColor.ConvertToString(Settings.Default.CROSSHAIR_Color.ToArgb()), "STYLES");
        }

        private void metroSetEllipse4_Click(object sender, EventArgs e)
        {

            MetroSet_UI.Controls.MetroSetEllipse metroSetEllipse = (MetroSet_UI.Controls.MetroSetEllipse)sender;

            if (metroSetEllipse.Name == metroSetEllipse4.Name)
            {
                Settings.Default.AIMOT_Target = 1;
                Aimbot.TARGET = Aimbot.HEAD;
                label34.Text = "HEAD";

                metroSetEllipse2.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse3.NormalColor = Settings.Default.Default_Color;
            }

            else if (metroSetEllipse.Name == metroSetEllipse2.Name)
            {
                Settings.Default.AIMOT_Target = 2;
                Aimbot.TARGET = Aimbot.BODY;
                label34.Text = "BODY";

                metroSetEllipse4.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse3.NormalColor = Settings.Default.Default_Color;
            }

            else if (metroSetEllipse.Name == metroSetEllipse3.Name)
            {
                Settings.Default.AIMOT_Target = 3;
                Aimbot.TARGET = Aimbot.FOOT;
                label34.Text = "FOOT";

                metroSetEllipse4.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse2.NormalColor = Settings.Default.Default_Color;
            }

            metroSetEllipse.NormalColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse.HoverColor = Settings.Default.Menu_Border_Color;
            INIConfig.Write("Aim_Target", Convert.ToString(Settings.Default.AIMOT_Target), "AIMBOT");
        }

        private void metroSetTrackBar4_Scroll(object sender)
        {
            Settings.Default.AIMBOT_Acceleration = metroSetTrackBar4.Value;
            label36.Text = Settings.Default.AIMBOT_Acceleration.ToString();
            INIConfig.Write("Acceleration", Convert.ToString(Settings.Default.AIMBOT_Acceleration), "AIMBOT");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes("profiles/" + Settings.Default.Profiles + "/config.ini", Resources.config);
            InitializationCONTROLS();
            label45.Text = "Done";
        }

        private void metroSetTrackBar6_Scroll(object sender)
        {
            Settings.Default.Aim_FOV = metroSetTrackBar6.Value;
            label57.Text = Settings.Default.Aim_FOV.ToString();
            INIConfig.Write("Aim_FOV", Convert.ToString(Settings.Default.Aim_FOV), "AIMBOT");
        }

        private void customCheckBox7_Ovalclick()
        {
            if (Settings.Default.Draw_Aim_Fov == false && (Security.pfpf == 1))
            {
                Settings.Default.Draw_Aim_Fov = true;
                label59.Text = "Enabled";
            }
            else
            {
                Settings.Default.Draw_Aim_Fov = false;
                label59.Text = "Disabled";
            }

            INIConfig.Write("Draw_Aim_Fov", Convert.ToString(Settings.Default.Draw_Aim_Fov), "AIMBOT");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Process.Start("http://reset.vecrproject.com");
        }

        private void button25_MouseEnter(object sender, EventArgs e)
        {

            Button button = sender as Button;

            if (button.Name == button2.Name)
            {
                button.BackgroundImage.Dispose();
                button.BackgroundImage = RL.Properties.Resources.Close02;
            }
            else if (button.Name == button4.Name)
            {
                button.BackgroundImage.Dispose();
                button.BackgroundImage = RL.Properties.Resources.NHIDE_02;
            }
            else if (button.Name == button25.Name)
            {
                button.BackgroundImage.Dispose();
                button.BackgroundImage = RL.Properties.Resources._256x256_ME;
            }
        }

        private void button25_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button.Name == button2.Name)
            {
                button.BackgroundImage.Dispose();
                button.BackgroundImage = RL.Properties.Resources.Close;
            }
            else if (button.Name == button4.Name)
            {
                button.BackgroundImage.Dispose();
                button.BackgroundImage = RL.Properties.Resources.NHIDE_01;
            }
            else if (button.Name == button25.Name)
            {
                button.BackgroundImage.Dispose();
                button.BackgroundImage = RL.Properties.Resources._256x256;
            }
        }

        private void metroSetTrackBar7_Scroll(object sender)
        {
            Settings.Default.FPS = metroSetTrackBar7.Value;
            label67.Text = metroSetTrackBar7.Value.ToString();
            if (metroSetTrackBar7.Value >= 999)
            {
                label67.Text = "Max";
            }
            INIConfig.Write("FPS", Convert.ToString(Settings.Default.FPS), "SETTINGS");
        }

        private void customCheckBox8_Ovalclick()
        {
            if (Settings.Default.Show_FPS == false)
            {
                Settings.Default.Show_FPS = true;
                label69.Text = "Showed";
            }
            else
            {
                Settings.Default.Show_FPS = false;
                label69.Text = "Hidden";
            }

            INIConfig.Write("Show_FPS", Convert.ToString(Settings.Default.Show_FPS), "SETTINGS");
        }

        private void customCheckBox9_Ovalclick()
        {
            if (Settings.Default.VSAT == false)
            {
                Settings.Default.VSAT = true;
            }
            else
            {
                Settings.Default.VSAT = false;
            }

            INIConfig.Write("Enable_VSAT", Convert.ToString(Settings.Default.VSAT), "MISC");
        }

        private void customCheckBox10_Ovalclick()
        {
            if (Settings.Default.Show_Allies == false)
            {
                Settings.Default.Show_Allies = true;
                label72.Text = "Showed";
            }
            else
            {
                Settings.Default.Show_Allies = false;
                label72.Text = "Hidden";
            }

            INIConfig.Write("Show_Allies", Convert.ToString(Settings.Default.Show_Allies), "ESP");
        }

        private void customCheckBox11_Ovalclick()
        {
            if (Settings.Default.Show_Name == false && Security.pfpf == 1)
            {
                Settings.Default.Show_Name = true;
                label74.Text = "Showed";
            }
            else
            {
                Settings.Default.Show_Name = false;
                label74.Text = "Hidden";
            }
            INIConfig.Write("Show_Name", Convert.ToString(Settings.Default.Show_Name), "ESP");
        }

        private void customCheckBox12_Ovalclick()
        {
            if (Settings.Default.VSYNC == false)
            {
                Settings.Default.VSYNC = true;

                if (label76.Text == "Restart required")
                {
                    label76.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                    label76.Text = "Enabled";
                }
                else
                {
                    label76.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    label76.Text = "Restart required";
                }
            }
            else
            {
                Settings.Default.VSYNC = false;

                if (label76.Text == "Restart required")
                {
                    label76.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                    label76.Text = "Disabled";
                }
                else
                {
                    label76.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    label76.Text = "Restart required";
                }
            }
            INIConfig.Write("VSYNC", Convert.ToString(Settings.Default.VSYNC), "SETTINGS");
        }

        private void metroSetEllipse8_Click(object sender, EventArgs e)
        {
            MetroSet_UI.Controls.MetroSetEllipse metroSetEllipse = (MetroSet_UI.Controls.MetroSetEllipse)sender;

            if (metroSetEllipse.Name == metroSetEllipse8.Name)
            {
                Settings.Default.Aim_Filter = 1;
                label12.Text = "CROSSHAIR";

                metroSetEllipse7.NormalColor = Settings.Default.Default_Color;
            }

            else if (metroSetEllipse.Name == metroSetEllipse7.Name)
            {
                Settings.Default.Aim_Filter = 2;
                label12.Text = "DISTANCE";

                metroSetEllipse8.NormalColor = Settings.Default.Default_Color;
            }

            metroSetEllipse.NormalColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse.HoverColor = Settings.Default.Menu_Border_Color;
            INIConfig.Write("Aim_Filter", Convert.ToString(Settings.Default.Aim_Filter), "AIMBOT");
        }

        private void metroSetEllipse6_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.AIMBOT_FOV_Color = colorDialog1.Color;

            label78.Text = Settings.Default.AIMBOT_FOV_Color.Name;

            metroSetEllipse6.NormalColor = colorDialog1.Color;
            metroSetEllipse6.HoverColor = colorDialog1.Color;

            INIConfig.Write("FOV_Color", ConvertColor.ConvertToString(Settings.Default.AIMBOT_FOV_Color.ToArgb()), "STYLES");
        }

        private void metroSetTrackBar3_Scroll(object sender)
        {
            Settings.Default.ESP_SIZE_TEXT = metroSetTrackBar3.Value;
            label82.Text = metroSetTrackBar3.Value.ToString();
            INIConfig.Write("Text_Size", Convert.ToString(Settings.Default.ESP_SIZE_TEXT), "MISC");
        }

        private void metroSetEllipse11_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.ESP_ALLIES_COLOR = colorDialog1.Color;

            label90.Text = Settings.Default.ESP_ALLIES_COLOR.Name;

            metroSetEllipse11.NormalColor = colorDialog1.Color;
            metroSetEllipse11.HoverColor = colorDialog1.Color;

            INIConfig.Write("Allies_Color", ConvertColor.ConvertToString(Settings.Default.ESP_ALLIES_COLOR.ToArgb()), "STYLES");
        }

        private void metroSetEllipse10_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.ESP_FILLED_BORDER_COLOR = colorDialog1.Color;

            label88.Text = Settings.Default.ESP_FILLED_BORDER_COLOR.Name;

            metroSetEllipse10.NormalColor = colorDialog1.Color;
            metroSetEllipse10.HoverColor = colorDialog1.Color;

            INIConfig.Write("Filled_Border_Color", ConvertColor.ConvertToString(Settings.Default.ESP_FILLED_BORDER_COLOR.ToArgb()), "STYLES");
        }

        private void button31_Click(object sender, EventArgs e)
        {

            if (button31.Text == "Load")
            {
                Settings.Default.Profiles = metroSetComboBox1.Text;
                INIConfig = new Clsini("profiles/" + Settings.Default.Profiles + "/config.ini");
                INIBestProfiles.Write("Best", ConvertColor.ConvertToString(metroSetComboBox1.Text), "STARTUP");
                InitializationCONTROLS();

                label45.Text = "Loaded";
            }
            else if (button31.Text == "Apply and Save")
            {
                Settings.Default.Profiles = textBox1.Text;
                INIConfig = new Clsini("profiles/" + Settings.Default.Profiles + "/config.ini");

                Directory.CreateDirectory("profiles/" + Settings.Default.Profiles);
                File.WriteAllBytes("profiles/" + Settings.Default.Profiles + "/config.ini", Resources.config);
                INIBestProfiles.Write("Best", ConvertColor.ConvertToString(textBox1.Text), "STARTUP");
                InitializationCONTROLS();

                label45.Text = "Saved";
                button31.Size = new Size(78, 24);
                button31.Text = "Load";
                metroSetComboBox1.Visible = true;
                button30.Visible = true;
                textBox1.Visible = false;
                textBox1.Clear();
            }
        }

        private void metroSetComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int last = metroSetComboBox1.Items.Count - 1;
            if (metroSetComboBox1.SelectedIndex == last)
            {
                metroSetComboBox1.Visible = false;
                button30.Visible = false;
                textBox1.Visible = true;
                button31.Size = new Size(164, 24);
                button31.Text = "Apply and Save";
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                string Dir = @"profiles\" + metroSetComboBox1.Text;
                Directory.Delete(Dir, true);
                InitializationCONTROLS();

                label45.Text = "Done";
            }
            catch { label45.Text = "Error"; }
        }

        private void customCheckBox5_Ovalclick()
        {
            if (Settings.Default.Show_PING == false && Security.pfpf == 1)
            {
                Settings.Default.Show_PING = true;
                label96.Text = "Showed";
            }
            else
            {
                Settings.Default.Show_PING = false;
                label96.Text = "Hidden";
            }
            INIConfig.Write("Show_Ping", Convert.ToString(Settings.Default.Show_PING), "ESP");
        }

        private void customCheckBox13_Ovalclick()
        {
            if (Settings.Default.Show_Text_Border == false && Security.pfpf == 1)
            {
                Settings.Default.Show_Text_Border = true;
                label98.Text = "Showed";
            }
            else
            {
                Settings.Default.Show_Text_Border = false;
                label98.Text = "Hidden";
            }
            INIConfig.Write("Show_Text_Border", Convert.ToString(Settings.Default.Show_Text_Border), "MISC");
        }

        private void metroSetEllipse15_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.Menu_Border_Color = colorDialog1.Color;

            label103.Text = Settings.Default.AIMBOT_FOV_Color.Name;

            metroSetEllipse15.NormalColor = colorDialog1.Color;
            metroSetEllipse15.HoverColor = colorDialog1.Color;

            INIConfig.Write("Menu_Color", ConvertColor.ConvertToString(Settings.Default.Menu_Border_Color.ToArgb()), "STYLES");

            InitializationCONTROLS();

        }

        private void metroSetEllipse9_Click(object sender, EventArgs e)
        {
            MetroSet_UI.Controls.MetroSetEllipse metroSetEllipse = (MetroSet_UI.Controls.MetroSetEllipse)sender;

            if (metroSetEllipse.Name == metroSetEllipse9.Name)
            {
                Settings.Default.FPS_CORNER = 1;
                label101.Text = "TOP L";

                metroSetEllipse12.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse13.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse14.NormalColor = Settings.Default.Default_Color;
            }

            if (metroSetEllipse.Name == metroSetEllipse14.Name)
            {
                Settings.Default.FPS_CORNER = 2;
                label101.Text = "TOP R";

                metroSetEllipse12.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse13.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse9.NormalColor = Settings.Default.Default_Color;
            }

            if (metroSetEllipse.Name == metroSetEllipse12.Name)
            {
                Settings.Default.FPS_CORNER = 3;
                label101.Text = "BOT L";

                metroSetEllipse9.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse13.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse14.NormalColor = Settings.Default.Default_Color;
            }

            if (metroSetEllipse.Name == metroSetEllipse13.Name)
            {
                Settings.Default.FPS_CORNER = 4;
                label101.Text = "BOT R";

                metroSetEllipse12.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse9.NormalColor = Settings.Default.Default_Color;
                metroSetEllipse14.NormalColor = Settings.Default.Default_Color;
            }

            metroSetEllipse.NormalColor = Settings.Default.Menu_Border_Color;
            metroSetEllipse.HoverColor = Settings.Default.Menu_Border_Color;
            INIConfig.Write("FPS_Corner", Convert.ToString(Settings.Default.FPS_CORNER), "SETTINGS");
        }

        private void metroSetTrackBar8_Scroll(object sender)
        {
            Settings.Default.AUTO_FIRE_MS = metroSetTrackBar8.Value;
            label105.Text = Settings.Default.AUTO_FIRE_MS.ToString() + " MS";
            INIConfig.Write("Auto_Fire_MS", Convert.ToString(Settings.Default.AUTO_FIRE_MS), "AIMBOT");
        }

        private void metroSetEllipse16_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.Player_Name_Color = colorDialog1.Color;

            label66.Text = Settings.Default.Player_Name_Color.Name;

            metroSetEllipse16.NormalColor = colorDialog1.Color;
            metroSetEllipse16.HoverColor = colorDialog1.Color;

            INIConfig.Write("Player_Name_Color", ConvertColor.ConvertToString(Settings.Default.Player_Name_Color.ToArgb()), "STYLES");
        }

        private void metroSetEllipse18_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            Settings.Default.Player_Ping_Color = colorDialog1.Color;

            label110.Text = Settings.Default.Player_Ping_Color.Name;

            metroSetEllipse18.NormalColor = colorDialog1.Color;
            metroSetEllipse18.HoverColor = colorDialog1.Color;

            INIConfig.Write("Player_Ping_Color", ConvertColor.ConvertToString(Settings.Default.Player_Ping_Color.ToArgb()), "STYLES");
        }

        private void metroSetTrackBar9_Scroll(object sender)
        {
            Settings.Default.Menu_Opacity = metroSetTrackBar9.Value;
            label112.Text = metroSetTrackBar9.Value.ToString();
            if (metroSetTrackBar9.Value >= 100)
            {
                label112.Text = "Max";
            }
            INIConfig.Write("Menu_Opacity", Convert.ToString(Settings.Default.Menu_Opacity), "SETTINGS");
            double Result = Convert.ToInt32(Settings.Default.Menu_Opacity) / (double)100;
            this.Opacity = Result;
        }

        private void Menu_Shown(object sender, EventArgs e)
        {
            double Result = Convert.ToInt32(Settings.Default.Menu_Opacity) / (double)100;
            this.Opacity = Result;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();

            Settings.Default.FontText = fontDialog1.Font.Name.ToString();
            INIConfig.Write("Font_Text", Convert.ToString(Settings.Default.FontText), "MISC");
            label114.Text = Settings.Default.FontText;

        }

        private void button41_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 3;
            button41.Text = "Press...";
            GetKeyController();
        }

        private void customCheckBox1_Ovalclick_2()
        {
            if (Settings.Default.TextAntiAliasing == false)
            {
                Settings.Default.TextAntiAliasing = true;

                if (label92.Text == "Restart required")
                {
                    label92.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                    label92.Text = "Enabled";
                }
                else
                {
                    label92.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    label92.Text = "Restart required";
                }
            }
            else
            {
                Settings.Default.TextAntiAliasing = false;

                if (label92.Text == "Restart required")
                {
                    label92.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                    label92.Text = "Disabled";
                }
                else
                {
                    label92.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    label92.Text = "Restart required";
                }
            }

            INIConfig.Write("Anti_Aliasing", Convert.ToString(Settings.Default.TextAntiAliasing), "SETTINGS");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 1;
            button44.Text = "Press...";
            GetKeyController();
        }

        private void buttonRemoveController_Click(object sender, EventArgs e)
        {
            KeysConverter kc = new KeysConverter(); //Convert to int val to string val

            Button button = (Button)sender;

            if (button48.Name == button.Name)
            {
                INIConfig.Write("AIM_Key", kc.ConvertToString("Unknown"), "CONTROLLER");
                Settings.Default.ControllerButtonAIMBOT = INIConfig.Read("AIM_Key", "CONTROLLER");
                label117.Text = kc.ConvertToString(Settings.Default.ControllerButtonAIMBOT);
            }
            else if (button45.Name == button.Name)
            {
                INIConfig.Write("TOGGLE_AIMBOT", kc.ConvertToString("Unknown"), "CONTROLLER");
                Settings.Default.ControllerToggleAIMBOT = INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER");
                label118.Text = kc.ConvertToString(Settings.Default.ControllerToggleAIMBOT);
            }
            else if (button46.Name == button.Name)
            {
                INIConfig.Write("TOGGLE_ESP", kc.ConvertToString("Unknown"), "CONTROLLER");
                Settings.Default.ControllerToggleESP = INIConfig.Read("TOGGLE_ESP", "CONTROLLER");
                label95.Text = kc.ConvertToString(Settings.Default.ControllerToggleAIMBOT);
            }
            else if (button47.Name == button.Name)
            {
                INIConfig.Write("TOGGLE_SNAPLINE", kc.ConvertToString("Unknown"), "CONTROLLER");
                Settings.Default.ControllerToggleSNAPLINE = INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER");
                label121.Text = kc.ConvertToString(Settings.Default.ControllerToggleSNAPLINE);
            }
            else if (button49.Name == button.Name)
            {
                INIConfig.Write("TOGGLE_VSAT", kc.ConvertToString("Unknown"), "CONTROLLER");
                Settings.Default.ControllerToggleVSAT = INIConfig.Read("TOGGLE_VSAT", "CONTROLLER");
                label125.Text = kc.ConvertToString(Settings.Default.ControllerToggleVSAT);
            }
            else if (button51.Name == button.Name)
            {
                INIConfig.Write("TOGGLE_CROSSHAIR", kc.ConvertToString("Unknown"), "CONTROLLER");
                Settings.Default.ControllerToggleCROSSHAIR = INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER");
                label129.Text = kc.ConvertToString(Settings.Default.ControllerToggleCROSSHAIR);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 4;
            button42.Text = "Press...";
            GetKeyController();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 5;
            button43.Text = "Press...";
            GetKeyController();
        }

        private void button50_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 6;
            button50.Text = "Press...";
            GetKeyController();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Settings.Default.Active_Func_Key = 7;
            button53.Text = "Press...";
            GetKeyController();
        }

        private void customCheckBox14_Ovalclick()
        {
            if (Settings.Default.Draw_Enemy_Close == false && Security.pfpf == 1)
            {
                Settings.Default.Draw_Enemy_Close = true;
                label23.Text = "Showed";
            }
            else
            {
                Settings.Default.Draw_Enemy_Close = false;
                label23.Text = "Hidden";
            }
            INIConfig.Write("Draw_Enemy_Close", Convert.ToString(Settings.Default.Draw_Enemy_Close), "AIMBOT");
        }
    }
}
