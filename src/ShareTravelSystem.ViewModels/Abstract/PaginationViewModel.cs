namespace ShareTravelSystem.ViewModels.Abstract
{
    public abstract class PaginationViewModel
    {
        public int Size { get; set; }

        public int Page { get; set; }

        public int Count { get; set; }
    }
}