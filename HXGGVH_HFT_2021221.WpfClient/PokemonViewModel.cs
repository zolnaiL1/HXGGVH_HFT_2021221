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
    public class PokemonViewModel : ObservableRecipient
    {
        public RestCollection<Pokemon> Pokemons { get; set; }
        public RestService restService { get; set; } //nonCRUD


        private Pokemon selectedPokemon;

        public Pokemon SelectedPokemon
        {
            get { return selectedPokemon; }
            set 
            {
                if (value != null)
                {
                    selectedPokemon = new Pokemon()
                    {
                        Name = value.Name,
                        Health = value.Health,
                        Attack = value.Attack,
                        Defense = value.Defense,
                        Speed = value.Speed,
                        Type = value.Type,
                        TrainerID = value.TrainerID,
                        PokemonID = value.PokemonID
                    };
                    OnPropertyChanged();
                    (DeletePokemonCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreatePokemonCommand { get; set; }
        public ICommand DeletePokemonCommand { get; set; }
        public ICommand UpdatePokemonCommand { get; set; }

        public IEnumerable<Pokemon> PokemonsInKantoRegion //nonCRUD
        {
            get { return restService.Get<Pokemon>("stat/PokemonsInKantoRegion"); }
        }
        public IEnumerable<Pokemon> PokemonsWhereTrainerWinIs10 //nonCRUD
        {
            get { return restService.Get<Pokemon>("stat/PokemonsWhereTrainerWinIs10"); }
        }
        public IEnumerable<Pokemon> PokemonsWhereTrainerLevelUnder10 //nonCRUD
        {
            get { return restService.Get<Pokemon>("stat/PokemonsWhereTrainerLevelUnder10"); }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public PokemonViewModel()
        {     
            if (!IsInDesignMode)
            {
                restService = new RestService("http://localhost:35206/"); //nonCRUD
                Pokemons = new RestCollection<Pokemon>("http://localhost:35206/", "pokemon", "hub");
                CreatePokemonCommand = new RelayCommand(() =>
                {
                    Pokemons.Add(new Pokemon()
                    {
                        Name = SelectedPokemon.Name,
                        Health = SelectedPokemon.Health,
                        Attack = SelectedPokemon.Attack,
                        Defense = SelectedPokemon.Defense,
                        Speed = SelectedPokemon.Speed,
                        Type = SelectedPokemon.Type,
                        TrainerID = SelectedPokemon.TrainerID
                    });
                });

                UpdatePokemonCommand = new RelayCommand(() =>
                {
                    Pokemons.Update(SelectedPokemon);
                });

                DeletePokemonCommand = new RelayCommand(() =>
                {
                    Pokemons.Delete(SelectedPokemon.PokemonID);
                },
                () =>
                {
                    return SelectedPokemon != null;
                });
            }
            SelectedPokemon = new Pokemon();
        }
    }
}
