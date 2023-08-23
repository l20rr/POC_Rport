using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ReportApp.Components;
using ReportApp.Services;


namespace ReportApp.Shared
{
    public partial class FeedbackMenuComponent
    {
        public bool showmenu = false;

        private bool showBugReportComponent = false;


        public IEnumerable<BugReport> BugReports { get; set; }


        [Inject]
        public IBugReportDataService BugReportDataService { get; set; }


        [Inject]
        public IUserDataService UserDataService { get; set; }

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
            showmenu = !showmenu;
        }

        protected bool isAddBugReportInitialized = false;

        protected async Task QuickAddBug()
        {
           
            StateHasChanged(); // Atualiza a renderização para esconder o menu
            await Task.Delay(10); // Aguarda um curto período para a renderização ser atualizada

            showBugReportComponent = true; // Agora é seguro definir isso como true
            StateHasChanged(); // Atualiza a renderização para mostrar o componente

            // Aguarda até que o componente AddBugReport seja inicializado completamente
            while (AddBugReport == null)
            {
                await Task.Delay(10);
            }

            AddBugReport.ShowAsync(); // Agora chama o método Show()
        }
    }
}
