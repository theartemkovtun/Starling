namespace Starling.Shared.Layout.Database
{
    public static class Functions
    {
        public static string GetFileMetadata { get; } = "get_file_metadata";
        public static string AddFile { get; } = "add_file";
        public static string UpdateFile { get; } = "update_file";
        public static string GetFileContent { get; } = "get_file_content";
        public static string GetFiles { get; } = "get_files";
        public static string AddUser { get; } = "add_user";
        public static string GetUser { get; } = "get_user";
        public static string IsUserExists { get; } = "is_user_exist";
        public static string AddUserFile { get; } = "add_user_file";
        public static string GetUsers { get; } = "get_users";

        public static string AddShare { get; } = "add_share";
        public static string AddShareUser { get; } = "add_share_user";
        public static string AddShareFile { get; } = "add_share_file";
        public static string AddShareFileUser { get; } = "add_share_file_user";
        public static string GetUserShares { get; } = "get_user_shares";
        public static string GetShareForUser { get; } = "get_share_for_user";
        public static string GetShareFileSigning { get; } = "get_share_file_signing";
        public static string GetShareUsersInfo { get; } = "get_share_user_info";
        public static string AcceptShare { get; } = "accept_share";
        public static string RejectShare { get; } = "reject_share";
        public static string GetShareFilesQuantity { get; } = "get_share_files_quantity";
        public static string GetShareUsersQuantity { get; } = "get_share_users_quantity";
        public static string GetSignaturesVerificationData { get; } = "get_signatures_verification_data";
        public static string DeleteShare { get; } = "delete_share";

        public static string ValidateRefreshToken { get; } = "validate_refresh_token";
        public static string AddRefreshToken { get; } = "add_refresh_token";
        public static string DeleteRefreshToken { get; } = "delete_refresh_token";
    }
}