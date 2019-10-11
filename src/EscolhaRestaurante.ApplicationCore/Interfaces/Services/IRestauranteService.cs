using EscolhaRestaurante.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.ApplicationCore.Interfaces.Services
{
    public interface IRestauranteService
    {
        Restaurante GetById(int id);
    }
}
