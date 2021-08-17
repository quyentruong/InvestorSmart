using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace InvestorSmart.Model
{
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double PriceCurrent { get; set; }
        public double RoeCurrent { get; set; }
        public double RoaCurrent { get; set; }
        public double PeCurrent { get; set; }
        public double PeOptional { get; set; }
        public long ShareOutStanding { get; set; }
        public double Eps { get; set; }
        public int Years { get; set; }
        public double Marr { get; set; }
        public double Growth { get; set; }
        public double EpsFuture => Eps * Math.Pow(1 + Growth, Years);
        public double PriceFuture => EpsFuture * PeOptional;
        public double PriceRealValue => PriceFuture / Math.Pow(1 + Marr, Years);
        public double Mos50 => PriceRealValue * 50 / 100;
        public double Mos80 => PriceRealValue * 80 / 100;

        public static double MarketCap(double price, long shareOutStanding)
        {
            return price * shareOutStanding / Math.Pow(10, 6);
        }

    }
}