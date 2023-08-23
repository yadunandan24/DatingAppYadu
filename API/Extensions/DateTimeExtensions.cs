using System.Runtime.CompilerServices;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var age = today.Year - dob.Year;
        
            if(dob > today.AddYears(-age)) //for bday which have not crosses yet
            {
                age--;
            }

            return age;
        }
    }
}
