using ResvoyageMobileApp.Models.Car;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResvoyageMobileApp.ViewModels.Car
{
    public class CarShoppingCartViewModel : BaseViewModel
    {
        public CarShoppingCartViewModel(CarInformation selectedCar)
        {
            _selectedCar = selectedCar;
        }
        private CarInformation _selectedCar;

        public CarInformation SelectedCar
        {
            get { return _selectedCar; }
            set { SetValue(ref _selectedCar, value); }
        }
        private bool _taxesVisibility;

        public bool TaxesVisibility
        {
            get { return _taxesVisibility; }
            set { SetValue(ref _taxesVisibility, value); }
        }
        private bool _insuranceVisibility;

        public bool InsuranceVisibility
        {
            get { return _insuranceVisibility; }
            set { SetValue(ref _insuranceVisibility, value); }
        }
        public ICommand TexesCollapse => new Command(CollapseTaxes);
        public ICommand InsuranceCollapse => new Command(CollapseInsurance);

        private void CollapseInsurance()
        {
            InsuranceVisibility = !InsuranceVisibility;
        }

        private void CollapseTaxes()
        {
            TaxesVisibility = !TaxesVisibility;
        }
    }
}
