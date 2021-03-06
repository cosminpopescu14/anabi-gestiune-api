﻿using Anabi.DataAccess.Ef.DbModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anabi.DataAccess.Ef.EntityConfigurators
{
    public class IdentifierConfig : BaseEntityConfig<IdentifierDb>
    {
        public override void Configure(EntityTypeBuilder<IdentifierDb> builder)
        {
            base.Configure(builder);
        }
    }
}
