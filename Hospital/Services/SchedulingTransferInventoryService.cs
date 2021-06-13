using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace Hospital.Services
{
    class SchedulingTransferInventoryService
    {
        private ITransferInventoryRepository transferInventoryRepository;
        private IStaticInventoryRepository staticInventoryRepository;
        public SchedulingTransferInventoryService() 
        {
            transferInventoryRepository = new TransferInventoryFileRepository();
            staticInventoryRepository = new StaticInventoryFileRepository();
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

        public string ProcessRequest()
        {
            if (!AreTransferAttributesValid())
                return "Niste uneli ispravne podatke!";

            if (!CheckAvailabilityOfFreeBeds())
                return "Premeštanje kreveta se ne može izvršiti zbog popunjenosti kapaciteta sobe!";

            if (NotEnoughItemForNewTransfer() && IsTransferBeforeFirstScheduledTransferOfItem())
                ScheduleTemporaryTransferBeforeFirstReservedTransfer();
            else if (NotEnoughItemForNewTransfer() && IsTransferAfterFirstScheduledTransferOfItem())
                return "Prenos se ne može izvršiti zbog nedostatka opreme!";
            

            SaveTransfer(TransferRequest);
            return "Prebacivanje je uspešno zakazano!";
        }

        public bool AreTransferAttributesValid()
        {
            Inventory ItemForTransfer = staticInventoryRepository.GetOneItemFromRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
            if (TransferRequest.TransferDate < DateTime.Now)
                return false;
            
            else if (ItemForTransfer.Quantity < TransferRequest.Quantity)
                return false;
            
            return true;
        }

        public bool CheckAvailabilityOfFreeBeds()
        {
            Inventory ItemForTransfer = staticInventoryRepository.GetOneItemFromRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
            IRoomRepository roomRepository = new RoomFileRepository();
            Room firstRoom = roomRepository.GetByID(TransferRequest.FirstRoomID);
            if(ItemForTransfer.Name.ToLower().Contains("krevet"))
                if (firstRoom.FreeBeds < TransferRequest.Quantity)
                    return false;
            
            return true;
        }
  

        private int GetTotalQuantityForAllTransfersOfItem()
        {
            int totalQuantityForTransfer = 0;
            foreach (TransferInventory ti in transferInventoryRepository.GetAll())
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                    totalQuantityForTransfer += ti.Quantity;
            
            return totalQuantityForTransfer;
        }

        private TransferInventory GetFirstScheduledTransferOfItem()
        {
            TransferInventory firstReservedTransfer = new TransferInventory();

            foreach (TransferInventory ti in transferInventoryRepository.GetAll())
                if (ti.ItemID.Equals(TransferRequest.ItemID) && ti.FirstRoomID.Equals(TransferRequest.FirstRoomID))
                    if (ti.TransferDate > firstReservedTransfer.TransferDate)
                        firstReservedTransfer = ti;
                          
            return firstReservedTransfer;
        }

        public bool NotEnoughItemForNewTransfer()
        {
            int totalQuantityForEachTransferOfItem = GetTotalQuantityForAllTransfersOfItem();
            Inventory ItemForTransfer = staticInventoryRepository.GetOneItemFromRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
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
                transferInventoryRepository.EditTransfer(_firstReservedTransfer);
            }
            else
            {
                int newQuantity = _firstReservedTransfer.Quantity - TransferRequest.Quantity;
                _firstReservedTransfer.FirstRoomID = TransferRequest.DestinationRoomID;
                _firstReservedTransfer.Quantity = TransferRequest.Quantity;
                transferInventoryRepository.EditTransfer(_firstReservedTransfer);

                Inventory ItemForTransfer = staticInventoryRepository.GetOneItemFromRoom(TransferRequest.ItemID, TransferRequest.FirstRoomID);
                TransferInventory newTransfer = new TransferInventory(_firstReservedTransfer.ItemID, newQuantity, ItemForTransfer.RoomID, _firstReservedTransfer.DestinationRoomID, _firstReservedTransfer.TransferDate + new TimeSpan(0, 0, 2));
                SaveTransfer(newTransfer);
            }
        }

        private void SaveTransfer(TransferInventory transfer)
        {
            transfer.TransferID = GenerateTransferID();
            transferInventoryRepository.Save(transfer);
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
    }
}
