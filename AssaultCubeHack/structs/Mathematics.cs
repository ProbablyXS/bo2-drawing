using RL.game;
using System;

namespace AssaultCubeHack
{
    class Mathematics
    {
        public static float Distance(Vector3 vector)
        {
            float dx = vector.x - Player.SelfPosFoot.x;
            float dy = vector.y - Player.SelfPosFoot.y;
            float dz = vector.z - Player.SelfPosFoot.z;

            double distance = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

            distance /= 10;
            distance = Math.Abs(Convert.ToInt32(distance));

            return (float)distance;
        }
    }
}
