using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Api
{
    [ApiController]
    [Route("/ad")]
    public class ClassifiedAdsCommandsApi : Controller
    {

        public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService) => _applicationService = applicationService;
        
        private readonly ClassifiedAdsApplicationService _applicationService;
        
        [HttpPost]
        public async Task<IActionResult> Post(Contracts.ClassifiedAds.V1.Create request)
        {
            _applicationService.Handle(request);
            return Ok();
        }
    }
}
