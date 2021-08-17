using InvestorSmart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestorSmart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailTabbedPage : TabbedPage
    {
        private readonly Stock _selectedStock;

        public StockDetailTabbedPage()
        {
            InitializeComponent();
            _selectedStock = App.SelectedStockInTabPage;
        }

        private async void Edit_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditStockPage(_selectedStock));
        }

        private async void Delete_OnClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Confirm?", $"Are you sure to delete this stock {_selectedStock.Symbol}?",
                "Yes", "No");
            if (answer)
            {
                var db = new SQLiteAsyncConnection(App.DatabaseLocation);
                await db.DeleteAsync(_selectedStock);
                await Navigation.PopAsync();
            }
        }
    }
}