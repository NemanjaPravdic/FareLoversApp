using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.Resources;
using ResvoyageMobileApp.Views.Hotel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class HotelFiltersViewModel : BaseViewModel
    {
		public HotelFiltersViewModel(HotelResultViewModel results)
		{
			_results = results;
			_choosenFilters = new FiltersViewModel
			{
				Sort = _results.Filters.Sort,
				MinPrice = _results.Filters.MinPrice == 0 ? _results.MinPrice : _results.Filters.MinPrice,
				MaxPrice = _results.Filters.MaxPrice == 0 ? _results.MaxPrice : _results.Filters.MaxPrice,
				Chains = new List<CheckboxViewModel>(),
				ChoosenChains = new List<CheckboxViewModel>(),
				Stars = new List<CheckboxViewModel>(),
				NumChains = _results.Filters.NumChains,
				Amenities = new List<CheckboxViewModel>(),
				ChoosenAmenities = new List<CheckboxViewModel>(),
				NumAmenities = _results.Filters.NumAmenities
			};
			_results.Filters.Chains.ForEach(x => _choosenFilters.Chains.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			_results.Filters.Amenities.ForEach(x => _choosenFilters.Amenities.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			_results.Filters.Stars.ForEach(x => _choosenFilters.Stars.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
		}
		private HotelResultViewModel _results;

		public HotelResultViewModel Results
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
		public ICommand ShowChains => new Command(ShowChainsFilters);
		public ICommand ShowHotelAmenities => new Command(ShowHotelAmenitiesFilters);
		public ICommand CheckStar => new Command<CheckboxViewModel>(CheckStarRate);
		public ICommand ApplyFilters => new Command(ApplyHotelFilters);
		public ICommand ResetFilters => new Command(ResetHotelFilters);

		private void CheckStarRate(CheckboxViewModel obj)
		{
			obj.IsSelected = !obj.IsSelected;
		}

		private void ShowChainsFilters()
		{
			_choosenFilters.ChoosenChains = new List<CheckboxViewModel>();
			_choosenFilters.Chains.ForEach(x => _choosenFilters.ChoosenChains.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			var page = new HotelChainFilterPage(_choosenFilters);
			_navigation.Navigation.PushAsync(page, true);
		}
		private void ShowHotelAmenitiesFilters()
		{
			_choosenFilters.ChoosenAmenities = new List<CheckboxViewModel>();
			_choosenFilters.Amenities.ForEach(x => _choosenFilters.ChoosenAmenities.Add(new CheckboxViewModel { IsSelected = x.IsSelected, Title = x.Title }));
			var page = new HotelAmenitiesFilterPage(_choosenFilters);
			_navigation.Navigation.PushAsync(page, true);
		}
		private void ApplyHotelFilters()
		{
			_results.Filters = _choosenFilters;
			if (_results.Results != null && _results.Results.Count > 0)
			{
				var filterdResults = new List<HotelInformation>(_results.Results);
				if (_results.Filters.Stars.Any(x => x.IsSelected))
				{
					List<string> stars = _results.Filters.Stars.Where(x => x.IsSelected).Select(x => x.Title).ToList();
					filterdResults = filterdResults.FindAll(x => x.StarRating != null && stars.Contains(x.StarRating));
				}
				if (_results.Filters.Chains.Any(x => x.IsSelected))
				{ 
					List<string> chains = _results.Filters.Chains.Where(x => x.IsSelected).Select(x => x.Title).ToList();
					filterdResults = filterdResults.FindAll(x => x.ChainName != null && chains.Contains(x.ChainName));
				}
				if (_results.Filters.Amenities.Any(x => x.IsSelected))
				{
					List<string> amenities = _results.Filters.Amenities.Where(x => x.IsSelected).Select(x => x.Title).ToList();
					filterdResults = filterdResults.FindAll(x => x.HotelAmenitiesCollection != null && x.HotelAmenitiesCollection.Count > 0 && amenities.All(y => x.HotelAmenitiesCollection.Any(it => it.Name == y)));
				}
				filterdResults = filterdResults.FindAll(x => x.DailyRatePerRoom >= _results.Filters.MinPrice && x.DailyRatePerRoom <= _results.Filters.MaxPrice);
				_results.FilterdResults = new ObservableCollection<HotelInformation>(filterdResults);

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
					_results.FilterdResults = new ObservableCollection<HotelInformation>(_results.FilterdResults.OrderBy(x => x.DailyRatePerRoom));
				}
				else if (selectedSorting.Title == AppResources.HF_STAR_RATING)
				{
					_results.FilterdResults = new ObservableCollection<HotelInformation>(_results.FilterdResults.OrderByDescending(x => x.StarRatingDecimal));
				}
				else if (selectedSorting.Title == AppResources.HF_HOTEL_NAME)
				{
					_results.FilterdResults = new ObservableCollection<HotelInformation>(_results.FilterdResults.OrderBy(x => x.HotelName));
				}
			}
		}

		private void ResetHotelFilters()
		{
			_results.FilterdResults = _results.Results;
			_results.Filters = new FiltersViewModel(new List<HotelInformation>(_results.Results));
			_navigation.Navigation.PopAsync(true);
		}
	}
}
