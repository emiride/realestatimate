using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using RealEstatimate.Models;
using RealEstatimate.Interfaces;
using Scraper.Extensions;
using Scraper;

namespace OLXScraper
{
    public class OlxDataCollector
    {
        private readonly HtmlWeb _web;
        public HtmlDocument HtmlDocument { get; set; }
        public List<Flat> Flats { get; set; }
        public OlxDataCollector(List<Uri> urls)
        {
            Flats = new List<Flat>();
        }

        public Flat GetFlatFromUri(Uri uri)
        {
            HtmlDocument = _web.Load(uri);
            Flat flat = new Flat()
            {
                Price = GetPrice(),
                Name = GetName(),
                Address = GetAddress(),
                SquareMeters = GetSquareMeters(),
                RealEstatePropertyId = new Guid(),
                Currency = "KM",
                Built = GetBuiltDateTime(),
                Balcony = GetBalcony(),
                Elevator = GetElevator(),
                Floor = GetFloor(),
                Location = GetLocation(),
                NumberOfRooms = GetNumberOfRooms(),
                Parking = GetParking(),
                Renovated = GetRenovatedTime(),
                Description = GetDescription()
            };
            return flat;
        }

        private string GetDescription()
        {
            return HtmlDocument.DocumentNode.SelectSingleNode(".//div[@id='detaljni-opis']").InnerText;
        }

        private DateTime GetRenovatedTime()
        {
            throw new NotImplementedException();
        }

        private bool GetParking()
        {
            bool parking = false;
            try
            {
                HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Parking')]");
                parking = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                parking = false;
            }

            return parking;
        }

        private int GetNumberOfRooms()
        {
            int numberOfRooms = 0;
            try
            {
                var numberOfRoomsText = HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Broj Soba')]").NextSibling.InnerText;
                numberOfRooms = int.Parse(Regex.Replace(numberOfRoomsText, @"\D", ""));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return 0;
        }

        private Location GetLocation()
        {
            var stringWithLocation = HtmlDocument.DocumentNode.SelectSingleNode(".//script[contains(text(), 'LatLng')]")
                .InnerText;
            var latlng = stringWithLocation.Split("maps.LatLng(")[1].Split(")")[0].Split(",");

            Location location = new Location()
            {
                LocationId = Guid.NewGuid(),
                Latitude = double.Parse(latlng[0]),
                Longitude = double.Parse(latlng[1])
            };
            return location;
        }

        private int GetFloor()
        {
            int floor = 0;
            try
            {
                floor = HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Sprat')]").NextSibling.InnerInt();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return floor;
        }

        private bool GetElevator()
        {
            bool elevator = false;
            try
            {
                HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Lift')]");
                elevator = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                elevator = false;
            }

            return elevator;
        }

        private bool GetBalcony()
        {
            bool balcony = false;
            try
            {
                HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Balkon')]");
                balcony = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                balcony = false;
            }

            return balcony;
        }

        private DateTime GetBuiltDateTime()
        {
            var godina = HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Godina izgradnje')]")
                .NextSibling.InnerText.Trim();
            DateTime year = new DateTime();
            try
            {
                year = DateTime.Parse(godina);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return year;
        }

        private int GetSquareMeters()
        {
            return HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Kvadrata')]")
                .NextSibling.InnerInt();
        }

        public int GetPrice()
        {
            return HtmlDocument.DocumentNode.SelectSingleNode(".//span[@class='markadesno']/parent::p").InnerInt();
        }

        public string GetName()
        {
            return HtmlDocument.DocumentNode.SelectSingleNode(".//span[@id='naslovartikla']").InnerText.Trim();
        }

        public Address GetAddress()
        {
            return new Address()
            {
                AddressId = Guid.NewGuid(),
                City = HtmlDocument.DocumentNode.SelectSingleNode(".//div[@data-original-title='Lokacija']").GetAttributeValue("data-content", "").Split(",")[0],
                Country = "BiH",
                Municipality = HtmlDocument.DocumentNode.SelectSingleNode(".//div[@data-original-title='Lokacija']/a/p").NextSibling.InnerText.Trim(),
                StreetName = HtmlDocument.DocumentNode.SelectSingleNode(".//div[@class='df1' and contains(text(), 'Adresa')]").NextSibling.InnerText.Trim(),
                StreetNumber = "0",
                Zip = "0"
            };
        }
    }
}
