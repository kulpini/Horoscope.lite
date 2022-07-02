using System;

namespace Horoscope.Model
{
    public class Person
    {
        public DateTime BirthDate { get; set; }
        public Sex Gender { get; set; }
        public PersonalHoroscope Horoscope { get; set; }
        public Person(Sex gender)
        {
            Gender = gender;
            BirthDate = DateTime.Now;
            Horoscope = new PersonalHoroscope();
        }
        public void CalculateHoroscope()
        {
            Horoscope.Calculate(BirthDate);
        }
    }
}
