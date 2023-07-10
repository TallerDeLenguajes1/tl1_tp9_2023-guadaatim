using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Monedas;


internal class Program
{
    private static void Main(string[] args)
    {
        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;
                    
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        Moneda mon = JsonSerializer.Deserialize<Moneda>(responseBody);

                        Console.WriteLine("-------PRECIOS-------");
                        Console.WriteLine("USD: $" + mon.bpi.USD.rate_float);
                        Console.WriteLine("EUR: $" + mon.bpi.EUR.rate_float);
                        Console.WriteLine("GBP: $" + mon.bpi.GBP.rate_float);

                        //elegir moneda
                        Console.WriteLine("Ingrese una moneda: 1-USD, 2-EUR, 3-GBP");
                        int opcion;
                        bool control = int.TryParse(Console.ReadLine(), out opcion);
                       
                        if (control)
                        {
                            switch (opcion)
                            {
                                case 1:
                                    Console.WriteLine("Code: " + mon.bpi.USD.code);
                                    Console.WriteLine("Symbol: " + mon.bpi.USD.symbol);
                                    Console.WriteLine("Rate: $" + mon.bpi.USD.rate);
                                    Console.WriteLine("Description: " + mon.bpi.USD.description);
                                    Console.WriteLine("Rate Float: $" + mon.bpi.USD.rate_float);                                
                                    break;
                                case 2:
                                    Console.WriteLine("Code: " + mon.bpi.EUR.code);
                                    Console.WriteLine("Symbol: " + mon.bpi.EUR.symbol);
                                    Console.WriteLine("Rate: $" + mon.bpi.EUR.rate);
                                    Console.WriteLine("Description: " + mon.bpi.EUR.description);
                                    Console.WriteLine("Rate Float: $" + mon.bpi.EUR.rate_float);
                                    break;
                                case 3:
                                    Console.WriteLine("Code: " + mon.bpi.GBP.code);
                                    Console.WriteLine("Symbol: " + mon.bpi.GBP.symbol);
                                    Console.WriteLine("Rate: $" + mon.bpi.GBP.rate);
                                    Console.WriteLine("Description: " + mon.bpi.GBP.description);
                                    Console.WriteLine("Rate Float: $" + mon.bpi.GBP.rate_float);
                                    break;
                                default:
                                    Console.WriteLine("La moneda ingresada no se encuentra disponible");
                                    break;
                            }
                        }
                        
                    }
                        
            
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
            throw;
        }
    }

   

}