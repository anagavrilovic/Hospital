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
                _transferInventory = JsonConvert.DeserializeObject<ObservableCollection<TransferInventory>>(sr.ReadToEnd());

                if (_transferInventory == null)
                {
                    return new ObservableCollection<TransferInventory>();
                }
            }

            return _transferInventory;
        }

        public void Save(TransferInventory parameter1)
        {
            _transferInventory = GetAll();

            if (_transferInventory == null)
            {
                _transferInventory = new ObservableCollection<TransferInventory>();
            }

            _transferInventory.Add(parameter1);

            DoSerialization();
        }

        public void Delete(TransferInventory transfer)
        {
            _transferInventory = GetAll();
            _transferInventory.Remove(transfer);
          
            foreach(TransferInventory ti in _transferInventory)
            {
                if(ti.ItemID.Equals(transfer.ItemID) && ti.FirstRoomID.Equals(transfer.FirstRoomID) && ti.DestinationRoomID.Equals(transfer.DestinationRoomID))
                {
                    _transferInventory.Remove(ti);
                    break;
                }
            }
            
            DoSerialization();
        }

        public void EditTransfer(TransferInventory editedTransfer)
        {
            foreach (TransferInventory transfer in _transferInventory)
            {
                if (transfer.ItemID.Equals(editedTransfer.ItemID) && transfer.FirstRoomID.Equals(editedTransfer.FirstRoomID) && transfer.DestinationRoomID.Equals(editedTransfer.DestinationRoomID))
                {
                    _transferInventory.Remove(transfer);
                    _transferInventory.Add(editedTransfer);
                    DoSerialization();
                    break;
                }
            }
        }

        public void DoSerialization()
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _transferInventory);
            }
        }

        private ObservableCollection<TransferInventory> _transferInventory = new ObservableCollection<TransferInventory>();
        public String fileName;
    }
}
