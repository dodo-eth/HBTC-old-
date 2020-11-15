using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;
using System.Globalization;

using System.Timers;

namespace HBTC_BOT
{

    public partial class Form1 : Form
    {
        static Telegram.Bot.TelegramBotClient Bot = new Telegram.Bot.TelegramBotClient("1470290796:AAE_hLDL1FjOJ5-YuTmQXHvNiutfjDqwfYo");


        BackgroundWorker bw;
        static public string link_pars = "1";
        static public string link_new = "1";
        static public string link_New_Listings = "1";
        static public string link_New_Listings_new = "1";
        static public string link_Notice = "1";
        static public string link_Notice_new = "1";
        static public string link_Others = "1";
        static public string link_Others_new = "1";

        static public int count = 0;
        static public int count1 = 0;
        static public int count2 = 0;
        static public int count3 = 0;
        private static System.Timers.Timer aTimer;
        static public long chat_id = -1001159086291;


        private System.Object lockThis = new System.Object();
        public Form1()
        {
            aTimer = new System.Timers.Timer(6000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            InitializeComponent();
            this.bw = new BackgroundWorker();
            this.bw.DoWork += bw_DoWork;

            this.bw.RunWorkerAsync();


        }



        private async static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //Recent Activities 
            if (link_pars != link_new)
            {

                if (Form1.count != 0)
                {
                    await Bot.SendTextMessageAsync(chat_id, "https://support.hbtc.co" + link_new, ParseMode.Html, false, false, 0, null);
                }
                Form1.count++;
                link_pars = link_new;
            }
            System.Net.WebClient wc1 = new System.Net.WebClient();
            String link_Response = wc1.DownloadString("https://support.hbtc.co/hc/en-us/sections/360002667194-Recent-Activities");
            link_new = System.Text.RegularExpressions.Regex.Match(link_Response, @"(/hc/en-us/articles)+(.+)(?="" class)").Groups[0].Value;




            //listing
            if (link_New_Listings != link_New_Listings_new)
            {

                if (Form1.count1 != 0)
                {
                    await Bot.SendTextMessageAsync(chat_id, "https://support.hbtc.co" + link_New_Listings_new, ParseMode.Html, false, false, 0, null);
                }
                Form1.count1++;
                link_New_Listings = link_New_Listings_new;
            }
            System.Net.WebClient wc2 = new System.Net.WebClient();
            String link_Response_Listings = wc2.DownloadString("https://support.hbtc.co/hc/en-us/sections/360009462813-New-Listings");
            link_New_Listings_new = System.Text.RegularExpressions.Regex.Match(link_Response_Listings, @"(/hc/en-us/articles)+(.+)(?="" class)").Groups[0].Value;


            // Notice
            if (link_Notice != link_Notice_new)
            {

                if (Form1.count2 != 0)
                {
                    await Bot.SendTextMessageAsync(chat_id, "https://support.hbtc.co" + link_Notice_new, ParseMode.Html, false, false, 0, null);
                }
                Form1.count2++;
                link_Notice = link_Notice_new;
            }
            System.Net.WebClient wc3 = new System.Net.WebClient();
            String link_Response_Notice = wc3.DownloadString("https://support.hbtc.co/hc/en-us/sections/360009462793-Withdrawal-Opening-Suspension-Notice");
            link_Notice_new = System.Text.RegularExpressions.Regex.Match(link_Response_Notice, @"(/hc/en-us/articles)+(.+)(?="" class)").Groups[0].Value;

            // others
            if (link_Others != link_Others_new)
            {

                if (Form1.count3 != 0)
                {
                    await Bot.SendTextMessageAsync(chat_id, "https://support.hbtc.co" + link_Others_new, ParseMode.Html, false, false, 0, null);
                }
                Form1.count3++;
                link_Others = link_Others_new;
            }
            System.Net.WebClient wc4 = new System.Net.WebClient();
            String link_Response_Others = wc4.DownloadString("https://support.hbtc.co/hc/en-us/sections/360001994473-Others");
            link_Others_new = System.Text.RegularExpressions.Regex.Match(link_Response_Others, @"(/hc/en-us/articles)+(.+)(?="" class)").Groups[0].Value;


        }

