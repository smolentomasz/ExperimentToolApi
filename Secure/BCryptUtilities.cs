namespace ExperimentToolApi.Secure
{
    public class BCryptUtilities
    {
        public static string encodePassword(string password){
            string additionalSalt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, additionalSalt);

            return hashedPassword;
        }
        public static bool passwordMatch(string password, string hashedPassword){
            if(BCrypt.Net.BCrypt.Verify(password, hashedPassword)){
                return true;
            }
            else{
                return false;
            }
        }
    }
}