using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF_Core_Database_First.Contexts;

namespace EF_Core_Database_First.Repositories
{
    public class ProdutoRepository
    {
        private readonly LojaContext _ctx;

        public ProdutoRepository()
        {
            _ctx = new LojaContext();
        }
    }
}