        //listing https://support.hbtc.co/hc/en-us/sections/360009462813-New-Listings

        // Notice  https://support.hbtc.co/hc/en-us/sections/360009462793-Withdrawal-Opening-Suspension-Notice
        // others https://support.hbtc.co/hc/en-us/sections/360001994473-Others

        async void bw_DoWork(object sender, DoWorkEventArgs e)
        {



            var worker = sender as BackgroundWorker;
            try
            {

                ; // инициализируем API



                try
                {
                    await Bot.SetWebhookAsync(""); // !!!!!!!!!!!!!!!!!!!!!!ЦИКЛ ПЕРЕЗАПУСКА

                }
                catch
                {
                    await Bot.SetWebhookAsync("");
                }


                { /*
                // Inlin'ы
                Bot.OnMessage += async (object sender2, Telegram.Bot.Args.MessageEventArgs e1) =>
               
                    Bot.StartReceiving(Array.Empty<UpdateType>()); 
                    var timer = new Timer();
                        timer.Interval = 7000;
                        timer.Tick += new EventHandler(SimpleFunc); //И печатает на экран что-то
                        timer.Start();

                        void SimpleFunc(object sendear, EventArgs e2)
                        {
                            if (link_pars != link_new)
                            {

                                link_pars = link_new;
                            }

                            System.Net.WebClient wc1 = new System.Net.WebClient();
                            String link_Response = wc1.DownloadString("https://support.hbtc.co/hc/en-us/sections/360002667194-Recent-Activities");
                            link_new = System.Text.RegularExpressions.Regex.Match(link_Response, @"(/hc/en-us/articles)+(.+)(?="" class)").Groups[0].Value;
                            Bot.SendTextMessageAsync(@"pesik123d", "", ParseMode.Html, false, false, 0, null);
                            if (Form1.count != 0)
                            {
                                Bot.SendTextMessageAsync(@"pesik123d", "", ParseMode.Html, false, false, 0, null);
                            }
                            Form1.count++;
                            Task.Delay(60000);
                        }

                    Bot.StopReceiving();
                    
                };
                   
                    Bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                {

                };
 */
                    Bot.OnUpdate += async (object su, Telegram.Bot.Args.UpdateEventArgs evu) =>
                {



                    try
                    {



                        var update = evu.Update;
                        var message = update.Message;



                        if (message == null) return;







                        /*
                        if (question1.Count == 2 || message.From.Username == @"off_fov")
                        {
                            question1[1] = message.Text;
                            await Bot.SendTextMessageAsync(message.Chat.Id, question1[0] + "\n" + question1[1], ParseMode.Html, false, false, 0, keyboard_full);
                            await Bot.SendTextMessageAsync(message.Chat.Id, @"Вопрос ответ! Добавлен", ParseMode.Html, false, false, 0, keyboard_full);

                        }*/



                        if (message.Text == "/win" & message.From.Username == @"off_fov")
                        {
                            await Bot.SendTextMessageAsync(message.Chat.Id, @"сам макака", ParseMode.Html, false, false, 0, null);

                        }



                        // https://www.hbtc.com/api/v1/hobbit/repurchase/info

                        // @"https://api.hbtc.com/openapi/quote/v1/ticker/price"

                        // charts 
                        // https://finviz.com/crypto_charts.ashx?t=ALL&tf=h1

                        if (message.Text == "/daily_repo@HBTC_RU_BOT")
                        {
                            try
                            {
                                System.Net.WebClient wc1 = new System.Net.WebClient();
                                String price_Responsehbc = wc1.DownloadString("https://api.hbtc.com/openapi/quote/v1/ticker/price");
                                String hbc_price1 = System.Text.RegularExpressions.Regex.Match(price_Responsehbc, @"HBCUSDT""+,""price"":""+[0-9]+.[0-9]+").Groups[0].Value;
                                String hbc_price = System.Text.RegularExpressions.Regex.Match(hbc_price1, @"[0-9]+.[0-9]+").Groups[0].Value;


                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://www.hbtc.com/api/v1/hobbit/repurchase/info");
                                String Distributed1 = System.Text.RegularExpressions.Regex.Match(price_Response, @"allocated"":""+[0-9]+.[0-9][0-9]").Groups[0].Value;
                                String Distributed = System.Text.RegularExpressions.Regex.Match(Distributed1, @"[0-9]+.[0-9][0-9]").Groups[0].Value;

                                System.Net.WebClient wc2 = new System.Net.WebClient();
                                String LatestPricefor10xPE_Response = wc2.DownloadString("https://www.hbtc.com/api/v1/hobbit/repurchase/info");
                                String LatestPricefor10xPE1 = System.Text.RegularExpressions.Regex.Match(LatestPricefor10xPE_Response, @"tenTimesPrice"":""+[0-9]+.[0-9][0-9]").Groups[0].Value;
                                String LatestPricefor10xPE = System.Text.RegularExpressions.Regex.Match(LatestPricefor10xPE1, @"[0-9]+.[0-9][0-9]").Groups[0].Value;


                                System.Net.WebClient wc3 = new System.Net.WebClient();
                                String LatestPricefor5xPE_Response = wc3.DownloadString("https://www.hbtc.com/api/v1/hobbit/repurchase/info");
                                String LatestPricefor5xPE1 = System.Text.RegularExpressions.Regex.Match(LatestPricefor5xPE_Response, @"fiveTimesPrice"":""+[0-9]+.[0-9][0-9]").Groups[0].Value;
                                String LatestPricefor5xPE = System.Text.RegularExpressions.Regex.Match(LatestPricefor5xPE1, @"[0-9]+.[0-9][0-9]").Groups[0].Value;

                                System.Net.WebClient wc4 = new System.Net.WebClient();
                                String LockedVolume_Response = wc4.DownloadString("https://www.hbtc.com/api/v1/hobbit/repurchase/info");
                                String LockedVolume1 = System.Text.RegularExpressions.Regex.Match(LockedVolume_Response, @"lockTotal"":""+[0-9]+").Groups[0].Value;
                                String LockedVolume = System.Text.RegularExpressions.Regex.Match(LockedVolume1, @"[0-9]+").Groups[0].Value;

                                CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");


                                double dist_usdt = Convert.ToDouble(Convert.ToString(hbc_price)) * Convert.ToDouble(Convert.ToString(Distributed));

                                decimal dist_usdt_out = Convert.ToDecimal(dist_usdt.ToString("0.##"));

                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                {
                                        new [] { new InlineKeyboardButton { Text = "Перейти на hbtc.com", CallbackData = "demo", Url = "https://www.hbtc.com/captain/daily_repo" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                await Bot.SendTextMessageAsync(message.Chat.Id, "<code>Accu.to be Distributed: " + Distributed + " HBC" + "\n" + "Accu.to be Distributed: ~ " + dist_usdt_out + " USDT" + "\n" + "Latest Price 10xPE: " + LatestPricefor10xPE + " USDT \n" + "Latest Price 5PE: " + LatestPricefor5xPE + " USDT \n" + "LockedVolume: " + LockedVolume + " HBC</code>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);


                            }
                            catch
                            {

                            }
                        }
                        if (message.Text == "/hbc_usdt@HBTC_RU_BOT")
                        {
                            try
                            {
                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://api.hbtc.com/openapi/quote/v1/ticker/price");
                                String hbc_price1 = System.Text.RegularExpressions.Regex.Match(price_Response, @"HBCUSDT""+,""price"":""+[0-9]+.[0-9]+").Groups[0].Value;
                                String hbc_price = System.Text.RegularExpressions.Regex.Match(hbc_price1, @"[0-9]+.[0-9]+").Groups[0].Value;
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре HBC/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/HBC/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                await Bot.SendTextMessageAsync(message.Chat.Id, "<code><b>HBC/USDT</b>" + "\n" + "Цена:" + hbc_price + " USDT</code>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);
                            }
                            catch
                            {

                            }
                        }
                        if (message.Text == "/btc_usdt@HBTC_RU_BOT")
                        {
                            try
                            {
                                //new site api
                                //[0-9]+.[0-9]+(?=,"lastVolume")
                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://finviz.com/api/quote.ashx?&ticker=BTCUSD&instrument=crypto&timeframe=i5");
                                String btc_price = System.Text.RegularExpressions.Regex.Match(price_Response, @"[0-9]+.[0-9]+(?=,""lastVolume"")").Groups[0].Value;
                                //https://finviz.com/api/quote.ashx?&ticker=BTCUSD&instrument=crypto&timeframe=i5


                                //htbc site api
                                /*
                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://api.hbtc.com/openapi/quote/v1/ticker/price");
                                String btc_price1 = System.Text.RegularExpressions.Regex.Match(price_Response, @"BTCUSDT""+,""price"":""+[0-9]+.[0-9]+").Groups[0].Value;
                                String btc_price = System.Text.RegularExpressions.Regex.Match(btc_price1, @"[0-9]+.[0-9]+").Groups[0].Value;
                                */

                                System.Net.WebClient wc1 = new System.Net.WebClient();
                                String rev_Response = wc1.DownloadString("https://finviz.com/crypto_charts.ashx");
                                String rev_1 = System.Text.RegularExpressions.Regex.Match(rev_Response, @"&rev=[0-9]+").Groups[0].Value;
                                String rev = System.Text.RegularExpressions.Regex.Match(rev_1, @"[0-9]+").Groups[0].Value;
                                //photo rev // [0-9]+(?=" width="320")
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                            {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре BTC/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/BTC/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                //await Bot.SendTextMessageAsync(message.Chat.Id, "<code><b>BTC/USDT</b>" + "\n" + "Цена:" + btc_price + " USDT</code>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);
                                await Bot.SendPhotoAsync(message.Chat.Id, photo: "https://finviz.com/fx_image.ashx?btcusd_m5_s.png&rev=" + rev, " <code><b>BTC/USDT</b>" + "\n" + "Цена:" + btc_price + " USDT</code>", ParseMode.Html, false, 0, inlineKeyboardMarkup);

                            }
                            catch
                            {

                            }
                        }
                        if (message.Text == "/bch_usdt@HBTC_RU_BOT")
                        {
                            try
                            {
                                //new site api
                                //[0-9]+.[0-9]+(?=,"lastVolume")
                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://finviz.com/api/quote.ashx?&ticker=BCHUSD&instrument=crypto&timeframe=i5");
                                String BCH_price = System.Text.RegularExpressions.Regex.Match(price_Response, @"[0-9]+.[0-9]+(?=,""lastVolume"")").Groups[0].Value;



                                //htbc site api
                                /*
                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://api.hbtc.com/openapi/quote/v1/ticker/price");
                                String btc_price1 = System.Text.RegularExpressions.Regex.Match(price_Response, @"BTCUSDT""+,""price"":""+[0-9]+.[0-9]+").Groups[0].Value;
                                String btc_price = System.Text.RegularExpressions.Regex.Match(btc_price1, @"[0-9]+.[0-9]+").Groups[0].Value;
                                */

                                System.Net.WebClient wc1 = new System.Net.WebClient();
                                String rev_Response = wc1.DownloadString("https://finviz.com/crypto_charts.ashx");
                                String rev_1 = System.Text.RegularExpressions.Regex.Match(rev_Response, @"&rev=[0-9]+").Groups[0].Value;
                                String rev = System.Text.RegularExpressions.Regex.Match(rev_1, @"[0-9]+").Groups[0].Value;
                                //photo rev // [0-9]+(?=" width="320")
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                            {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре BCH/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/BCH/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                //await Bot.SendTextMessageAsync(message.Chat.Id, "<code><b>BTC/USDT</b>" + "\n" + "Цена:" + btc_price + " USDT</code>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);
                                await Bot.SendPhotoAsync(message.Chat.Id, photo: "https://finviz.com/fx_image.ashx?bchusd_m5_s.png&rev=" + rev, " <code><b>BCH/USDT</b>" + "\n" + "Цена:" + BCH_price + " USDT</code>", ParseMode.Html, false, 0, inlineKeyboardMarkup);

                            }
                            catch
                            {

                            }
                        }
                        if (message.Text == "/eth_usdt@HBTC_RU_BOT")
                        {
                            try
                            {

                                System.Net.WebClient wc1 = new System.Net.WebClient();
                                String rev_Response = wc1.DownloadString("https://finviz.com/crypto_charts.ashx");
                                String rev_1 = System.Text.RegularExpressions.Regex.Match(rev_Response, @"&rev=[0-9]+").Groups[0].Value;
                                String rev = System.Text.RegularExpressions.Regex.Match(rev_1, @"[0-9]+").Groups[0].Value;

                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://finviz.com/api/quote.ashx?&ticker=ETHUSD&instrument=crypto&timeframe=i5");
                                String ETH_price = System.Text.RegularExpressions.Regex.Match(price_Response, @"[0-9]+.[0-9]+(?=,""lastVolume"")").Groups[0].Value;
                                //https://finviz.com/api/quote.ashx?&ticker=BTCUSD&instrument=crypto&timeframe=i5
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                            {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре ETH/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/ETH/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                await Bot.SendPhotoAsync(message.Chat.Id, photo: "https://finviz.com/fx_image.ashx?ethusd_m5_s.png&rev=" + rev, " <code><b>ETH/USDT</b>" + "\n" + "Цена:" + ETH_price + " USDT</code>", ParseMode.Html, false, 0, inlineKeyboardMarkup);

                            }
                            catch
                            {

                            }
                        }
                        if (message.Text == "/ltc_usdt@HBTC_RU_BOT")
                        {
                            try
                            {

                                System.Net.WebClient wc1 = new System.Net.WebClient();
                                String rev_Response = wc1.DownloadString("https://finviz.com/crypto_charts.ashx");
                                String rev_1 = System.Text.RegularExpressions.Regex.Match(rev_Response, @"&rev=[0-9]+").Groups[0].Value;
                                String rev = System.Text.RegularExpressions.Regex.Match(rev_1, @"[0-9]+").Groups[0].Value;

                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://finviz.com/api/quote.ashx?&ticker=LTCUSD&instrument=crypto&timeframe=i5");
                                String ltc_price = System.Text.RegularExpressions.Regex.Match(price_Response, @"[0-9]+.[0-9]+(?=,""lastVolume"")").Groups[0].Value;
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре LTC/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/LTC/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                await Bot.SendPhotoAsync(message.Chat.Id, photo: "https://finviz.com/fx_image.ashx?ltcusd_m5_s.png&rev=" + rev, " <code><b>LTC/USDT</b>" + "\n" + "Цена:" + ltc_price + " USDT</code>", ParseMode.Html, false, 0, inlineKeyboardMarkup);
                            }
                            catch
                            {

                            }
                        }

                        if (message.Text == "/eos_usdt@HBTC_RU_BOT")
                        {
                            try
                            {
                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://api.hbtc.com/openapi/quote/v1/ticker/price");
                                String EOS_price1 = System.Text.RegularExpressions.Regex.Match(price_Response, @"EOSUSDT""+,""price"":""+[0-9]+.[0-9]+").Groups[0].Value;
                                String EOS_price = System.Text.RegularExpressions.Regex.Match(EOS_price1, @"[0-9]+.[0-9]+").Groups[0].Value;
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре EOS/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/EOS/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                await Bot.SendTextMessageAsync(message.Chat.Id, "<code><b>EOS/USDT</b>" + "\n" + "Цена:" + EOS_price + " USDT</code>", ParseMode.Html, false, false, 0, inlineKeyboardMarkup);
                            }
                            catch
                            {

                            }
                        }
                        if (message.Text == "/xrp_usdt@HBTC_RU_BOT")
                        {
                            try
                            {
                                System.Net.WebClient wc1 = new System.Net.WebClient();
                                String rev_Response = wc1.DownloadString("https://finviz.com/crypto_charts.ashx");
                                String rev_1 = System.Text.RegularExpressions.Regex.Match(rev_Response, @"&rev=[0-9]+").Groups[0].Value;
                                String rev = System.Text.RegularExpressions.Regex.Match(rev_1, @"[0-9]+").Groups[0].Value;

                                System.Net.WebClient wc = new System.Net.WebClient();
                                String price_Response = wc.DownloadString("https://finviz.com/api/quote.ashx?&ticker=XRPUSD&instrument=crypto&timeframe=i5");
                                String xrp_price = System.Text.RegularExpressions.Regex.Match(price_Response, @"[0-9]+.[0-9]+(?=,""lastVolume"")").Groups[0].Value;
                                var inlineKeyboardMarkup = new InlineKeyboardMarkup(new[]
                                {
                                        new [] { new InlineKeyboardButton { Text = "Перейти к паре XRP/USDT", CallbackData = "demo", Url = "https://www.hbtc.co/exchange/XRP/USDT" } }
                                });
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                await Bot.SendPhotoAsync(message.Chat.Id, photo: "https://finviz.com/fx_image.ashx?xrpusd_m5_s.png&rev=" + rev, " <code><b>XRP/USDT</b>" + "\n" + "Цена:" + xrp_price + " USDT</code>", ParseMode.Html, false, 0, inlineKeyboardMarkup);
                            }
                            catch
                            {

                            }
                        }


                        if (message.Type == MessageType.ChatMemberLeft)
                        {
                            try
                            {
                                await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                            }
                            catch
                            {

                            }
                            return;

                        }


                        var entities = message.Entities.Where(t => t.Type == MessageEntityType.Url
                                           || t.Type == MessageEntityType.Mention);
                        foreach (var entity in entities)
                        {
                            if (entity.Type == MessageEntityType.Url)
                            {
                                try
                                {
                                    //40103694 - @off_fov
                                    //571522545 -  @ProAggressive                                    
                                    //320968789 - @timcheg1
                                    //273228404 - @hydranik
                                    //435567580 - Никита                           
                                    //352345393 - @i_am_zaytsev
                                    //430153320 - @KingOfMlnD
                                    //579784 - @kamiyar
                                    //536915847 - @m1Bean
                                    //460657014 - @DenisSenatorov

                                    if (message.From.Username == @"off_fov" || message.From.Username == @"bar1nn" || message.From.Username == @"doretos" || message.From.Username == @"ProAggressive" || message.From.Username == @"Mira_miranda")
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        await Bot.DeleteMessageAsync(message.Chat.Id, message.MessageId);
                                        if (update.Message.From.Username != null)
                                        {
                                            await Bot.SendTextMessageAsync(message.Chat.Id, "@" + message.From.Username + ", Ссылки запрещены!");
                                            return;
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(message.Chat.Id, message.From.FirstName + ", Ссылки запрещены!");
                                            return;
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                return;


                            }
                        }
                    }

                    catch
                    {

                    }
                };

                    Bot.StartReceiving();


                    // запускаем прием обновлений


                }
            }

            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message); // если ключ не подошел - пишем об этом в консоль отладки
            }

        }


    }
}
