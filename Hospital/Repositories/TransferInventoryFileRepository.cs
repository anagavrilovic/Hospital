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
            List<TransferInventory> allTransfers = GetAll();
            foreach (TransferInventory transfer in allTransfers)
                if (transfer.TransferID.Equals(id))
                {
                    allTransfers.Remove(transfer);
                    break;
                }
            Serialize(allTransfers);
        }

        public List<TransferInventory> GetAll()
        {
            List<TransferInventory> transferInventory = new List<TransferInventory>();
            using (StreamReader sr = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                transferInventory = JsonConvert.DeserializeObject<List<TransferInventory>>(sr.ReadToEnd());

                if (transferInventory == null)
                    return new List<TransferInventory>();
            }

            return transferInventory;
        }

        public TransferInventory GetByID(string id)
        {
            List<TransferInventory> allTransfers = GetAll();
            foreach (TransferInventory transfer in allTransfers)
                if (transfer.TransferID.Equals(id))
                    return transfer;

            return null;
        }

        public void Save(TransferInventory parameter)
        {
            List<TransferInventory> transferInventory = GetAll();

            if (transferInventory == null)
                transferInventory = new List<TransferInventory>();

            transferInventory.Add(parameter);

            Serialize(transferInventory);
        }

        public List<int> GetAllScheduledTransferIDs()
        {
            List<int> allIDs = new List<int>();
            List<TransferInventory> allScheduledTransfers = GetAll();
            foreach (TransferInventory transfer in allScheduledTransfers)
                allIDs.Add(Int32.Parse(transfer.TransferID));

            return allIDs;

        }

        public void EditTransfer(TransferInventory editedTransfer)
        {
            List<TransferInventory> transferInventory = GetAll();
            foreach (TransferInventory transfer in transferInventory)
            {
                if (transfer.TransferID.Equals(editedTransfer.TransferID))
                {
                    transferInventory.Remove(transfer);
                    transferInventory.Add(editedTransfer);
                    Serialize(transferInventory);
                    break;
                }
            }
        }

        public void Serialize(List<TransferInventory> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }
    }
}
