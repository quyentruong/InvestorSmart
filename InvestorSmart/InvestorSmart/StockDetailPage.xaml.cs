using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InvestorSmart.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvestorSmart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockDetailPage : ContentPage
    {
        private readonly Stock _selectedStock;

        public StockDetailPage()
        {
            InitializeComponent();
            _selectedStock = App.SelectedStockInTabPage;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Symbol.Text = _selectedStock.Symbol;
            PriceCurrent.Text = $"Price: {_selectedStock.PriceCurrent:C}";
            PriceRealValue.Text = $"Real Value: {_selectedStock.PriceRealValue:C}";
            PriceFuture.Text = $"Price Future: {_selectedStock.PriceFuture:C}";
            Eps.Text = $"EPS: {_selectedStock.Eps:C}";
            EpsFuture.Text = $"EPS Future: {_selectedStock.EpsFuture:C}";
            ShareOutStanding.Text = $"Shares: {_selectedStock.ShareOutStanding:N0}";
            Years.Text = $"Years: {_selectedStock.Years}";
            Growth.Text = $"Growth: {_selectedStock.Growth:P}";
            PeCurrent.Text = $"PE Current: {_selectedStock.PeCurrent:C}";
            PeOptional.Text = $"PE Optional: {_selectedStock.PeOptional:C}";
            RoaCurrent.Text = $"ROA Current: {_selectedStock.RoaCurrent:P}";
            RoeCurrent.Text = $"ROE Current: {_selectedStock.RoeCurrent:P}";
            Mos50.Text = $"MOS50: {_selectedStock.Mos50:C}";
            Mos80.Text = $"MOS80: {_selectedStock.Mos80:C}";
            
        }

    }
}