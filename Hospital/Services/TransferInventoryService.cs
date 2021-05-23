using Hospital.Model;
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
        public TransferInventoryService(TransferInventory transfer)
        {
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
            Inventory inventoryItem = _inventoryStorage.GetOneByRoom(Transfer.ItemID, Transfer.FirstRoomID);
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
        }

        private bool IsTransferingItemExistsInDestinationRoom()
        {
            foreach (Inventory item in _inventoryStorage.GetByRoomID(Transfer.DestinationRoomID))
            {
                if (item.Id.Equals(Transfer.ItemID))
                    return true;
            }
            return false;
        }

        private void IncreaseItemQuantityInDestinationRoom()
        {
            Inventory itemInDestinationRoom = _inventoryStorage.GetOneByRoom(Transfer.ItemID, Transfer.DestinationRoomID);
            itemInDestinationRoom.Quantity += Transfer.Quantity;
            _inventoryStorage.EditItem(itemInDestinationRoom);
        }

        private void AddTransferingItemDestinationRoom()
        {
            Inventory itemFromFirstRoom = _inventoryStorage.GetOneByRoom(Transfer.ItemID, Transfer.FirstRoomID);
            itemFromFirstRoom.Quantity = Transfer.Quantity;
            itemFromFirstRoom.RoomID = Transfer.DestinationRoomID;
            _inventoryStorage.Save(itemFromFirstRoom);
        }

        private void ReduceItemQuantitiyInFirstRoom()
        {
            Inventory itemInFirstRoom = _inventoryStorage.GetOneByRoom(Transfer.ItemID, Transfer.FirstRoomID);
            itemInFirstRoom.Quantity -= Transfer.Quantity;
            _inventoryStorage.EditItem(itemInFirstRoom);
        }

        public TransferInventory Transfer { get; set; }
        private InventoryStorage _inventoryStorage = new InventoryStorage();
    }
}
