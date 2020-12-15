using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace ServerApplication.Models.V1.Sample
{
    public class SampleDto : BaseDto<SampleDto, Entities.Sample, Guid>
    {
        public string FirstSampleFiled { get; set; }
        public string SecondSampleFiled { get; set; }

    }
}
