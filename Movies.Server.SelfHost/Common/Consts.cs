namespace Movies.Server.SelfHost.Common
{
    /// <summary>
    /// Class containing static conts to be used by the entire project
    /// </summary>
    public class Consts
    {
        #region Json Consts
        public static readonly string C_MT_JSON = "application/json";
        public static readonly string VALIDATION_ERROR_RESPONSE_PHRASE = "Validation error";
        #endregion

        #region GenderController Consts
        public static readonly string C_GENDER_NOT_FOUND = "Gender not found";
        public static readonly string C_GENDER_DELETED = "Gender deleted successfully";
        #endregion

        #region MovieController Consts
        public static readonly string C_MOVIE_NOT_FOUND = "Movie not found";
        public static readonly string C_MOVIE_DELETED = "Movie deleted successfully";
        #endregion

        #region RentalController Consts
        public static readonly string C_RENTAL_NOT_FOUND = "Rental not found";
        public static readonly string C_RENTAL_DELETED = "Rental deleted successfully";
        public static readonly string C_RENTAL_CPF_ERROR_MESSAGE = "CPF field is not correctly formatted.";
        public static readonly string C_INVALID_RENTAL_ERROR_MESSAGE = "Rental contains an invalid Movie Id. Crate this movie first.";
        #endregion

        #region UserController Consts
        public static readonly string C_USER_NOT_FOUND = "User not found";
        public static readonly string INVALID_USER_DATA = "Invalid User data";        
        #endregion

        #region AccessTokenProvider
        public static readonly string C_INVALID_ACCESS = "Invalid Access";
        public static readonly string C_INVALID_ACCESS_MESSAGE = "User credentials do not match";
        #endregion
    }

}
