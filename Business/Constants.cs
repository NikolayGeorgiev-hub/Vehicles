namespace Business
{
    public class Constants
    {
        public class ValidationMessage
        {
            public const string RequiredErrorMessage = "{0} is required";
            public const string RangeErrorMessage = "{0} must by between {1} - {2}";
            public const string EnumTypeErrorMessage = "Not supported type";
            public const string InvalidEmailErrorMessage = "Not valid email";

            public const string NotFoundVehicle = "Not found vehicle";
            public const string SuccessfulRemove = "Successful remove";
            public const string SuccessfulUpdate = "Successful update";
        }

        public class LoginMessage
        {
            public const string SuccessfulRegistration = "Successful registration Email: {0}";
            public const string SuccessfulSetToRole = "Successful add permissions to Role: {0} Email: {1}";
            public const string SuccessfulAddRole = "Successful add new role: {0}";
            public const string AlreadyTakenEmail = "Email '{0}' is alreadt taken";

            public const string Login = "{0} login";
            public const string Logount = "User: {0} logout";
        }

        public class ErrorMessage
        {
            public const string RoleNameExists = "Role with name: {0} exists";
            public const string NotFoundRole = "Not found role with this name: {0}";
            public const string NotFoundUser = "Not found user with this: {0} ID";
            public const string AlreadyLogged = "User: {0} already logged in";
            public const string FailedLogin = "Wrong username or password";
        }

    }
}
