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
    class RegionViewModel : ObservableRecipient
    {
        public RestCollection<Region> Regions { get; set; }
        public RestService restService { get; set; } //nonCRUD

        private Region selectedRegion;

        public Region SelectedRegion
        {
            get { return selectedRegion; }
            set
            {
                if (value != null)
                {
                    selectedRegion = new Region()
                    {
                        Name = value.Name,
                        RegionID = value.RegionID
                    };
                    OnPropertyChanged();
                    (DeleteRegionCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateRegionCommand { get; set; }
        public ICommand DeleteRegionCommand { get; set; }
        public ICommand UpdateRegionCommand { get; set; }

        public IEnumerable<Region> RegionWherePikachuLives //nonCRUD
        {
            get { return restService.Get<Region>("stat/RegionWherePikachuLives"); }
        }
        public IEnumerable<Region> RegionWherePokemonsTypeIsWater //nonCRUD
        {
            get { return restService.Get<Region>("stat/RegionWherePokemonsTypeIsWater"); }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public RegionViewModel()
        {
            if (!IsInDesignMode)
            {
                restService = new RestService("http://localhost:35206/"); //nonCRUD

                Regions = new RestCollection<Region>("http://localhost:35206/", "region", "hub");
                CreateRegionCommand = new RelayCommand(() =>
                {
                    Regions.Add(new Region()
                    {
                        Name = SelectedRegion.Name
                    });
                });

                UpdateRegionCommand = new RelayCommand(() =>
                {
                    Regions.Update(SelectedRegion);
                });

                DeleteRegionCommand = new RelayCommand(() =>
                {
                    Regions.Delete(SelectedRegion.RegionID);
                },
                () =>
                {
                    return SelectedRegion != null;
                });
            }
            SelectedRegion = new Region();
        }
    }
}
