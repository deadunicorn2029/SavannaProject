namespace Savanna.Services
{
    public static class ServicesTextConstants
    {
        //Error messages
        public static string UsernamePasswordEmpty = "Username or password is empty!";
        public static string UserNotFound = "User can not be found.";
        public static string IncorrectPassword = "Password is not correct!";

        //Error messages for register
        public static string EmptySecurityQuestion = "Choose a security question!";
        public static string EmptyAnserToSecurityQuestion = "Answer to the question is empty.";
        public static string UsernameTaken = "This username is already taken :(";
        public static string EmailTaken = "This email is already used :(";

        //Error messages for change password
        public static string UsernameEmpty = "Username is empty.";
        public static string EmailEmpty = "Email is empty.";
        public static string PasswordEmpty = "Password is empty.";
        public static string PasswordIncorrectCriteria = "Password does not meet the criteria!";
        public static string AnswerIncorrect = "Answer is not correct.";

        //Regex
        public static string Regex = @"^(?=.*[A-Z])(?=.*[\W_]).*$";
    }
}
