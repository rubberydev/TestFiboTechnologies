﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;
using TestFiboTechnologies.View;
using Xamarin.Forms;

namespace TestFiboTechnologies.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigatedAware
    {
        INavigationService _navigationService;
        IPageDialogService _dialogService;
        IDbService _dbService;
        IApiService _apiService;
        private DelegateCommand _onSingInCommand;
        private string _user_name;
        private DelegateCommand _onSingUpCommand;
        private bool _isvisibleKindOfCorrals;
        private DelegateCommand _onSaveCorralsCommand;
        private string _password;
        private int _capacity;
        private ObservableCollection<Charles> _corrals;

        public string Password
        {
            get => this._password;
            set => this.SetProperty(ref this._password, value);
        }

        public string UserName
        {
            get =>this._user_name;
            set => this.SetProperty(ref this._user_name, value);
        }

       

        public DelegateCommand OnSingInCommand => _onSingInCommand ?? (_onSingInCommand = new DelegateCommand(SingIn));

        public DelegateCommand OnSingUpCommand => _onSingUpCommand ?? (_onSingUpCommand = new DelegateCommand(SingUp));

        

        public MainPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IDbService dbService,
            IApiService apiService)
          : base(navigationService)
        {
            this.Title = "Home Page";
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._dbService = dbService;
            this._apiService = apiService;
        }

        async void SingUp()=> await this._navigationService.NavigateAsync(nameof(SingUpPage));
            
        



        private async void SingIn()
        {
            if (string.IsNullOrEmpty(this.UserName))
            {
                await this._dialogService.DisplayAlertAsync("Error!", "You must enter an user name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await this._dialogService.DisplayAlertAsync("Error!", "You must enter a password", "OK");
                return;
            }

            var user = new Users();
            user.UserName = this.UserName;
            user.Password = this.Password;

            try
            {
               var users = await this._dbService.GetUsersAsync();

                var userExist = users.Where(u => u.UserName.Trim() == this.UserName.Trim() && u.Password.Trim() == this.Password.Trim()).FirstOrDefault();

                if (userExist != null)
                {
                    ApiService apiService = new ApiService();

                    var isConnected = ApiService.CheckConnection();

                    if (!isConnected)
                        await this._dialogService.DisplayAlertAsync("Error", "You must turn on internet settings to get products from server", "Ok");


                    string urlBase = Application.Current.Resources["urlBase"].ToString();
                    string prefixService = Application.Current.Resources["prefixService"].ToString();
                    var apiResponse = await this._apiService.Get<ProductsModel>(urlBase, prefixService);

                    if(!apiResponse.IsSuccess)
                    {
                        await this._dialogService.DisplayAlertAsync("Success", "it could not get the products list", "Ok");
                        return;
                    }
                    await this._dialogService.DisplayAlertAsync("Success", "navigate success :)", "Ok");
                    return;
                    //navigate
                }
                else
                {
                    await this._dialogService.DisplayAlertAsync("Success", "you must sing up first, to singIn on this application", "Ok");
                    return;

                }

            }
            catch (Exception ex)
            {
                await this._dialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
                return;
            }

        }

    }
}
