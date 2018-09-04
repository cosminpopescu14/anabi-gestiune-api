using Anabi.Common.ViewModels;
using Anabi.DataAccess.Ef.DbModels;
using Anabi.Domain.Asset.Commands;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Anabi.Domain.Asset
{
    public class AddAssetAddressHandler : BaseHandler, IAsyncRequestHandler<AddAssetAddress, AddressViewModel>
    {
        public AddAssetAddressHandler(BaseHandlerNeeds needs) : base(needs) {}

        public async Task<AddressViewModel> Handle(AddAssetAddress message)
        {
            var address = new AddressDb()
            {
                CountyId = message.CountyId,
                Street = message.Street,
                City =  message.City,
                Building = message.Building,
                Stair = message.Stair,
                Floor = message.Floor,
                FlatNo = message.FlatNo,
                Description = message.Description,
                UserCodeAdd = UserCode(),
                AddedDate = DateTime.Now
            }; 

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var response = mapper.Map<AddAssetAddress, AddressViewModel>(message);
            response.Id = address.Id;
            response.Journal = new JournalViewModel
            {
                UserCodeAdd = address.UserCodeAdd,
                AddedDate = address.AddedDate,
            };

            return response;
        }
    } 
}