namespace AssaultCubeHack
{
    class Offsets
    {
        //PLAYER MOVEMENT
        public static int baseAddress = 0x01140878;
        public static int ptrPlayerPositionArray = 0x380;

        //PLAYERLIST
        public static int PlayersLIST = 0x3B5D4;
        public static int ptrPlayerLISTArray = 0x808;

        public static int headPos = 0x2C;
        public static int footPos = 0x2C;

        public static int PlayerTEAM = 0x20;
        public static int PlayerTEAMForFFA = 0x28;
        public static int PlayerISALIVE = 0xC4; // 0 is alive || 1 or higher is dead
        public static int PlayerISALIVE2 = 0x80; // 0 is alive || 1 or higher is dead
        public static int PlayerNAME = 0x00;
        public static int PlayerNumberID = 0x4;
        public static int PlayerPING = 0x7C;
        public static int PlayerTagTeamName = 0x74;
        public static int PlayerWeapon = 0xD4;
        public static int PlayerCROUCH = 0x578;
        //END

        public static int VSAT = 0x5CAEC;

        public static int viewMatrix = 0x1065F40;  //Up 1 down -1

        //LOCAL PLAYER
        public static int SelfLocalPlayer = 0x02E3D838; //SES PROPRE INFORMATIONS
        public static int SelfLocalPlayerNumberID = 0x1F8; //SA PROPRE ID (baseGame - A506C)
        public static int SelfLocalPlayerPOSITION = 0x1C8; //SES PROPRE INFORMATIONS
        public static int SelfLocalPlayerTEAM = 0x200; //SA PROPRE TEAM
        //END

    }
}
