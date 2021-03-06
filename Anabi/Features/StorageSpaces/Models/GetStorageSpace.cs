﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anabi.Features.StorageSpaces.Models
{
    public class GetStorageSpace : IRequest<List<StorageSpaceViewModel>>
    {
        public int? Id { get; set; }
    }
}
