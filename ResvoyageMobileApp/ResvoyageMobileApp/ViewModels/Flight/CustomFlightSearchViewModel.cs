using ResvoyageMobileApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Flight
{
    public class CustomFlightSearchViewModel : BaseViewModel
    {
        public CustomFlightSearchViewModel()
        {
            _isRoundTripVisible = true;
            _searchTypes = GetSearchTypes();
        }

        private bool _isRoundTripVisible;

        public bool IsRoundTripVisible
        {
            get { return _isRoundTripVisible; }
            set { SetValue(ref _isRoundTripVisible, value); }
        }
        private bool _isOneWayVisible;

        public bool IsOneWayVisible
        {
            get { return _isOneWayVisible; }
            set { SetValue(ref _isOneWayVisible, value); }
        }
        private bool _isMultiCityVisible;

        public bool IsMultiCityVisible
        {
            get { return _isMultiCityVisible; }
            set { SetValue(ref _isMultiCityVisible, value); }
        }
        private List<CheckboxViewModel> _searchTypes;

        public List<CheckboxViewModel> SearchTypes
        {
            get { return _searchTypes; }
            set { SetValue(ref _searchTypes, value); }
        }
        public ICommand ChangeType => new Command<CheckboxViewModel>(ChangeSearchType);

        private void ChangeSearchType(CheckboxViewModel obj)
        {
            SearchTypes.ForEach(x => x.IsSelected = false);
            obj.IsSelected = true;
            IsRoundTripVisible = false;
            IsOneWayVisible = false;
            IsMultiCityVisible = false;
            if (obj.Title == AppResources.SF_ROUND_TRIP)
                IsRoundTripVisible = true;
            else if (obj.Title == AppResources.SF_ONE_WAY)
                IsOneWayVisible = true;
            else if (obj.Title == AppResources.SF_MULTI_CITY)
                IsMultiCityVisible = true;
        }
        private List<CheckboxViewModel> GetSearchTypes()
        {
            return new List<CheckboxViewModel>
            {
                new CheckboxViewModel{ IsSelected = true, Title=AppResources.SF_ROUND_TRIP },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.SF_ONE_WAY },
                new CheckboxViewModel{ IsSelected = false, Title=AppResources.SF_MULTI_CITY }
            };
        }
    }
}
