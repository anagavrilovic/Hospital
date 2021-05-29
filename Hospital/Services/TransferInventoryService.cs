using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class TransferInventoryService
    {
        private IStaticInventoryRepository staticInventoryRepository;
        public TransferInventory Transfer { get; set; }

        public TransferInventoryService(TransferInventory transfer)
        {
            staticInventoryRepository = new StaticInventoryFileRepository();
            Transfer = transfer;
        }

        public void ScheduleTransfer()
        {
            Task task = new Task(() => this.CheckTransferStatus());
            task.Start();
        }

        public void CheckTransferStatus()
        {
            TimeSpan timeSpan = Transfer.TransferDate.Subtract(DateTime.Now);

            if (Transfer.TransferDate > DateTime.Now)
                Thread.Sleep(timeSpan);

            FinishTransfer();
        }

        public void FinishTransfer()
        {
            UpdateInventory();
            RemoveTransferRequest();
        }

        public void UpdateInventory()
        {
            Inventory inventoryItem = staticInventoryRepository.GetOneItemFromRoom(Transfer.ItemID, Transfer.FirstRoomID);
            if (inventoryItem.Quantity >= Transfer.Quantity)
            {
                if (IsTransferingItemExistsInDestinationRoom())
                    IncreaseItemQuantityInDestinationRoom();
                else
                    AddTransferingItemDestinationRoom();

                ReduceItemQuantitiyInFirstRoom();
            }
        }

        public void RemoveTransferRequest()
        {
            TransferInventoryStorage transferStorage = new TransferInventoryStorage();
            transferStorage.Delete(Transfer);
            //TransferInventoryFileRepository transferRepository = new TransferInventoryFileRepository();
           // transferRepository.Delete(Transfer.TransferID);
        }

        private bool IsTransferingItemExistsInDestinationRoom()
        {
            foreach (Inventory item in staticInventoryRepository.GetAllInventoryFromRoom(Transfer.DestinationRoomID))
            {
                if (item.Id.Equals(Transfer.ItemID))
                    return true;
            }
            return false;
        }

        private void IncreaseItemQuantityInDestinationRoom()
        {
            Inventory itemInDestinationRoom = staticInventoryRepository.GetOneItemFromRoom(Transfer.ItemID, Transfer.DestinationRoomID);
            itemInDestinationRoom.Quantity += Transfer.Quantity;
            staticInventoryRepository.EditItem(itemInDestinationRoom);
        }

        private void AddTransferingItemDestinationRoom()
        {
            Inventory itemFromFirstRoom = staticInventoryRepository.GetOneItemFromRoom(Transfer.ItemID, Transfer.FirstRoomID);
            itemFromFirstRoom.Quantity = Transfer.Quantity;
            itemFromFirstRoom.RoomID = Transfer.DestinationRoomID;
            staticInventoryRepository.Save(itemFromFirstRoom);
        }

        private void ReduceItemQuantitiyInFirstRoom()
        {
            Inventory itemInFirstRoom = staticInventoryRepository.GetOneItemFromRoom(Transfer.ItemID, Transfer.FirstRoomID);
            itemInFirstRoom.Quantity -= Transfer.Quantity;
            staticInventoryRepository.EditItem(itemInFirstRoom);
        }
    }
}
