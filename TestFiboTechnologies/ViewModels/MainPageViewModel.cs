using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;

namespace TestFiboTechnologies.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;
        private DelegateCommand _onSaveClicked;
        private string _name;
        private DelegateCommand _navigateCommand;
        private bool _isvisibleKindOfCorrals;
        private DelegateCommand _onSaveCorralsCommand;
        private string _kind;
        private int _capacity;
        private ObservableCollection<Charles> _corrals;
        private Charles _corralsSelected;
        private int _cantOfAnimalsToEnterInCorral;

        public int Capacity
        {
            get => this._capacity;
            set => this.SetProperty(ref this._capacity, value);
        }
        public string Kind
        {
            get => this._kind;
            set => this.SetProperty(ref this._kind, value);
        }

        public bool IsvisibleKindOfCorrals
        {
            get => this._isvisibleKindOfCorrals;
            set => this.SetProperty(ref this._isvisibleKindOfCorrals, value);
        }

        public string Name
        {
            get =>this._name;
            set => this.SetProperty(ref this._name,value);
        }

        public int CantOfAnimalsToEnterInCorral
        {
            get => this._cantOfAnimalsToEnterInCorral;
            set => this.SetProperty(ref this._cantOfAnimalsToEnterInCorral, value);
        }

        public ObservableCollection<Charles> Corrals
        {
            get => this._corrals;
            set => this.SetProperty(ref this._corrals, value);
        }

        public Charles CorralsSelected
        {
            get => this._corralsSelected;
            set => this.SetProperty(ref this._corralsSelected, value);
        }

        //public ObservableCollection<Animals> Animals { get; set; }

        public DelegateCommand OnSaveClicked => _onSaveClicked ?? (_onSaveClicked = new DelegateCommand(SaveItem));

        public DelegateCommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand(Navigate));

        public DelegateCommand OnSaveCorralsCommand => _onSaveCorralsCommand ?? (_onSaveCorralsCommand = new DelegateCommand(SaveKindOfCorrals));

        

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IDbService dbService)
          : base(navigationService)
        {
            this.Title = "HomePage";
            this.IsvisibleKindOfCorrals = false;
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._dbService = dbService;
            this.InitializeCorrals();
        }

        private async void InitializeCorrals()
        {
            var corrals = await _dbService.GetCorralsAsync();

            if (corrals.Count > 0)
            {
                this.Corrals = new ObservableCollection<Charles>(corrals);
                this.IsvisibleKindOfCorrals = true;

            }

        }

        private async void Navigate()
        {
          
            var listOfAnimals = await this._dbService.GetItemsAsync();
            var parameters = new NavigationParameters
            {

                { "Animals", listOfAnimals }

            };

            await this._navigationService.NavigateAsync("AnimalsPage", parameters);


        }

        

        private async void SaveItem()
        {

            try
            {
                if(Equals(CorralsSelected, null))
                {
                    await this._dialogService.DisplayAlertAsync("bad thing :(", "you must select a kind of animal to enter in a corral and try again...", "Ok");
                    return;

                }

                var corral = await this._dbService.GetCorralAsyncById(CorralsSelected.ID);


                if(corral.MaximunByCorral> this.CantOfAnimalsToEnterInCorral)
                {
                    //save item
                    Animals animal = new Animals();
                    animal.Name = this.Name;
                    animal.CantOfAnimals = this.CantOfAnimalsToEnterInCorral;
                    animal.CharlesId = CorralsSelected.ID;
                    await _dbService.InsertItemAsync(animal);
                    await this._dialogService.DisplayAlertAsync("Good :)", "Register stored sucessfully!!", "Ok");
                    this.Name = string.Empty;
                    this.CantOfAnimalsToEnterInCorral = 0;
                }
                else
                {
                    await this._dialogService.DisplayAlertAsync("bad thing :(", "the amount of animals is bigger than of total capacity!!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await this._dialogService.DisplayAlertAsync("Error :(", ex.Message, "Ok");

            }

        }

        private async void SaveKindOfCorrals()
        {
            try
            {

                
                Charles charles = new Charles()
                {
                    CategoryName = this.Kind,
                    MaximunByCorral = this.Capacity
                };

                await _dbService.InsertItemAsync(charles);
                await this._dialogService.DisplayAlertAsync("Good :)", "Register stored sucessfully!!", "Ok");
                this.IsvisibleKindOfCorrals = true;
                this.InitializeCorrals();
                this.Kind = string.Empty;
                this.Capacity = 0;

            }
            catch (Exception ex)
            {
                await this._dialogService.DisplayAlertAsync("Error :(", ex.Message, "Ok");

            }

        }




    }
}
