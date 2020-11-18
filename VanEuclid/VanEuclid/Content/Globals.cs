using System;
namespace VanEuclid.Content
{
    public static class Globals
    {
        static bool VanLogged = false;

        public static bool LoginStatus
        {
            get
            {
                return VanLogged;
            }

            set
            {
                VanLogged = value;
            }
        }
    }
}
