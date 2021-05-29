using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
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
        private ITransferInventoryRepository transferInventoryRepository;
        public SchedulingTransferInventoryService() 
        {
            transferInventoryRepository = new TransferInventoryFileRepository();
        }
        public SchedulingTransferInventoryService(TransferInventory transfer)
        {
            TransferRequest = transfer;
            transferInventoryRepository = new TransferInventoryFileRepository();
        }

        public List<TransferInventory> GetAll()
        {
            return transferInventoryRepository.GetAll();
        }

        public void ProcessRequest()
        {
            if (!AreTransferAttributesValid())
                return;

            if (NotEnoughItemForNewTransfer() && IsTransferBeforeFirstScheduledTransferOfItem())
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
            IStaticInventoryRepository staticInventoryRepository = new StaticInventoryFileRepository();
            Inventory ItemForTransfer = staticInventoryRepository.GetOneItemFromRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
            if (TransferRequest.TransferDate < DateTime.Now)
            {
                MessageBox.Show("Niste ispravno uneli vreme!"); //TODO: throw exception
                return false;
            }
            else if (ItemForTransfer.Quantity < TransferRequest.Quantity)
            {
                MessageBox.Show("Pogrešan unos količine!");    //TODO: throw exception
                return false;
            }
            return true;
        }

        private void TransferCantBeExecuted()
        {
            //TODO: throw exception
            MessageBox.Show("Sala ne raspolaže traženom količinom stavke. \n Pokušajte sa manjom količinom ili pogledajte stanje u drugim salama.");
            return;
        }

        private int GetTotalQuantityForAllTransfersOfItem()
        {
            int totalQuantityForTransfer = 0;
            foreach (TransferInventory ti in _transferInventoryStorage.GetAll())
            {
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                    totalQuantityForTransfer += ti.Quantity;
            }
            return totalQuantityForTransfer;
        }

        private TransferInventory GetFirstScheduledTransferOfItem()
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
            int totalQuantityForEachTransferOfItem = GetTotalQuantityForAllTransfersOfItem();
            InventoryStorage storage = new InventoryStorage();
            Inventory ItemForTransfer = storage.GetOneByRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
            if (totalQuantityForEachTransferOfItem + TransferRequest.Quantity > ItemForTransfer.Quantity)
                return true;

            return false;
        }

        private bool IsTransferAfterFirstScheduledTransferOfItem()
        {
            this._firstReservedTransfer = GetFirstScheduledTransferOfItem();
            if (TransferRequest.TransferDate > _firstReservedTransfer.TransferDate)
                return true;

            return false;
        }

        private bool IsTransferBeforeFirstScheduledTransferOfItem()
        {
            this._firstReservedTransfer = GetFirstScheduledTransferOfItem();
            if (TransferRequest.TransferDate < _firstReservedTransfer.TransferDate)
                return true;

            return false;
        }

        private void ScheduleTemporaryTransferBeforeFirstReservedTransfer()
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

        private void SaveTransfer(TransferInventory transfer)
        {
            transfer.TransferID = GenerateTransferID();
            _transferInventoryStorage.Save(transfer);
            TransferInventoryService service = new TransferInventoryService(transfer);
            service.ScheduleTransfer();
        }

        private string GenerateTransferID()
        {
            List<int> allScheduledTransfersIDs = transferInventoryRepository.GetAllScheduledTransferIDs();
            int id = 1;
            while(true)
            {
                if (!allScheduledTransfersIDs.Contains(id))
                    break;

                id += 1;
            }
            return id.ToString();
        }

        public TransferInventory TransferRequest { get; set; }
        private TransferInventory _firstReservedTransfer = new TransferInventory();
        private TransferInventoryStorage _transferInventoryStorage = new TransferInventoryStorage();
    }
}
