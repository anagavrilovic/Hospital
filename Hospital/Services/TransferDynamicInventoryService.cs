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
    class TransferDynamicInventoryService
    {
        private IDynamicInventoryRepository dynamicInventoryRepository;
        public TransferDynamicInventory TranferRequest;

        public TransferDynamicInventoryService(TransferDynamicInventory transfer)
        {
            dynamicInventoryRepository = new DynamicInventoryFileRepository();
            TranferRequest = transfer;
        }

        public void ProcessRequest()
        {
            if (!AreTransferAttributesValid())
                return;

            ExecuteTransfer();
        }

        private bool AreTransferAttributesValid()
        {
            DynamicInventory itemInFirstRoom = dynamicInventoryRepository.GetOneItemFromRoom(TranferRequest.ItemID, TranferRequest.FirstRoomID);
            if (TranferRequest.Quantity <= itemInFirstRoom.Quantity)
                return true;
            else
            {
                //MessageBox.Show("Sala ne raspolaže unetom količinom stavke. \n Pokušajte sa manjom količinom.");    //TODO: throw exception
                return false;
            }
        }

        private void ExecuteTransfer()
        {  
            UpdateDynamicInventory();
        }

        public void UpdateDynamicInventory()
        {
            if (IsTransferingItemExistsInDestinationRoom())
                IncreaseItemQuantityInDestinationRoom();
            else
                AddTransferingItemDestinationRoom();

            ReduceItemQuantitiyInFirstRoom();
        }

        private bool IsTransferingItemExistsInDestinationRoom()
        {
            foreach (DynamicInventory item in dynamicInventoryRepository.GetAllInventoryFromRoom(TranferRequest.DestinationRoomID))
                if (item.Id.Equals(TranferRequest.ItemID))
                    return true;
            
            return false;
        }

        private void IncreaseItemQuantityInDestinationRoom()
        {
            DynamicInventory itemInDestinationRoom = dynamicInventoryRepository.GetOneItemFromRoom(TranferRequest.ItemID, TranferRequest.DestinationRoomID);
            itemInDestinationRoom.Quantity += TranferRequest.Quantity;
            dynamicInventoryRepository.EditItem(itemInDestinationRoom);
        }

        private void AddTransferingItemDestinationRoom()
        {
            DynamicInventory itemFromFirstRoom = dynamicInventoryRepository.GetOneItemFromRoom(TranferRequest.ItemID, TranferRequest.FirstRoomID);
            itemFromFirstRoom.Quantity = TranferRequest.Quantity;
            itemFromFirstRoom.RoomID = TranferRequest.DestinationRoomID;
            dynamicInventoryRepository.Save(itemFromFirstRoom);
        }

        private void ReduceItemQuantitiyInFirstRoom()
        {
            DynamicInventory itemInFirstRoom = dynamicInventoryRepository.GetOneItemFromRoom(TranferRequest.ItemID, TranferRequest.FirstRoomID);
            itemInFirstRoom.Quantity -= TranferRequest.Quantity;
            dynamicInventoryRepository.EditItem(itemInFirstRoom);
        }
    }
}
