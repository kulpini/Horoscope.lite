using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Horoscope.Model
{
    public class Pair : INotifyPropertyChanged
    {
        public Person FirstPerson { get; set; }
        public Person SecondPerson { get; set; }

        private PairHoroscope horoscope;
        public PairHoroscope Horoscope
        {
            get => horoscope;
            set
            {
                horoscope = value;
                OnPropertyChanged(nameof(Horoscope));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Pair(Person firstPerson, Person secondPerson)
        {
            FirstPerson = firstPerson;
            SecondPerson = secondPerson;
            Horoscope = new PairHoroscope();
        }

        public void CalculateHoroscope()
        {
            FirstPerson.CalculateHoroscope();
            SecondPerson.CalculateHoroscope();
            Horoscope.Calculate(FirstPerson.Horoscope,SecondPerson.Horoscope);
        }
    }
}
