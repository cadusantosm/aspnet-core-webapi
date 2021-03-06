﻿using Demo.Core.Data.MySql.Entities;
using Framework.Data.EntityFramework;

namespace Demo.Core.Data.MySql.Repositories
{
    public class CharacterRepository : EFRepositoryBase<CharacterEntity>, ICharacterRepository
    {
        public CharacterRepository(DbzMySqlContext context) : base(context)
        {
        }
    }
}
