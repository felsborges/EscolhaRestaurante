using EscolhaRestaurante.ApplicationCore.Entities;
using EscolhaRestaurante.ApplicationCore.Interfaces.Repository;
using EscolhaRestaurante.Infratructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.Infratructure.Repository
{
    public class VotoRepository : EfRepository<Voto>, IVotoRepository
    {
        public VotoRepository(RestauranteContext dbContext) : base(dbContext)
        {

        }
    }
}
