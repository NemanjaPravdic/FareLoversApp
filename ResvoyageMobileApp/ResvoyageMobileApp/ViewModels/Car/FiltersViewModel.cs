using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class FiltersViewModel : BaseViewModel
    {
        public FiltersViewModel()
        {

        }
        public FiltersViewModel(List<CarInformation> results)
        {
            _sort = GetSortOptions();
            _categories = GetCategories(results);
            _types = GetTypes(results);
        }
        private List<CheckboxViewModel> _sort;

        public List<CheckboxViewModel> Sort
        {
            get { return _sort; }
            set { SetValue(ref _sort, value); }
        }
        private decimal _minPrice;

        public decimal MinPrice
        {
            get { return _minPrice; }
            set { SetValue(ref _minPrice, value); }
        }
        private decimal _maxPrice;

        public decimal MaxPrice
        {
            get { return _maxPrice; }
            set { SetValue(ref _maxPrice, value); }
        }
        private List<CheckboxViewModel> _choosenTypes;

        public List<CheckboxViewModel> ChoosenTypes
        {
            get { return _choosenTypes; }
            set { SetValue(ref _choosenTypes, value); }
        }
        private int? _numTypes;

        public int? NumTypes
        {
            get { return _numTypes; }
            set { SetValue(ref _numTypes, value); }
        }
        private List<CheckboxViewModel> _types;

        public List<CheckboxViewModel> Types
        {
            get { return _types; }
            set { SetValue(ref _types, value); }
        }
        private List<CheckboxViewModel> _choosenCategories;

        public List<CheckboxViewModel> ChoosenCategories
        {
            get { return _choosenCategories; }
            set { SetValue(ref _choosenCategories, value); }
        }
        private int? _numCategories;

        public int? NumCategories
        {
            get { return _numCategories; }
            set { SetValue(ref _numCategories, value); }
        }
        private List<CheckboxViewModel> _categories;

        public List<CheckboxViewModel> Categories
        {
            get { return _categories; }
            set { SetValue(ref _categories, value); }
        }
        private List<CheckboxViewModel> GetSortOptions()
        {
            return new List<CheckboxViewModel>
            {
                new CheckboxViewModel{ IsSelected = true, Title=AppResources.FF_CHEAPEST },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.CR_CAR_NAME },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.CR_CATEGORY }
            };
        }
        private List<CheckboxViewModel> GetCategories(List<CarInformation> results)
        {
            var categoires = new List<CheckboxViewModel>();
            foreach (var car in results)
            {
                if (!categoires.Any(x => x.Title == car.VehicleInfo.VehClass))
                    categoires.Add(new CheckboxViewModel() { Title = car.VehicleInfo.VehClass });
            }
            return categoires;


        }
        private List<CheckboxViewModel> GetTypes(List<CarInformation> results)
        {
            var types = new List<CheckboxViewModel>();
            foreach (var car in results)
            {
                if (!types.Any(x => x.Title == car.VehicleInfo.VehCategory))
                    types.Add(new CheckboxViewModel() { Title = car.VehicleInfo.VehCategory });
            }
            return types;
        }
        public ICommand ApplyCategories => new Command(ApplyCategoryFilter);
        public ICommand ResetCategories => new Command(ResetCategoryFilter);
        public ICommand ApplyCheckCategory => new Command<CheckboxViewModel>(CheckObject);
        public ICommand ApplyTypes => new Command(ApplyTypeFilter);
        public ICommand ResetTypes => new Command(ResetTypeFilter);
        public ICommand ApplyCheckType => new Command<CheckboxViewModel>(CheckObject);
        private void ApplyCategoryFilter()
        {
            Categories = ChoosenCategories;
            NumCategories = Categories.Where(x => x.IsSelected).Count();
            _navigation.Navigation.PopAsync(true);
        }
        private void ResetCategoryFilter()
        {
            if (Categories != null && Categories.Count > 0)
            {
                NumCategories = null;
                Categories.ForEach(x => x.IsSelected = false);
                ChoosenCategories.ForEach(x => x.IsSelected = false);
            }
            _navigation.Navigation.PopAsync(true);
        }
        private void ApplyTypeFilter()
        {
            Types = ChoosenTypes;
            NumTypes = Types.Where(x => x.IsSelected).Count();
            _navigation.Navigation.PopAsync(true);
        }
        private void ResetTypeFilter()
        {
            if (Types != null && Types.Count > 0)
            {
                NumTypes = null;
                Types.ForEach(x => x.IsSelected = false);
                ChoosenTypes.ForEach(x => x.IsSelected = false);
            }
            _navigation.Navigation.PopAsync(true);
        }
        private void CheckObject(CheckboxViewModel obj)
        {
            obj.IsSelected = !obj.IsSelected;
        }
    }
}
