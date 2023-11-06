using AssaultCubeHack;
using Examples;
using RL.Properties;
using System;
using VECRPROJECT.game;
using VECRPROJECT.util;
using WindowsInput;

namespace RL.game
{
    class Aimbot
    {
        public static bool ButtonCustomClick;
        public static bool aim = false;
        public static InputSimulator mousecontrol = new InputSimulator();

        public static Aimbot aimbot = new Aimbot();

        public readonly static float HEAD = 11F; //1
        public readonly static float BODY = 20F; //2
        public readonly static float FOOT = 54F; //3

        public static float TARGET = BODY;

        public void UpdateAimbot()
        {
            try
            {

                if (Security.pfpf == 0 || Security.bpAcc == false)
                {
                    //fail
                    Security.FAILACC();
                }

                if (Example.GetActiveWindowTitleAIMBOT() == false) return;

                if (Settings.Default.ControllerAIMBOTButtonPressed == true && (Clsini.INIConfig.Read("AIM_Key", "CONTROLLER") == Settings.Default.ControllerButtonAIMBOT)) //CONTROLLER
                {
                    ButtonCustomClick = true;
                }
                else
                {
                    ButtonCustomClick = false;
                }

                //if not aiming or no players, escape
                if (!aim && !ButtonCustomClick || Player.players.Count == 0 || Settings.Default.AIMBOT == false || Security.pfpf == 0 || Example.GetActiveWindowTitle() == false) return;

                Player target = null;
                //find closest enemy player
                if (Settings.Default.Aim_Filter == 1) { target = GetClosestEnemyToCrossHair(); }
                else if (Settings.Default.Aim_Filter == 2) { target = GetClosestEnemyToDistance(); }

                if (target == null) return;

                //calculate verticle angle between enemy and player (pitch)

                if (Settings.Default.AIMOT_Target == 1) //HEAD
                {
                    TARGET = HEAD;
                }
                else if (Settings.Default.AIMOT_Target == 2) //BODY
                {
                    TARGET = BODY;
                }
                else if (Settings.Default.AIMOT_Target == 3) //FOOT
                {
                    TARGET = FOOT;
                }

                if (target.PlayerCROUCH == 1) //FOOT
                {
                    TARGET = 15F;
                }
                if (target.PlayerCROUCH == 2) //FOOT
                {
                    TARGET = 16.5F;
                }

                //set self angles to calculated angles
                Vector3 test = (target.PositionHead);
                test.z -= TARGET;

                Vector2 headPos, headPos1, footPos1;
                if (Matrix.viewMatrix.WorldToScreen(target.PositionHead, Example.gameWidth, Example.gameHeight, out headPos1) &&
                    Matrix.viewMatrix.WorldToScreen(target.PositionFoot, Example.gameWidth, Example.gameHeight, out footPos1) &&
                    Matrix.viewMatrix.WorldToScreen(test, Example.gameWidth, Example.gameHeight, out headPos))
                {

                    float num = Example.gameWidth / 2; //GameWidth
                    float num2 = Example.gameHeight / 2;  //GameHeight
                    float smoothSpeed = Settings.Default.AIMBOT_Acceleration;

                    float radius = -Settings.Default.Aim_FOV * 2 + Example.gameHeight / Settings.Default.Aim_FOV;
                    float distance = Mathematics.Distance(target.PositionFoot);
                    if (distance <= 15)
                    {
                        radius = -1 * 2 + Example.gameHeight / 1;
                    }

                    if (!(Math.Abs(headPos1.x - num) > radius) && !(Math.Abs(footPos1.y - num2) > radius) ||
                        !(Math.Abs(headPos1.x - num) > radius) && !(Math.Abs(headPos1.y - num2) > radius)) //FOV
                    {
                        float num3 = 0f;
                        float num4 = 0f;

                        if (headPos.x > num)
                        {
                            num3 = 0f - (num - headPos.x);
                            num3 /= smoothSpeed;
                        }
                        else if (headPos.x < num)
                        {
                            num3 = headPos.x - num;
                            num3 /= smoothSpeed;
                        }

                        if (headPos.y > num2)
                        {
                            num4 = (0f - (num2 - headPos.y));
                            num4 /= smoothSpeed;
                        }
                        else if (headPos.y < num2)
                        {
                            num4 = (headPos.y - num2);
                            num4 /= smoothSpeed;
                        }

                        //Move mouse to entity
                        mousecontrol.Mouse.MoveMouseBy((int)num3, (int)num4);

                        //Use auto fire if activated
                        if (Settings.Default.AIM_Auto_Fire == true && (Security.pfpf == 1) && Security.bpAcc == true)
                        {
                            Rapid_Fire.Fire();
                        }
                    }
                }
            }
            catch { }
        }

