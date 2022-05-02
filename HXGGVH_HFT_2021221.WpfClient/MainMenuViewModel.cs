using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HXGGVH_HFT_2021221.WpfClient
{
    class MainMenuViewModel
    {
        public ICommand PokemonWindowCommand { get; set; }
        public ICommand TrainerWindowCommand { get; set; }
        public ICommand RegionWindowCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public MainMenuViewModel()
        {
            PokemonWindowCommand = new RelayCommand(() =>
            {
                var pokemonWindow = new PokemonWindow();
                pokemonWindow.Show();
            });

            TrainerWindowCommand = new RelayCommand(() =>
            {
                var trainerWindow = new TrainerWindow();
                trainerWindow.Show();
            });

            RegionWindowCommand = new RelayCommand(() =>
            {
                var regionWindow = new RegionWindow();
                regionWindow.Show();
            });

            ExitCommand = new RelayCommand(() => 
                          Environment.Exit(0));
        }
    }
}
