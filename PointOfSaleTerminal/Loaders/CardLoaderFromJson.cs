using Newtonsoft.Json;
using PointOfSaleTerminal.DataModels;
using PointOfSaleTerminal.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace PointOfSaleTerminal.Loaders
{
    class CardLoaderFromJson : ICardLoader
    {
        public string pathToFile = $"{Environment.ExpandEnvironmentVariables("%UserProfile%")}\\Cards.xml";
        XmlSerializer formatter = new XmlSerializer(typeof(int));

        public double GetCard(string id)
        {
            var cardsJson = File.ReadAllText(pathToFile);
            return JsonConvert.DeserializeObject<DiscountCard>(cardsJson).TotalSum;
        }

        public void Save(DiscountCard card)
        {
            throw new NotImplementedException();
        }

        public void SaveData( double totalSum)
        {
            using (FileStream filesttream = new FileStream(pathToFile, FileMode.Create))
                formatter.Serialize(filesttream, totalSum);
        }

        DiscountCard ICardLoader.GetCard(string id)
        {
            throw new NotImplementedException();
        }

        /*
               public string pathToFile = $"{Environment.ExpandEnvironmentVariables("%UserProfile%")}\\PersonsDB.xml";

        XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Person>));

        public ObservableCollection<Person> Open()
        {
            if (!File.Exists(pathToFile))
                Save(new ObservableCollection<Person>());

            using (FileStream fileStream = new FileStream(pathToFile, FileMode.Open))
                return (ObservableCollection<Person>)formatter.Deserialize(fileStream);
        }

        public void Save(ObservableCollection<Person> _persons)
        {
            using (FileStream filesttream = new FileStream(pathToFile, FileMode.Create))
                formatter.Serialize(filesttream, _persons);
        }
        */
    }
}
