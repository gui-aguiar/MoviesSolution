using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Server.SelfHost.Common
{
    /// <summary>
    /// Class containing static conts to be used by the entire project
    /// </summary>
    public class Consts
    {
        #region Json Conts

        public static readonly string C_MT_JSON = "application/json";
        public static readonly string VALIDATION_ERROR_RESPONSE_PHRASE = "application/json";

        #endregion

        #region GenderController Conts

        public static readonly string C_GENDER_NOT_FOUND = "Gender not found";
        public static readonly string C_GENDER_DELETED = "Gender deleted successfully";

        #endregion

        #region MovieController Conts

        public static readonly string C_MOVIE_NOT_FOUND = "Movie not found";
        public static readonly string C_MOVIE_DELETED = "Movie deleted successfully";

        #endregion

        #region RentalController Conts

        public static readonly string C_RENTAL_NOT_FOUND = "Rental not found";
        public static readonly string C_RENTAL_DELETED = "Rental deleted successfully";
        public static readonly string C_RENTAL_CPF_ERROR_MESSAGE = "CPF field is not correctly formatted.";
        public static readonly string C_RENTAL_INVALID_MOVIE_ERROR_MESSAGE = "Rental contains an invalid Movie Id. Crate this movie first.";
        #endregion
    }
}
