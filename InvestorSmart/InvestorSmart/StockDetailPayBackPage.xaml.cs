using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvestorSmart.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestorSmart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailPayBackPage : ContentPage
    {
        private readonly Stock _selectedStock;

        public StockDetailPayBackPage()
        {
            InitializeComponent();
            _selectedStock = App.SelectedStockInTabPage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (PickerMarketCap.Items.Count < 4)
            {
                PickerMarketCap.Items.Add(
                    $"{Stock.MarketCap(_selectedStock.PriceRealValue, _selectedStock.ShareOutStanding):C}");
                PickerMarketCap.Items.Add(
                    $"{Stock.MarketCap(_selectedStock.PriceCurrent, _selectedStock.ShareOutStanding):C}");
                PickerMarketCap.Items.Add(
                    $"{Stock.MarketCap(_selectedStock.Mos50, _selectedStock.ShareOutStanding):C}");
                PickerMarketCap.Items.Add(
                    $"{Stock.MarketCap(_selectedStock.Mos80, _selectedStock.ShareOutStanding):C}");
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            PickerMarketCap.Items.Clear();
        }

        private void PickerMarketCap_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridPayBack.Children.Clear();
            var accumulatedProfit = 0.0;
            var profit = new Stack<double>();

            GridPayBack.Children.Add(new Label() { Text = "Year" }, 0, 0);
            GridPayBack.Children.Add(new Label() { Text = "Profit" }, 1, 0);
            GridPayBack.Children.Add(new Label() { Text = "Accumulated Profit" }, 2, 0);
            GridPayBack.Children.Add(new Label() { Text = "Positive Year" }, 3, 0);
            var countPayBack = 0;
            for (var i = 0; i < 15; i++)
            {
                double temp;
                double positiveYear;
                if (i == 0)
                {
                    temp = Stock.MarketCap(_selectedStock.Eps, _selectedStock.ShareOutStanding);
                    profit.Push(temp);
                    positiveYear = 0;
                }
                else
                {
                    temp = profit.Pop() * (1 + _selectedStock.Growth);
                    profit.Push(temp);
                    accumulatedProfit += temp;
                    var pickerSelected = double.Parse(PickerMarketCap.SelectedItem.ToString().Remove(0, 1));
                    positiveYear = accumulatedProfit - pickerSelected;
                    if (positiveYear < 0)
                    {
                        countPayBack++;
                    }
                }

                GridPayBack.Children.Add(new Label() { Text = $"{i}" }, 0, i + 1);
                GridPayBack.Children.Add(new Label() { Text = $"{temp:C}" }, 1, i + 1);
                GridPayBack.Children.Add(new Label() { Text = $"{accumulatedProfit:C}" }, 2, i + 1);
                GridPayBack.Children.Add(
                    new Label()
                    {
                        Text =
                            $"{positiveYear:C}",
                        TextColor = positiveYear < 0 ? Color.Red : Color.Default
                    }, 3, i + 1);
            }

            PayBack.Text = $"PayBack period in {countPayBack} year(s)";
            PayBack.IsVisible = true;
        }
    }
}