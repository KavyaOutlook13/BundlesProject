namespace BundlesProject.Models
{
    public class BundleRequest
    {
        public List<BundleComponentRequest> BundleComponents;
        public Dictionary<string, int> TotalStock;
    }
}