        public Player GetClosestEnemyToCrossHair()
        {

            try
            {

                if (Security.pfpf == 0 && Security.bpAcc == true)
                {
                    //fail
                    Security.FAILACC();
                }

                //find first living enemy player in view
                Vector2 targetPos = new Vector2();
                Player target = Player.players.Find(p => Player.PlayerIsValidForAimbot(p) &&
                Matrix.viewMatrix.WorldToScreen(p.PositionHead, Example.gameWidth, Example.gameHeight, out targetPos));
                if (target == null) return null;

                //calculate distance to crosshair
                Vector2 crossHair = new Vector2(Example.gameWidth / 2, Example.gameHeight / 2);
                float dist = crossHair.Distance(targetPos);

                float num = Example.gameWidth / 2; //GameWidth
                float num2 = Example.gameHeight / 2;  //GameHeight

                //find player closest to crosshair
                foreach (Player p in Player.players)
                {
                    if (Player.PlayerIsValidForAimbot(p))
                    {

                        Vector2 headPos;
                        if (Matrix.viewMatrix.WorldToScreen(p.PositionHead, Example.gameWidth, Example.gameHeight, out headPos))
                        {
                            float radius = -Settings.Default.Aim_FOV * 2 + Example.gameWidth / Settings.Default.Aim_FOV;

                            if (!(Math.Abs(headPos.x - num) > radius) && !(Math.Abs(p.PositionFoot.y - num2) > radius) ||
                                !(Math.Abs(headPos.x - num) > radius) && !(Math.Abs(headPos.y - num2) > radius)) //FOV
                            {
                                float newDist = crossHair.Distance(headPos);

                                if (newDist < dist)
                                {
                                    target = p;
                                    dist = newDist;
                                }
                            }
                        }
                    }
                }

                return target;

            }
            catch { return null; }
        }

        public Player GetClosestEnemyToDistance()
        {

            try
            {

                if (Security.pfpf == 0 && Security.bpAcc == true)
                {
                    //fail
                    Security.FAILACC();
                }

                float num = Example.gameWidth / 2; //GameWidth
                float num2 = Example.gameHeight / 2;  //GameHeight

                //find first living enemy player in view
                Player target = Player.players.Find(p => Player.PlayerIsValidForAimbot(p));

                if (target == null) return null;

                Vector3 MyDist = new Vector3();
                float FirstDist = 99999f;

                //find player closest to distance
                foreach (Player p in Player.players)
                {
                    if (Player.PlayerIsValidForAimbot(p))
                    {
                        Vector2 headPos;
                        if (Matrix.viewMatrix.WorldToScreen(p.PositionHead, Example.gameWidth, Example.gameHeight, out headPos))
                        {
                            float radius = -Settings.Default.Aim_FOV * 2 + Example.gameHeight / Settings.Default.Aim_FOV;

                            float distance = Mathematics.Distance(target.PositionFoot);
                            if (distance <= 15)
                            {
                                radius = -1 * 2 + Example.gameHeight / 1;
                            }

                            if (!(Math.Abs(headPos.x - num) > radius) && !(Math.Abs(target.PositionFoot.y - num2) > radius) ||
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
                }
                if (target == null)
                {
                    //find closest enemy player
                    return target = GetClosestEnemyToCrossHair();
                }

                return target;

            }
            catch { return GetClosestEnemyToCrossHair(); }
        }
    }
}
