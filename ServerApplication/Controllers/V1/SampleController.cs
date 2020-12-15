using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data.Repositories;
using Entities;
using ServerApplication.Models.V1.Sample;
using WebFramework.Api;

namespace ServerApplication.Controllers.V1
{

    public class SampleController : CrudController<SampleDto, SampleDto, Sample, Guid>
    {
        public SampleController(IRepository<Sample> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public override Task<ActionResult<List<SampleDto>>> Get(CancellationToken cancellationToken)
        {
            return base.Get(cancellationToken);
        }
    }
}
