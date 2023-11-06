using RL.game;
using RL.Properties;
using System.Threading.Tasks;

namespace VECRPROJECT.game
{
    class Rapid_Fire
    {

        private static bool FireButtonClicked = false;

        public static async void Fire()
        {
            if (FireButtonClicked == false && Settings.Default.AIM_Key.ToString().ToLower() != "LButton".ToLower())
            {
                FireButtonClicked = true;
                Aimbot.mousecontrol.Mouse.LeftButtonClick();
                await Task.Delay(Settings.Default.AUTO_FIRE_MS);
                FireButtonClicked = false;
            }
        }
    }
}
