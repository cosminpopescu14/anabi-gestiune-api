﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anabi.Common.Exceptions;
using Anabi.Common.Utils;
using Anabi.Common.ViewModels;
using Anabi.DataAccess.Ef.DbModels;
using Anabi.Domain.Common.Address;
using Anabi.Domain.Models;
using Anabi.Domain.StorageSpaces.Commands;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anabi.Domain.StorageSpaces
{
    public class StorageSpaceHandler : BaseHandler
        ,IRequestHandler<AddStorageSpace, int>
        ,IRequestHandler<EditStorageSpace, StorageSpace>
        ,IRequestHandler<DeleteStorageSpace>
    {
        public StorageSpaceHandler(BaseHandlerNeeds needs) : base(needs)
        {
        }

        /*Handles adding of new storage spaces without address*/
        public async Task<int> Handle(AddStorageSpace message, CancellationToken cancellationToken)
        {
            var newStorageSpace = new StorageSpaceDb();

            Mapper.Map(message, newStorageSpace);

            context.StorageSpaces.Add(newStorageSpace);

            await SetNewAddressToStorageSpace(message, newStorageSpace);

            await context.SaveChangesAsync();

            return newStorageSpace.Id;

        }

        /*Handles editing of storage spaces*/
        public async Task<StorageSpace> Handle(EditStorageSpace message, CancellationToken cancellationToken)
        {

            var storageSpace = await context.StorageSpaces.FindAsync(message.Id);


            ValidateStorageSpace(storageSpace);

            this.mapper.Map(message, storageSpace);

            await this.SetNewAddressToStorageSpace(message, storageSpace);

            await context.SaveChangesAsync();


            var responseModel = new StorageSpace();
            this.mapper.Map(storageSpace, responseModel);
            return responseModel;

        }

        /*Handles deleting of a storage spaces*/
        public async Task<Unit> Handle(DeleteStorageSpace message, CancellationToken cancellationToken)
        {
            var command = context.StorageSpaces.Where(m => m.Id == message.Id).AsQueryable()
                .Include(a => a.Address);

            var storageSpace = await command.FirstOrDefaultAsync();
            ValidateStorageSpace(storageSpace);

            context.StorageSpaces.Remove(storageSpace);

            await context.SaveChangesAsync();
            return Unit.Value;
        }

        private static void ValidateStorageSpace(StorageSpaceDb storageSpace)
        {
            if (storageSpace == null)
            {
                throw new AnabiEntityNotFoundException(Constants.NO_STORAGE_SPACES_FOUND);
            }
        }

        /*Setting a new address to storage spaces*/
        private async Task SetNewAddressToStorageSpace(IAddAddressMinimal message, StorageSpaceDb storageSpace)
        {
            AddressDb address;


            if (storageSpace.AddressId > 0)
            {
                address = await this.context.Addresses.FindAsync(storageSpace.AddressId);
                this.mapper.Map(message, address);
            }
            else
            {
                address = this.mapper.Map<IAddAddressMinimal, AddressDb>(message);
            }

            var countyCode = message.CountyCode.ToUpperInvariant();
            address.County = context.Counties.SingleOrDefault(x => x.Abreviation == countyCode);
            storageSpace.Address = address;
        }
    }
}
