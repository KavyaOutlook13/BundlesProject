using BundlesProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace BundlesProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BundleController
    {
        [HttpGet(Name = "GetBundle")]
        public ActionResult<int> GetMaximumBundles(BundleRequest request)
        {
            var response = CountOfPossibleBundles(request);
            Console.WriteLine(response);
            return response;
        }

        Dictionary<string, int> oneBundle = new();
        private int CountOfPossibleBundles(BundleRequest bundle)
        {
            foreach (var x in bundle.BundleComponents)
            {
                if (x.IsBundle == true)
                {
                    Resolve(x.Value, x.SubBundleComponents);
                }
                else
                {
                    oneBundle.Add(x.Name, x.Value);
                }
            }

            return FineshedBundlesCounter(oneBundle, bundle.TotalStock);
        }

        private void Resolve(int parentQuantity, List<BundleComponentRequest> ChildComponent)
        {
            foreach (var y in ChildComponent)
            {
                oneBundle.Add(y.Name, y.Value * parentQuantity);
                if (y.IsBundle == true)
                {
                    Resolve(y.Value, y.SubBundleComponents);
                }
            }
        }

        private int FineshedBundlesCounter(Dictionary<string, int> SingleBundle, Dictionary<string, int> Stock)
        {
            int minBundles = Stock.MaxBy(k => k.Value).Value;
            foreach (var i in SingleBundle.Keys)
            {
                if (Stock[i] >= SingleBundle[i] && Stock[i] / SingleBundle[i] <= minBundles)
                {
                    minBundles = Stock[i] / SingleBundle[i];
                }
            }
            return minBundles;
        }
    }
}
