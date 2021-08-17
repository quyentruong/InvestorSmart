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
    public partial class NewStockPage : ContentPage
    {
        public NewStockPage()
        {
            InitializeComponent();
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var db = new SQLiteAsyncConnection(App.DatabaseLocation);
                await db.CreateTableAsync<Stock>();
                var stock = new Stock
                {
                    Symbol = Symbol.Text,
                    PriceCurrent = double.Parse(PriceCurrent.Text),
                    RoeCurrent = double.Parse(RoeCurrent.Text),
                    RoaCurrent = double.Parse(RoaCurrent.Text),
                    PeCurrent = double.Parse(PeCurrent.Text),
                    PeOptional = double.Parse(PeOptional.Text),
                    ShareOutStanding = long.Parse(ShareOutStanding.Text),
                    Eps = double.Parse(Eps.Text),
                    Years = int.Parse(Years.Text),
                    Marr = double.Parse(Marr.Text),
                    Growth = double.Parse(Growth.Text),
                };
                var rows = await db.InsertAsync(stock);
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Stock successfully inserted", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Failure", "Stock failed to be inserted", "Ok");
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