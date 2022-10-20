using ResvoyageMobileApp.Models.Hotel;
using ResvoyageMobileApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class FiltersViewModel : BaseViewModel
    {
        public FiltersViewModel()
        {

        }
        public FiltersViewModel(List<HotelInformation> results)
        {
            _sort = GetSortOptions();
            _chains = GetChains(results);
            _amenities = GetAmenities(results);
            _choosenChains = new List<CheckboxViewModel>();
            _chains.ForEach(x => _choosenChains.Add(new CheckboxViewModel { Title = x.Title }));
            _choosenAmenities = new List<CheckboxViewModel>();
            _amenities.ForEach(x => _choosenAmenities.Add(new CheckboxViewModel { Title = x.Title }));
            _stars = GetStars();
        }

        private List<CheckboxViewModel> _sort;

        public List<CheckboxViewModel> Sort
        {
            get { return _sort; }
            set { SetValue(ref _sort, value); }
        }
        private List<CheckboxViewModel> _choosenChains;

        public List<CheckboxViewModel> ChoosenChains
        {
            get { return _choosenChains; }
            set { SetValue(ref _choosenChains, value); }
        }
        private int? _numChains;

        public int? NumChains
        {
            get { return _numChains; }
            set { SetValue(ref _numChains, value); }
        }
        private List<CheckboxViewModel> _chains;

        public List<CheckboxViewModel> Chains
        {
            get { return _chains; }
            set { SetValue(ref _chains, value); }
        }
        private List<CheckboxViewModel> _choosenAmenities;

        public List<CheckboxViewModel> ChoosenAmenities
        {
            get { return _choosenAmenities; }
            set { SetValue(ref _choosenAmenities, value); }
        }
        private int? _numAmenities;

        public int? NumAmenities
        {
            get { return _numAmenities; }
            set { SetValue(ref _numAmenities, value); }
        }
        private List<CheckboxViewModel> _amenities;

        public List<CheckboxViewModel> Amenities
        {
            get { return _amenities; }
            set { SetValue(ref _amenities, value); }
        }
        private List<CheckboxViewModel> _stars;

        public List<CheckboxViewModel> Stars
        {
            get { return _stars; }
            set { SetValue(ref _stars, value); }
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
        private List<CheckboxViewModel> GetSortOptions()
        {
            return new List<CheckboxViewModel>
            {
                new CheckboxViewModel{ IsSelected = true, Title=AppResources.FF_CHEAPEST },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.HF_STAR_RATING },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.HF_HOTEL_NAME }
            };
        }
        private List<CheckboxViewModel> GetChains(List<HotelInformation> results)
        {
            var chains = new List<CheckboxViewModel>();
            foreach (var hotel in results)
            {
                if(!chains.Any(x => x.Title == hotel.ChainName))
                    chains.Add(new CheckboxViewModel() { Title = hotel.ChainName });
            }
            return chains;
        }
        private List<CheckboxViewModel> GetAmenities(List<HotelInformation> results)
        {
            var amenities = new List<CheckboxViewModel>();
            foreach (var hotel in results)
            {
                foreach (var listAmenities in hotel.HotelAmenitiesCollection)
                {
                    if (!amenities.Any(x => x.Title == listAmenities.Name))
                        amenities.Add(new CheckboxViewModel() { Title = listAmenities.Name });
                }
                
            }
            return amenities.OrderBy(x => x.Title).ToList();
        }
        private List<CheckboxViewModel> GetStars()
        {
            return new List<CheckboxViewModel>
            {
                new CheckboxViewModel {Title = "1"},
                new CheckboxViewModel {Title = "2"},
                new CheckboxViewModel {Title = "3"},
                new CheckboxViewModel {Title = "4"},
                new CheckboxViewModel {Title = "5"}
            };
        }
        public ICommand ApplyChain => new Command(ApplyChainFilter);
        public ICommand ResetChain => new Command(ResetChainFilter);
        public ICommand ApplyCheckChain => new Command<CheckboxViewModel>(CheckObject);
        public ICommand ApplyCheckAmenity => new Command<CheckboxViewModel>(CheckObject);
        public ICommand ApplyAmenity => new Command(ApplyAmenityFilter);
        public ICommand ResetAmenity => new Command(ResetAmenityFilter);

        private void ApplyChainFilter()
        {
            Chains = ChoosenChains;
            NumChains = Chains.Where(x => x.IsSelected).Count();
            _navigation.Navigation.PopAsync(true);
        }
        private void ResetChainFilter()
        {
            if (Chains != null && Chains.Count > 0)
            {
                NumChains = null;
                Chains.ForEach(x => x.IsSelected = false);
                ChoosenChains.ForEach(x => x.IsSelected = false);
            }
            _navigation.Navigation.PopAsync(true);
        }
        private void ApplyAmenityFilter()
        {
            Amenities = ChoosenAmenities;
            NumAmenities = Amenities.Where(x => x.IsSelected).Count();
            _navigation.Navigation.PopAsync(true);
        }
        private void ResetAmenityFilter()
        {
            if (Amenities != null && Amenities.Count > 0)
            {
                NumAmenities = null;
                Amenities.ForEach(x => x.IsSelected = false);
                ChoosenAmenities.ForEach(x => x.IsSelected = false);
            }
            _navigation.Navigation.PopAsync(true);
        }
        private void CheckObject(CheckboxViewModel obj)
        {
            obj.IsSelected = !obj.IsSelected;
        }
    }
}
