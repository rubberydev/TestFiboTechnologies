using System;
using Prism.Commands;
using Prism.Navigation;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.View;

namespace TestFiboTechnologies.ItemsViewModel
{
    public class ProductsItemViewModel : ProductsDbModel
    {
        private INavigationService _navigationService;

        private DelegateCommand _selectProductCommand;

        public DelegateCommand SelectProductCommand => _selectProductCommand ?? (_selectProductCommand = new DelegateCommand(NavigateToViewDetailOfProduct));

        

        public ProductsItemViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        async void NavigateToViewDetailOfProduct()
        {
            var parameter = new NavigationParameters { { "product", this } };

            await this._navigationService.NavigateAsync(nameof(ProductDetailPage), parameter);

            
        }
    }
}
