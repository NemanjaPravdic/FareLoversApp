using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ResvoyageMobileApp.ViewModels.Hotel
{
    public class RoomInfoViewModel : BaseViewModel
    {
        public RoomInfoViewModel(HotelRequestViewModel request)
        {
            _request = request;
            _request.SelectedRoomsInfo = new ObservableCollection<RoomViewModel>();
            foreach (var room in _request.RoomsInfo)
            {
                var roomInfo = new RoomViewModel
                {
                    Adults = room.Adults,
                    Children = room.Children,
                    RoomNumber = room.RoomNumber,
                    IsRemovable = room.IsRemovable
                };
                foreach (var childAge in room.ChildrenAge)
                {
                    roomInfo.ChildrenAge.Add(new ChildAgeViewModel { Age = childAge.Age, Id = childAge.Id });
                }
                _request.SelectedRoomsInfo.Add(roomInfo);
            }
            SetButtons(_request.SelectedRoomsInfo);
        }


        private HotelRequestViewModel _request;

        public HotelRequestViewModel Request
        {
            get { return _request; }
            set { SetValue(ref _request, value); }
        }
        private bool _showAddButton;

        public bool ShowAddButton
        {
            get { return _showAddButton; }
            set { SetValue(ref _showAddButton, value); }
        }
        public ICommand ReduceAdults => new Command<RoomViewModel>(ReduceAdultsInfo);
        public ICommand AddAdults => new Command<RoomViewModel>(AddAdultsInfo);
        public ICommand ReduceChildren => new Command<RoomViewModel>(ReduceChildrenInfo);
        public ICommand AddChildren => new Command<RoomViewModel>(AddChildrenInfo);
        public ICommand ReduceChildrenAge => new Command<ChildAgeViewModel>(ReduceChildrenAgeInfo);
        public ICommand AddChildrenAge => new Command<ChildAgeViewModel>(AddChildrenAgeInfo);
        public ICommand AddRoom => new Command(AddNewRoom);
        public ICommand RemoveRoom => new Command<RoomViewModel>(RemoveSelectedRoom); 
        public ICommand ApplyRoomInfo => new Command(ApplyRoomInfoValues);

        private void AddAdultsInfo(RoomViewModel obj)
        {
            obj.Adults++;
        }

        private void ReduceAdultsInfo(RoomViewModel obj)
        {
            obj.Adults--;
        }
        private void AddChildrenInfo(RoomViewModel obj)
        {
            obj.Children++;
            obj.ChildrenAge.Add(new ChildAgeViewModel { Id = obj.ChildrenAge.Count + 1 });
        }

        private void ReduceChildrenInfo(RoomViewModel obj)
        {
            obj.Children--;
            obj.ChildrenAge.RemoveAt(obj.ChildrenAge.Count - 1);
        }

        private void ReduceChildrenAgeInfo(ChildAgeViewModel obj)
        {
            obj.Age--;
        }

        private void AddChildrenAgeInfo(ChildAgeViewModel obj)
        {
            obj.Age++;

        }
        private void AddNewRoom()
        {
            _request.SelectedRoomsInfo.Add(new RoomViewModel() { RoomNumber = _request.SelectedRoomsInfo.Count + 1 });
            _request.SelectedRoomsInfo.ForEach(x => x.IsRemovable = true);
            SetButtons(_request.SelectedRoomsInfo);

        }
        private void RemoveSelectedRoom(RoomViewModel obj)
        {
            _request.SelectedRoomsInfo.Remove(obj);
            for (int i = 0; i < _request.SelectedRoomsInfo.Count; i++)
            {
                _request.SelectedRoomsInfo[i].RoomNumber = i + 1;
            }
            if (_request.SelectedRoomsInfo.Count == 1)
            {
                _request.SelectedRoomsInfo.FirstOrDefault().IsRemovable = false;
            }
            SetButtons(_request.SelectedRoomsInfo);
        }

        private void SetButtons(ObservableCollection<RoomViewModel> roomsInfo)
        {
            if (roomsInfo.Count == 4)
                ShowAddButton = false;
            else
                ShowAddButton = true;
        }
        private void ApplyRoomInfoValues() 
        {
            _request.RoomsInfo = _request.SelectedRoomsInfo;
            _request.NoOfRooms = _request.SelectedRoomsInfo.Count;
            _request.Adults = _request.SelectedRoomsInfo.Sum(x => x.Adults);
            _request.Children = _request.SelectedRoomsInfo.Sum(x => x.Children);

            _navigation.Navigation.PopAsync(true);
        }
    }
}
