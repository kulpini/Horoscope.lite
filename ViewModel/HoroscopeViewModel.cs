using Horoscope.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Horoscope.ViewModel
{
    internal class HoroscopeViewModel : INotifyPropertyChanged
    {
        public Person FirstPerson { get; set; }

        public Person SecondPerson { get; set; }

        public Pair Pair { get; set; }

        private PersonalHoroscope personalHoroscope;
        public PersonalHoroscope PersonalHoroscope
        {
            get => personalHoroscope;
            set
            {
                personalHoroscope = value;
                OnPropertyChanged(nameof(PersonalHoroscope));
            }
        }

        private int reportProgress;
        public int ReportProgress
        {
            get => reportProgress;
            set
            {
                reportProgress = value;
                OnPropertyChanged(nameof(ReportProgress));
            }
        }

        public HoroscopeViewModel()
        {
            FirstPerson = new Person(Sex.Female);
            SecondPerson = new Person(Sex.Male);
            PersonalHoroscope = new PersonalHoroscope();
            Pair = new Pair(FirstPerson, SecondPerson);
        }

        private RelayCommand setFirstPersonGender;
        public RelayCommand SetFirstPersonGender =>
            setFirstPersonGender ??= new RelayCommand(obj =>
            {
                FirstPerson.Gender = obj.ToString() == "Male" ? Sex.Male : Sex.Female;
            });

        private RelayCommand calculateHoroscope;
        public RelayCommand CalculateHoroscope =>
            calculateHoroscope ??= new RelayCommand(obj =>
            {
                PersonalHoroscope.Calculate(((Person)obj).BirthDate);
                ((Person)obj).CalculateHoroscope();
            });

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            ReportProgress = e.ProgressPercentage;
        }

        private RelayCommand personalReport;
        public RelayCommand PersonalReport =>
            personalReport ??= new RelayCommand(obj =>
            {
                ReportProgress = 0;
                BackgroundWorker worker = new();
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.DoWork += MakePersonalReport;
                worker.RunWorkerAsync(obj);
            });

        private RelayCommand pairReport;
        public RelayCommand PairReport =>
            pairReport ??= new RelayCommand(obj =>
            {
                ReportProgress = 0;
                BackgroundWorker worker = new();
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.DoWork += MakePairReport;
                worker.RunWorkerAsync(obj);
            });

        private void MakePersonalReport(object? sender, DoWorkEventArgs e)
        {
            PersonalReport report = new((Person)e.Argument);
            report.MakeReport(sender);
            MessageBox.Show("Файл сформовано!");
        }

        private void MakePairReport(object? sender, DoWorkEventArgs e)
        {
            PairReport report = new(Pair);
            report.MakeReport(sender);
            MessageBox.Show("Файл сформовано!");
        }

        private RelayCommand setSecondPersonGender;
        public RelayCommand SetSecondPersonGender =>
            setSecondPersonGender ??= new RelayCommand(obj =>
            {
                SecondPerson.Gender = obj.ToString() == "Male" ? Sex.Male : Sex.Female;
            });

        private RelayCommand calculatePairHoroscope;
        public RelayCommand CalculatePairHoroscope =>
            calculatePairHoroscope ??= new RelayCommand(obj =>
            {
                Pair.CalculateHoroscope();
            });

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
