using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App
{
    public class OfficeDayController
    {
        private readonly OfficeDayService _officeDayService;
        private ObservableCollection<OfficeDay> _officeDays;

        public ObservableCollection<OfficeDay> OfficeDays
        {
            get => _officeDays;
            set => _officeDays = value;
        }

        public OfficeDayController(OfficeDayService officeDayService)
        {
            _officeDayService = officeDayService;
            _officeDays = new ObservableCollection<OfficeDay>();
        }

        public async Task GetAllOfficeDays()
        {
            OfficeDays = await _officeDayService.GetAllOfficeDays();
        }

        public async Task<bool> CreateOfficeDay(DateOnly date, Guid userId)
        {
            return await _officeDayService.CreateOfficeDay(date, userId);
        }
    }
}
