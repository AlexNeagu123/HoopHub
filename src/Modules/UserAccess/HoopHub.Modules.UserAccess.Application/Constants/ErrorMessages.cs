namespace HoopHub.Modules.UserAccess.Application.Constants
{
    public class ErrorMessages
    {
        public const string InvalidCredentials = "Invalid Credentials";
        public const string UserNameRequired = "Username is required";
        public const string PasswordRequired = "Password is required";
        public const string InvalidEmailAddress = "Please enter a valid email address";
        public const string EmailAddressRequired = "Email address is required";
        public const string PasswordLengthViolation = "Password should have at least 8 characters";
        public const string PasswordsDoNotMatch = "Passwords do not match";
        public const string UserAlreadyExists = "Username is already taken";
        public const string EmailAlreadyExists = "Email is already taken";
        public const string InvalidEmail = "The provided mail for the password reset does not exist in the application";
        public const string EmailNotSent = "Email could not be sent";
        public const string UserNotFound = "User not found";
        public const string UserNotAuthorized = "You are not authorized to perform this action";
        public const string UserNameTooLong = "Username is too long. Maximum 20 characters are allowed";
    }
}
