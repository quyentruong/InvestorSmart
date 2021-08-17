using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class EditStockPage : ContentPage
    {
        private readonly Stock _selectedStock;

        public EditStockPage(Stock selectedStock)
        {
            InitializeComponent();
            _selectedStock = selectedStock;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Symbol.Text = _selectedStock.Symbol;
            PriceCurrent.Text = $"{_selectedStock.PriceCurrent}";
            Eps.Text = $"{_selectedStock.Eps}";
            ShareOutStanding.Text = $"{_selectedStock.ShareOutStanding}";
            Years.Text = $"{_selectedStock.Years}";
            Growth.Text = $"{_selectedStock.Growth}";
            PeCurrent.Text = $"{_selectedStock.PeCurrent}";
            PeOptional.Text = $"{_selectedStock.PeOptional}";
            RoaCurrent.Text = $"{_selectedStock.RoaCurrent}";
            RoeCurrent.Text = $"{_selectedStock.RoeCurrent}";
            Marr.Text = $"{_selectedStock.Marr}";
        }

        private async void Save_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var db = new SQLiteAsyncConnection(App.DatabaseLocation);
                _selectedStock.PriceCurrent = double.Parse(PriceCurrent.Text);
                _selectedStock.RoeCurrent = double.Parse(RoeCurrent.Text);
                _selectedStock.RoaCurrent = double.Parse(RoaCurrent.Text);
                _selectedStock.PeCurrent = double.Parse(PeCurrent.Text);
                _selectedStock.PeOptional = double.Parse(PeOptional.Text);
                _selectedStock.ShareOutStanding = long.Parse(ShareOutStanding.Text);
                _selectedStock.Eps = double.Parse(Eps.Text);
                _selectedStock.Years = int.Parse(Years.Text);
                _selectedStock.Marr = double.Parse(Marr.Text);
                _selectedStock.Growth = double.Parse(Growth.Text);
                var rows = await db.UpdateAsync(_selectedStock);
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Stock successfully updated", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Failure", "Stock failed to be updated", "Ok");
                }
            }
            catch (ArgumentNullException ane)
            {
                Debug.WriteLine(ane.Message);
                await DisplayAlert("Failure", "Missing some fields", "Ok");
            }
            catch (FormatException fe)
            {
                Debug.WriteLine(fe.Message);
                await DisplayAlert("Failure", "Missing some fields", "Ok");
            }
            catch (OverflowException ofe)
            {
                Debug.WriteLine(ofe.Message);
                await DisplayAlert("Failure", "Value is too big", "Ok");
            }
        }
    }
}