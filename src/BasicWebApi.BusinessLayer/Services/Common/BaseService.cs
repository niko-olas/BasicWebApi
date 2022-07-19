using AutoMapper;
using BasicWebApi.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Entities = BasicWebApi.DataAccessLayer.Entity;
using Models = BasicWebApi.Shared.Models;

namespace BasicWebApi.BusinessLayer.Services.Common
{
    public class BaseService
    {
        protected readonly IDataContext Context;
        protected readonly IMapper Mapper;

        public BaseService(IDataContext context, IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }
    }
}
