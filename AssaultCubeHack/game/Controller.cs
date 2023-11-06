using AssaultCubeHack;
using Com.Okmer.GameController;
using RL.Properties;
using System;
using VECRPROJECT.util;

namespace RL.game
{
    class Controller
    {
        //CONTROLLER
        public static XBoxController controller = new XBoxController();
        public static Controller CONTROLLER = new Controller();
        public static bool ActionControllerButtonClicked;

        public static bool ControllerActivated = false;
        public static bool ControllerConnected = false;
        public static int ControllerBattery = 0;


        public void startController()
        {
            //Connection
            controller.Connection.ValueChanged += (s, e) => ControllerConnected = e.Value;

            //Buttons A, B, X, Y
            #region //A BUTTON
            controller.A.ValueChanged += (s, e) =>
            {
                string buttonName = "A";
                bool controllerValue = (Convert.ToBoolean(controller.A.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //B BUTTON
            controller.B.ValueChanged += (s, e) =>
            {
                string buttonName = "B";
                bool controllerValue = (Convert.ToBoolean(controller.B.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //X BUTTON
            controller.X.ValueChanged += (s, e) =>
            {
                string buttonName = "X";
                bool controllerValue = (Convert.ToBoolean(controller.X.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //Y BUTTON
            controller.Y.ValueChanged += async (s, e) =>
            {
                string buttonName = "Y";
                bool controllerValue = (Convert.ToBoolean(controller.Y.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion

            ////Buttons Start, Back
            #region //START BUTTON
            controller.Start.ValueChanged += (s, e) =>
            {
                string buttonName = "START";
                bool controllerValue = (Convert.ToBoolean(controller.Start.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //SELECT BUTTON
            controller.Back.ValueChanged += (s, e) =>
            {
                string buttonName = "SELECT";
                bool controllerValue = (Convert.ToBoolean(controller.Back.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            ////Buttons D-Pad Up, Down, Left, Right
            #region //D-Pad Up BUTTON
            controller.Up.ValueChanged += (s, e) =>
            {
                string buttonName = "Up";
                bool controllerValue = (Convert.ToBoolean(controller.Up.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //D-Pad Down BUTTON
            controller.Down.ValueChanged += (s, e) =>
            {
                string buttonName = "Down";
                bool controllerValue = (Convert.ToBoolean(controller.Down.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //D-Pad Left BUTTON
            controller.Left.ValueChanged += (s, e) =>
            {
                string buttonName = "Left";
                bool controllerValue = (Convert.ToBoolean(controller.Left.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //D-Pad Right BUTTON
            controller.Right.ValueChanged += (s, e) =>
            {
                string buttonName = "Right";
                bool controllerValue = (Convert.ToBoolean(controller.Right.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion

            ////Buttons Shoulder Left, Right
            #region //L1 BUTTON
            controller.LeftShoulder.ValueChanged += (s, e) =>
            {
                string buttonName = "L1";
                bool controllerValue = (Convert.ToBoolean(controller.LeftShoulder.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //R1 BUTTON
            controller.RightShoulder.ValueChanged += (s, e) =>
            {
                string buttonName = "R1";
                bool controllerValue = (Convert.ToBoolean(controller.RightShoulder.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion

            //Trigger Position Left, Right
            #region //L2 BUTTON
            controller.LeftTrigger.ValueChanged += (s, e) =>
            {
                string buttonName = "L2";
                bool controllerValue = Convert.ToBoolean(controller.LeftTrigger.Value);

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3 && controllerValue == false)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4 && controllerValue == false)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5 && controllerValue == false)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6 && controllerValue == false)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
            #region //R2 BUTTON
            controller.RightTrigger.ValueChanged += (s, e) =>
            {
                string buttonName = "R2";
                bool controllerValue = (Convert.ToBoolean(controller.RightTrigger.Value));

                if (controllerValue == true && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = true;
                }
                else if (controllerValue == false && Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == buttonName)
                {
                    Settings.Default.ControllerAIMBOTButtonPressed = false;
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_AIMBOT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.AIMBOT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.AIMBOT = true;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "true", "AIMBOT");
                    }
                    else
                    {
                        Settings.Default.AIMBOT = false;
                        Clsini.INIConfig.Write("Enable_AIMBOT", "false", "AIMBOT");
                    }
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_ESP", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.ESP == false && (Security.pfpf == 1))
                    {
                        Settings.Default.ESP = true;
                        Clsini.INIConfig.Write("Enable_ESP", "true", "ESP");
                    }
                    else
                    {
                        Settings.Default.ESP = false;
                        Clsini.INIConfig.Write("Enable_ESP", "false", "ESP");
                    }
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_SNAPLINE", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.SNAPLINE == false && (Security.pfpf == 1))
                    {
                        Settings.Default.SNAPLINE = true;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "true", "SNAPLINE");
                    }
                    else
                    {
                        Settings.Default.SNAPLINE = false;
                        Clsini.INIConfig.Write("Enable_SNAPLINE", "false", "SNAPLINE");
                    }
                }

                if (controllerValue == false && Clsini.INIConfig.Read("TOGGLE_VSAT", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.VSAT == false && (Security.pfpf == 1))
                    {
                        Settings.Default.VSAT = true;
                        Clsini.INIConfig.Write("Enable_VSAT", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.VSAT = false;
                        Clsini.INIConfig.Write("Enable_VSAT", "false", "MISC");
                    }
                }

                if (controllerValue == true && Clsini.INIConfig.Read("TOGGLE_CROSSHAIR", "CONTROLLER") == buttonName)
                {
                    if (Settings.Default.CROSSHAIR == false && (Security.pfpf == 1))
                    {
                        Settings.Default.CROSSHAIR = true;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "true", "MISC");
                    }
                    else
                    {
                        Settings.Default.CROSSHAIR = false;
                        Clsini.INIConfig.Write("Enable_CROSSHAIR", "false", "MISC");
                    }
                }

                if (Settings.Default.Active_Func_Key == 1 && controllerValue == false)
                {
                    ActionControllerButtonClicked = true;
                    Settings.Default.ControllerButtonAIMBOT = buttonName;
                }
                else if (Settings.Default.Active_Func_Key == 3)
                {
                    Settings.Default.ControllerToggleAIMBOT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 4)
                {
                    Settings.Default.ControllerToggleESP = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 5)
                {
                    Settings.Default.ControllerToggleSNAPLINE = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 6)
                {
                    Settings.Default.ControllerToggleVSAT = buttonName;
                    ActionControllerButtonClicked = true;
                }
                else if (Settings.Default.Active_Func_Key == 7)
                {
                    Settings.Default.ControllerToggleCROSSHAIR = buttonName;
                    ActionControllerButtonClicked = true;
                }
            };
            #endregion
        }
    }
}
