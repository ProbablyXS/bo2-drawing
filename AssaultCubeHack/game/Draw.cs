using AssaultCubeHack;
using Examples;
using RL.Properties;
using SharpDX.DirectWrite;
using System;
using VECRPROJECT.util;

namespace RL.game
{
    class Draw
    {
        public static Draw DRAW = new Draw();

        public void DrawOverlay(bbjuyhgazdf.Drawing.Graphics gfx)
        {
            if (Settings.Default.OVERLAY == true)
            {
                int OverlayInt01 = 100;
                int OverlayInt02 = 97;

                //ARRIERE PLAN
                gfx.FillRectangle(OVERLAYCOLOR01(), 340, 23, 180, OverlayInt01);
                //FOND
                gfx.FillRectangle(OVERLAYCOLOR02(), 337, 26, 183, OverlayInt02);

                //TEXT
                gfx.DrawText(Examples.Example._fonts["consolas"], Examples.Example._brushes["white"], Examples.Example.gameWidth - 95, Examples.Example.gameHeight - 20, "VECR PROJECT");
                gfx.DrawText(Examples.Example._fonts["OVERLAY01"], Examples.Example._brushes["white"], 190, 28, "Show Menu: [" + Settings.Default.Key_Show_MENU + "]");
                gfx.DrawLine(OVERLAYCOLOR01(), 180, 50, 338, 50, 2F);

                gfx.DrawText(Examples.Example._fonts["OVERLAY01"], Examples.Example._brushes["white"], 190, 38, "Active Aimbot: [" + Settings.Default.AIM_Key + "|" + Settings.Default.ControllerButtonAIMBOT + "]");

                //FUNCTION            
                if (Settings.Default.ESP == false)
                {
                    gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["white"], 190, 52, "ESP:");
                    gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["red"], 220, 52, "[OFF]");
                }
                else
                {
                    gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["white"], 190, 52, "ESP:");
                    gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["green"], 220, 52, "[ON]");
                }

                if (Security.pfpf == 1 && Security.bpAcc == true)
                {
                    if (Settings.Default.SNAPLINE == false)
                    {
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["white"], 190, 66, "SNAPLINE:");
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["red"], 252, 66, "[OFF]");
                    }
                    else
                    {
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["white"], 190, 66, "SNAPLINE:");
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["green"], 252, 66, "[ON]");
                    }

                    if (Settings.Default.AIMBOT == false)
                    {
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["white"], 190, 80, "AIMBOT:");
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["red"], 239, 80, "[OFF]");
                    }
                    else
                    {
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["white"], 190, 80, "AIMBOT:");
                        gfx.DrawText(Examples.Example._fonts["OVERLAY02"], Examples.Example._brushes["green"], 239, 80, "[ON]");
                    }
                }
            }
        }

        public void DrawAimFov(bbjuyhgazdf.Drawing.Graphics gfx)
        {
            if (Settings.Default.Draw_Aim_Fov == true && Security.pfpf == 1 && Security.bpAcc == true)
            {
                if (Settings.Default.AIMBOT == true)
                {
                    float radius = -Settings.Default.Aim_FOV * 2 + Examples.Example.gameHeight / Settings.Default.Aim_FOV;
                    gfx.DrawEllipse(AIMBOTFOVCOLOR(), Examples.Example.gameWidth / 2, Examples.Example.gameHeight / 2, radius, radius, 2f);
                }
            }
        }

        public void DrawCrosshair(bbjuyhgazdf.Drawing.Graphics gfx)
        {
            if (Settings.Default.CROSSHAIR == true)
            {
                gfx.DrawLine(CROSSHAIRCOLOR(), Examples.Example.gameWidth / 2, Examples.Example.gameHeight / 2 - Settings.Default.CROSSHAIR_Size, Examples.Example.gameWidth / 2, Examples.Example.gameHeight / 2 + Settings.Default.CROSSHAIR_Size, Settings.Default.CROSSHAIR_Thickness);
                gfx.DrawLine(CROSSHAIRCOLOR(), Examples.Example.gameWidth / 2 - Settings.Default.CROSSHAIR_Size, Examples.Example.gameHeight / 2, Examples.Example.gameWidth / 2 + Settings.Default.CROSSHAIR_Size, Examples.Example.gameHeight / 2, Settings.Default.CROSSHAIR_Thickness);
            }
        }

        public void DrawESP(bbjuyhgazdf.Drawing.Graphics gfx)
        {
            if (Settings.Default.ESP == true)
            {

                //find first living enemy player in view
                Player target = Player.players.Find(p => Player.PlayerIsValidForAimbot(p));
                float num = Example.gameWidth / 2; //GameWidth
                float num2 = Example.gameHeight / 2;  //GameHeight
                float FirstDist = 99999f;
                float radius = -Settings.Default.Aim_FOV * 2 + Example.gameHeight / Settings.Default.Aim_FOV;

                foreach (Player p in Player.players.ToArray())
                {
                    try
                    {
                        if (p == null || !Player.PlayerIsValid(p)) continue;

                        Vector2 headPos, footPos;

                        if (Matrix.viewMatrix.WorldToScreen(p.PositionHead, Examples.Example.gameWidth, Examples.Example.gameHeight, out headPos) &&
                            Matrix.viewMatrix.WorldToScreen(p.PositionFoot, Examples.Example.gameWidth, Examples.Example.gameHeight, out footPos))
                        {
                            float height = Math.Abs(headPos.y - footPos.y);
                            float width = height / 2F;

                            try
                            {

                                //ESP SHOW DISTANCE
                                if (Settings.Default.Show_DISTANCE || Settings.Default.Show_Name && (Security.pfpf == 1 && Security.bpAcc))
                                {
                                    string playerName = "";
                                    string playerPing = "";

                                    bbjuyhgazdf.Drawing.Point outputSize = gfx.MeasureString(ESP_TEXT_SIZE(), playerName + playerPing);

                                    if (Settings.Default.Show_Name == true)
                                    {
                                        playerName = p.PlayerNAME;
                                        outputSize = gfx.MeasureString(ESP_TEXT_SIZE(), playerName);
                                    }
                                    if (Settings.Default.Show_DISTANCE == true)
                                    {
                                        playerPing = "[" + Mathematics.Distance(p.PositionFoot) + "m]";
                                        outputSize = gfx.MeasureString(ESP_TEXT_SIZE(), playerPing);
                                    }
                                    if (Settings.Default.Show_Name == true && Settings.Default.Show_DISTANCE == true)
                                    {
                                        outputSize = gfx.MeasureString(ESP_TEXT_SIZE(), playerName + playerPing);
                                    }
                                    if (Settings.Default.Show_Text_Border == true)
                                    {
                                        var brush = Examples.Example._brushes["ESPFILLEDBORDERCOLOR"];
                                        brush.Color = new bbjuyhgazdf.Drawing.Color(255, 0, 0);
                                        gfx.DrawBox2D(Examples.Example._brushes["background"], Examples.Example._brushes["TextRectangle"], footPos.x - outputSize.X / 2f + -6, headPos.y - outputSize.Y + Settings.Default.ESP_SIZE_TEXT + 6, footPos.x + outputSize.X / 2f + 6, headPos.y - outputSize.Y, Settings.Default.ESP_Size);
                                    }

                                    gfx.DrawText(ESP_TEXT_SIZE(), PLAYERTEXTCOLOR(p), footPos.x - outputSize.X / 2f, headPos.y - outputSize.Y, playerName + playerPing);
                                }
                                //END

                                //ESP SHOW PING
                                if (Settings.Default.Show_PING && (Security.pfpf == 1 && Security.bpAcc))
                                {
                                    string Text = p.PlayerPing + "ms";

                                    bbjuyhgazdf.Drawing.Point outputSize = gfx.MeasureString(ESP_TEXT_SIZE(), Text);

                                    if (Settings.Default.Show_Text_Border)
                                    {
                                        var brush = Examples.Example._brushes["ESPFILLEDBORDERCOLOR"];
                                        brush.Color = new bbjuyhgazdf.Drawing.Color(255, 0, 0);
                                        gfx.DrawBox2D(Examples.Example._brushes["background"], Examples.Example._brushes["TextRectangle"], footPos.x - outputSize.X / 2f + -6, headPos.y + height + Settings.Default.ESP_SIZE_TEXT + 6, footPos.x + outputSize.X / 2f + 6, headPos.y + height, Settings.Default.ESP_Size);
                                    }

                                    gfx.DrawText(ESP_TEXT_SIZE(), PLAYERPINGCOLOR(p), footPos.x - outputSize.X / 2f, headPos.y + height, Text);
                                }
                                //END
                            }

                            catch { }

                            //ESP
                            if (Settings.Default.Draw_Enemy_Close)
                            {
                                if (Player.PlayerIsValidForAimbot(p))
                                {
                                    //GET THE BEST LOWER DISTANCE ENNEMI AT YOU
                                    if (!(Math.Abs(headPos.x - num) > radius) && !(Math.Abs(p.PositionFoot.y - num2) > radius) ||
                                        !(Math.Abs(headPos.x - num) > radius) && !(Math.Abs(headPos.y - num2) > radius)) //FOV
                                    {
                                        float newDist = Mathematics.Distance(p.PositionFoot);

                                        if (newDist <= FirstDist)
                                        {
                                            target = p;
                                            FirstDist = newDist;
                                        }
                                    }
                                }
                            }

                            if (Settings.Default.ESP_Form == 1)
                            {
                                gfx.DrawRoundedRectangle(ESPCOLOR(p), footPos.x - width / 2f, headPos.y, footPos.x + width / 2f, footPos.y, 2f, Settings.Default.ESP_Size);
                                //gfx.DrawHorizontalProgressBar(ESPCOLOR(p), CROSSHAIRCOLOR(), footPos.x - width / 2f, headPos.y, footPos.x + width / 2f, footPos.y, 2f, 100f); //A mettre pour plus tard quand j'aurais trouver  la vie des joeurs
                            }
                            else if (Settings.Default.ESP_Form == 2)
                            {
                                gfx.DrawBox2D(ESPFILLEDBORDERCOLOR(), ESPCOLOR(p), footPos.x - width / 2f, headPos.y, footPos.x + width / 2f, footPos.y, Settings.Default.ESP_Size);
                            }
                            else if (Settings.Default.ESP_Form == 3)
                            {
                                gfx.DrawRoundedRectangle(ESPCOLOR(p), footPos.x - width / 2f, headPos.y, footPos.x + width / 2f, footPos.y, 888f, Settings.Default.ESP_Size);
                            }
                            //END

                            //SNAPLINE
                            Draw.DRAW.DrawSnapline(gfx, p, footPos, headPos, height);

                        }
                    }
                    catch { }
                }


                //DRAW LOWER ESP DISTANCE

                //Check if player is valid

                if (Settings.Default.Draw_Enemy_Close)
                {
                    if (target == null || !Player.PlayerIsValidForAimbot(target)) return;

                    Vector2 headPos1, footPos1;
                    if (Matrix.viewMatrix.WorldToScreen(target.PositionHead, Example.gameWidth, Example.gameHeight, out headPos1) &&
                        Matrix.viewMatrix.WorldToScreen(target.PositionFoot, Example.gameWidth, Example.gameHeight, out footPos1))
                    {
                        if (Settings.Default.AIMBOT)
                        {
                            float distance = Mathematics.Distance(target.PositionFoot);
                            if (distance <= 15)
                            {
                                radius = -1 * 2 + Example.gameHeight / 1;
                            }

                            if (!(Math.Abs(headPos1.x - num) > radius) && !(Math.Abs(footPos1.y - num2) > radius) ||
                                !(Math.Abs(headPos1.x - num) > radius) && !(Math.Abs(headPos1.y - num2) > radius)) //FOV
                            {
                                float height1 = Math.Abs(headPos1.y - footPos1.y);
                                float width1 = height1 / 2F;

                                Vector3 test = (target.PositionHead);
                                test.z -= Aimbot.TARGET;
                                Vector2 headPostest = new Vector2();
                                if (Matrix.viewMatrix.WorldToScreen(test, Example.gameWidth, Example.gameHeight, out headPostest))
                                {
                                    gfx.DrawLine(SNAPLINETOAIMBOTTARGET(target), Example.gameWidth / 2, Example.gameHeight / 2, footPos1.x, headPostest.y, Settings.Default.SNAPLINE_Size);
                                }

                                gfx.DrawRoundedRectangle(SNAPLINETOAIMBOTTARGET(target), footPos1.x - width1 / 2f, headPos1.y, footPos1.x + width1 / 2f, footPos1.y, 2f, Settings.Default.ESP_Size);
                            }
                        }
                    }
                }
            }
        }

        public void DrawSnapline(bbjuyhgazdf.Drawing.Graphics gfx, Player p, Vector2 footPos, Vector2 headPos, float height)
        {
            //SNAPLINE
            if (Settings.Default.SNAPLINE == true && Security.pfpf == 1 && Security.bpAcc == true)
            {
                //TOP LEFT
                if (Settings.Default.SNAPLINE_Starting_Point == 1)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), 0, 0, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //CENTER TOP
                else if (Settings.Default.SNAPLINE_Starting_Point == 2)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), Examples.Example.gameWidth / 2, 0, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //TOP RIGHT
                else if (Settings.Default.SNAPLINE_Starting_Point == 3)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), Examples.Example.gameWidth, 0, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //CENTER LEFT
                else if (Settings.Default.SNAPLINE_Starting_Point == 4)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), 0, Examples.Example.gameHeight / 2, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //CENTER
                else if (Settings.Default.SNAPLINE_Starting_Point == 5)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), Examples.Example.gameWidth / 2, Examples.Example.gameHeight / 2, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);

                }
                //CENTER RIGHT
                else if (Settings.Default.SNAPLINE_Starting_Point == 6)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), Examples.Example.gameWidth, Examples.Example.gameHeight / 2, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //BOTTOM LEFT
                else if (Settings.Default.SNAPLINE_Starting_Point == 7)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), 0, Examples.Example.gameHeight, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //CENTER BOT
                else if (Settings.Default.SNAPLINE_Starting_Point == 8)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), Examples.Example.gameWidth / 2, Examples.Example.gameHeight, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
                //BOTTOM RIGHT
                else if (Settings.Default.SNAPLINE_Starting_Point == 9)
                {
                    gfx.DrawLine(SNAPLINECOLOR(p), Examples.Example.gameWidth, Examples.Example.gameHeight, footPos.x, headPos.y + height, Settings.Default.SNAPLINE_Size);
                }
            }
        }

        private bbjuyhgazdf.Drawing.Font ESP_TEXT_SIZE()
        {
            try
            {

                var font = Examples.Example._fonts["ESP_TEXT_SIZE"];

                font = new bbjuyhgazdf.Drawing.Font(new Factory(), Settings.Default.FontText, Settings.Default.ESP_SIZE_TEXT);

                return font;

            }
            catch { return null; }
        }

        private bbjuyhgazdf.Drawing.SolidBrush ESPCOLOR(Player p)
        {

            try
            {
                var brush = Examples.Example._brushes["ESPCOLOR"];

                if (p.PlayerTEAMForFFA == Player.GameModeIsNotFFA)
                {

                    if (p.PlayerTEAM != Player.SelfPlayerTeam)
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_Color.R, Settings.Default.ESP_Color.G, Settings.Default.ESP_Color.B);
                    }
                    else
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_ALLIES_COLOR.R, Settings.Default.ESP_ALLIES_COLOR.G, Settings.Default.ESP_ALLIES_COLOR.B);
                    }
                }
                else
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_Color.R, Settings.Default.ESP_Color.G, Settings.Default.ESP_Color.B);
                }

                if (p.PlayerISALIVE > Player.isAliveTrue1) //IF IS DEAD
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(0, 0, 0);
                }

                return brush;

            }
            catch { return null; }
        }

        private bbjuyhgazdf.Drawing.SolidBrush PLAYERTEXTCOLOR(Player p)
        {

            try
            {

                var brush = Examples.Example._brushes["ESPCOLOR"];

                if (p.PlayerTEAMForFFA == Player.GameModeIsNotFFA)
                {

                    if (p.PlayerTEAM != Player.SelfPlayerTeam)
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.Player_Name_Color.R, Settings.Default.Player_Name_Color.G, Settings.Default.Player_Name_Color.B);
                    }
                    else
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_ALLIES_COLOR.R, Settings.Default.ESP_ALLIES_COLOR.G, Settings.Default.ESP_ALLIES_COLOR.B);
                    }
                }
                else
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.Player_Name_Color.R, Settings.Default.Player_Name_Color.G, Settings.Default.Player_Name_Color.B);
                }

                if (p.PlayerISALIVE > Player.isAliveTrue1) //IF IS DEAD
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(0, 0, 0);
                }

                return brush;

            }
            catch { return null; }
        }

        private bbjuyhgazdf.Drawing.SolidBrush PLAYERPINGCOLOR(Player p)
        {

            try
            {

                var brush = Examples.Example._brushes["ESPCOLOR"];

                if (p.PlayerTEAMForFFA == Player.GameModeIsNotFFA)
                {

                    if (p.PlayerTEAM != Player.SelfPlayerTeam)
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.Player_Ping_Color.R, Settings.Default.Player_Ping_Color.G, Settings.Default.Player_Ping_Color.B);
                    }
                    else
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_ALLIES_COLOR.R, Settings.Default.ESP_ALLIES_COLOR.G, Settings.Default.ESP_ALLIES_COLOR.B);
                    }
                }
                else
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.Player_Ping_Color.R, Settings.Default.Player_Ping_Color.G, Settings.Default.Player_Ping_Color.B);
                }

                if (p.PlayerISALIVE > Player.isAliveTrue1) //IF IS DEAD
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(0, 0, 0);
                }

                return brush;

            }
            catch { return null; }
        }

        private bbjuyhgazdf.Drawing.SolidBrush SNAPLINETOAIMBOTTARGET(Player p)
        {

            try
            {
                var brush = Examples.Example._brushes["ESPCOLOR"];

                if (p.PlayerTEAMForFFA == Player.GameModeIsNotFFA)
                {

                    if (p.PlayerTEAM != Player.SelfPlayerTeam)
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.CROSSHAIR_Color.R, Settings.Default.CROSSHAIR_Color.G, Settings.Default.CROSSHAIR_Color.B);
                    }
                    else
                    {
                        brush.Color = new bbjuyhgazdf.Drawing.Color(0, 0, 0, 0);
                    }
                }
                else
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.CROSSHAIR_Color.R, Settings.Default.CROSSHAIR_Color.G, Settings.Default.CROSSHAIR_Color.B);
                }

                if (p.PlayerISALIVE > Player.isAliveTrue1) //IF IS DEAD
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(0, 0, 0, 0);
                }

                return brush;

            }
            catch { return null; }
        }

        private bbjuyhgazdf.Drawing.SolidBrush ESPFILLEDBORDERCOLOR()
        {
            var brush = Examples.Example._brushes["ESPFILLEDBORDERCOLOR"];

            brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_FILLED_BORDER_COLOR.R, Settings.Default.ESP_FILLED_BORDER_COLOR.G, Settings.Default.ESP_FILLED_BORDER_COLOR.B);

            return brush;
        }

        private bbjuyhgazdf.Drawing.SolidBrush OVERLAYCOLOR01()
        {
            var brush = Examples.Example._brushes["OVERLAY01"];

            brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.Menu_Border_Color.R, Settings.Default.Menu_Border_Color.G, Settings.Default.Menu_Border_Color.B);

            return brush;
        }

        private bbjuyhgazdf.Drawing.SolidBrush OVERLAYCOLOR02()
        {
            var brush = Examples.Example._brushes["OVERLAY02"];

            brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.Default_Background_Color.R, Settings.Default.Default_Background_Color.G, Settings.Default.Default_Background_Color.B);

            return brush;
        }

        private bbjuyhgazdf.Drawing.SolidBrush AIMBOTFOVCOLOR()
        {
            var brush = Examples.Example._brushes["AIMBOT_FOV"];

            brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.AIMBOT_FOV_Color.R, Settings.Default.AIMBOT_FOV_Color.G, Settings.Default.AIMBOT_FOV_Color.B);

            return brush;
        }

        private bbjuyhgazdf.Drawing.SolidBrush CROSSHAIRCOLOR()
        {
            var brush = Examples.Example._brushes["CROSSHAIR"];

            brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.CROSSHAIR_Color.R, Settings.Default.CROSSHAIR_Color.G, Settings.Default.CROSSHAIR_Color.B);

            return brush;
        }

        private bbjuyhgazdf.Drawing.SolidBrush SNAPLINECOLOR(Player p)
        {
            var brush = Examples.Example._brushes["SNAPLINE"];

            if (p.PlayerTEAMForFFA == Player.GameModeIsNotFFA)
            {

                if (p.PlayerTEAM != Player.SelfPlayerTeam)
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.SNAPLINE_Color.R, Settings.Default.SNAPLINE_Color.G, Settings.Default.SNAPLINE_Color.B);
                }
                else
                {
                    brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.ESP_ALLIES_COLOR.R, Settings.Default.ESP_ALLIES_COLOR.G, Settings.Default.ESP_ALLIES_COLOR.B);
                }
            }
            else
            {
                brush.Color = new bbjuyhgazdf.Drawing.Color(Settings.Default.SNAPLINE_Color.R, Settings.Default.SNAPLINE_Color.G, Settings.Default.SNAPLINE_Color.B);
            }

            if (p.PlayerISALIVE > Player.isAliveTrue1) //IF IS DEAD
            {
                brush.Color = new bbjuyhgazdf.Drawing.Color(0, 0, 0, 0);
            }

            return brush;
        }
    }
}
