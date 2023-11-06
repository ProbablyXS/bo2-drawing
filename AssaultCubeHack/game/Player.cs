using AssaultCubeHack;
using System.Collections.Generic;

namespace RL.game
{
    class Player
    {
        public static List<Player> players = new List<Player>();

        public int strPlayerPosition;
        public int strPlayerList;

        public static int GameModeIsNotFFA = 0;
        public static int[] PlayerNotChooseTeam = { 0, 9 }; //Player not choose TEAM (Spectator = 9)

        public static int isAliveTrue1 = 0;
        public static int isAliveTrue2 = 0;

        public Vector3 PositionHead
        {
            get { return Memory.ReadHEAD(strPlayerPosition + Offsets.headPos, PlayerCROUCH); }
        }

        public Vector3 PositionFoot
        {
            get { return Memory.ReadFOOT(strPlayerPosition + Offsets.footPos); }
        }

        public int PlayerTEAM
        {
            get { return Memory.Read<int>(strPlayerList + Offsets.PlayerTEAM); }
        }

        public int PlayerNumberId
        {
            get { return Memory.Read<int>(strPlayerList - Offsets.PlayerNumberID); }
        }

        public int PlayerTEAMForFFA
        {
            get { return Memory.Read<int>(strPlayerList + Offsets.PlayerTEAMForFFA); }
        }

        public int PlayerISALIVE
        {
            get { return Memory.Read<int>(strPlayerList + Offsets.PlayerISALIVE); }
        }

        public int PlayerISALIVE2
        {
            get { return Memory.Read<int>(strPlayerList + Offsets.PlayerISALIVE2); }
        }

        public int PlayerCROUCH
        {
            get { return Memory.Read<int>(strPlayerList + Offsets.PlayerCROUCH); }
        }

        public string PlayerNAME
        {
            get { return Memory.ReadString(strPlayerList + Offsets.PlayerNAME, 16); }
        }

        public int PlayerWeaponId
        {
            get { return Memory.Read<int>(strPlayerList + 0x5B8); }
        }

        public int PlayerPing
        {
            get { return Memory.Read<int>(strPlayerList + Offsets.PlayerPING); }
        }

        public static Vector3 SelfPosFoot
        {
            get { return Memory.ReadFOOT(Offsets.SelfLocalPlayer + Offsets.SelfLocalPlayerPOSITION); }
        }

        public static int SelfPlayerTeam
        {
            get { return Memory.Read<int>(Offsets.SelfLocalPlayer + Offsets.SelfLocalPlayerTEAM); }
        }

        public static int SelfPlayerNumberID
        {
            get { return Memory.Read<int>(Offsets.SelfLocalPlayer + Offsets.SelfLocalPlayerNumberID); }
        }

        public static bool PlayerIsValid(Player p)
        {

            if (p == null || p.PlayerISALIVE > isAliveTrue1 || p.PlayerISALIVE2 > isAliveTrue2) return false; //ON EST MORT

            if (p.PlayerTEAMForFFA == GameModeIsNotFFA)
            {
                if (p.PlayerTEAM == PlayerNotChooseTeam[0] || p.PlayerTEAM == PlayerNotChooseTeam[1]) return false; //ON EST PAS SUR LA MAP
            }

            return true;
        }

        public static bool PlayerIsValidForAimbot(Player p)
        {

            if (p == null || p.PlayerISALIVE > isAliveTrue1 || p.PlayerISALIVE2 > isAliveTrue2) //ON EST MORT
            {
                return false;
            }

            if (p.PlayerTEAMForFFA == GameModeIsNotFFA)
            {
                if (p.PlayerTEAM == PlayerNotChooseTeam[0] || p.PlayerTEAM == PlayerNotChooseTeam[1] || p.PlayerTEAM == SelfPlayerTeam) return false; //ON EST PAS SUR LA MAP
            }

            return true;
        }

        public Player(int strPlayerPosition, int strPlayerList)
        {
            this.strPlayerPosition = strPlayerPosition;
            this.strPlayerList = strPlayerList;
        }

    }
}
