using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class TransferInventoryStorage
    {
        public TransferInventoryStorage()
        {
            this.fileName = "transferInventory.json";
        }

        public ObservableCollection<TransferInventory> GetAll()
        {
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                transferInventory = JsonConvert.DeserializeObject<ObservableCollection<TransferInventory>>(sr.ReadToEnd());

                if (transferInventory == null)
                {
                    return new ObservableCollection<TransferInventory>();
                }
            }

            return transferInventory;
        }

        public void Save(TransferInventory parameter1)
        {
            transferInventory = GetAll();

            if (transferInventory == null)
            {
                transferInventory = new ObservableCollection<TransferInventory>();
            }

            transferInventory.Add(parameter1);

            doSerialization();
        }

        public void Delete(TransferInventory transfer)
        {
            transferInventory = GetAll();
            transferInventory.Remove(transfer);
          
            foreach(TransferInventory ti in transferInventory)
            {
                if(ti.ItemID.Equals(transfer.ItemID) && ti.FirstRoomID.Equals(transfer.FirstRoomID) && ti.DestinationRoomID.Equals(transfer.DestinationRoomID))
                {
                    transferInventory.Remove(ti);
                    break;
                }
            }
            
            doSerialization();
        }

        public void doSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, transferInventory);
            }
        }

        public static ObservableCollection<TransferInventory> transferInventory = new ObservableCollection<TransferInventory>();
        public String fileName;
    }
}
