namespace School.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/List";
            public const string GetBYId = Prefix + "/{id}";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{id}";
            public const string Paginated = Prefix + "/Paginated";
            public const string PaginatedStudent = Paginated + "/List";



        }
        public static class DepartmentRounting
        {
            public const string Prefix = Rule + "Department";
            public const string List = Prefix + "/List";
            public const string GetBYId = Prefix;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/Delete/{id}";
            public const string Paginated = Prefix + "/Paginated";
            public const string PaginatedStudent = Paginated + "/List";



        }
        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule + "ApplicationUserRouting";
            public const string Create = Prefix + "/Create";
            public const string Paginated = Prefix + "/Paginated";
            public const string GetByID = Prefix + "/{id}";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + "/{id}";
            public const string ChangePassword = Prefix + "/ChangePassword";
        }
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/RefreshToken";
            public const string ValidateToken = Prefix + "/Validate-Token";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";

            public const string SendResetPasswordCode = Prefix + "/SendResetPasswordCode";
            public const string ConfirmResetPasswordCode = Prefix + "/ConfirmResetPasswordCode";
            public const string ResetPassword = Prefix + "/ResetPassword";

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "Authorization";
            public const string Create = Prefix + "/Role/Create";
            public const string Edit = Prefix + "/Role/Edit";
            public const string Delete = Prefix + "/Role/Delete{id}";
            public const string RoleList = Prefix + "/Role/RoleList";
            public const string RoleById = Prefix + "/Role/RoleById{id}";
            public const string ManageUserRole = Prefix + "/Role/ManageUserRole{id}";
            public const string UpdateUserRoles = Prefix + "/Role/UpdateUserRoles";
            public const string UpdateUserClaims = Prefix + "/Claim/UpdateUserRoles";
            public const string ManageUserClaims = Prefix + "/Claim/ManageUserClaims/{userId}";
        }
        public static class EmailsRoute
        {
            public const string Prefix = Rule + "EmailsRoute";
            public const string SendEmail = Prefix + "/SendEmail";
        }
        public static class InstructorRouting
        {
            public const string Prefix = Rule + "Instructor";
            public const string AddInstructor = Prefix + "/Create";
        }
    }
}
