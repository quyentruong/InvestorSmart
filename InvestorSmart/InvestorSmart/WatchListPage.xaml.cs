using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestorSmart.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestorSmart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchListPage : ContentPage
    {
        public WatchListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var db = new SQLiteAsyncConnection(App.DatabaseLocation);
            await db.CreateTableAsync<Stock>();
            var query = db.Table<Stock>().OrderBy(s => s.Symbol);
            var result = await query.ToListAsync();
            WatchListView.ItemsSource = result;
        }

        private async void WatchListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (WatchListView.SelectedItem is Stock selectedStock)
            {
                App.SelectedStockInTabPage = selectedStock;
                await Navigation.PushAsync(new StockDetailTabbedPage());
            }
        }
    }
}