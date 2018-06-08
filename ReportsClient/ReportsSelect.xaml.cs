using Models.ConfigurationDB;
using Models.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReportsClient
{
    /// <summary>
    /// Interaction logic for ReportsSelect.xaml
    /// </summary>
    public partial class ReportsSelect : Window
    {
        public IList<EventHistory> EventsList;
        public ReportsSelect()
        {
            InitializeComponent();
            LoadReportsList();
        }


        private async Task<IList<T>> GetListAsync<T>(string apiRequset)
        {
            RestClient client = new RestClient(ServerAdress.Text);
            await client.Get(apiRequset);
            return JsonHelper.DeserializeToList<T>(client.Response);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string apiRequest = $"api/values/from={DateFrom.SelectedDate.Value.ToString("yyyy-MM-dd")}T{ TimeFrom.Text}&to={DateTo.SelectedDate.Value.ToString("yyyy-MM-dd")}T{TimeTo.Text}";
            Grid1.ItemsSource = await GetListAsync<EventHistory>(apiRequest);
        }

        private async void LoadReportsList()
        {
            string apiReaquest = $"api/config/reports";
            IList<Report> list = new List<Report>();
            list=await GetListAsync<Report>(apiReaquest);
            foreach(Report report in list)
            {
                Button newBtn = new Button() { Content = report.Name, Margin= new Thickness(-750,100*report.Id,0,300),Height=40,Width=300 };
                Panel.Children.Add(newBtn);
            }
        }
    }
}

