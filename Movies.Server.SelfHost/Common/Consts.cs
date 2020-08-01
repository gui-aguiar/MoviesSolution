using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Server.SelfHost.Common
{
    public class Consts
    {
        #region Json Conts

        public static readonly string C_MT_JSON = "application/json";

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

        #endregion
    }
}
