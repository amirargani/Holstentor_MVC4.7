using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.Plugin
{
    public class Data
    {
        public Random RanKey = new Random();
        public string Str = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public string GetCode(string code = null)
        {
            Object Str1 = RanKey.Next(0, 72);
            Object Str2 = RanKey.Next(0, 72);
            Object Str3 = RanKey.Next(0, 72);
            Object Str4 = RanKey.Next(0, 72);
            Object Str5 = RanKey.Next(0, 72);
            Object Str6 = RanKey.Next(0, 72);
            Object Str7 = RanKey.Next(0, 72);
            Object Str8 = RanKey.Next(0, 72);
            Object Str9 = RanKey.Next(0, 72);
            Object Str10 = RanKey.Next(0, 72);
            Object Str11 = RanKey.Next(0, 72);
            Object Str12 = RanKey.Next(0, 72);
            Object Str13 = RanKey.Next(0, 72);
            Object Str14 = RanKey.Next(0, 72);
            Object Str15 = RanKey.Next(0, 72);
            Object Str16 = RanKey.Next(0, 72);
            Object Str17 = RanKey.Next(0, 72);
            Object Str18 = RanKey.Next(0, 72);
            Object Str19 = RanKey.Next(0, 72);
            Object Str20 = RanKey.Next(0, 72);
            Object Str21 = RanKey.Next(0, 72);
            Object Str22 = RanKey.Next(0, 72);
            Object Str23 = RanKey.Next(0, 72);
            Object Str24 = RanKey.Next(0, 72);
            Object Str25 = RanKey.Next(0, 72);
            Object Str26 = RanKey.Next(0, 72);
            Object Str27 = RanKey.Next(0, 72);
            Object Str28 = RanKey.Next(0, 72);
            Object Str29 = RanKey.Next(0, 72);
            Object Str30 = RanKey.Next(0, 72);
            Object Str31 = RanKey.Next(0, 72);
            string Strsub1 = Str.Substring(Convert.ToInt32(Str1), 1);
            string Strsub2 = Str.Substring(Convert.ToInt32(Str2), 1);
            string Strsub3 = Str.Substring(Convert.ToInt32(Str3), 1);
            string Strsub4 = Str.Substring(Convert.ToInt32(Str4), 1);
            string Strsub5 = Str.Substring(Convert.ToInt32(Str5), 1);
            string Strsub6 = Str.Substring(Convert.ToInt32(Str6), 1);
            string Strsub7 = Str.Substring(Convert.ToInt32(Str7), 1);
            string Strsub8 = Str.Substring(Convert.ToInt32(Str8), 1);
            string Strsub9 = Str.Substring(Convert.ToInt32(Str9), 1);
            string Strsub10 = Str.Substring(Convert.ToInt32(Str10), 1);
            string Strsub11 = Str.Substring(Convert.ToInt32(Str11), 1);
            string Strsub12 = Str.Substring(Convert.ToInt32(Str12), 1);
            string Strsub13 = Str.Substring(Convert.ToInt32(Str13), 1);
            string Strsub14 = Str.Substring(Convert.ToInt32(Str14), 1);
            string Strsub15 = Str.Substring(Convert.ToInt32(Str15), 1);
            string Strsub16 = Str.Substring(Convert.ToInt32(Str16), 1);
            string Strsub17 = Str.Substring(Convert.ToInt32(Str17), 1);
            string Strsub18 = Str.Substring(Convert.ToInt32(Str18), 1);
            string Strsub19 = Str.Substring(Convert.ToInt32(Str19), 1);
            string Strsub20 = Str.Substring(Convert.ToInt32(Str20), 1);
            string Strsub21 = Str.Substring(Convert.ToInt32(Str21), 1);
            string Strsub22 = Str.Substring(Convert.ToInt32(Str22), 1);
            string Strsub23 = Str.Substring(Convert.ToInt32(Str23), 1);
            string Strsub24 = Str.Substring(Convert.ToInt32(Str24), 1);
            string Strsub25 = Str.Substring(Convert.ToInt32(Str25), 1);
            string Strsub26 = Str.Substring(Convert.ToInt32(Str26), 1);
            string Strsub27 = Str.Substring(Convert.ToInt32(Str27), 1);
            string Strsub28 = Str.Substring(Convert.ToInt32(Str28), 1);
            string Strsub29 = Str.Substring(Convert.ToInt32(Str29), 1);
            string Strsub30 = Str.Substring(Convert.ToInt32(Str30), 1);
            string Strsub31 = Str.Substring(Convert.ToInt32(Str31), 1);
            string rnd = Strsub1 + Strsub2 + Strsub3 + Strsub4 + Strsub5 + "-" +
                Strsub6 + Strsub7 + Strsub8 + Strsub9 + Strsub10 + "-" +
                Strsub11 + Strsub12 + Strsub13 + Strsub14 + Strsub15 + "-" +
                Strsub16 + Strsub17 + Strsub18 + Strsub19 + "-" +
                Strsub20 + Strsub21 + Strsub22 + Strsub23 + Strsub24 + Strsub25 + Strsub26 + Strsub27 + Strsub28 + Strsub29 + Strsub30 + Strsub31;
            return rnd;
        }
        public string GetPassword(string code = null)
        {
            Object Str1 = RanKey.Next(0, 72);
            Object Str2 = RanKey.Next(0, 72);
            Object Str3 = RanKey.Next(0, 72);
            Object Str4 = RanKey.Next(0, 72);
            Object Str5 = RanKey.Next(0, 72);
            Object Str6 = RanKey.Next(0, 72);
            Object Str7 = RanKey.Next(0, 72);
            Object Str8 = RanKey.Next(0, 72);
            string Strsub1 = Str.Substring(Convert.ToInt32(Str1), 1);
            string Strsub2 = Str.Substring(Convert.ToInt32(Str2), 1);
            string Strsub3 = Str.Substring(Convert.ToInt32(Str3), 1);
            string Strsub4 = Str.Substring(Convert.ToInt32(Str4), 1);
            string Strsub5 = Str.Substring(Convert.ToInt32(Str5), 1);
            string Strsub6 = Str.Substring(Convert.ToInt32(Str6), 1);
            string Strsub7 = Str.Substring(Convert.ToInt32(Str7), 1);
            string Strsub8 = Str.Substring(Convert.ToInt32(Str8), 1);
            string rnd = Strsub1 + Strsub2 + Strsub3 + Strsub4 + Strsub5 + Strsub6 + Strsub7 + Strsub8;
            return rnd;
        }
    }
}