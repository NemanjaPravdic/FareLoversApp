using ResvoyageMobileApp.Models.Car;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Car;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarFilterViewModel : BaseViewModel
    {
        public CarFilterViewModel(CarResultViewModel results)
        {
			_results = results;
			_choosenFilters = new FiltersViewModel
			{
				Sort = _results.Filters.Sort,
				MinPrice = _results.Filters.MinPrice == 0 ? _results.MinPrice : _results.Filters.MinPrice,
				MaxPrice = _results.Filters.MaxPrice == 0 ? _results.MaxPrice : _results.Filters.MaxPrice,
				Categories = new List<CheckboxViewModel>(),
				ChoosenCategories = new List<CheckboxViewModel>(),
				NumCategories = _results.Filters.NumCategories,
				Types = new List<CheckboxViewModel>(),
				ChoosenTypes = new List<CheckboxViewModel>(),
				NumTypes = _results.Filters.NumCategories,
			};
			results.Filters.Categories.ForEach(x => _choosenFilters.Categories.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			results.Filters.Types.ForEach(x => _choosenFilters.Types.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
		}
		private CarResultViewModel _results;

		public CarResultViewModel Results
		{
			get { return _results; }
			set { SetValue(ref _results, value); }
		}
		private FiltersViewModel _choosenFilters;

		public FiltersViewModel ChoosenFilters
		{
			get { return _choosenFilters; }
			set { SetValue(ref _choosenFilters, value); }
		}
		public ICommand ShowCategories => new Command(ShowCategoryFilters);
		public ICommand ShowTypes => new Command(ShowTypeFilters);
		public ICommand ApplyFilters => new Command(ApplyCarFilters);
		public ICommand ResetFilters => new Command(ResetCarFilters);
		private void ShowCategoryFilters()
		{
			_choosenFilters.ChoosenCategories = new List<CheckboxViewModel>();
			_choosenFilters.Categories.ForEach(x => _choosenFilters.ChoosenCategories.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			var page = new CarCategoryFilterPage(_choosenFilters);
			_navigation.Navigation.PushAsync(page, true);
		}
		private void ShowTypeFilters()
		{
			_choosenFilters.ChoosenTypes = new List<CheckboxViewModel>();
			_choosenFilters.Types.ForEach(x => _choosenFilters.ChoosenTypes.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			var page = new CarTypeFilterPage(_choosenFilters);
			_navigation.Navigation.PushAsync(page, true);
		}
		private void ApplyCarFilters()
		{
			_results.Filters = _choosenFilters;
			if (_results.Results != null && _results.Results.Count > 0)
			{
				var filterdResults = new List<CarInformation>(_results.Results);
				if (_results.Filters.Categories.Any(x => x.IsSelected))
				{
					List<string> categories = _results.Filters.Categories.Where(x => x.IsSelected).Select(x => x.Title).ToList();
					filterdResults = filterdResults.FindAll(x => x.VehicleInfo.VehClass != null && categories.Contains(x.VehicleInfo.VehClass));
				}
				if (_results.Filters.Types.Any(x => x.IsSelected))
				{
					List<string> types = _results.Filters.Types.Where(x => x.IsSelected).Select(x => x.Title).ToList();
					filterdResults = filterdResults.FindAll(x => x.VehicleInfo.VehCategory != null && types.Contains(x.VehicleInfo.VehCategory));
				}
				filterdResults = filterdResults.FindAll(x => x.VehicleInfo.RateTotalAmount >= _results.Filters.MinPrice && x.VehicleInfo.RateTotalAmount <= _results.Filters.MaxPrice);
				_results.FilterdResults = new ObservableCollection<CarInformation>(filterdResults);

				SortResults();

			}
			_navigation.Navigation.PopAsync(true);
		}

		private void SortResults()
		{
			var selectedSorting = _results.Filters.Sort.Where(x => x.IsSelected).FirstOrDefault();
			if (selectedSorting != null)
			{
				if (selectedSorting.Title == AppResources.FF_CHEAPEST)
				{
					_results.FilterdResults = new ObservableCollection<CarInformation>(_results.FilterdResults.OrderBy(x => x.VehicleInfo.RateTotalAmount));
				}
				else if (selectedSorting.Title == AppResources.CR_CAR_NAME)
				{
					_results.FilterdResults = new ObservableCollection<CarInformation>(_results.FilterdResults.OrderBy(x => x.VehicleInfo.VehModel));
				}
				else if (selectedSorting.Title == AppResources.CR_CATEGORY)
				{
					_results.FilterdResults = new ObservableCollection<CarInformation>(_results.FilterdResults.OrderBy(x => x.VehicleInfo.VehClass));
				}
			}
		}

		private void ResetCarFilters()
		{
			_results.FilterdResults = _results.Results;
			_results.Filters = new FiltersViewModel(new List<CarInformation>(_results.Results));
			_navigation.Navigation.PopAsync(true);
		}
	}
}
