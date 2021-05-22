using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class TransferInventoryFileRepository : ITransferInventoryRepository
    {
        private string fileName = "transferInventory.json";

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TransferInventory> GetAll()
        {
            ObservableCollection<TransferInventory> transferInventory = new ObservableCollection<TransferInventory>();
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                transferInventory = JsonConvert.DeserializeObject<ObservableCollection<TransferInventory>>(sr.ReadToEnd());

                if (transferInventory == null)
                    return new ObservableCollection<TransferInventory>();
            }

            return transferInventory;
        }

        public TransferInventory GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(TransferInventory parameter)
        {
            ObservableCollection<TransferInventory> transferInventory = GetAll();

            if (transferInventory == null)
            {
                transferInventory = new ObservableCollection<TransferInventory>();
            }

            transferInventory.Add(parameter);

            Serialize(transferInventory);
        }

        public void Serialize(ObservableCollection<TransferInventory> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }
    }
}
