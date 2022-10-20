using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class RoomViewModel : BaseViewModel
    {
		public RoomViewModel()
		{
			_childrenAge = new ObservableCollection<ChildAgeViewModel>();
		}
		private int _roomNumber = 1;

		public int RoomNumber
		{
			get { return _roomNumber; }
			set { SetValue(ref _roomNumber, value); }
		}
		private int _adults = 1;

		public int Adults
		{
			get { return _adults; }
			set { SetValue(ref _adults, value); }
		}
		private int _children;

		public int Children
		{
			get { return _children; }
			set { SetValue(ref _children, value); }
		}
		private bool _isRemovable;

		public bool IsRemovable
		{
			get { return _isRemovable; }
			set { SetValue(ref _isRemovable, value); }
		}
		private ObservableCollection<ChildAgeViewModel> _childrenAge;

		public ObservableCollection<ChildAgeViewModel> ChildrenAge
		{
			get { return _childrenAge; }
			set { SetValue(ref _childrenAge, value); }
		}
	}
}
