using System.ComponentModel.DataAnnotations;

namespace Barber.Models
{
    public class Appoint
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
