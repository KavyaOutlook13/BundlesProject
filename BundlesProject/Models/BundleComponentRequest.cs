namespace BundlesProject.Models
{
    public class BundleComponentRequest
    {
        public string Name;
        public int Value;
        public bool IsBundle = false;
        public List<BundleComponentRequest>? SubBundleComponents = null;
    }
}