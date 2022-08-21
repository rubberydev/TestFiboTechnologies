using System;
using Prism.Navigation;
using Prism.Services;
using TestFiboTechnologies.Models;
using TestFiboTechnologies.Services;

namespace TestFiboTechnologies.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IDbService _dbService;
        IPageDialogService _dialogService;
        private ProductsDbModel _character;
        private string _rating;
        private string _price;
        private string _category;
        private string _nameProduct;

        public string Rating
        {
            get => this._rating;
            set=> SetProperty(ref this._rating, value);
        }

        public string Price
        {
            get => this._price;
            set => SetProperty(ref this._price, value);
        }

        public string Category
        {
            get => this._category;
            set => SetProperty(ref this._category, value);
        }
       
            public string NameProduct
        {
            get => this._nameProduct;
            set => SetProperty(ref this._nameProduct, value);
        }
        public ProductDetailPageViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IDbService dbService)
          : base(navigationService)
        {
            this.Title = "Product Detail Page";
            this._navigationService = navigationService;
            this._dialogService = dialogService;
            this._dbService = dbService;
        }
        public ProductsDbModel Product
        {
            get => this._character;
            set => SetProperty(ref this._character, value);
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)   
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                this.Product = parameters.GetValue<ProductsDbModel>("product");

                this.NameProduct = this.Product.Title;

                var rating = await this._dbService.GetRatingAsync((int)this.Product.Id);

                this.Rating = $"Rating: {rating.Rate}";
                this.Price = $"Price: {this.Product.Price}";
                this.Category = $"Category: {this.Product.Category}";

            }
        }
    }
}
