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
                        Bpi moneda = JsonSerializer.Deserialize<Bpi>(responseBody);

                        Console.WriteLine("Ingrese una moneda: 1-USD, 2-EUR, 3-GBP");
                        int opcion;
                        bool control = int.TryParse(Console.ReadLine(), out opcion);
                       
                        if (control)
                        {
                            switch (opcion)
                            {
                                case 1:
                                    Console.WriteLine("Code: " + moneda.USD.code);
                                    Console.WriteLine("Symbol: " + moneda.USD.symbol);
                                    Console.WriteLine("Rate: " + moneda.USD.rate);
                                    Console.WriteLine("Description: " + moneda.USD.description);
                                    Console.WriteLine("Rate Float: " + moneda.USD.rate_float);                                
                                    break;
                                case 2:
                                    Console.WriteLine("Code: " + moneda.EUR.code);
                                    Console.WriteLine("Symbol: " + moneda.EUR.symbol);
                                    Console.WriteLine("Rate: " + moneda.EUR.rate);
                                    Console.WriteLine("Description: " + moneda.EUR.description);
                                    Console.WriteLine("Rate Floar: " + moneda.EUR.rate_float);
                                    break;
                                case 3:
                                    Console.WriteLine("Code: " + moneda.GBP.code);
                                    Console.WriteLine("Symbol: " + moneda.GBP.symbol);
                                    Console.WriteLine("Rate: " + moneda.GBP.rate);
                                    Console.WriteLine("Description: " + moneda.GBP.description);
                                    Console.WriteLine("Rate Floar: " + moneda.GBP.rate_float);
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