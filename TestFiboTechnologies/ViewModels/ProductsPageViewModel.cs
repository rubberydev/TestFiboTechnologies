using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;

namespace TestFiboTechnologies.ViewModels
{
    public class ProductsPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;
        private ObservableCollection<ProductsDbModel> _products;

        public ObservableCollection<ProductsDbModel> Products
        {
            get=> this._products;
            set=>SetProperty(ref this._products, value);
        }
        public ObservableCollection<ProductsDbModel> AuxProducts
        {
            get;
            set;
        }

        public ProductsPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IDbService dbService)
          : base(navigationService)
        {
            this.Title = "Products page";
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._dbService = dbService;
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("products"))
            {
                this.AuxProducts = new ObservableCollection<ProductsDbModel>(parameters.GetValue<IEnumerable<ProductsDbModel>>("products"));

                this.Products = new ObservableCollection<ProductsDbModel>();
                foreach (var product in this.AuxProducts)
                {
                    var ratingOfProduct = await this._dbService.GetRatingAsync((int)product.Id);

                    if(ratingOfProduct.Rate >= 4.0) product.IsVisible = true;

                    this.Products.Add(product);

                }

            }
        }
    }
}
