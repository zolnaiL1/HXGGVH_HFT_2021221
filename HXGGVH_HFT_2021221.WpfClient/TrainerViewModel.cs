using HXGGVH_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HXGGVH_HFT_2021221.WpfClient
{
    class TrainerViewModel : ObservableRecipient
    {
        public RestCollection<Trainer> Trainers { get; set; }

        private Trainer selectedTrainer;

        public Trainer SelectedTrainer
        {
            get { return selectedTrainer; }
            set
            {
                if (value != null)
                {
                    selectedTrainer = new Trainer()
                    {

                        Name = value.Name,
                        Wins = value.Wins,
                        Level = value.Level,
                        TrainerID = value.TrainerID,
                        RegionID = value.RegionID
                    };
                    OnPropertyChanged();
                    (DeleteTrainerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateTrainerCommand { get; set; }
        public ICommand DeleteTrainerCommand { get; set; }
        public ICommand UpdateTrainerCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public TrainerViewModel()
        {
            if (!IsInDesignMode)
            {
                Trainers = new RestCollection<Trainer>("http://localhost:35206/", "trainer", "hub");
                CreateTrainerCommand = new RelayCommand(() =>
                {
                    Trainers.Add(new Trainer()
                    {
                        Name = SelectedTrainer.Name,
                        Wins = SelectedTrainer.Wins,
                        Level = SelectedTrainer.Level,
                        RegionID = SelectedTrainer.RegionID                       
                    });
                });

                UpdateTrainerCommand = new RelayCommand(() =>
                {
                    Trainers.Update(SelectedTrainer);
                });

                DeleteTrainerCommand = new RelayCommand(() =>
                {
                    Trainers.Delete(SelectedTrainer.RegionID);
                },
                () =>
                {
                    return SelectedTrainer != null;
                });
            }
            SelectedTrainer = new Trainer();
        }
    }
}
