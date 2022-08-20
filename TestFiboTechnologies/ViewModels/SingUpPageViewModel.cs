using System;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;

namespace TestFiboTechnologies.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;
        DelegateCommand _onSingUpCommand;
        string _password;
        string _user_name;
        private string _confirm_user_name;
        private string _confirmPassword;

        public string Password
        {
            get => this._password;
            set => this.SetProperty(ref this._password, value);
        }

        public string UserName
        {
            get => this._user_name;
            set => this.SetProperty(ref this._user_name, value);
        }

        public string ConfirmPassword
        {
            get => this._confirmPassword;
            set => this.SetProperty(ref this._confirmPassword, value);
        }
        public DelegateCommand OnSingUpCommand => _onSingUpCommand ?? (_onSingUpCommand = new DelegateCommand(SingUp));

        

        public SingUpPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IDbService dbService)
          : base(navigationService)
        {
            this.Title = "Sing Up page";
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._dbService = dbService;
        }

        async void SingUp()
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
            if(this.Password.Trim() != this.ConfirmPassword.Trim())
            {
                await this._dialogService.DisplayAlertAsync("Error!", "the password not match... ", "OK");
                return;
            }

            var user = new Users();
            user.UserName = this.UserName;
            user.Password = this.Password;

            try
            {
               await this._dbService.InsertUserAsync(user);
               await this._dialogService.DisplayAlertAsync("Success", "the user was store successfully :)", "Ok");
               this.UserName = string.Empty;
               this.Password = string.Empty;

            }
            catch (Exception ex)
            {
                await this._dialogService.DisplayAlertAsync("Error", "it was not possible to sing up the user please try again", "Ok");
                return;
            }
        }
    }
}
