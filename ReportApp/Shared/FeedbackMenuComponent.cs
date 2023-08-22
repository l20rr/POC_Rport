using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ReportApp.Components;
using ReportApp.Services;


namespace ReportApp.Shared
{
    public partial class FeedbackMenuComponent
    {
        public bool show = false;

        public bool showbug = false;

        public bool showfeed = false;

        public bool showcontact = false;
        public IEnumerable<BugReport> BugReports { get; set; }


        [Inject]
        public IBugReportDataService BugReportDataService { get; set; }


        protected AddBugReport AddBugReport { get; set; }


        [Parameter]
        public EventCallback<bool> OnClickEventCallback { get; set; }

       

        protected async override Task OnInitializedAsync()
        {
            BugReports = (await BugReportDataService.GetAllBugReports()).ToList();
        }


        public async void AddBugReport_OnDialogClose()
        {
            BugReports = (await BugReportDataService.GetAllBugReports()).ToList();
            StateHasChanged();
        }


       

        private void ToggleMenu()
        {
            show = !show;
        }


    }
}
