using Project_QLNCC.Debugging;

namespace Project_QLNCC
{
    public class Project_QLNCCConsts
    {
        public const string LocalizationSourceName = "Project_QLNCC";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "7f03d87d9abb44ad8823b34e9c6d410e";
    }
}
