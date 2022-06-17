using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;

namespace TestFiboTechnologies.ViewModels
{
    public class AnimalsPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;
        private string _name;
        private ObservableCollection<Animals> _animals;


        public ObservableCollection<Animals> Animals
        {
            get => this._animals;
            set => this.SetProperty(ref this._animals, value);
        }
        

        public string Name
        {
            get => this._name;
            set => this.SetProperty(ref this._name, value);
        }

        public DelegateCommand BackCommand => _backCommand ?? (_backCommand = new DelegateCommand(Back));
        private DelegateCommand _backCommand;

        public AnimalsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
            : base(navigationService)
        {
            this.Title = "List of Animals";
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._dbService = dbService;
        }

        private void Back() => this._navigationService.GoBackAsync();


        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("Animals"))
            {
                this.Animals = new ObservableCollection<Animals>(parameters.GetValue<IEnumerable<Animals>>("Animals"));

                var auxAnimals = new ObservableCollection<Animals>();
                foreach (var item in this.Animals)
                {
                    if (string.IsNullOrEmpty(item.KindOfCorral))
                    {
                        var corral = await _dbService.GetCorralAsyncById(item.CharlesId);

                        item.KindOfCorral = corral.CategoryName;
                        
                    }
                    item.Name = item.Name;
                    item.CantOfAnimals = item.CantOfAnimals;
                    auxAnimals.Add(item);
                }

                this.Animals = new ObservableCollection<Animals>();
                this.Animals = auxAnimals;
                
                this.Name = $"{Animals.Select(a => a.Name).FirstOrDefault()} {Animals.Select(a => a.CantOfAnimals).FirstOrDefault()} ";

            }
        }
    }
}
