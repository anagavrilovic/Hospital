using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Services
{
    class SchedulingTransferInventoryService
    {
        public SchedulingTransferInventoryService(TransferInventory transfer)
        {
            TransferRequest = transfer;
        }

        public void ProcessRequest()
        {
            if (!AreTransferAttributesValid())
                return;

            if (NotEnoughItemForNewTransfer() && IsTransferBeforeFirstScheduledTransferOfTime())
                ScheduleTemporaryTransferBeforeFirstReservedTransfer();
            else if (NotEnoughItemForNewTransfer() && IsTransferAfterFirstScheduledTransferOfItem())
            {
                TransferCantBeExecuted();
                return;
            }

            SaveTransfer(TransferRequest);
        }

        public bool AreTransferAttributesValid()
        {
            InventoryStorage storage = new InventoryStorage();
            Inventory ItemForTransfer = storage.GetOneByRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
            if (TransferRequest.TransferDate < DateTime.Now)
            {
                MessageBox.Show("Niste ispravno uneli vreme!");
                return false;
            }
            else if (ItemForTransfer.Quantity < TransferRequest.Quantity)
            {
                MessageBox.Show("Pogrešan unos količine!");
                return false;
            }
            return true;
        }

        private void TransferCantBeExecuted()
        {
            MessageBox.Show("Sala ne raspolaže traženom količinom stavke. \n Pokušajte sa manjom količinom ili pogledajte stanje u drugim salama.");
            return;
        }

        private int getTotalQuantityForAllTransfersOfItem()
        {
            int totalQuantityForTransfer = 0;
            foreach (TransferInventory ti in _transferInventoryStorage.GetAll())
            {
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                    totalQuantityForTransfer += ti.Quantity;
            }
            return totalQuantityForTransfer;
        }

        private TransferInventory getFirstScheduledTransferOfItem()
        {
            TransferInventory firstReservedTransfer = new TransferInventory();

            foreach (TransferInventory ti in _transferInventoryStorage.GetAll())
            {
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                {
                    if (ti.TransferDate > firstReservedTransfer.TransferDate)
                        firstReservedTransfer = ti;
                }
            }
            return firstReservedTransfer;
        }

        public bool NotEnoughItemForNewTransfer()
        {
            int totalQuantityForEachTransferOfItem = getTotalQuantityForAllTransfersOfItem();
            InventoryStorage storage = new InventoryStorage();
            Inventory ItemForTransfer = storage.GetOneByRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
            if (totalQuantityForEachTransferOfItem + TransferRequest.Quantity > ItemForTransfer.Quantity)
                return true;

            return false;
        }

        public bool IsTransferAfterFirstScheduledTransferOfItem()
        {
            this._firstReservedTransfer = getFirstScheduledTransferOfItem();
            if (TransferRequest.TransferDate > _firstReservedTransfer.TransferDate)
                return true;

            return false;
        }

        public bool IsTransferBeforeFirstScheduledTransferOfTime()
        {
            this._firstReservedTransfer = getFirstScheduledTransferOfItem();
            if (TransferRequest.TransferDate < _firstReservedTransfer.TransferDate)
                return true;

            return false;
        }

        public void ScheduleTemporaryTransferBeforeFirstReservedTransfer()
        {
            if (_firstReservedTransfer.Quantity <= TransferRequest.Quantity)
            {
                _firstReservedTransfer.FirstRoomID = TransferRequest.DestinationRoomID;
                _transferInventoryStorage.EditTransfer(_firstReservedTransfer);
            }
            else
            {
                int newQuantity = _firstReservedTransfer.Quantity - TransferRequest.Quantity;
                _firstReservedTransfer.FirstRoomID = TransferRequest.DestinationRoomID;
                _firstReservedTransfer.Quantity = TransferRequest.Quantity;
                _transferInventoryStorage.EditTransfer(_firstReservedTransfer);

                InventoryStorage storage = new InventoryStorage();
                Inventory ItemForTransfer = storage.GetOneByRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
                TransferInventory newTransfer = new TransferInventory(_firstReservedTransfer.ItemID, newQuantity, ItemForTransfer.RoomID, _firstReservedTransfer.DestinationRoomID, _firstReservedTransfer.TransferDate + new TimeSpan(0, 0, 2));
                SaveTransfer(newTransfer);
            }
        }

        public void SaveTransfer(TransferInventory transfer)
        {
            _transferInventoryStorage.Save(transfer);
            TransferInventoryService service = new TransferInventoryService(transfer);
            service.ScheduleTransfer();
        }

        public TransferInventory TransferRequest { get; set; }
        private TransferInventory _firstReservedTransfer = new TransferInventory();
        private TransferInventoryStorage _transferInventoryStorage = new TransferInventoryStorage();
    }
}
