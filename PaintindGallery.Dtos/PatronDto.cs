using System;

namespace PaintindGallery.Dtos
{
    public class PatronDto
    {
        public int Id { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Picture { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Death { get; set; }
    }
}
