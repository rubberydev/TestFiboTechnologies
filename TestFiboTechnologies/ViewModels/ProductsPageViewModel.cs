using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.ItemsViewModel;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;

namespace TestFiboTechnologies.ViewModels
{
    public class ProductsPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IDbService _dbService;
        private ObservableCollection<ProductsItemViewModel> _products;

        public ObservableCollection<ProductsItemViewModel> Products
        {
            get=> this._products;
            set=>SetProperty(ref this._products, value);
        }
        public ObservableCollection<ProductsItemViewModel> AuxProducts
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
                this.AuxProducts = new ObservableCollection<ProductsItemViewModel>(this.ToProductsItemViewModel(parameters.GetValue<IEnumerable<ProductsDbModel>>("products")));

                this.Products = new ObservableCollection<ProductsItemViewModel>();
                foreach (var product in this.AuxProducts)
                {
                    var ratingOfProduct = await this._dbService.GetRatingAsync((int)product.Id);

                    if(ratingOfProduct.Rate >= 4.0) product.IsVisible = true;

                    this.Products.Add(product);

                }

            }
        }

        private IEnumerable<ProductsItemViewModel> ToProductsItemViewModel(IEnumerable<ProductsDbModel> products)
        {
            return products.Select(p => new ProductsItemViewModel(_navigationService)
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Category,
                Price = p.Price,
                Image = p.Image,
                Description = p.Description
            }).ToList();
        }
    }
}
